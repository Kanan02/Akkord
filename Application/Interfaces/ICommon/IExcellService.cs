using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Application.Interfaces.ICommon
{
    public interface IExcellService
    {
        MemoryStream ExportList<T>(IReadOnlyList<T> list, string sheetName);
    }
}
