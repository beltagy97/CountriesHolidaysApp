﻿using Data.Context;
using Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CountryHolidayServices : ICountryHolidayServices
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
            //move the url to settings
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

        public string deleteHoliday(string code, int id)
        {
            try
            {
                Country country = context.Countries.Where(country => country.code == code).SingleOrDefault();
                if (country == null) return JsonConvert.SerializeObject(new { message = "DELETE FAILED" });
                int countryID = country.CountryID;
                Holiday specificHoliday = context.Holidays.Where(holiday => holiday.countryID == countryID && holiday.ID == id).Single();
                context.Remove(specificHoliday);
                context.SaveChanges();
                return JsonConvert.SerializeObject(new { message = "DELETE SUCCESSFUL" });
            }
            catch
            {
                return JsonConvert.SerializeObject(new { message = "DELETE FAILED" });
            }

        }

        public string addHoliday(Holiday newHoliday)
        {
            try
            {
                //checks if countryID is available
                Country country = context.Countries.Where(country => country.CountryID == newHoliday.countryID).SingleOrDefault();
                if (country == null) return JsonConvert.SerializeObject(new { message = "DELETE FAILED" });

                context.Add(newHoliday);
                context.SaveChanges();


                return JsonConvert.SerializeObject(new { message = "Success" });
            }
            catch
            {
                return JsonConvert.SerializeObject(new { message = "Failure" });
            }
        }

        public bool checkCountryID(int cID)
        {
            try
            {

                Country country = context.Countries.Where(country => country.CountryID == cID).SingleOrDefault();
                return country == null ? false : true;
            }
            catch
            {
                return false;
            }
        }

        public string modifyHoliday(int id, Holiday newHoliday)
        {
            try
            {

                Holiday desiredHoliday = context.Holidays.Find(id);

                if (desiredHoliday == null) return JsonConvert.SerializeObject(new { message = "COULD NOT MODIFY RECORD" });


                if (!checkCountryID(newHoliday.countryID)) return JsonConvert.SerializeObject(new { message = "COULD NOT MODIFY RECORD" });

                desiredHoliday.Name = newHoliday.Name;
                desiredHoliday.start_date = newHoliday.start_date;
                desiredHoliday.end_date = newHoliday.end_date;
                desiredHoliday.countryID = newHoliday.countryID;


                context.SaveChanges();



                return JsonConvert.SerializeObject(new { message = "Record Modified" });
            }
            catch
            {
                return JsonConvert.SerializeObject(new { message = "COULD NOT MODIFY RECORD" });
            }
        }



        public string getCountries(int pageNumber, int pageSize = 50)
        {
            //move page size as parameter
            int skippedPages = (pageNumber - 1) * pageSize;
            var countries = context.Countries.Skip(skippedPages).Take(pageSize).Select(country => new { Name = country.name, Code = country.code }).ToList();
            return JsonConvert.SerializeObject(countries);
        }
    }
}
