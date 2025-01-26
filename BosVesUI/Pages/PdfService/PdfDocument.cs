namespace BosVesUI.Pages.PdfService;

public class PdfDocument
{
   public int Id { get; set; } // Уникальный идентификатор
   public string FileName { get; set; } // Имя файла
   public byte[] FileContent { get; set; } // Содержимое PDF в формате байтов
}
