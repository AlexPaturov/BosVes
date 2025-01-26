using Blazor.IndexedDB;
using Microsoft.JSInterop;
using System;

namespace BosVesUI.Pages.PdfService;

public class ExampleDb : IndexedDb
{

   public ExampleDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }

   public IndexedSet<Person> People { get; set; }
   public IndexedSet<PdfDocument> PdfDocuments { get; set; }

}
