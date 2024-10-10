using System.Net.Sockets;
using System.Text;

namespace BosVesAppLibrary.DataAccess;

public class TcpClientService : IDisposable
{
   private TcpClient _client;
   private NetworkStream _stream;
   private readonly ILogger<TcpClientService> _logger;
   private readonly byte[] _buffer;
   private CancellationTokenSource _cancellationTokenSource;

   public TcpClientService(ILogger<TcpClientService> logger, int bufferSize = 4096)
   {
      _logger = logger;
      _buffer = new byte[bufferSize];
   }

   public async Task<bool> ConnectAsync(string ip, int port, TimeSpan sendTimeout, TimeSpan receiveTimeout)
   {
      try
      {
         _client = new TcpClient();
         _client.NoDelay = true;
         _client.SendTimeout = (int)sendTimeout.TotalMilliseconds;
         _client.ReceiveTimeout = (int)receiveTimeout.TotalMilliseconds;
         _client.ReceiveBufferSize = _buffer.Length;

         await _client.ConnectAsync(ip, port);
         _stream = _client.GetStream();

         _logger.LogInformation($"Connected to {ip}:{port}");
         return true;
      }
      catch (Exception ex)
      {
         _logger.LogError($"Connection error: {ex.Message}");
         throw;
      }
   }

   public async Task<string> SendCommandAsync(string command)
   {
      if (_stream == null || !_client.Connected)
         throw new InvalidOperationException("Not connected to the server.");

      try
      {
         byte[] data = Encoding.GetEncoding(1251).GetBytes(command);
         await _stream.WriteAsync(data, 0, data.Length);
         _logger.LogInformation($"Sent command: {command}");
         return "Command sent successfully.";
      }
      catch (Exception ex)
      {
         _logger.LogError($"Error sending command: {ex.Message}");
         throw;
      }
   }

   public async Task<string> ReceiveDataAsync(Func<string, Task> onDataReceived, CancellationToken token)
   {
      try
      {
         while (!token.IsCancellationRequested)
         {
            if (_stream.DataAvailable)
            {
               int bytesRead = await _stream.ReadAsync(_buffer, 0, _buffer.Length, token);
               if (bytesRead > 0)
               {
                  string receivedText = Encoding.GetEncoding(1251).GetString(_buffer, 0, bytesRead);
                  _logger.LogInformation($"Received {bytesRead} bytes: {receivedText}");
                  await onDataReceived(receivedText);
               }
            }
            else
            {
               await Task.Delay(10, token);
            }
         }

         return "Reception canceled.";
      }
      catch (OperationCanceledException)
      {
         _logger.LogInformation("Data reception canceled.");
         return "Reception canceled.";
      }
      catch (Exception ex)
      {
         _logger.LogError($"Data receiving error: {ex.Message}");
         throw;
      }
   }

   public void Dispose()
   {
      _stream?.Dispose();
      _client?.Close();
      _client?.Dispose();
      _cancellationTokenSource?.Cancel();
   }
}

