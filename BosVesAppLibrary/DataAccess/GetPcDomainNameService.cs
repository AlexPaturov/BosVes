namespace BosVesAppLibrary.DataAccess;

public class GetPcDomainNameService : IDisposable
{
   private readonly TcpClientService _tcpClientService;
   private readonly ILogger<GetPcDomainNameService> _logger;
   private readonly BosVesAppSettings _bosVesAppSettings;
   private readonly string _ip;        // ip службы отдающей имя пк
   private readonly int _port;         // порт службы отдающей имя пк
   private readonly string _command;   // порт службы отдающей имя пк
   private CancellationTokenSource _cancellationTokenSource;

   public GetPcDomainNameService(TcpClientService tcpClientService, 
                                 ILogger<GetPcDomainNameService> logger, 
                                 BosVesAppSettings bosVesAppSettings)
   {
      _tcpClientService = tcpClientService;
      _bosVesAppSettings = bosVesAppSettings;
      _logger = logger;
      _ip = "127.0.0.1";
      _port = 8887;
      _command = "<Request method='getPcName'/>";
   }

   // Called when the application starts.It connects to the TCP server and starts receiving data.
   public async Task StartAsync()
   {
      _logger.LogInformation("GetPcDomainNameService is starting.");
      _cancellationTokenSource = new CancellationTokenSource();// Initialize cancellation token

      // Example: Connect to the server on startup
      try
      {
         bool connected = await _tcpClientService.ConnectAsync(_ip, _port, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
         if (connected)
         {
            _logger.LogInformation("Connected to TCP server.");
            _ = _tcpClientService.ReceiveDataAsync(OnDataReceived, _cancellationTokenSource.Token);   // Start receiving data
            await SendCommand(); 
         }
      }
      catch (Exception ex)
      {
         _logger.LogError($"Error connecting to TCP server: {ex.Message}");
      }

   }

   private async Task SendCommand()
   {
      if (_tcpClientService.IsConnected)
      {
         try
         {
            await _tcpClientService.SendCommandAsync(_command);
            _logger.LogInformation($"Sent command: {_command}");
         }
         catch (Exception ex)
         {
            _logger.LogError($"Error sending command: {ex.Message}");
         }
      }
      else 
      {
         // бросить исключение - _tcpClientService не подключена
      }
   }

   // Callback for processing received data.
   private async Task OnDataReceived(string data)
   {
      _logger.LogInformation($"Data received: {data}");
      // Process the received data as needed
      // For example, trigger notifications, update state, etc.

      _bosVesAppSettings.pcName = data;
      await StopAsync();
   }

   // Called when the application is stopping. It cancels ongoing operations and disposes of resources.
   public Task StopAsync()
   {
      _logger.LogInformation("GetPcDomainNameService is stopping.");
      _cancellationTokenSource.Cancel();
      return Task.CompletedTask;
   }

   //  Cleans up timers and cancellation tokens.
   public void Dispose()
   {
      _cancellationTokenSource?.Dispose();
   }
}
