using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MvcMonitor.Data.Repositories;
using MvcMonitor.Models;

namespace MvcMonitor
{
    public partial class AppSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Repo = " + MonitorConfiguration.ErrorRepository);
            sb.AppendLine("<br />");
            sb.AppendLine("Apps = " + string.Join(",", MonitorConfiguration.Applications));
            sb.AppendLine("<br />");


            ltlOutput.Text = sb.ToString();
        }

        protected void btnTestSave_OnClick(object sender, EventArgs e)
        {
            var repo = new ErrorRepositoryFactory("NHibernateRepository").GetRepository();

            var ErrorModel = new ErrorModel
            {
                Application = "App1",
                ErrorId = Guid.NewGuid(),
                ExceptionLocations = new List<string>(),
                ExceptionMessage = "broked",
                ExceptionSource = "sauce",
                ExceptionStackTrace = "",
                ExceptionType = "Ex type",
                Host = "Hosting",
                QueryString = "q",
                RequestMethod = "POST",
                ServerApplicationPath = "/",
                ServerName = "server name",
                ServerPort = 80,
                StatusCode = HttpStatusCode.BadRequest,
                ServerPortSecure = "noper",
                Url = "somethign.com",
                UserAgent = "age",
                Username = "herro",
                Time = DateTime.UtcNow,
                ServerApplicationPathTranslated = "soemthing"
            };

            repo.Add(ErrorModel);
        }
    }
}