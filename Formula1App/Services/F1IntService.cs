using AndroidX.AppCompat.View.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Formula1App.Models;

namespace Formula1App.Services
{
    public class F1IntService
    {
        private static string serverIp = "frwczxqq-7209.uks1.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        //school tunnel
        //h61v9h1k-7209.euw
        //home tunnel
        //frwczxqq-7209.uks1
        public static string BaseAddress = "https://frwczxqq-7209.uks1.devtunnels.ms/api/";

        public F1IntService()
        {
            HttpClientHandler handler = new();
            handler.CookieContainer = new System.Net.CookieContainer();
            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        #region Login
        public async Task<User?> LoginAsync(LoginUser userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}Login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    User? result = JsonSerializer.Deserialize<User>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Sign Up
        public async Task<User?> SignUpAsync(User user)
        {
            string url = $"{this.baseUrl}Register";
            try
            {
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    User? result = JsonSerializer.Deserialize<User>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
