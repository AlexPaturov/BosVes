namespace BosVesUI.Pages;
public partial class KorrGpri
{
   private GpriModel vagon = new GpriModel();
   private readonly Logger<KorrGpri> _logger;

   private async Task OnInitialize()
   {

   }

   private void HandleValidSubmit()
   {
      // Handle the valid form submission
      //if (vagon.ModelState.isValid)
   }

   private async void SaveToDb()
   {
      await gpridata.InsNew(vagon);
   }

   private async void GetByDtVr(string pDt, string pVr, string vikno) // @onclick=ShowDtVr
   {
      _logger.LogDebug("GetByDtVr");
      await gpridata.GetByDtVr(pDt, pVr, vikno);
   }

   private void HandleClick(string parameter)
   {
      // message = parameter;
   }
}