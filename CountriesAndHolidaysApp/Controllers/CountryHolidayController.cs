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
        // GET: api/<ValuesController>
        [HttpPost, Route("sync")]
        public async Task<object> PostData()
        {
            object response = await srvc.sync();
            return response;

        }

       

        // GET api/<ValuesController>/5
        [HttpGet("{code}")]
        public  string Get(string code)
        {
            return srvc.getCountryHolidays(code);
        }

        

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
