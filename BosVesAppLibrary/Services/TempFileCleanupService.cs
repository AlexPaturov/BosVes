using BosVesAppLibrary.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BosVesAppLibrary.Services;

public class TempFileCleanupService : BackgroundService
{
   private readonly IWebHostEnvironment _webHostEnvironment;
   private static readonly object _fileLock = new object();

   public TempFileCleanupService(IWebHostEnvironment webHostEnvironment)
   {
      _webHostEnvironment = webHostEnvironment;
   }

   protected override async Task ExecuteAsync(CancellationToken stoppingToken)
   {
      string dirPath = string.Empty;

      while (!stoppingToken.IsCancellationRequested)
      {
         dirPath = Path.Combine(_webHostEnvironment.WebRootPath, "temp");
         if (Directory.Exists(dirPath))
         {
            var files = Directory.GetFiles(dirPath);

            foreach (var file in files)
            {
               var creationTime = File.GetCreationTime(file);
               if (DateTime.Now - creationTime > TimeSpan.FromMinutes(1)) // Удаляем файлы старше 
               {
                  SafeDeleteFileWithCheck(file);
               }
            }
         }
         else 
         {
            if (!string.IsNullOrWhiteSpace(dirPath)) 
            {
               try
               {
                  Directory.CreateDirectory(dirPath);
               }
               catch (Exception ex) 
               {
                  // write to log
               }
            }

         }

         await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Проверка каждые 5 минут
      }
   }

   public void SafeDeleteFileWithCheck(string filePath)
   {
      if (!File.Exists(filePath))
         return;

      try
      {
         using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
         {
            stream.Close(); // Закрываем поток, если удалось открыть файл.
         }
         File.Delete(filePath);
      }
      catch (IOException)
      {
         // переделать на telemetry client
         Console.WriteLine("Файл используется другим процессом. Удаление невозможно.");
      }
      catch (UnauthorizedAccessException)
      {
         // переделать на telemetry client
         Console.WriteLine("Нет доступа к файлу. Удаление невозможно.");
      }
   }
  
}
