using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using IOT_DeviceManager.API.Helpers.Web;
using Microsoft.AspNetCore.Mvc;

namespace IOT_DeviceManager.API.Helpers.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static void SetXPaginationResponseHeaders<T>(this ControllerBase controller, string routeName, Paginator<T> paginator, ResourceParameters resourceParameters)
        {
            var previousPageLink = paginator.HasPrevious ?
                controller.Url.Link(routeName, new
                {
                    pageNumber = resourceParameters.PageNumber - 1,
                    pageSize = resourceParameters.PageSize,
                    orderBy = resourceParameters.OrderBy,
                    sortDirection = resourceParameters.SortDirection,
                    searchQuery = resourceParameters.SearchQuery
                }) : null;

            var currentPageLink = controller.Url.Link(routeName, new
            {
                pageNumber = resourceParameters.PageNumber,
                pageSize = resourceParameters.PageSize,
                orderBy = resourceParameters.OrderBy,
                sortDirection = resourceParameters.SortDirection,
                searchQuery = resourceParameters.SearchQuery
            });

            var nextPageLink = paginator.HasNext ?
                controller.Url.Link(routeName, new
                {
                    pageNumber = resourceParameters.PageNumber + 1,
                    pageSize = resourceParameters.PageSize,
                    orderBy = resourceParameters.OrderBy,
                    sortDirection = resourceParameters.SortDirection,
                    searchQuery = resourceParameters.SearchQuery
                }) : null;

            var paginationMetaData = new PaginationMetaData
            {
                TotalCount = paginator.TotalCount,
                PageSize = paginator.PageSize,
                CurrentPage = paginator.CurrentPage,
                TotalPages = paginator.TotalPages,
                PreviousPageUrl = previousPageLink,
                CurrentPageUrl = currentPageLink,
                NextPageUrl = nextPageLink
            };

            controller.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
        }
    }
}
