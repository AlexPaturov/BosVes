using Dapper;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BosVesAppLibrary.Services;
public class InsertQueueService
{
   private readonly ConcurrentQueue<(GpriModel, TaskCompletionSource<int>)> _queue = new();
   private readonly ConcurrentDictionary<string, TaskCompletionSource<int>> _activeTasks = new();
   private readonly SemaphoreSlim _semaphore = new(1, 1); // Ограничиваем выполнение одним потоком
   private readonly GpriData _dbOneGpri;

   public InsertQueueService(GpriData dbOneGpri)
   {
      _dbOneGpri = dbOneGpri;
   }

   private string GenerateUniqueKey(GpriModel vagon)
   {
      return $"{vagon.DT.Date.ToShortDateString()}_{vagon.VR}_{vagon.NVAG}_{vagon.VESY}"; // Уникальный ключ
   }

   public async Task<int> EnqueueInsertAsync(GpriModel vagon)
   {
      string key = GenerateUniqueKey(vagon);
      var taskCompletionSource = new TaskCompletionSource<int>();

      if (_activeTasks.TryAdd(key, taskCompletionSource))
      {
         _queue.Enqueue((vagon, taskCompletionSource));
         _ = ProcessQueueAsync(); // Запускаем обработку очереди в фоне
      }
      else
      {
         Console.WriteLine($"[LOG] Ожидание завершения вставки для {key}");
         return await _activeTasks[key].Task; // Ожидаем завершения предыдущей вставки
      }

      return await taskCompletionSource.Task; // Ждем результат из ProcessQueueAsync()
   }

   private async Task ProcessQueueAsync()
   {
      await _semaphore.WaitAsync();
      try
      {
         while (_queue.TryDequeue(out var item))
         {
            var (vagon, taskCompletionSource) = item;
            string key = GenerateUniqueKey(vagon);

            try
            {
               int insertedId = await CheckAndInsertAsync(vagon);
               taskCompletionSource.SetResult(insertedId);
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Ошибка при вставке {key}: {ex.Message}");
               taskCompletionSource.SetException(ex);
            }
            finally
            {
               _activeTasks.TryRemove(key, out _);
            }
         }
      }
      finally
      {
         _semaphore.Release();
      }
   }

   // изменённый метод вставки в базу
   private async Task<int> CheckAndInsertAsync(GpriModel vagon)
   {
      var existing = await _dbOneGpri.СheckExisting(vagon);

      if (existing.HasValue)
      {
         Console.WriteLine($"[LOG] Запись {GenerateUniqueKey(vagon)} уже существует в БД (ID={existing.Value})");
         return existing.Value;
      }

      return await _dbOneGpri.InsNew(vagon);  // Если записи нет — выполняем вставку
   }
}
