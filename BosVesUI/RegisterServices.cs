using Blazor.IndexedDB;
using BosVesUI.Shared.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace BosVesUI;

public static class RegisterServices
{
   public static void ConfigureServices(this WebApplicationBuilder builder) 
   {
      if (builder.Environment.IsDevelopment()) 
      {
         builder.Configuration.AddUserSecrets<Program>();
      }

      builder.Services.AddRazorPages();
      builder.Services.AddServerSideBlazor();
      builder.Services.AddControllers();
      builder.Logging.ClearProviders();
      builder.Logging.AddConsole();
      //builder.Logging.AddApplicationInsights();
      //builder.Services.AddApplicationInsightsTelemetry();
      builder.Services.AddHttpContextAccessor();
      builder.Services.AddScoped<CircuitHandler, CustomCircuitHandler>();
      builder.Services.AddMemoryCache();
      builder.Services.AddHostedService<TempFileCleanupService>();            // сервис для очистки временного хранилища отчётов

      // Добавляем HttpClient для взаимодействия с API
      builder.Services.AddHttpClient("ReportApi", client =>
      {
         client.BaseAddress = new Uri("https://localhost:7221/");             // Замените на базовый URL вашего API
         client.Timeout = TimeSpan.FromSeconds(300);                          // Устанавливаем таймаут
         client.DefaultRequestHeaders.Add("Accept", "application/json");      // Задаем заголовки
      });

      builder.Services.AddScoped<IIndexedDbFactory, IndexedDbFactory>();
      builder.Services.AddSingleton<XmlDecodeService>();       // служба хмл - декодер
      builder.Services.AddSingleton<UserService>();            // служба служит обработчиком и хранилищем информации о пользователе
      //builder.Services.AddSingleton<TcpClientService>();       // служба получающая имя пк юзера по TCP от службы windows
      //builder.Services.AddHostedService<CommandService>();     // 
      builder.Services.Configure<ActiveDirectoryHelper>(builder.Configuration.GetSection("ActiveDirectory"));
      builder.Services.Configure<BosVesAppSettings>(builder.Configuration.GetSection("BosVesAppSettings")); // заполняю конфигурационными настройками классс, который планирую использовать во всём приложении
      builder.Services.AddSingleton<BosVesAppSettings>();      // 
      builder.Services.AddSingleton<GpriData>();               // 
      builder.Services.AddSingleton<GrasData>();               // 
      builder.Services.AddSingleton<GruzGdData>();             // 
      builder.Services.AddSingleton<KcexGdData>();             // 
      builder.Services.AddSingleton<OtvesnayaRPData>();        // 
      builder.Services.AddSingleton<ShablonData>();            // 
      builder.Services.AddSingleton<TaraPodstanovkaData>();    // 
      builder.Services.AddSingleton<VikVagData>();             // 
      builder.Services.AddSingleton<BruttoAsTaraData>();       // 
      builder.Services.AddSingleton<VagonsAPIData>();          // 
      builder.Services.AddSingleton<VagonsService>();          // 
      builder.Services.AddScoped<ProtectedSessionStorage>();   // InsertQueueService
      builder.Services.AddSingleton<InsertQueueService>();     // сервис для записи в базу данных, контролирующий, что в базе не существует записи с одинаковым dt, vr, nvag, vesy 
   }
}
