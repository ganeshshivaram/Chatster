using System;
using Microsoft.AspNetCore.Http;

namespace ChatsterApi.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse httpResponse, string message)
        {
            httpResponse.Headers.Add("Application-Error", message);
            httpResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            httpResponse.Headers.Add("Access-Control-Expose_Headers", "Application-Error");
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