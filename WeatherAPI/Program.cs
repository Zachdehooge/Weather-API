/*
 * Author: Zach DeHooge // https://github.com/Zachdehooge
 * Application Description: Simple console application that makes an API call request to WeatherAPI.com // https://rapidapi.com/weatherapi/api/weatherapi-com/
 */

using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

Console.Write("Please enter the city and state you want the current temperature for (Ex: Atlanta, Georgia): ");
var question = Console.ReadLine();
var question2 = Regex.Replace(question!, @"\s+", "");

HttpClient client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/current.json?q={question2}"),
    Headers =
    {
        { "X-RapidAPI-Key", "9932e1e975msha5ba4d6e2d28957p1868abjsnfbf56fbdc9f5" },
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
    var city = (string)obj.SelectToken("location.name");
    var tempF = (string)obj.SelectToken("current.temp_f");
    var feelsF = (string)obj.SelectToken("current.condition.text");
    
    Console.WriteLine();
    
    Console.WriteLine($"Current temperature: {tempF}°F");
    Console.WriteLine($"Conditions in {city} currently: {feelsF}");
    Console.ReadLine();
}