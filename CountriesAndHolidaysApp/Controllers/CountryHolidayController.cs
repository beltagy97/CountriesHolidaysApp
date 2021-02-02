using CountriesAndHolidaysApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesAndHolidaysApp.Services;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesAndHolidaysApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryHolidayController : ControllerBase
    {

        private readonly ICountryHolidayServices srvc;
        public CountryHolidayController(ICountryHolidayServices _srvc)
        {
            srvc = _srvc;
        }

        // GET method to return specific holidays for a given country
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



        // GET: api/<ValuesController>
        [HttpPost, Route("sync")]
        public async Task<object> PostData()
        {
            object response = await srvc.sync();
            return response;

        }


        // POST: new holiday
        [HttpPost, Route("add")]
        public string PostHoliday([FromBody] Holiday newHoliday)
        {
            if (srvc.addHoliday(newHoliday)) return "SUCESS";
            return "FAILURE";

        }


        // PUT MODIFY AN EXISTING HOLIDAY
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Holiday newHoliday)
        {
            if(srvc.modifyHoliday(id,newHoliday)) return "Record Modified";
            return "COULD NOT MODIFY RECORD";
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{code}/{id}")]
        public string Delete(string code,int id)
        {
            if(id < 0 )
            {
                return "Bad Request!";
            }
            if(srvc.deleteHoliday(code, id))
            {
                return "DELETE SUCCESSFUL";
            }
            return "DELETE FAILED";
        }
    }
}
