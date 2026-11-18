using System.Threading;
using System.Threading.Tasks;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources.Interfaces
{
    public interface IHelpCenterLocalesResource
    {
        Task<HelpCenterLocales> GetAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}