using CountriesAndHolidaysApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesAndHolidaysApp.Services;

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
        [Route("/sync")]
        [HttpGet]
        public async Task<object> Get()
        {
           object response = await srvc.sync();
            return response;
        }

       

        // GET api/<ValuesController>/5
        [HttpGet("{code}")]
        public  string Get(string code)
        {
            string response =  srvc.getCountryHolidays(code);
            return response;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
