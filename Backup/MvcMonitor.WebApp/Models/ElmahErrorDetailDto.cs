namespace MvcMonitor.Models
{
    public class ElmahErrorDetailDto
    {
        public string application { get; set; }
        public string host { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string source { get; set; }
        public string detail { get; set; }
        public string user { get; set; }
        public string time { get; set; }
        public string statusCode { get; set; }
        public ServerVariablesDto serverVariables { get; set; }
    }

    public class ServerVariablesDto
    {
        public string ALL_HTTP { get; set; }
        public string ALL_RAW { get; set; }
        public string APPL_PHYSICAL_PATH { get; set; }
        public string AUTH_TYPE { get; set; }
        public string AUTH_USER { get; set; }
        public string AUTH_PASSWORD { get; set; }
        public string LOGON_USER { get; set; }
        public string REMOTE_USER { get; set; }
        public string CONTENT_LENGTH { get; set; }
        public string LOCAL_ADDR { get; set; }
        public string PATH_INFO { get; set; }
        public string PATH_TRANSLATED { get; set; }
        public string REMOTE_ADDR { get; set; }
        public string REMOTE_HOST { get; set; }
        public string REQUEST_METHOD { get; set; }
        public string SCRIPT_NAME { get; set; }
        public string SERVER_NAME { get; set; }
        public string SERVER_PORT { get; set; }
        public string SERVER_PORT_SECURE { get; set; }
        public string SERVER_PROTOCOL { get; set; }
        public string URL { get; set; }
        public string HTTP_CACHE_CONTROL { get; set; }
        public string HTTP_CONNECTION { get; set; }
        public string HTTP_ACCEPT { get; set; }
        public string HTTP_ACCEPT_ENCODING { get; set; }
        public string HTTP_ACCEPT_LANGUAGE { get; set; }
        public string HTTP_COOKIE { get; set; }
        public string HTTP_HOST { get; set; }
        public string HTTP_USER_AGENT { get; set; }
        public string QUERY_STRING { get; set; }
    }
}