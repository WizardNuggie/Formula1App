using Formula1App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Formula1App.Services
{
    public class F1Service
    {
        private static string PerAPI = "";
        private static string ExtAPI = "http://ergast.com/api/f1/";
        private string currYear = DateTime.Now.Year.ToString() +"/";
        private HttpClient client;
        private string baseUrl;
        public static string baseAddress = "https://" + PerAPI + "/api/";

        public F1Service()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = baseAddress;
        }

        public async Task<List<MyDriver>> GetCurrDriversAsync()
        {
            return await GetDriversByYearAsync(currYear);
        }

        public async Task<List<Constructor>> GetCurrConstructorsAsync()
        {
            return await GetConstructorsByYearAsync(currYear);
        }

        public async Task<List<MyDriver>> GetDriversByYearAsync(string year)
        {
            string url = ExtAPI + year + "drivers";
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
                    List<Driver>? result = JsonSerializer.Deserialize<List<Driver>>(resContent, options);
                    List<MyDriver>? dList = new();
                    foreach (Driver d in result)
                    {
                        dList.Add(new MyDriver()
                        {
                            DriverId = d.driverId,
                            PermanentNumber = d.permanentNumber,
                            Code = d.code,
                            Url = d.url,
                            FirstName = d.givenName,
                            LastName = d.familyName,
                            DateOfBirth = d.dateOfBirth,
                            Nationality = d.nationality,
                            FullName = d.givenName + " " + d.familyName
                        });
                    }
                    return dList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }

        public async Task<List<Constructor>> GetConstructorsByYearAsync(string year)
        {
            string url = ExtAPI + year + "constructors";
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
                    List<Constructor>? result = JsonSerializer.Deserialize<List<Constructor>>(resContent, options);
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
    }
}
