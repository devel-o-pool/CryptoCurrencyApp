﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Web;

namespace CryptoCurrencyApp
{
    //auto-generated class for handling events
    public partial class CryptoCurrency2 : UserControl
    {
        private Thread cryptoCurrency2Thread;

        URLs GetURLs = new URLs();

        //Values for storing the data retrieved from the API
        private double currency = 0;
        private String currencyURL = "";
        private double high = 0;
        private double low = 99999999;
        private double userHigh = 99999999;
        private double userLow = 0;
        private double userDifference = 0;
        private bool rise = false;
        private double previous = 0;
        private char currencyType = 'e';

        public CryptoCurrency2()
        {
            InitializeComponent();

            //Initialize each tab with a default cryptocurrency
            currencyURL = GetURLs.getURL(currencyType);
            cryptoCurrency2Chart.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
            label1.Text = "ETH";
        }

        //Retreives data from the API, with the help of a loop
        private void getRLTData()
        {

            while (true)
            {

                //uses Newtonsoft.JSON framework to serialize the data
                var currency2Rates = _download_serialized_json_data<JSONStructure>(currencyURL);

                //get the current value of the cryptocurrency
                try
                {
                    //changes the url depending upon the currency that the user has picked
                    if (currencyType=='e')
                        currency = currency2Rates.ETH.USD;
                    else if (currencyType == 'b')
                        currency = currency2Rates.BTC.USD;
                    else if (currencyType == 'l')
                        currency = currency2Rates.LTC.USD;
                    else if (currencyType == 'd')
                        currency = currency2Rates.DASH.USD;
                    else if (currencyType == 'z')
                        currency = currency2Rates.ZEC.USD;

                }

                catch (Exception e)
                {
                    if (e.Equals("NullReferenceException"))
                        continue;
                }

                //verifies that the thread exists
                if (cryptoCurrency2Chart.IsHandleCreated)
                {
                    //invokes the UpdateChart method
                    this.Invoke((MethodInvoker)delegate { UpdateChart(); });
                }

                //Pauses the loop for 10 seconds
                Thread.Sleep(10000);

            }


        }

        private void UpdateChart()
        {

            //Add point to the graph
            cryptoCurrency2Chart.Series["Series1"].Points.AddY(currency);

            //If the currency reaches a new high
            if (currency > high)
                high = currency;

            //If the currency reaches a new low
            if (currency < low)
                low = currency;

            //Generate an alert if the currency rises above the value specified by the user
            if (currency > userHigh)
                MessageBox.Show(("Attention the Cryptocurrency has risen above " + userHigh));

            //Generate an alert if the currency rises above the value specified by the user
            if (currency < userLow)
                MessageBox.Show(("Attention the Cryptocurrency has fallen bellow " + userLow));

            //Check whether the currency is rising or falling
            if (previous > currency)
                rise = false;
            else
                rise = true;

            //Display wheter the currency is rising or falling
            if (rise)
                graphSlope.Text = ("Rising" + currency.ToString() );
            else
                graphSlope.Text = ("Falling" + currency.ToString() );

            //Assign the current value as the previous value
            previous = currency;

        }

        //get the JSON data
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

        //Button to start the program or refresh the program (when refreshed, the graph is cleared)
        private void refreshCurrency_Click(object sender, EventArgs e)
        {
            //clears the old data from the chart
            cryptoCurrency2Chart.Series["Series1"].Points.Clear();
            //kills the previous thread
            if (cryptoCurrency2Thread!=null)
                cryptoCurrency2Thread.Abort();
            //creates a new thread
            cryptoCurrency2Thread = new Thread(new ThreadStart(this.getRLTData));
            cryptoCurrency2Thread.IsBackground = true;
            cryptoCurrency2Thread.Start();
            refreshCurrency.Text = "Refresh";
        }

        //first trading rule
        private void setUserHigh_Click(object sender, EventArgs e)
        {
            userHigh = Double.Parse (getUserHigh.Text);
        }

        //second trading rule
        private void setUserLow_Click(object sender, EventArgs e)
        {
            userLow = Double.Parse(getUserLow.Text);
        }

        //radio buttons used to cycle between currencies
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //when user picks a new currency
            if (radioButton1.Checked)
            {
                //uses the url for the new currency, and refreshes the program
                currencyType = 'b';
                currencyURL = GetURLs.getURL(currencyType);
                label1.Text = "BTC";
                refreshCurrency.PerformClick();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                currencyType = 'e';
                currencyURL = GetURLs.getURL(currencyType);
                label1.Text = "ETH";
                refreshCurrency.PerformClick();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                currencyType = 'l';
                currencyURL = GetURLs.getURL(currencyType);
                label1.Text = "LTC";
                refreshCurrency.PerformClick();
            }
        }
    }

}
