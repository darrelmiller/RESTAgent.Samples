using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Http;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMShopClient.Tools.Interfaces;
using TMShopClient.Tools.Services;

namespace TMShopClient.Tests.Tools.Services {
    [TestClass]
    public class ServiceTests {


        [Ignore]
        [TestMethod]
        public void CreateRequestsFast() {
            

            bool stop = false;
            var sw = new Stopwatch();

            int count = 1;
            sw.Start();
            while (!stop) {
                try {
                    Uri url = new Uri("http://tmserver:8700/shopclient");
                    HttpClient hc = new HttpClient();
                    hc.TransportSettings.Credentials = new NetworkCredential("darrel", "olecom");
                    hc.TransportSettings.ConnectionTimeout = TimeSpan.FromMilliseconds(2500);
                    HttpResponseMessage resp = hc.Get(url);
                    string result = resp.Content.ReadAsString();
                }
                catch  {
                    stop = true;
                }
                count++;
                if (count == 100) stop = true;
            }

            sw.Stop();
            var speed = 100.00 / (sw.ElapsedMilliseconds/1000.00);
            Debug.WriteLine("That took " + speed);
        }
    }
}
