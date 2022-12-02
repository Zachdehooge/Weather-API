/*
   Author: Zach DeHooge // https://github.com/Zachdehooge
   Application Description: Simple console application that makes an API call request to WeatherAPI.com
   
        RapidAPI: https://rapidapi.com/weatherapi/api/weatherapi-com/
        WeatherAPI: https://www.weatherapi.com/docs/
 */

/*
 TODO: Secure key outside of main Program.cs
 TODO: Add future forecast result   
*/

using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

// 9932e1e975msha5ba4d6e2d28957p1868abjsnfbf56fbdc9f5

string config = "9932e1e975msha5ba4d6e2d28957p1868abjsnfbf56fbdc9f5";

Console.Write("Please enter the city and state you want the current temperature for (Ex: Atlanta, GA): ");
var question = Console.ReadLine();
var question2 = Regex.Replace(question!, @"\s+", "");

Console.Write("Enter how many days you want a forecast for: ");
var question3 = Convert.ToInt32(Console.ReadLine());

HttpClient client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/forecast.json?q={question2}&days={question3}"),
    Headers =
    {
        { "X-RapidAPI-Key", $"{config}" },
        { "X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    //Console.WriteLine(body);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    var obj = JObject.Parse(body);
    var minTempF = obj.SelectToken("forecast.forecastday.date")!;
    var city = (string)obj.SelectToken("location.name")!;
    var tempF = (string)obj.SelectToken("current.temp_f")!;
    var feelsF = (string)obj.SelectToken("current.condition.text")!;
    
    Console.WriteLine();
    
    Console.WriteLine($"Minimum temperature: {minTempF}°F");
    Console.WriteLine($"Current temperature: {tempF}°F");
    Console.Write($"Conditions in {city} currently: {feelsF}");
}
