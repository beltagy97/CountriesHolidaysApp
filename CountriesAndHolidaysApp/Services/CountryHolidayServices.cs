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

            public IList<Holiday> HolidayJSONParser(string response)
        {
            JArray holidaysArr = (JArray)JObject.Parse(response)["items"];

            IList<Holiday> holidays = null;
            holidays = holidaysArr.Select(p => new Holiday
            {
                Name = (string)p["summary"],
                start_date = (string)p["start"]["date"],
                end_date = (string)p["end"]["date"]

            }).ToList();

            return holidays;
        }

        public IList<Country> CountryJSONParser(string response)
        {
            JArray countriesArr = (JArray)JObject.Parse(response)["result"];

            IList<Country> countries = null;
            countries = countriesArr.Select(p => new Country
            {
                name = (string)p["name"],
                code = (string)p["code"]

            }).ToList();

            return countries;
        }



        public Country getCountryObject(string countryCode , string countryName , string correspondingHolidays)
        {
            Country newCountry = new Country();
            newCountry.code = countryCode;
            newCountry.name = countryName;


            if (correspondingHolidays == "404")
            {
                return newCountry;
            }

            IList<Holiday> holidays = HolidayJSONParser(correspondingHolidays);
            newCountry.Holidays = holidays;

            return newCountry;
        }


        public async Task<object> sync(CountriesAndHolidaysContext context)
        {
            string countriesResponse = await GetDataFromAPI("https://api.printful.com/countries");
            IList<Country> countries = CountryJSONParser(countriesResponse);

            int idx = 0;
            foreach(Country country in countries)
            {
                
                string correspondingHolidayURL = "https://www.googleapis.com/calendar/v3/calendars/en." + country.code + "%23holiday%40group.v.calendar.google.com/events?key=AIzaSyBpSZoCr4xUGsNzmAuxVw_WT0Q4hVW9Bos";

                if (context.Countries.Any(o => o.code == country.code)) continue;
                
                try
                {
                    string correspondingHolidays = await GetDataFromAPI(correspondingHolidayURL);

                    //getCountryObject

                    Country newCountry = null;
                    newCountry = getCountryObject(country.code, country.name, correspondingHolidays);

                    context.Add(newCountry);
                    context.SaveChanges();
                    idx++;

                }
                catch(Exception e)
                {
                    return new { error = e };
                }
            }

            
            return new {numberOfRowsAffected = idx };


        }
    }
}
