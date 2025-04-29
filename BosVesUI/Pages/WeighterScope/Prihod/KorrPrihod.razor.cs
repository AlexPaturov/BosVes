using FastReport;
using FastReport.Export.PdfSimple;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NetcodeHub.Packages.Components.Modal;
using System.Globalization;

namespace BosVesUI.Pages.WeighterScope.Prihod;
public partial class KorrPrihod
{
   //------------ nullable timespan begin --------------------------------------------------

   private TimeSpan? myTimeSpan = null;

   private void HandleTimeSpanChanged(TimeSpan? newTimeSpan)
   {
      myTimeSpan = newTimeSpan;
      StateHasChanged(); // Обновляем UI
   }

   //------------ nullable timespan end  ---------------------------------------------------

   private string FormattedDate
   {
      get => inDt.ToString("dd.MM.yyyy"); // Get formatted string
      set
      {
         if (DateTime.TryParseExact(value, "dd.MM.yyyy",
                                    CultureInfo.InvariantCulture,
                                    DateTimeStyles.None, out var parsedDate))
         {
            inDt = parsedDate; // Only set _date if parsing succeeds
         }
         else
         {
            // Handle invalid date input if necessary (e.g., show an error message)
         }
      }
   }
   #region
   private string? inputValue;

   private void OnInput(string value)
   {
      var newValue = value ?? string.Empty;

      inputValue = newValue.Length > 4 ? "Long!" : newValue;
   }
   #endregion
   private decimal decimalValue = 0;
   private NumberStyles style = NumberStyles.Integer | NumberStyles.AllowLeadingSign;
   private CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
   private string DecimalValue
   {
      get
      {
         return vagon.NDOK?.ToString("0", culture) ?? string.Empty;
      }
      set
      {
         if (Decimal.TryParse(value, style, culture, out var number))
         {
            if (number > 99999999)
            {
               vagon.NDOK = 0;
               decimalValue = 0;
            }
            else
            {
               vagon.NDOK = number;
               decimalValue = number;
            }
         }
      }
   }
   private double? NDOK { get; set; }
   private void HandleInput(ChangeEventArgs e)
   {
      // Get the input value as a string
      var inputValue = e.Value?.ToString();

      // Validate or manipulate the input value (example: restrict to max 8 digits)
      if (inputValue != null && inputValue.Length > 8)
      {
         inputValue = inputValue.Substring(0, 8); // Restrict to max 8 characters
      }

      // Update the bound property with the validated/modified value
      if (double.TryParse(inputValue, out double result))
      {
         NDOK = result;
         StateHasChanged();
      }
      else
      {
         NDOK = null; // Reset or handle invalid input
      }

   }
   private bool isLoading = false;       // To control loading state
   private bool isSubmitting = false;
   #region Переменные формы
   // private DateTime inDt = DateTime.ParseExact("2024-07-05", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
   // private DateTime inDt = DateTime.Parse("05.07.2024");
   private DateTime inDt = DateTime.ParseExact("05.07.2024", "dd.MM.yyyy", CultureInfo.InvariantCulture);
   private DateTime inVr = DateTime.ParseExact("10:13:33", "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
   private string inNvag = string.Empty;
   private List<GpriModel> listVags = null;
   private GpriModel vagon = new GpriModel();
   private int? selectedRowId;                                             // Временно для отображения Id выбранной ячейки.
   private bool isSaving = false;
   private string selRowID = string.Empty;
   private Dictionary<int, bool> selectedItems = new Dictionary<int, bool>();
   private bool isChecked = false; // for checkbox - temp
                                   // NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

   private bool isDeleting = false;

   #endregion
   #region Переменные для модального окноа Брутто как тара
   private DateTime inDTBEGIN = DateTime.Today.AddDays(-30);
   private DateTime inDTEND = DateTime.Today;
   private bool IsOpenBrAsTar { get; set; }
   private string? SelectedActionBrAsTar { get; set; }
   private bool ConfirmBrAsTar { get; set; }
   private Confirmation? confirmationBrAsTar;
   private int? selectedRowIdBruttoAsTara = 0;
   private List<BruttoAsTaraModel> listBruttoAsTara = null;                // Для заполнения таблицы в модальном окне "Брутто как тара".
   private BruttoAsTaraModel bruttoAsTaraOne = new BruttoAsTaraModel();    // Для получения выбранной в модальном окне строки.
   #endregion
   #region Переменные для модального окноа Взять тару
   private DateTime inDTBEGINtara = DateTime.Today.AddDays(-30);
   private DateTime inDTENDtara = DateTime.Today;
   private bool IsOpenTara { get; set; }
   private string? SelectedActionTara { get; set; }
   private bool ConfirmTara { get; set; }
   private Confirmation? confirmationTara;
   private int? selectedRowIdTara = 0;
   private List<TaraPodstanovkaModel> listTara = null;                // Для заполнения таблицы в модальном окне "Брутто как тара".
   private TaraPodstanovkaModel taraOne = new TaraPodstanovkaModel();    // Для получения выбранной в модальном окне строки.
   #endregion
   #region работаем с модальным окном взять цех
   private bool IsOpenGetCex { get; set; }
   private int? selectedRowCex = 0;
   private string PdfUrl;


   private void OpenModalGetCex()
   {
      IsOpenGetCex = true;
   }
   private void ButtonGetCexActions()
   {

   }
   private void GetCexClick()
   {

   }
   #endregion
   #region GetRowClassCex()
   private string GetRowClassCex(short? cex)
   {
      return selectedRowCex == cex ? "selected-row" : "";
   }
   #endregion
   #region OnInitializedAsync()
   protected async override Task OnInitializedAsync()
   {
      // перерисовываю ширину окна броузера - раз в секунду

   }
   #endregion

   // *****************************************************
   private void Print()
   {
      if ((listVags is not null) && (listVags.Count > 0))
      {
         string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot\\otvesnaya.frx");
         var pdf = new PDFSimpleExport();
         FastReport.Utils.Config.WebMode = true;
         Report rep = new Report();
         rep.Load(path);
         rep.RegisterData(listVags, "GpriModelRef");
         rep.PrepareAsync();
         rep.Export(pdf, "C:\\temp\\test.pdf");
      }
   }
   // *****************************************************

   // *****************************************************
   private async Task GenerateAndDownloadReport()
   {
      if ((listVags is not null) && (listVags.Count > 0))
      {
         string reportPath = Path.Combine(WebHostEnvironment.ContentRootPath, "wwwroot", "otvesnaya.frx");// Путь к шаблону отчета
         using var report = new FastReport.Report();                         // Создание объекта отчета
         report.Load(reportPath);
         report.RegisterData(listVags, "GpriModelRef");                      // Регистрация данных
         await report.PrepareAsync();                                        // Подготовка отчета
         using var memoryStream = new MemoryStream();                        // Экспорт отчета в PDF
         var pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
         report.Export(pdfExport, memoryStream);
         memoryStream.Position = 0;                                          // Сбросить позицию потока для передачи данных
         var pdfBytes = memoryStream.ToArray();                              // Передача PDF-файла на скачивание
         var base64Pdf = Convert.ToBase64String(pdfBytes);
         await JS.InvokeVoidAsync("downloadFile", "Report.pdf", base64Pdf);
      }
   }
   // *****************************************************

   // Без сохранения файла локально - сразу вывожу на печать в iframe, этот вариант я использую в bos-ves
   // ******************************************************
   private async Task GenerateAndShowReport()
   {
      if ((listVags is not null) && (listVags.Count > 0))
      {
         string reportPath = Path.Combine(WebHostEnvironment.ContentRootPath, "wwwroot", "otvesnaya.frx");
         using var report = new Report();                                                             // Создание объекта отчета
         report.Load(reportPath);
         report.RegisterData(listVags, "GpriModelRef");                                                          // Регистрация данных
         await report.PrepareAsync();                                                                            // Подготовка отчета
         using var memoryStream = new MemoryStream();                                                            // Экспорт отчета в PDF
         var pdfExport = new PDFSimpleExport();
         report.Export(pdfExport, memoryStream);
         memoryStream.Position = 0;                                                                              // Сбросить позицию потока для передачи данных

         // Преобразуем PDF в base64
         var pdfBase64 = Convert.ToBase64String(memoryStream.ToArray());
         // Создаем Data URL для iframe
         PdfUrl = $"data:application/pdf;base64,{pdfBase64}";
         StateHasChanged();
      }
   }
   // ******************************************************
   private async Task GenerateAndStorePdf()
   {
      // Пример PDF-данных
      byte[] pdfData = System.Text.Encoding.UTF8.GetBytes("Example PDF content");

      // Передаем PDF в JavaScript
      await JS.InvokeVoidAsync("savePdfToIndexedDB", pdfData, "example.pdf");
   }

   private async Task RetrieveAndPrintPdf()
   {
      // Получаем PDF из IndexedDB
      await JS.InvokeVoidAsync("getPdfFromIndexedDB", "example.pdf", DotNetObjectReference.Create(this));
   }

   [JSInvokable]
   public void OpenPdf(byte[] pdfBlob)
   {
      var base64Pdf = Convert.ToBase64String(pdfBlob);
      var url = $"data:application/pdf;base64,{base64Pdf}";
      JS.InvokeVoidAsync("window.open", url, "_blank");
   }

   //******************************************************
   private async Task OpenReport()
   {
      // // URL API для генерации отчета
      // var reportUrl = $"/api/Report/GeneratePdf?dtBegin={inDt.ToString("yyyy-MM-dd")}&vrBegin={inVr.ToString("HH:mm:ss")}&vesy={"10"}";

      // // Открываем отчет в новом окне или вкладке
      // await JS.InvokeVoidAsync("open", reportUrl, "_blank");


      if ((listVags is not null) && (listVags.Count > 0))
      {
         // Получаем HttpClient из фабрики
         var client = HttpClientFactory.CreateClient("ReportApi");

         try
         {
            // Выполняем POST-запрос
            var response = await client.PostAsJsonAsync("api/Report/GeneratePdfForPrint", listVags);

            if (response.IsSuccessStatusCode)
            {
               string result = await response.Content.ReadAsStringAsync();
               result = result.Replace('"', ' ').Trim();


               // Открываем PDF в новом окне для печати
               await JS.InvokeVoidAsync("openPdfForPrint", result);
            }
            else
            {
               Console.WriteLine($"Ошибка при генерации отчета: {response.ReasonPhrase}");
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine($"Ошибка: {ex.Message}");
         }


      }

   }

   //*************** DownloadReportAsync() **************************************/
   private async Task DownloadReportAsync() 
   {
      // Пример данных
      var data = new List<GpriModelT>
        {
            new GpriModelT { DT = DateTime.Now, VR = new TimeSpan(12, 0, 0), NVAG = "12345", BRUTTO = 61.5, ID = 1 },
            new GpriModelT { DT = DateTime.Now.AddDays(-1), VR = new TimeSpan(14, 30, 0), NVAG = "67890", BRUTTO = 62.1, ID = 2 }
        };

      // Генерация отчёта
      var excelBytes = await GenerateExcelReportAsync(data);

      // Создание Base64 строки для передачи в браузер
      var base64 = Convert.ToBase64String(excelBytes);
      var fileName = "Отчёт.xlsx";
      var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      var url = $"data:{mimeType};base64,{base64}";

      // Вызов JavaScript для скачивания файла
      await JS.InvokeVoidAsync("BlazorDownloadFile", url, fileName);
   }

   public async Task<byte[]> GenerateExcelReportAsync(List<GpriModelT> data)
   {
      using var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Отчёт");

      // Заголовки столбцов
      worksheet.Cell(1, 1).Value = "Дата";
      worksheet.Cell(1, 2).Value = "Время";
      worksheet.Cell(1, 3).Value = "Номер вагона";
      worksheet.Cell(1, 4).Value = "Брутто";
      worksheet.Cell(1, 5).Value = "ID";

      // Установка стилей для заголовков
      var headerRange = worksheet.Range(1, 1, 1, 5);
      headerRange.Style.Font.Bold = true;
      headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

      // Заполнение данными
      for (int i = 0; i < data.Count; i++)
      {
         var row = i + 2; // Начинаем со второй строки
         worksheet.Cell(row, 1).Value = data[i].DT.ToString("dd.MM.yyyy");
         worksheet.Cell(row, 2).Value = data[i].VR.ToString(@"hh\:mm\:ss");
         worksheet.Cell(row, 3).Value = data[i].NVAG;
         worksheet.Cell(row, 4).Value = data[i].BRUTTO ?? 0;
         worksheet.Cell(row, 5).Value = data[i].ID;
      }

      // Автоматическое изменение ширины столбцов
      worksheet.Columns().AdjustToContents();

      // Сохранение в память
      using var stream = new MemoryStream();
      workbook.SaveAs(stream);
      return await Task.FromResult(stream.ToArray());
   }
   //*************** ReportEXEL() ***************************************/

   private async Task TestJsFunction()
   {
      await JS.InvokeVoidAsync("console.log", "JavaScript is working!");
      await JS.InvokeVoidAsync("openPdfForPrint", "https://example.com/sample.pdf");
   }

   #region  HandleValidSubmit()
   private void HandleValidSubmit()
   {
      //
   }
   #endregion
   #region OpenModalGetTara()
   private async Task OpenModalGetTara()
   {
      IsOpenTara = true;
      await GetTaraClick();
   }
   #endregion
   #region GetTaraClick()
   private async Task GetTaraClick()
   {
      if ((selectedRowId is not null) && (selectedRowId > 0)) // если клик по строке был и есть выбранный вагон
      {   // На открытии окна - забирать данные с условием для 2-х и есть шаблон -> 1, иначе -> 2
         if ((vagon is not null) && (vagon.VESY != null) && (vagon.VESY == 2) && (!string.IsNullOrEmpty(vagon.SHABLON)) && (vagon.SHABLON.ToLower() == "yes")) // приводить к нижнему регистру
         {
            listTara = (List<TaraPodstanovkaModel>)await taraPodstanovka.GetTwentyOneDays(
                (listVags.Where(i => i.ID == selectedRowId).FirstOrDefault()).NVAG
            ); // получил список вагонов для выборки подстановки тары с ограничением в 21 день
         }
         else
         {
            listTara = (List<TaraPodstanovkaModel>)await taraPodstanovka.GetFromYear(
                inDTBEGINtara, inDTENDtara, (listVags.Where(i => i.ID == selectedRowId).FirstOrDefault()).NVAG
            ); // получил список вагонов для выборки подстановки тары -> ограничение 1 год
         }
      }
   }
   #endregion
   #region GetRowClassTara()
   private string GetRowClassTara(int? id)
   {
      return selectedRowIdTara == id ? "selected-row" : "";
   }
   #endregion

   #region Работа с чекбоксами таблицы промсмотра
   private void InitializeSelected(List<GpriModel> listVagons)
   {

      // Initialize the dictionary with item IDs
      foreach (var item in listVags)
      {
         selectedItems[item.ID] = false; // Initially set all as unselected
      }
   }
   #endregion
   #region OnRowGetTaraClick()
   private void OnRowGetTaraClick(int? id)
   {
      selectedRowIdTara = id;
   }
   #endregion
   #region ButtonGetTaraActions()
   void ButtonGetTaraActions(string action)
   {
      SelectedActionTara = action;
      if (action == "ok")
      {
         ConfirmTara = true; //
         if (selectedRowIdTara > 0)
         {
            taraOne = (TaraPodstanovkaModel)(listTara.Where(i => i.ID == selectedRowIdTara).FirstOrDefault()).Clone();
            if ((vagon is not null) && (taraOne is not null) && (vagon.BRUTTO > taraOne.TAR_BRS))// Присвоить, брутто под тару, пересчитать нетто
            {
               vagon.TAR_BRS = taraOne.TAR_BRS;
               vagon.NETTO = vagon.BRUTTO - vagon.TAR_BRS;
            }

            taraOne = new TaraPodstanovkaModel();                      // стираем данные в объекте
            selectedRowIdTara = 0;
         }
      }
      else if (action == "cancel")
      {
         taraOne = new TaraPodstanovkaModel();                      // стираем данные в объекте
         selectedRowIdTara = 0;
         ConfirmTara = false;    // you can also do something
         inDTBEGINtara = DateTime.Today.AddDays(-30);              // Для модального окошка "Брутто как тара".
         inDTENDtara = DateTime.Today;
      }
   }
   #endregion
   #region GetByDtVr()
   private async Task GetByDtVr()
   {
      //listVags = ((List<GpriModel>)await gpridata.GetByDtVr(inDt.ToString("yyyy-MM-dd"), inVr.ToString("HH:mm:ss"), "10")).OrderByDescending(x => x.ID).ToList();
      listVags = ((List<GpriModel>)await gpridata.GetByDt(inDt.ToString("yyyy-MM-dd"),  "10")).OrderByDescending(x => x.ID).ToList();
      InitializeSelected(listVags);
   }

   #endregion

   #region действия на клике по строке в гриде просмотра вагонов
   private void OnRowClick(int? id)
   {
      CleanModels();                                                                      // Чистим поля при переходе
      selectedRowId = id;
      selRowID = selectedRowId.ToString();                                                // Для отладки - отображаю Id записи по которой кликнул.
      vagon = (GpriModel)(listVags.Where(i => i.ID == id).FirstOrDefault()).Clone();      // Взять все данные из клика по гриду -> переместить в объект.
   }

   private string GetRowClass(int id)
   {
      // Use TryGetValue to safely access the dictionary
      return selectedItems.TryGetValue(id, out bool isSelected) && isSelected ? "table-active" : string.Empty;
   }
   #endregion
   #region GetRowClassBruttoAsTara()
   private string GetRowClassBruttoAsTara(int? id)
   {
      return selectedRowIdBruttoAsTara == id ? "selected-row" : "";
   }
   #endregion
   #region OnRowGetBruttoAsTaraClick()
   private void OnRowGetBruttoAsTaraClick(int? id)
   {
      selectedRowIdBruttoAsTara = id;
   }
   #endregion
   #region OnRowGetCexClick()
   private void OnRowGetCexClick(int? id)
   {
      selectedRowIdBruttoAsTara = id;
   }
   #endregion
   #region ButtonGetBruttoAsTaraActions(string)
   void ButtonGetBruttoAsTaraActions(string action)
   {
      SelectedActionBrAsTar = action;
      if (action == "ok")
      {
         ConfirmBrAsTar = true; //
         if (selectedRowIdBruttoAsTara > 0)
         {
            bruttoAsTaraOne = (BruttoAsTaraModel)(listBruttoAsTara.Where(i => i.ID == selectedRowIdBruttoAsTara).FirstOrDefault()).Clone();
            if ((vagon is not null) && (bruttoAsTaraOne is not null) && (vagon.BRUTTO > bruttoAsTaraOne.BRUTTO))// Присвоить, брутто под тару, пересчитать нетто
            {
               vagon.TAR_BRS = bruttoAsTaraOne.BRUTTO;
               vagon.NETTO = vagon.BRUTTO - vagon.TAR_BRS;
            }
            bruttoAsTaraOne = new BruttoAsTaraModel();                      // стираем данные в объекте
            selectedRowIdBruttoAsTara = 0;
         }
      }
      else if (action == "cancel")
      {
         bruttoAsTaraOne = new BruttoAsTaraModel();
         selectedRowIdBruttoAsTara = 0;
         ConfirmBrAsTar = false;    // you can also do something
         inDTBEGIN = DateTime.Today.AddDays(-30);               // Для модального окошка "Брутто как тара".
         inDTEND = DateTime.Today;
      }
   }
   #endregion
   #region OpenModalGetBruttoAsTara()
   private async Task OpenModalGetBruttoAsTara()
   {
      IsOpenBrAsTar = true;
      await GetBruttoAsTaraClick();
   }
   #endregion
   #region GetBruttoAsTaraClick()
   private async Task GetBruttoAsTaraClick()
   {
      listBruttoAsTara = (List<BruttoAsTaraModel>)await bruttoAsTara.GetByRangeDtAndNvag(inDTBEGIN.ToString("yyyy-MM-dd"), inDTEND.ToString("yyyy-MM-dd"), vagon.NVAG, "10");
   }
   #endregion
   #region UpdVag
   private async Task UpdVag()
   {
      if (vagon is not null)
      {
         try
         {
            var affectedId = await gpridata.UpdVagAsync(vagon);
            Console.WriteLine($"affectedId:{affectedId}" );
            vagon = new GpriModel();    // чистим модель
            if(affectedId > 0)
            await GetByDtVr();          // обновляем данные на странице
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.ToString());
            // переделать на запись в файл
         }

      }
      // иначе показать всплывающее окошко - нечего сохранять
   }
   #endregion
   //-------------------------------------------------------------------------------
   private async Task InsVag()
   {
      if (isLoading)
      {
         Console.WriteLine("Was try to write second record the same time");
         return;
      }

      isLoading = true;                                       // Show the loading indicator
      var resutTmp = await vagonService.SaveVagons(vagon);    // Simulate a long-running database query (replace this with actual database call)
      Console.WriteLine($"Vagon {resutTmp} was inserted in database");
      vagon = new GpriModel();                                // чистим модель
      await GetByDtVr();                                      // обновляем данные на странице

      isLoading = false;                                      // Hide the loading indicator

   }
   //-------------------------------------------------------------------------------
   #region Показываю справочник цехов
   [Parameter]
   public bool IsCheckedCexList { get; set; } = false;
   private bool isCheckedCexList;
   protected override void OnParametersSet()
   {
      isCheckedCexList = this.IsCheckedCexList;
   }

   private void GetListCex(ChangeEventArgs e)
   {
      if (e != null)
      {
         bool res = (bool)e.Value;
         if (res)
         {
            OpenModalGetCex();
         }
      }
   }
   #endregion
   #region CleanModel()
   private void CleanModels()
   {
      vagon = new GpriModel();
      bruttoAsTaraOne = new BruttoAsTaraModel();                      // стираем данные в объекте
      selectedRowIdBruttoAsTara = 0;
      selectedRowId = 0;
   }
   #endregion
   #region ClosePage()
   private void ClosePage()
   {
      navManager.NavigateTo("/");
   }
   #endregion
}

public class GpriModelT
{
   public DateTime DT { get; set; }

   public TimeSpan VR { get; set; }

   public string NVAG { get; set; }

   public double? BRUTTO { get; set; }

   public int ID { get; set; }
}