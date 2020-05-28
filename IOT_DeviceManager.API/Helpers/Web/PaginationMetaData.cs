using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IOT_DeviceManager.API.Helpers.Web
{
    public class PaginationMetaData
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string PreviousPageUrl { get; set; }
        public string CurrentPageUrl { get; set; }
        public string NextPageUrl { get; set; }
    }
}
