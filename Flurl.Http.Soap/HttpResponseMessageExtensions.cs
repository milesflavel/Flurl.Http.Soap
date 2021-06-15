using System.Threading.Tasks;

namespace Flurl.Http.Soap
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<SoapEnvelope<T>> ReceiveSoap<T>(this Task<IFlurlResponse> response)
        {
            IFlurlResponse responseMessage = await response;

            return new SoapEnvelope<T>(await responseMessage.ResponseMessage.Content.ReadAsStringAsync());
        }
    }
}
