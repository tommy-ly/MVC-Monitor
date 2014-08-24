namespace MvcMonitor.Models
{
    public class ErrorLocationModel
    {
        public string Application { get; set; }
        public string Location { get; set; }
        public int Occurences { get; set; }
    }
}