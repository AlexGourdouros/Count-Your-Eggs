using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Test30.Models;

namespace Test30.Controllers
{
    public class DashboardController : Controller
    {
        private Test30Entities db = new Test30Entities();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BubbleBreak()
        {
            var db = new Test30Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.Items select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 400, height: 200, theme: ChartTheme.Green)
                .AddTitle("Item Quantites")
                .AddSeries("Default", chartType: "Bar", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;


        }
        public ActionResult BubbleBreak2()
        {
            var db = new Test30Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.UsedItems select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.Item.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 400, height: 200, theme: ChartTheme.Green)
                .AddTitle("Used Item Quantites")
                .AddSeries("Default", chartType: "Bar", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;


        }

        public ActionResult BubbleBreak3()
        {
            var db = new Test30Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.Spoilages select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.Item.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 400, height: 200, theme: ChartTheme.Green)
                .AddTitle("Spoilage Quantites")
                .AddSeries("Default", chartType: "Bar", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;

        }
        public ActionResult BubbleBreak4()
        {
            var db = new Test30Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.PayBridges select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.PaymentDate));
            results.ToList().ForEach(rs => yValue.Add(rs.Payment));

            new Chart(width: 400, height: 200, theme: ChartTheme.Green)
                .AddTitle("Payments By Date")
                .AddSeries("Default", chartType: "Bar", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;

        }
        public ActionResult BubbleBreak5()
        {
            var db = new Test30Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.InvoiceItems select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.Item.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 400, height: 200, theme: ChartTheme.Green)
                .AddTitle("Invoice Item Quantites")
                .AddSeries("Default", chartType: "Bar", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;

        }
        public ActionResult BubbleBreak6()
        {
            var db = new Test30Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.Invoices select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.InvoiceNumber));
            results.ToList().ForEach(rs => yValue.Add(rs.Total));

            new Chart(width: 400, height: 200, theme: ChartTheme.Green)
                .AddTitle("Invoice Totals")
                .AddSeries("Default", chartType: "Bar", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;

        }
    }
    }