using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesAndHolidaysApp.Services;
using Newtonsoft.Json;
using Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesAndHolidaysApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryHolidayController : ControllerBase
    {
        //make sure that resource is restful

        private readonly ICountryHolidayServices srvc;
        public CountryHolidayController(ICountryHolidayServices _srvc)
        {
            srvc = _srvc;
        }

        // GET method to return specific holidays for a given country
        // ROUTE : api/countryHoliday/{code}
        [HttpGet("{code}")]
        public string Get(string code)
        {
            return srvc.getCountryHolidays(code);
        }

        // GET method to return list of Countries
        // ROUTE : api/countryHoliday/page/{pageNumber}
        [HttpGet("page/{pageNumber}")]
        public string GetCountryList(int pageNumber)
        {
            return srvc.getCountries(pageNumber);
        }



        // POST method to sync data from external APIs
        // ROUTE : api/countryHoliday/sync
        [HttpPost, Route("sync")]
        public async Task<object> PostData()
        {
            object response = await srvc.sync();
            return response;

        }


        // POST method to add a holiday
        // ROUTE : api/countryHoliday
        [HttpPost]
        public string PostHoliday([FromBody] Holiday newHoliday)
        {
            return srvc.addHoliday(newHoliday);
        }


        // Put method to modify an existing holiday
        // ROUTE : api/countryHoliday/{holidayID}
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Holiday newHoliday)
        {
            return srvc.modifyHoliday(id, newHoliday);
            
        }

        // DELETE method to delete a holiday
        // ROUTE : api/countryHoliday/{code}/{id}
        [HttpDelete("{code}/{id}")]
        public string Delete(string code,int holidayId)
        {
            if(holidayId < 0 )
            {
                return JsonConvert.SerializeObject(new { message = "Bad Request!" }); 
            }
            return srvc.deleteHoliday(code, holidayId);
            
        }
    }
}
