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
        private CountriesAndHolidaysContext CountryHolidayContext;
        private CountryHolidayServices srvc;
        public CountryHolidayController(CountriesAndHolidaysContext cntxt)
        {
            CountryHolidayContext = cntxt;
            srvc = new CountryHolidayServices();
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<object> Get()
        {
           object response = await srvc.sync(CountryHolidayContext);
            return response;
        }

       



        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
