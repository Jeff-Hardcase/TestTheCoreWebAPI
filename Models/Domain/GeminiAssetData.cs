using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTheCoreWebAPI.Models.Domain
{
    public class GeminiAssetData
    {
        public string Bid { get; set; }
        public string Ask { get; set; }
        public Volume Volume { get; set; }
        public string Last { get; set; }
    }

    public class Volume
    {
        public string BTC { get; set; }
        public string ETH { get; set; }
        public string USD { get; set; }
        public long Timestamp { get; set; }
    }
}
