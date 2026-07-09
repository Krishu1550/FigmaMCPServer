using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigmaMcp.Application.Interfaces
{
    public interface IImageExporter
    {
        Task<string> ExportAsync(
            string fileKey,
            string nodeId,
            string format);
    }
}
