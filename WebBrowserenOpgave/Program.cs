using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebBrowserenOpgave
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            try
            {
                //url from which to gather HTML Data
                string url = "http://www.dr.dk";

                //Regex to find HTML tags
                Regex rx = new Regex(@"<(\/*?)(?!(em|p|br\s*\/|strong))\w+?.+?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                //The response from the url
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                
                //Converts the response to string
                string responseBody = await responseMessage.Content.ReadAsStringAsync();

                //Collection of matches from when sorting through the responseMessage with the given regex statement
                MatchCollection matches = rx.Matches(responseBody);

                //Prints a version of the original response data with blank spaces instead of HTML tags
                Console.WriteLine(Regex.Replace(responseBody, @"<(\/*?)(?!(em|p|br\s*\/|strong))\w+?.+?>", ""));

                //Prints the amount of matches was found in the responseMessage string
                Console.WriteLine("{0} Regex Matches found and removed", matches.Count);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.ReadKey();
        }
    }
}
