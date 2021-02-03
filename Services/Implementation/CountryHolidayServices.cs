using Data.Context;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Repository;
using Repository.Implementations;
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
        private readonly ICountryRepository countryRepo;
        private readonly IHolidayRepository holidayRepo;

        public CountryHolidayServices(ICountryRepository countryRepo, IHolidayRepository holidayRepo)
        {
            this.countryRepo = countryRepo;
            this.holidayRepo = holidayRepo;
        }

        public async Task<ResponseMessage> sync()
        {
            
            //move the url to settings
            string countriesResponse = await GetDataFromAPI("https://api.printful.com/countries");
            IList<Country> countries = CountryJSONParser(countriesResponse);
            int idx = 0;
            foreach (Country country in countries)
            {

                if (countryRepo.FindbyCode(country.code)!= null) continue;

                try
                {
                    string correspondingHolidayURL = "https://www.googleapis.com/calendar/v3/calendars/en." + country.code + "%23holiday%40group.v.calendar.google.com/events?key=AIzaSyBpSZoCr4xUGsNzmAuxVw_WT0Q4hVW9Bos";
                    string correspondingHolidays = await GetDataFromAPI(correspondingHolidayURL);

                    //getCountryObject
                    Country newCountry = null;
                    newCountry = getCountryObject(country.code, country.name, correspondingHolidays);

                    
                    countryRepo.Add(newCountry);
                    idx++;

                }
                catch (Exception e)
                {
                    return new ResponseMessage{ Message = e.ToString() };
                }
            }


            return new ResponseMessage{ Message = "NumberOfRowsAffected "+idx };


        }

        public string getCountryHolidays(string countryCode)
        {
      
            return JsonConvert.SerializeObject(countryRepo.getCountryHolidays(countryCode));
        }

        public ResponseMessage deleteHoliday(string code, int holidayID)
        {
            
            Country country = countryRepo.FindbyCode(code);
            if (country == null) return new ResponseMessage{ Message = "DELETE FAILED" };


            Holiday specificHoliday = holidayRepo.Find(holidayID);

            if (specificHoliday == null) return new ResponseMessage{ Message = "DELETE FAILED" };

                holidayRepo.Delete(specificHoliday);
                return new ResponseMessage { Message = "DELETE SUCCESSFUL" };

        }

        public ResponseMessage addHoliday(Holiday newHoliday)
        {
                //checks if countryID is available
                if (!countryRepo.Exists(newHoliday.countryID)) return new ResponseMessage { Message = "ADD FAILED" };
                
                holidayRepo.CreateHoliday(newHoliday);
                return new ResponseMessage { Message = "Success" };
        }

        public ResponseMessage modifyHoliday(int id, Holiday newHoliday)
        {
            
                Holiday desiredHoliday = holidayRepo.Find(id);

                if (desiredHoliday == null) return new ResponseMessage { Message = "COULD NOT MODIFY RECORD" };

                
                if (!countryRepo.Exists(newHoliday.countryID)) return new ResponseMessage { Message = "COULD NOT MODIFY RECORD" };


                holidayRepo.UpdateHoliday(desiredHoliday,newHoliday);

                return new ResponseMessage { Message = "Record Modified" };
            
            
        }



        public string getCountries(int pageNumber, int pageSize = 50)
        {   
            return JsonConvert.SerializeObject(countryRepo.getCountriesList(pageNumber,pageSize));
        }


//- ---------------------------------------------------------------------------------------------------------------------------------------------------------

        private async Task<string> GetDataFromAPI(string url)
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

        private IList<Holiday> HolidayJSONParser(string response)
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

        private IList<Country> CountryJSONParser(string response)
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



        private Country getCountryObject(string countryCode, string countryName, string correspondingHolidays)
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
    }
}
