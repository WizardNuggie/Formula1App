using Formula1App.ModelsExt;
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
        //https://github.com/jolpica/jolpica-f1/blob/main/docs/README.md
        private static string ExtAPI = "https://api.jolpi.ca/ergast/f1/";
        private string currYear = DateTime.Now.Year.ToString() +"/";
        private HttpClient client;
        public F1ExtService()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
        }

        public async Task<List<MyDriver>> GetCurrDriversStandingsAsync()
        {
            return await GetDriversStandingsByYearAsync(currYear);
        }

        public async Task<List<Constructor>> GetCurrConstructorsStandingsAsync()
        {
            return await GetConstructorsStandingsByYearAsync(currYear);
        }

        public async Task<List<MyDriver>> GetDriversStandingsByYearAsync(string year)
        {
            string url = ExtAPI + year + "driverstandings.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"DriverStandingsData\":");
                    DriverStandingsApi result = JsonSerializer.Deserialize<DriverStandingsApi>(resContent);
                    List<Standingslist> sList = result.DriverStandingsData.StandingsTable.StandingsLists.ToList();
                    List<Driverstanding> dList = new();
                    foreach (Standingslist s in sList)
                    {
                        dList = s.DriverStandings.ToList();
                    }
                    List<MyDriver>? newDList = new();
                    //foreach (Driver d in dList)
                    //{
                    //    newDList.Add(new MyDriver()
                    //    {
                    //        DriverId = d.driverId,
                    //        PermanentNumber = d.permanentNumber,
                    //        Code = d.code,
                    //        Url = d.url,
                    //        FirstName = d.givenName,
                    //        LastName = d.familyName,
                    //        DateOfBirth = d.dateOfBirth,
                    //        Nationality = d.nationality,
                    //        FullName = d.givenName + " " + d.familyName
                    //    });
                    //}
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

        public async Task<List<Constructor>> GetConstructorsStandingsByYearAsync(string year)
        {
            string url = ExtAPI + year + "constructorstandings.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"ConstructorsData\":");
                    ConstructorsApi result = JsonSerializer.Deserialize<ConstructorsApi>(resContent);
                    List<Constructor> cList = result.ConstructorsData.ConstructorTable.Constructors.ToList();
                    return cList;
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
