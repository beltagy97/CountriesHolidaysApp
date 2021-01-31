using CountriesAndHolidaysApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Services
{
    public class CountryHolidayServices : ICountryHolidayServices
    {
        public async Task<string> GetDataFromAPI(string url)
        {
            HttpClient client = new HttpClient();
            String data = null;
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                data = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                return data;
            }
            return "404";
        }

            public IList<Holidays> HolidayJSONParser(string response)
        {
            JArray holidaysArr = (JArray)JObject.Parse(response)["items"];

            IList<Holidays> holidays = null;
            holidays = holidaysArr.Select(p => new Holidays
            {
                Name = (string)p["summary"],
                start_date = (string)p["start"]["date"],
                end_date = (string)p["end"]["date"]

            }).ToList();

            return holidays;
        }

        public IList<Countries> CountryJSONParser(string response)
        {
            JArray countriesArr = (JArray)JObject.Parse(response)["result"];

            IList<Countries> countries = null;
            countries = countriesArr.Select(p => new Countries
            {
                Name = (string)p["name"],
                Code = (string)p["code"]

            }).ToList();

            return countries;
        }


        public async Task<object> sync(CountriesAndHolidaysContext context)
        {
            string countriesResponse = await GetDataFromAPI("https://api.printful.com/countries");
            IList<Countries> countries = CountryJSONParser(countriesResponse);

            int idx = 0;
            foreach(Countries country in countries)
            {
                
                string correspondingHolidayURL = "https://www.googleapis.com/calendar/v3/calendars/en." + country.Code + "%23holiday%40group.v.calendar.google.com/events?key=AIzaSyBpSZoCr4xUGsNzmAuxVw_WT0Q4hVW9Bos";

                if (context.Countries.Any(o => o.Code == country.Code)) continue;
                
                try
                {
                    string correspondingHolidays = await GetDataFromAPI(correspondingHolidayURL);
                    if (correspondingHolidays == "404") continue;
                    idx++;
                    IList<Holidays> holidays = HolidayJSONParser(correspondingHolidays);
                    

                    var newCountry = new Countries
                    {
                        Code = country.Code,
                        Name = country.Name,
                        Holidays = holidays
                    };

                    context.Add(newCountry);
                    context.SaveChanges();

                    

                }catch(Exception e)
                {
                    return new { error = e };
                }
            }


            return new {numberOfRowsAffected = idx };


        }
    }
}
