using System;
using Newtonsoft.Json;

namespace Qis.App.Domain
{
    public class Stock
    {
        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        public double DailyGain => Close - Open;
    }
}