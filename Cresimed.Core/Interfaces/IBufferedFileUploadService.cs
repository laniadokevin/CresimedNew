using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Microsoft.AspNetCore.Http;

namespace Cresimed.Core.Interfaces
{
    public interface IBufferedFileUploadService
    {
        bool UploadFile(IFormFile file, string date);
    }
}
