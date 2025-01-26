namespace BosVesUI.Pages.PdfService;

public class Person
{
   [System.ComponentModel.DataAnnotations.Key]
   public long Id { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
}
