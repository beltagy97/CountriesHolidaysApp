using CountriesAndHolidaysApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CountriesAndHolidaysApp.Services
{
    public class CountryHolidayServices :  ICountryHolidayServices
    {
        private readonly CountriesAndHolidaysContext context;

        public CountryHolidayServices(CountriesAndHolidaysContext ctx)
        {
            context = ctx;
        }


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



        public Country getCountryObject(string countryCode, string countryName, string correspondingHolidays)
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


        public async Task<object> sync()
        {
            string countriesResponse = await GetDataFromAPI("https://api.printful.com/countries");
            IList<Country> countries = CountryJSONParser(countriesResponse);

            int idx = 0;
            foreach (Country country in countries)
            {


                if (context.Countries.Any(o => o.code == country.code)) continue;

                try
                {
                    string correspondingHolidayURL = "https://www.googleapis.com/calendar/v3/calendars/en." + country.code + "%23holiday%40group.v.calendar.google.com/events?key=AIzaSyBpSZoCr4xUGsNzmAuxVw_WT0Q4hVW9Bos";
                    string correspondingHolidays = await GetDataFromAPI(correspondingHolidayURL);

                    //getCountryObject

                    Country newCountry = null;
                    newCountry = getCountryObject(country.code, country.name, correspondingHolidays);


                    context.Add(newCountry);
                    context.SaveChanges();
                    idx++;

                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    return new { error = e };
                }
            }


            return new { numberOfRowsAffected = idx };


        }

        public string getCountryHolidays(string countryCode)
        {
            IList<CountryHolidayResultSet> Holidays = context.Countries.Join(context.Holidays,
                country => country.CountryID,
                holiday => holiday.countryID,
                (country, holiday) => new CountryHolidayResultSet
                {
                    countryName = country.name,
                    code = country.code,
                    holidayName = holiday.Name,
                    start_date = holiday.start_date,
                    end_date = holiday.end_date
                }).Where(o => o.code == countryCode).ToList();

            return JsonConvert.SerializeObject(Holidays);

        }

        public bool deleteHoliday(string code , int id)
        {
            try
            {
                Country country = context.Countries.Where(country => country.code == code).Single();
                int countryID = country.CountryID;
                Holiday specificHoliday = context.Holidays.Where(holiday => holiday.countryID == countryID && holiday.ID == id).Single();
                context.Remove(specificHoliday);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool AddHoliday(Holiday newHoliday)
        {
            try
            {
                //checks if countryID is available
                Country country = context.Countries.Where(country => country.CountryID == newHoliday.countryID).Single();

                context.Add(newHoliday);
                context.SaveChanges();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool checkCountryID(int cID)
        {
            try
            {

                Country country = context.Countries.Where(country => country.CountryID == cID).Single();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ModifyHoliday(int id, Holiday newHoliday)
        {
            try
            {
               
                Holiday desiredHoliday = context.Holidays.Find(id);
                
                if (desiredHoliday == null) return false;
                
                
                if (!checkCountryID(newHoliday.countryID)) return false;

                desiredHoliday.Name = newHoliday.Name;
                desiredHoliday.start_date = newHoliday.start_date;
                desiredHoliday.end_date = newHoliday.end_date;
                desiredHoliday.countryID = newHoliday.countryID;

               
                context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
