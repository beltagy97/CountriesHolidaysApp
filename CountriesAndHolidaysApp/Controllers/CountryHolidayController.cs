using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Implementation;
using Services;
using Models;

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
        [HttpGet("page/{pageNumber}/{pageSize}")]
        public string GetCountryList(int pageNumber,int pageSize)
        {
            return srvc.getCountries(pageNumber,pageSize);
        }



        // POST method to sync data from external APIs
        // ROUTE : api/countryHoliday/sync
        [HttpPost, Route("sync")]
        public async Task<ResponseMessage> PostData()
        {
            ResponseMessage response = await srvc.sync();
            return response;

        }


        // POST method to add a holiday
        // ROUTE : api/countryHoliday
        [HttpPost]
        public ResponseMessage PostHoliday([FromBody] Holiday newHoliday)
        {
            return srvc.addHoliday(newHoliday);
        }


        // Put method to modify an existing holiday
        // ROUTE : api/countryHoliday/{holidayID}
        [HttpPut("{id}")]
        public ResponseMessage Put(int id, [FromBody] Holiday newHoliday)
        {
            return srvc.modifyHoliday(id, newHoliday);
            
        }

        // DELETE method to delete a holiday
        // ROUTE : api/countryHoliday/{code}/{id}
        [HttpDelete("{code}/{holidayId}")]
        public ResponseMessage Delete(string code,int holidayId)
        {
            if(holidayId < 0 )
            {
                return new ResponseMessage { Message = "Bad Request!" }; 
            }
            return srvc.deleteHoliday(code, holidayId);
            
        }
    }
}
