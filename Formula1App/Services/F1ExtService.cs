using Formula1App.ModelsExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Formula1App.Services
{
    public class F1ExtService
    {
        //https://github.com/jolpica/jolpica-f1/blob/main/docs/README.md
        private static string ExtAPI = "https://api.jolpi.ca/ergast/f1/";
        private static string FlagsAPI = "https://flagsapi.com/";
        private List<MyDriverStandings> drivers;
        private List<Constructorstanding> consts;
        private HttpClient client;
        public F1ExtService()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            InitData();
        }

        private async void InitData()
        {
            drivers = await GetCurrDriversStandingsAsync();
            consts = await GetCurrConstructorsStandingsAsync();
            foreach (MyDriverStandings d in drivers)
            {
                d.Constructor = (consts.Where(c => c.Constructor.constructorId == d.Constructors.Last().constructorId).First()).Constructor;
            }
            foreach (Constructorstanding c in consts)
            {
                c.Constructor.Drivers = new();
                foreach (MyDriverStandings m in drivers)
                {
                    if (m.Constructor.constructorId == c.Constructor.constructorId)
                    {
                        c.Constructor.Drivers.Add(m);
                    }
                }
            }
        }
        public async Task<List<MyDriverStandings>> GetCurrDriversStandingsAsync()
        {
            List<MyDriverStandings> mds = await GetDriversStandingsByYearAsync("current");
            return mds;
        }
        public async Task<List<MyDriver>> GetCurrDriversAsync()
        {
            return await GetDriversByYearAsync("current");
        }

        public async Task<List<Constructorstanding>> GetCurrConstructorsStandingsAsync()
        {
            return await GetConstructorsStandingsByYearAsync("current");
        }
        public async Task<List<Constructor>> GetCurrConstructorsAsync()
        {
            return await GetConstructorsByYearAsync("current");
        }
        public async Task<List<Race>> GetCurrRacesAsync()
        {
            return await GetRacesByYearAsync("current");
        }

        public async Task<List<MyDriverStandings>> GetDriversStandingsByYearAsync(string year)
        {
            string url = ExtAPI + year + "/driverstandings.json";
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
                        dList = s.DriverStandings;
                    }
                    List<MyDriverStandings>? newDList = new();
                    foreach (Driverstanding d in dList)
                    {
                        MyDriverStandings mds = new MyDriverStandings()
                        {
                            DriverId = d.Driver.driverId,
                            PermanentNumber = d.Driver.permanentNumber,
                            Code = d.Driver.code,
                            Url = d.Driver.url,
                            FirstName = d.Driver.givenName,
                            LastName = d.Driver.familyName,
                            DateOfBirth = d.Driver.dateOfBirth,
                            Nationality = d.Driver.nationality,
                            Position = d.position,
                            PositionText = d.positionText,
                            Points = d.points,
                            Wins = d.wins,
                            Constructors = d.Constructors,
                            Constructor = d.Constructors.Last()
                        };
                        newDList.Add(mds);
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
                return null;
            }
        }
        public async Task<List<MyDriver>> GetDriversByYearAsync(string year)
        {
            string url = ExtAPI + year + "/drivers.json?limit=100";
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
                return null;
            }
        }
        public async Task<List<MyDriver>> GetAllDriversAsync()
        {
            string total = "";
            int totalNum = 0;
            string url = ExtAPI + "drivers.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync((url + "/?limit=1"));
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"DriversData\":");
                    DriverApi result = JsonSerializer.Deserialize<DriverApi>(resContent);
                    total = result.DriversData.total;
                    int.TryParse(total, out totalNum);
                    List<Driver> dList = new();
                    int offset = 0;
                    if (totalNum / 100 == 0)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100"));
                        string newResContent = await response.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"DriversData\":");
                            DriverApi newResult = JsonSerializer.Deserialize<DriverApi>(newResContent);
                            dList = newResult.DriversData.DriverTable.Drivers.ToList();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= totalNum / 100; i++)
                        {
                            HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100&offset={offset.ToString()}"));
                            string newResContent = await res.Content.ReadAsStringAsync();
                            if (res.IsSuccessStatusCode)
                            {
                                newResContent = newResContent.Replace("\"MRData\":", "\"DriversData\":");
                                DriverApi newResult = JsonSerializer.Deserialize<DriverApi>(newResContent);
                                dList.AddRange(newResult.DriversData.DriverTable.Drivers.ToList());
                                offset += 100;
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
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
                return null;
            }
        }
        public async Task<List<Constructorstanding>> GetConstructorsStandingsByYearAsync(string year)
        {
            string url = ExtAPI + year + "/constructorstandings.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"ConstructorStandingsData\":");
                    ConstructorStandingsApi result = JsonSerializer.Deserialize<ConstructorStandingsApi>(resContent);
                    List<Standingslist> sList = result.ConstructorStandingsData.StandingsTable.StandingsLists.ToList();
                    List<Constructorstanding> cList = new();
                    foreach (Standingslist s in sList)
                    {
                        cList = s.ConstructorStandings.ToList();
                    }
                    if (consts != null && drivers != null)
                    {
                        foreach (Constructorstanding c in cList)
                        {
                            c.Constructor.Drivers = consts.Where(x => x.Constructor.constructorId == c.Constructor.constructorId).First().Constructor.Drivers; ;
                        }
                    }
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
        public async Task<List<Constructor>> GetConstructorsByYearAsync(string year)
        {
            string url = ExtAPI + year + "/constructors.json";
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
        public async Task<List<Constructor>> GetAllConstructorsAsync()
        {
            string total = "";
            int totalNum = 0;
            string url = ExtAPI + "constructors.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync((url + "/?limit=1"));
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"ConstructorsData\":");
                    ConstructorsApi result = JsonSerializer.Deserialize<ConstructorsApi>(resContent);
                    total = result.ConstructorsData.total;
                    int.TryParse(total, out totalNum);
                    List<Constructor> cList = new();
                    int offset = 0;
                    if (totalNum / 100 == 0)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100"));
                        string newResContent = await response.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"ConstructorsData\":");
                            ConstructorsApi newResult = JsonSerializer.Deserialize<ConstructorsApi>(newResContent);
                            cList = newResult.ConstructorsData.ConstructorTable.Constructors.ToList();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    for (int i = 0; i <= totalNum / 100; i++)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100&offset={offset.ToString()}"));
                        string newResContent = await res.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"ConstructorsData\":");
                            ConstructorsApi newResult = JsonSerializer.Deserialize<ConstructorsApi>(newResContent);
                            cList.AddRange(newResult.ConstructorsData.ConstructorTable.Constructors.ToList());
                            offset += 100;
                        }
                        else
                        {
                            return null;
                        }
                    }
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
        public async Task<List<Race>> GetRacesByYearAsync(string year)
        {
            string url = ExtAPI + year + "/races.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"RacesData\":");
                    RacesApi result = JsonSerializer.Deserialize<RacesApi>(resContent);
                    List<Race> rList = result.RacesData.RaceTable.Races.ToList();
                    return rList;
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
        public async Task<int> GetCurrRound()
        {
            string url = ExtAPI + "current/driverstandings.json?limit=1";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"DriverStandingsData\":");
                    DriverStandingsApi result = JsonSerializer.Deserialize<DriverStandingsApi>(resContent);
                    int.TryParse(result.DriverStandingsData.StandingsTable.round, out int round);
                    return round;
                }
                else
                {
                    return default;
                }
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public async Task<Race> GetRaceResultsAsync(string year, string round)
        {
            string url = $"{ExtAPI}{year}/{round}/results.json?limit=100";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"RaceResultsData\":");
                    RaceResultsApi result = JsonSerializer.Deserialize<RaceResultsApi>(resContent);
                    List<Race> rList = result.RaceResultsData.RaceTable.Races.ToList();
                    return rList.FirstOrDefault();
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
        public async Task<List<Season>> GetSeasonsAsync()
        {
            string total = "";
            int totalNum = 0;
            string url = ExtAPI + "seasons.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync((url + "/?limit=1"));
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"SeasonsData\":");
                    SeasonsApi result = JsonSerializer.Deserialize<SeasonsApi>(resContent);
                    total = result.SeasonsData.total;
                    int.TryParse(total, out totalNum);
                    List<Season> sList = new();
                    int offset = 0;
                    if (totalNum / 100 == 0)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100"));
                        string newResContent = await response.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"SeasonsData\":");
                            SeasonsApi newResult = JsonSerializer.Deserialize<SeasonsApi>(newResContent);
                            sList = newResult.SeasonsData.SeasonTable.Seasons;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    for (int i = 0; i <= totalNum / 100; i++)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100&offset={offset.ToString()}"));
                        string newResContent = await res.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"SeasonsData\":");
                            SeasonsApi newResult = JsonSerializer.Deserialize<SeasonsApi>(newResContent);
                            sList.AddRange(newResult.SeasonsData.SeasonTable.Seasons);
                            offset += 100;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return sList;
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
        public async Task<List<Race>> GetSeasonResultsAsync(string year)
        {
            string total = "";
            int totalNum = 0;
            string url = $"{ExtAPI}{year}/results.json";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url + "/?limit=1");
                string resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    resContent = resContent.Replace("\"MRData\":", "\"RaceResultsData\":");
                    RaceResultsApi result = JsonSerializer.Deserialize<RaceResultsApi>(resContent);
                    List<Race> races = result.RaceResultsData.RaceTable.Races.ToList();
                    total = result.RaceResultsData.total;
                    int.TryParse(total, out totalNum);
                    List<Race> rList = new();
                    int offset = 0;
                    if (totalNum / 100 == 0)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100"));
                        string newResContent = await response.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"RaceResultsData\":");
                            RaceResultsApi newResult = JsonSerializer.Deserialize<RaceResultsApi>(newResContent);
                            rList = newResult.RaceResultsData.RaceTable.Races.ToList();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    for (int i = 0; i <= totalNum / 100; i++)
                    {
                        HttpResponseMessage res = await client.GetAsync(($"{url}/?limit=100&offset={offset.ToString()}"));
                        string newResContent = await res.Content.ReadAsStringAsync();
                        if (res.IsSuccessStatusCode)
                        {
                            newResContent = newResContent.Replace("\"MRData\":", "\"RaceResultsData\":");
                            RaceResultsApi newResult = JsonSerializer.Deserialize<RaceResultsApi>(newResContent);
                            List<Race> rs = newResult.RaceResultsData.RaceTable.Races.ToList();
                            List<Race> rs2 = new();
                            foreach (Race r in rs)
                            {
                                Race rc = rList.Where(x => x.round == r.round).FirstOrDefault();
                                if (rc != null)
                                {
                                    rList.Last().Results.AddRange(r.Results);
                                    rs2.Add(r);
                                }
                            }
                            rs.RemoveRange(rs2);
                            rList.AddRange(rs);
                            offset += 100;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return rList;
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
