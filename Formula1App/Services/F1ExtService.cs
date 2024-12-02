using Formula1App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Formula1App.Services
{
    public class F1ExtService
    {
        private static string ExtAPI = "http://ergast.com/api/f1/";
        private string currYear = DateTime.Now.Year.ToString() +"/";
        private HttpClient client;
        public F1ExtService()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
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
            string url = ExtAPI + year + "drivers.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"DriversData\":");
                    DriverApi result = JsonSerializer.Deserialize<DriverApi>(resContent);
                    List<Driver> dList = result.DriversData.DriverTable.Drivers.ToList();
                    List<MyDriver>? newDList = new();
                    foreach (Driver d in dList)
                    {
                        newDList.Add(new MyDriver()
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
                    return newDList;
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
            string url = ExtAPI + year + "constructors.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent.Replace("\"MRData\":", "\"ConstructorsData\":");
                    List<Constructor>? result = JsonSerializer.Deserialize<List<Constructor>>(resContent);
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
