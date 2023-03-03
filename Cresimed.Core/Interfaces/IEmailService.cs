using System;
using System.Collections.Generic;
using System.Text;
using Cresimed.Core.Entities;
using System.Threading.Tasks;
using Cresimed.Core.Services;
using Cresimed.Core.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Cresimed.Core.Entities.Base;

namespace Cresimed.Core.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }

}
