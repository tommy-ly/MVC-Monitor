using Newtonsoft.Json;

namespace MvcMonitor.Models.Factories
{
    public class ElmahErrorDetailDtoFactory : IElmahErrorDetailDtoFactory
    {
        public ElmahErrorDetailDto Create(string errorParameters)
        {
            return JsonConvert.DeserializeObject<ElmahErrorDetailDto>(errorParameters);
        }
    }
}