using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extentions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers","Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this System.DateTime theDateTime)
        {
            var age = System.DateTime.Today.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > System.DateTime.Today)
                age--;

            return age;
        } 
        
    }
}