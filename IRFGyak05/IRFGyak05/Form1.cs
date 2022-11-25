using IRFGyak05.Entities;
using IRFGyak05.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace IRFGyak05
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();

            var mnbService1 = new MNBArfolyamServiceSoapClient();

            var request1 = new GetCurrenciesRequestBody();

            var response1 = mnbService1.GetCurrencies(request1);

            var result1 = response1.GetCurrenciesResult;

            XmlDocument xml1 = new XmlDocument();

            xml1.LoadXml(result1);

            Console.WriteLine(result1);

            foreach (XmlElement x in xml1.DocumentElement)
            {
                //string c = x.ChildNodes[1].InnerText;
                for (int i = 0; i < 75; i++)
                {
                    string c = x.ChildNodes[i].InnerText;
                    Currencies.Add(c);
                }
                //Currencies.Add(c);
            }

            dataGridView1.DataSource = Rates;
            chartRateData.DataSource = Rates;
            comboBox1.DataSource = Currencies;
            comboBox1.SelectedIndex = 1;

            RefreshData();
        }

        private void RefreshData()
        {
          
            Rates.Clear();

            Feladat1();
            Feladat5();

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void Feladat5()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Feladat1());
            foreach (XmlElement x in xml.DocumentElement)
            {
                RateData r = new RateData();
                Rates.Add(r);

                r.Date = DateTime.Parse(x.GetAttribute("date"));

                var childElement = (XmlElement)x.ChildNodes[0];
                if (childElement == null)
                    continue;
                r.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    r.Value = value / unit;
            }
        }

        private string Feladat1()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = (string)comboBox1.SelectedItem,
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            Console.WriteLine(result);

            return result;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
