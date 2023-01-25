using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;

namespace AdressUpdate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private List<Address> _addresses = new List<Address>();
        private string _path = @"C:\temp\Address.txt";

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /*
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        */

        [Route("GetAddresses")]
        [HttpGet]
        public IEnumerable<Address> GetAddresses()
        {
            foreach (string line in System.IO.File.ReadLines(_path))
            {
                System.Console.WriteLine(line);
  

                var adressdto = JsonConvert.DeserializeObject<Address>(line);
                _addresses.Add(adressdto);
            }
            return _addresses.ToArray();
        }


        [Route("AddAddress")]
        [HttpPost]
        public IActionResult AddAddress(Address address)
        {          

            var adressString = JsonConvert.SerializeObject(address);
            using (StreamWriter writer = new StreamWriter(_path, true))
            {
                writer.WriteLine(adressString);
            }

            return new OkResult();
        }


        [Route("AddAddressError")]
        [HttpPost]
        public IActionResult AddAddressError(Address address)
        {
            _logger.Log(LogLevel.Error,address.Id.ToString());
            throw new Exception("There was an error");
        }
    }
}