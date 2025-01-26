using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BosVesAppLibrary.Controllers;

[Route("api/Report")]
[ApiController]
public class ReportController : ControllerBase
{
   private readonly IWebHostEnvironment _webHostEnvironment;

   public ReportController(IWebHostEnvironment webHostEnvironment)
   {
      _webHostEnvironment = webHostEnvironment;
   }

   [HttpPost("GeneratePdfForPrint")]
   public async Task<IActionResult> GeneratePdfForPrint([FromBody] List<GpriModel> data)
   {
      if (data == null || !data.Any())
      {
         return BadRequest("No data provided.");
      }

      var reportPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", "otvesnaya.frx");
      using var report = new Report();
      report.Load(reportPath);
      report.RegisterData(data, "GpriModel");
      report.Prepare();

      using var memoryStream = new MemoryStream();
      var pdfExport = new PDFSimpleExport();
      report.Export(pdfExport, memoryStream);

      memoryStream.Position = 0;

      // Генерируем имя для PDF файла
      var pdfFileName = "Report_" + Guid.NewGuid().ToString() + ".pdf";
      var pdfFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "reports", pdfFileName);

      // Сохраняем PDF в директорию "wwwroot/reports"
      await System.IO.File.WriteAllBytesAsync(pdfFilePath, memoryStream.ToArray());

      // Возвращаем URL для доступа к сохраненному файлу
      //var pdfUrl = $"/reports/{pdfFileName}";
      return Ok($"/reports/{pdfFileName}");
   }

}