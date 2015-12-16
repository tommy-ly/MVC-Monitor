using System.Collections.Generic;
using MvcMonitor.Models;

namespace MvcMonitor.ErrorHandling
{
    public interface IErrorDataFactory
    {
        List<ErrorLocationModel> GetErrorLocations();
    }
}