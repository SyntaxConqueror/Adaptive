using ClosedXML.Excel;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LR7.Services
{
    public class VersioningService : IVersioningService
    {
        public IActionResult GetV1()
        {
            return new OkObjectResult(200);
        }

        public IActionResult GetV2()
        {
            return new OkObjectResult("Everything is ok!");
        }

        public IActionResult GetV3()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sample");
            worksheet.Cell(1, 1).Value = "Hello, ClosedXML!";

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "sample.xlsx"
            };
        }
    }
}
