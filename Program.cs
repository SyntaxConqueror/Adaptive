using LR5;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;



class Program
{
    static async Task Main(string[] args)
    {
        var apiClient = new ApiClient();

        var getResponse = await apiClient.Get<string>("https://randomuser.me/api/");
        Console.WriteLine($"GET Response: Status Code - {getResponse.StatusCode}, Message - {getResponse.Message}, Data - {getResponse.Data}");

        var postContent = new StringContent("Helloworld");
        var postResponse = await apiClient.Post<int>("https://www.toptal.com/developers/postbin/1708178012162-3688768388237", postContent);
        Console.WriteLine($"POST Response: Status Code - {postResponse.StatusCode}, Message - {postResponse.Message}, Data - {postResponse.Data}");
    }
}