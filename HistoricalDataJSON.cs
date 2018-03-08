using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyApp
{
    class HistoricalDataJSON
    {
        public Datum[] Data { get; set; }
    }

    public class Datum
    {
        
        public float close { get; set; }
        public float open { get; set; }
    }

}
