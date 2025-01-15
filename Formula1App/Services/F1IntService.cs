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
        private static string serverIp = "tk4zbkn3-7209.uks1.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        //school tunnel
        //tk4zbkn3-7209.uks1
        //home tunnel
        //frwczxqq-7209.uks1
        public static string BaseAddress = "https://tk4zbkn3-7209.uks1.devtunnels.ms/api/";

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

        #region News
        public async Task<List<Article>> GetNews()
        {
            string url = $"{this.baseUrl}GetNews";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Article> result = JsonSerializer.Deserialize<List<Article>>(resContent, options);
                    if (result == null)
                        return null;
                    foreach (Article a in result)
                    {
                        a.FirstSubject = a.Subjects.FirstOrDefault();
                    }
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

        public async Task<List<Article>> GetNewsBySubject(int subjectId)
        {
            string parameterKey = "subjectId";
            string parameterValue = subjectId.ToString();
            string url = $"{this.baseUrl}GetNewsBySubject?{parameterKey}={parameterValue}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Article> result = JsonSerializer.Deserialize<List<Article>>(resContent, options);
                    if (result == null)
                        return null;
                    foreach (Article a in result)
                    {
                        a.FirstSubject = a.Subjects.FirstOrDefault();
                    }
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

        #region Subjects
        public async Task<List<Subject>> GetSubjects()
        {
            string url = $"{this.baseUrl}GetSubjects";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Subject> result = JsonSerializer.Deserialize<List<Subject>>(resContent, options);
                    if (result == null)
                        return null;
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
