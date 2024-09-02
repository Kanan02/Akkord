using Application.Interfaces.ICommon;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Common
{
    public class ExcellService : IExcellService
    {
        public MemoryStream ExportList<T>(IReadOnlyList<T> list,string sheetName)
        {

            System.Text.EncodingProvider provider = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }

            stream.Position = 0;
            return stream;
        }
    }
}
