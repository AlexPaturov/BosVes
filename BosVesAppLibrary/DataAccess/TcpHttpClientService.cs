using System.Net.Sockets;
using System.Text;

namespace BosVesAppLibrary.DataAccess;

public class TcpHttpClientService
{
   private readonly string _ip;
   private readonly int _port;

   public TcpHttpClientService(string ip, int port)
   {
      _ip = ip;
      _port = port;
   }

   /// <summary>
   /// Makes an HTTP GET request to the specified path.
   /// </summary>
   public async Task<string> GetAsync(string path)
   {
      using TcpClient client = new TcpClient();
      await client.ConnectAsync(_ip, _port);

      using NetworkStream stream = client.GetStream();
      using StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
      using StreamReader reader = new StreamReader(stream, Encoding.ASCII);

      // Craft the HTTP GET request
      //string request = $"GET {path} HTTP/1.1\r\n" +
      //                 $"Host: {_ip}:{_port}\r\n" +
      //                 "Connection: close\r\n\r\n";

      string request = $"{path}";

      // Send the request
      await writer.WriteAsync(request);

      // Read the response
      string response = "not";
      try
      {
         response =  reader.ReadToEnd();
         Console.WriteLine(response);
      }
      catch (Exception ex) 
      {
         Console.WriteLine(ex.Message);
      }
      

      return response;
   }

   /// <summary>
   /// Makes an HTTP POST request to the specified path with the given body.
   /// </summary>
   public async Task<string> PostAsync(string path, string body, string contentType = "application/json")
   {
      using TcpClient client = new TcpClient();
      await client.ConnectAsync(_ip, _port);

      using NetworkStream stream = client.GetStream();
      using StreamWriter writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
      using StreamReader reader = new StreamReader(stream, Encoding.ASCII);

      // Calculate Content-Length
      int contentLength = Encoding.UTF8.GetByteCount(body);

      // Craft the HTTP POST request
      string request = $"POST {path} HTTP/1.1\r\n" +
                       $"Host: {_ip}:{_port}\r\n" +
                       "Connection: close\r\n" +
                       $"Content-Type: {contentType}\r\n" +
                       $"Content-Length: {contentLength}\r\n\r\n" +
                       $"{body}";

      // Send the request
      await writer.WriteAsync(request);

      // Read the response
      string response = await reader.ReadToEndAsync();

      return response;
   }
}
