﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Formula1App.Models;
using Formula1App.ModelsExt;

namespace Formula1App.Services
{
    public class F1IntService
    {
        private static string serverIp = "tk4zbkn3-7209.uks1.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://tk4zbkn3-7209.uks1.devtunnels.ms/api/";
        public static string ImageBaseAddress = "https://tk4zbkn3-7209.uks1.devtunnels.ms/";

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
        public async Task<ResponseUser?> SignUpAsync(User user)
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
                    ResponseUser? result = new()
                    {
                        User = JsonSerializer.Deserialize<User>(resContent, options),
                        IsExist = false
                    };
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    return new ResponseUser()
                    {
                        User = null,
                        IsExist = true
                    };
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
        public async Task<List<Article>> GetAllNews()
        {
            string url = $"{this.baseUrl}GetAllNews";
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
                        User w = await GetUserByArticle(a);
                        a.Writer = w;
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
                        User w = await GetUserByArticle(a);
                        a.Writer = w;
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
                        User w = await GetUserByArticle(a);
                        a.Writer = w;
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
        public async Task<List<Article>> GetNewsByUser(int userId)
        {
            string parameterKey = "userId";
            string parameterValue = userId.ToString();
            string url = $"{this.baseUrl}GetNewsByUser?{parameterKey}={parameterValue}";
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
                        User w = await GetUserByArticle(a);
                        a.Writer = w;
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

        #region Users

        public async Task<List<User>> GetUsers()
        {
            string url = $"{this.baseUrl}GetUsers";
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
                    List<User> result = JsonSerializer.Deserialize<List<User>>(resContent, options);
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
        public async Task<User> GetUserByArticle(Article a)
        {
            string url = $"{this.baseUrl}GetWriterByArticle?articleId={a.Id}";
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
                    User result = JsonSerializer.Deserialize<User>(resContent, options);
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

        public async Task<List<User>> GetUsersByUT(int utId)
        {
            string parameterKey = "userTypeId";
            string parameterValue = utId.ToString();
            string url = $"{this.baseUrl}GetUsersByUT?{parameterKey}={parameterValue}";
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
                    List<User> result = JsonSerializer.Deserialize<List<User>>(resContent, options);
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

        public async Task<bool> RemoveUser(User u)
        {
            string parameterKey = "userId";
            string parameterValue = u.Id.ToString();
            string url = $"{this.baseUrl}RemoveUser?{parameterKey}={parameterValue}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region UserTypes

        public async Task<List<UserType>> GetUserTypes()
        {
            string url = $"{this.baseUrl}GetUserTypes";
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
                    List<UserType> result = JsonSerializer.Deserialize<List<UserType>>(resContent, options);
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

        #region Article

        public async Task<Article?> UploadArticle(Article article)
        {
            string url = $"{this.baseUrl}UploadArticle";
            try
            {
                string json = JsonSerializer.Serialize(article);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Article? result = JsonSerializer.Deserialize<Article>(resContent, options);
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

        #region Upload photo

        public async Task<bool> UploadArticleImage(string imagePath, int id)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UploadArticleImage?id={id}";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Manage Articles
        public async Task<bool> ApproveArticle(Article a)
        {
            string parameterKey = "articleId";
            string parameterValue = a.Id.ToString();
            string url = $"{this.baseUrl}ApproveArticle?{parameterKey}={parameterValue}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeclineArticle(Article a)
        {
            string parameterKey = "articleId";
            string parameterValue = a.Id.ToString();
            string url = $"{this.baseUrl}DeclineArticle?{parameterKey}={parameterValue}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Edit User Details
        public async Task<ResponseUser?> EditUserDetails(User u)
        {
            string url = $"{this.baseUrl}EditUserDetails";
            try
            {
                string json = JsonSerializer.Serialize(u);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ResponseUser? result = new()
                    {
                        User = JsonSerializer.Deserialize<User>(resContent, options),
                        IsExist = false
                    };
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    return new ResponseUser()
                    {
                        User = null,
                        IsExist = true
                    };
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
