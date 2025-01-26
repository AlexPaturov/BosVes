using System.Collections.Concurrent;

namespace BosVesAppLibrary.Services;
public class InsertQueueService
{
   private readonly ConcurrentQueue<GpriModel> _queue = new();
   private readonly ConcurrentDictionary<string, TaskCompletionSource<bool>> _activeTasks = new();
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

      // Проверяем, есть ли уже активная задача для этого ключа
      var taskCompletionSource = new TaskCompletionSource<bool>();

      if (_activeTasks.TryAdd(key, taskCompletionSource))
      {
         _queue.Enqueue(vagon);
         _ = ProcessQueueAsync(); // Запускаем обработку очереди в фоне
      }
      else
      {
         Console.WriteLine($"[LOG] Ожидание завершения вставки для {key}");
         await _activeTasks[key].Task; // Ожидаем завершения предыдущей вставки
      }

      // После завершения ждем подтверждения результата

      return await CheckAndInsertAsync(vagon);
   }

   private async Task ProcessQueueAsync()
   {
      await _semaphore.WaitAsync();
      try
      {
         while (_queue.TryDequeue(out var vagon))
         {
            string key = GenerateUniqueKey(vagon);

            try
            {
               await CheckAndInsertAsync(vagon);
            }
            finally
            {
               // Сигнализируем, что запись обработана
               if (_activeTasks.TryRemove(key, out var tcs))
               {
                  tcs.SetResult(true);
               }
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

      //if (existing.HasValue)
      //{
      //   Console.WriteLine($"[LOG] Запись {GenerateUniqueKey(vagon)} уже существует в БД (ID={existing.Value})");
      //   return existing.Value;
      //}

      if (existing.HasValue)
         return existing.Value;

      // Если записи нет — выполняем вставку
      return await _dbOneGpri.InsNew(vagon);
   }
}
