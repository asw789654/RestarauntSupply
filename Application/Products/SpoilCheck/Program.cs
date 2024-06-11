using Auth.Application.Dtos;
using Auth.Application.Handlers.Commands.CreateJwtToken;
using Core.Application.Abstractions;
using Core.Application.DTOs;
using Core.Products.Domain;
using MediatR;
using Products.Applications.DTOs;
using Products.Applications.Handlers.Queries.CheckProductsSpoilTime;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Timer = System.Timers.Timer;

public class SpoilCheck
{
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

    private static Task<string> Autharization(string login,string password)
    {
        var authData = new CreateJwtTokenCommand()
        {
            Login = login,
            Password = password
        };
        JsonContent content = JsonContent.Create(authData);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5216/auth/api/v1/LoginJwt");
        // отправляем запрос
        httpRequest.Content = content;

        var response =  httpClient.Send(httpRequest);
        // получаем ответ
        var responceMessage = response.Content.ReadAsStringAsync();

        return responceMessage;
    }

    private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
    {
        var jwtToken = Autharization("asw789654", "123456789");

        //var serializedToken = JsonSerializer.Deserialize<JwtTokenDto>(jwtToken.Result);
        var serializedToken = jwtToken.Result.Split(@":")[1].Split("\"")[1];

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serializedToken);

        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5299/api/v1/Products/Spoiled");;

        var response = httpClient.Send(httpRequest);

        response.Content.ReadFromJsonAsync<List<Product>>();
    }

}
