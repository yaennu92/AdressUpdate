namespace AdressUpdate
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
    }
}