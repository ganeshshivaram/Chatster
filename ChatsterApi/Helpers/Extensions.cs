using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ChatsterApi.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse httpResponse, string message)
        {
            httpResponse.Headers.Add("Application-Error", message);
            httpResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            httpResponse.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
        }

        public static void AddPaginationHeaders(this HttpResponse httpResponse, int currentPage, int itemsPerPage,
        int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            httpResponse.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            httpResponse.Headers.Add("Access-Control-Expose-Headers", "Pagination");

        }
        public static int CalculateAge(this DateTime? theDateTime)
        {
            if (!theDateTime.HasValue)
                return 0;
            var age = DateTime.Now.Year - theDateTime.Value.Year;

            if (theDateTime.Value.AddYears(age).Year > DateTime.Now.Year)
                age--;

            return age;
        }
    }
}