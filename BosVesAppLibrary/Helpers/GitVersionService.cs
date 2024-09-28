using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace BosVesAppLibrary.Helpers;

public class GitVersionService
{
   private readonly IWebHostEnvironment _env;

   public GitVersionService(IWebHostEnvironment env)
   {
      _env = env;
   }

   public async Task<string> GetGitVersionAsync()
   {
      var filePath = Path.Combine(_env.WebRootPath, "gitversion.txt");
      if (File.Exists(filePath))
      {
         return await File.ReadAllTextAsync(filePath);
      }
      return "Unknown Version";
   }
}

