using System;
using System.Collections.Generic;
using System.Text;

namespace IOT_DeviceManager.APP.Models
{
    public class ResourceParameters
    {
        private const int MaxPageSize = 20;
        private int _pageSize = 10;

        public string SearchQuery { get; set; }
        public string OrderBy { get; set; }
        public string SortDirection { get; set; } = "asc";
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
