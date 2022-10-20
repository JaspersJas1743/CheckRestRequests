using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CheckRestRequests
{
    public class Book
    {
        [JsonPropertyName("userId")]
        public int UserID { get; set; }
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<Book>> GetData()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new("application/json")
            );
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 OPR/91.0.4516.72");

            var task = client.GetStreamAsync("https://jsonplaceholder.typicode.com/posts");
            return await JsonSerializer.DeserializeAsync<List<Book>>(await task);
        }

        public static async Task Main(string[] args)
        {
            Random rnd = new();
            var books = await GetData();
            for (int i = 0; i < 3; ++i)
                Console.WriteLine($"{i + 1}: {books.GetRandom().Title}");
        }
    }

    public static class IEnumerableExtension
    {
        public static Book GetRandom(this IEnumerable<Book> list) =>
            list.ElementAt(new Random().Next(list.Count()));
    }
}