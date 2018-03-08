using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace CryptoCurrencyApp
{

    public partial class Dashboard : Form
    {

        private double currency1 = 0;
        private double currency2 = 0;
        private double currency3 = 0;



        public Dashboard()
        {
            InitializeComponent();
            dashboardChart.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;

            var BTCRates = _download_serialized_json_data<HistoricalDataJSON>("https://min-api.cryptocompare.com/data/histoday?fsym=BTC&tsym=USD&limit=30");

            var LTCRates = _download_serialized_json_data<HistoricalDataJSON>("https://min-api.cryptocompare.com/data/histoday?fsym=LTC&tsym=USD&limit=30");

            var ETHRates = _download_serialized_json_data<HistoricalDataJSON>("https://min-api.cryptocompare.com/data/histoday?fsym=ETH&tsym=USD&limit=30");

            int i = 0;


            while (i < BTCRates.Data.Length)
            {

                try
                {
                    currency1 = BTCRates.Data[i].close;
                    currency2 = LTCRates.Data[i].close;
                    currency3 = ETHRates.Data[i].close;

                }

                catch (Exception e)
                {
                    if (e.Equals("NullReferenceException"))
                        continue;
                }

                
                    dashboardChart.Series["Series1"].Points.AddY(currency1);
                    dashboardChart.Series["Series2"].Points.AddY(currency2);
                    dashboardChart.Series["Series3"].Points.AddY(currency3);
                

                i++;
            }
        }


        

        private static T _download_serialized_json_data<T>(string url) where T : new()
        {
            using (WebClient w = new WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString(url);
                    Debug.WriteLine(json_data);
                }
                catch (Exception exe) { Debug.WriteLine(exe.ToString()); }
                // if string with JSON data is not empty, deserialize it to class and return its instance 
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cryptoCurrency22.Hide();
            cryptoCurrency22.SendToBack();
            cryptoCurrency21.Hide();
            cryptoCurrency21.SendToBack();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cryptoCurrency22.Show();
            cryptoCurrency22.BringToFront();
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cryptoCurrency21.Show();
            cryptoCurrency21.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }



}
