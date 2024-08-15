using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EmployerInfoMVC.Models;
using System.Runtime;

namespace EmployerInfoMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<EmployerDataViewModal> employerdata = new List<EmployerDataViewModal>();
            using (HttpClient client = new HttpClient())
            {

                var response = client.GetAsync("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==").Result;

                if (response.IsSuccessStatusCode)
                {
                    employerdata = JsonConvert.DeserializeObject<List<EmployerDataViewModal>>
                        (response.Content.ReadAsStringAsync().Result).ToList();

                }


            }
            var dt = employerdata.GroupBy(x => x.EmployeeName).ToList();


            List<Employerdata> employermonthlydata = new List<Employerdata>();

            foreach (var item in dt)
            {
                int hrs = 0;
                foreach (var s in item)
                {
                    hrs =hrs+ (s.EndTimeUtc - s.StarTimeUtc).Hours;
                }
                employermonthlydata.Add(new Employerdata { EmployeeName = item.Key, TotalTime = hrs });
            }
            // 24 days avg per month, each day 8 hours i.e total 192 hours
            //Pie Chart info
            List<DataPoint> dataPoints = new List<DataPoint>();
            
            foreach (var dataPoint in employermonthlydata)
            {
                dataPoints.Add(new DataPoint {label=dataPoint.EmployeeName, y=Convert.ToInt32((Convert.ToDecimal(dataPoint.TotalTime)/192)*100) });
            }
            

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View(employermonthlydata);
        }

       
        
    }
}
