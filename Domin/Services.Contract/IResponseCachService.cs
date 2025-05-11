using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer
{
    public interface IResponseCachService
    {
        Task CashResponseAsync(string CachKey, object Response, TimeSpan TimeLive);
        Task<string?> GetCashResponseAsync(string Cashkey);
    }
}
