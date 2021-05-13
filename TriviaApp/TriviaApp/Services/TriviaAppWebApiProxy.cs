using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TriviaApp.Models;

using System.Text.Json;

namespace TriviaApp.Services
{
    class TriviaAppWebApiProxy
    {
        private HttpClient client;
        private string baseUri;
        private static TriviaAppWebApiProxy proxy = null;


        public static TriviaAppWebApiProxy CreateProxy(string baseUri = "https://ramonws.azurewebsites.net/AmericanQuestions/")
        {
            if (proxy == null)
                proxy = new TriviaAppWebApiProxy(baseUri);
            return proxy;
        }
        private TriviaAppWebApiProxy(string baseUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
        }
        //Login Method
        public async Task<User> LoginAsync(string email, string password)
        {
            try
            {
                HttpResponseMessage respose = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={password}");
                if (respose.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    string content = await respose.Content.ReadAsStringAsync();
                    User user = JsonSerializer.Deserialize<User>(content, options);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Register Method
        public async Task<bool> RegisterAsync(User u)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string jsonUser = JsonSerializer.Serialize<User>(u, options);
                StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/RegisterUser", content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContext = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContext, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }
        // Get Random Question Method
        public async Task<AmericanQuestion> GetRandomQuestionAsync()
        {
            try
            {
                HttpResponseMessage respnose = await this.client.GetAsync($"{this.baseUri}/GetRandomQuestion");
                if (respnose.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await respnose.Content.ReadAsStringAsync();
                    AmericanQuestion question = JsonSerializer.Deserialize<AmericanQuestion>(content, options);
                    return question;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }            
        }
        // Post New Question Method
        public async Task<bool> PostNewQuestionAsync(AmericanQuestion q)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string jsonQuestion = JsonSerializer.Serialize<AmericanQuestion>(q, options);
                StringContent content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{baseUri}/PostNewQuestion", content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Delete Question Meth
        public async Task<bool> DeleteQuestionAsync(AmericanQuestion q)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string jsonQuestion = JsonSerializer.Serialize<AmericanQuestion>(q, options);
                StringContent content = new StringContent(jsonQuestion, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/DeleteQuestion", content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool b = JsonSerializer.Deserialize<bool>(jsonContent, options);
                    return b;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        
    }
    
}
