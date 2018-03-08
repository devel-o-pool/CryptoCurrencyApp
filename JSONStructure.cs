using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyApp
{

    class JSONStructure
    {

        public BTC BTC { get; set; }
        public ETH ETH { get; set; }
        public LTC LTC { get; set; }
        public DASH DASH { get; set; }
        public ZEC ZEC { get; set; }
    }

        public class BTC
        {
            public float USD { get; set; }
        }

        public class ETH
        {
            public float USD { get; set; }
        }

        public class LTC
        {
            public float USD { get; set; }
        }

    public class DASH
    {
        public float USD { get; set; }
    }

    public class ZEC
    {
        public float USD { get; set; }
    }

    public class URLs {

        public String getURL(char crypto){
            switch (crypto){
                
                case 'e':
                    return "https://min-api.cryptocompare.com/data/pricemulti?fsyms=ETH&tsyms=USD";
                case 'l':
                    return "https://min-api.cryptocompare.com/data/pricemulti?fsyms=LTC&tsyms=USD";
                case 'd':
                    return "https://min-api.cryptocompare.com/data/pricemulti?fsyms=DASH&tsyms=USD";
                case 'z':
                    return "https://min-api.cryptocompare.com/data/pricemulti?fsyms=ZEC&tsyms=USD";
                default:
                    return "https://min-api.cryptocompare.com/data/pricemulti?fsyms=BTC&tsyms=USD";
            }
        }

    }


}
