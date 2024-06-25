using Auth.Application.Handlers.Commands.CreateJwtToken;
using Core.Products.Domain;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Timer = System.Timers.Timer;

public class SpoilCheck
{
    private static readonly IConfiguration _configuration;
    private static Timer aTimer;
    static HttpClient httpClient = new HttpClient();
    public static void Main()
    {
        aTimer = new Timer();
        aTimer.Interval = 6000;


        aTimer.Elapsed += OnTimedEvent;


        aTimer.AutoReset = true;

        aTimer.Enabled = true;

        Console.WriteLine("Press the Enter key to exit the program at any time... ");
        Console.ReadLine();
    }

    private static Task<string> Autharization(string login, string password)
    {


        var authData = new CreateJwtTokenCommand()
        {
            Login = login,
            Password = password
        };
        JsonContent content = JsonContent.Create(authData);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, _configuration["Links:LoginStr"]); //"http://localhost:5216/auth/api/v1/LoginJwt"
        // отправляем запрос
        httpRequest.Content = content;

        var response = httpClient.Send(httpRequest);
        // получаем ответ
        var responceMessage = response.Content.ReadAsStringAsync();

        return responceMessage;
    }

    private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
        var jwtToken = Autharization(_configuration["Auth:Login"], _configuration["Auth:Password"]);

        //var serializedToken = JsonSerializer.Deserialize<JwtTokenDto>(jwtToken.Result);
        var serializedToken = jwtToken.Result.Split(@":")[1].Split("\"")[1];

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serializedToken);

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, _configuration["Links:IsSpoildLink"]); ;

        var response = httpClient.Send(httpRequest);

        response.Content.ReadFromJsonAsync<List<Product>>();
    }

}
