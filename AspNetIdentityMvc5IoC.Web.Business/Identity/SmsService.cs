using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AspNetIdentityMvc5IoC.Web.Business.Identity
{
	public class SmsService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			// Plug in your SMS service here to send a text message.
			return Task.FromResult(0);
		}
	}
}