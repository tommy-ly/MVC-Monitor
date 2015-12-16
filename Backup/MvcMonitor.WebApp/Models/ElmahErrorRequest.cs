namespace MvcMonitor.Models
{
    public class ElmahErrorRequest
    {
        public string ErrorId { private set; get; }
        public string SourceApplicationId { get; private set; }
        public string InfoUrl { get; private set; }
        public ElmahErrorDetailDto Error { get; private set; }

        public ElmahErrorRequest(string errorId, string sourceApplicationId, string infoUrl, ElmahErrorDetailDto error)
        {
            ErrorId = errorId;
            SourceApplicationId = sourceApplicationId;
            InfoUrl = infoUrl;
            Error = error;
        }
    }
}