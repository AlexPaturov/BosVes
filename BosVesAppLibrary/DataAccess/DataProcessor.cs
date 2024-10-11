namespace BosVesAppLibrary.DataAccess;


public class DataProcessor
{
   private readonly TcpClientService _tcpClientService;
   private readonly ILogger<DataProcessor> _logger;

   public DataProcessor(TcpClientService tcpClientService, ILogger<DataProcessor> logger)
   {
      _tcpClientService = tcpClientService;
      _logger = logger;
   }

   /// <summary>
   /// Sends a specific command to the TCP server and processes the response.
   /// </summary>
   public async Task<bool> SendCommandAndProcessAsync(string command)
   {
      if (!_tcpClientService.IsConnected)
      {
         _logger.LogWarning("Cannot send command. Not connected to the server.");
         return false;
      }

      try
      {
         await _tcpClientService.SendCommandAsync(command);
         _logger.LogInformation($"Sent command: {command}");

         // Optionally, wait for a specific response or handle data in the callback
         // For demonstration, we assume the response is handled elsewhere

         return true;
      }
      catch (Exception ex)
      {
         _logger.LogError($"Error sending command: {ex.Message}");
         return false;
      }
   }

   /// <summary>
   /// Example method to initiate a data processing task.
   /// </summary>
   //public async Task StartProcessingAsync()
   //{
   //   // Example: Send multiple commands or handle complex processing
   //   string[] commands = { "CMD1", "CMD2", "CMD3" };
   //   foreach (var cmd in commands)
   //   {
   //      bool success = await SendCommandAndProcessAsync(cmd);
   //      if (!success)
   //      {
   //         _logger.LogError($"Failed to send command: {cmd}");
   //         // Handle failure as needed
   //      }
   //   }
   //}
}


