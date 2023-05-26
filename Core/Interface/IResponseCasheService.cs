using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IResponseCasheService
    {
        Task CachResponseAsync(string casheKey, string reposonse, TimeSpan timetoLive);

        Task<string> CachResponseAsync(string casheKey);
    }
}
