using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using System.Text.RegularExpressions;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace FantasyCyclingParser
{
    public class FlightParser
    {
        public FlightParser()
        {

        }

        

        public void ParseCSV()
        {
            string path = @"c:\temp\BNA_Arrivals.csv";
            //string path = HttpContext.Current.Server.MapPath("~/App_Data/airportslist.csv");

            try
            {
                //using (ComNetSalesPipelineEntities edx = new ComNetSalesPipelineEntities())
                //{
                // using (sn2goEntities edx = new sn2goEntities())
                // {
                List<string> pipeLineItems = new List<string>();

                var lines1 = File.ReadLines(path);
                //var lines = File.ReadLines(path);
                var lines = lines1.Select(a => a.Split(','));
                var csv = from line in lines
                          select (from piece in line
                                  select piece).ToList();


                foreach (var item in csv)
                {
                    int x = 0;
                }
            }
            catch (Exception ex)
            {
                int z = 0; 
            }
        }



        public List<Flight> Parse(string ADI)
        {
            bool flag = false;
            List<Flight> flightList = new List<Flight>();


            string path = String.Empty;
            if (ADI == "D")
            {
                path = "C:\\Temp\\BNA_Dept.html";
                flag = false;
            }
            else
            {
                path = "C:\\Temp\\BNA_Arrivals.html";
                flag = true; 
            }

            FileStream fs = File.Open(path, FileMode.Open);
            var parser = new HtmlParser();

            var document = parser.Parse(fs);


            var tbl = document.QuerySelectorAll("#ffAlTbl");

            var all = document.All.Where(x => x.ClassList.Contains("flightValue") && x.ClassList.Contains("c10"));

            
            var p = all.First().Parent;

            foreach (var ele in all)
            {
                Flight flight = new Flight(flag);

                foreach (var item in ele.Parent.ChildNodes)
                {
                    if (item.NodeType.ToString() == "Element")
                    {
                        AngleSharp.Dom.Html.IHtmlDivElement myDiv = (AngleSharp.Dom.Html.IHtmlDivElement)item;

                        if (myDiv.ClassList.Contains("c1"))
                        {

                            AngleSharp.Dom.Html.IHtmlTableElement table = (AngleSharp.Dom.Html.IHtmlTableElement)myDiv.ChildNodes[1];
                            flight.Airline = table.QuerySelectorAll("td.ffAlLbl").First().InnerHtml;
                            
                        }

                        //if (myDiv.ClassList.Contains("c2"))
                        //{
                        //    int z = 0;
                        //}
                        if (myDiv.ClassList.Contains("c3"))
                        {
                            //int x = 0;
                            flight.FlightNumber = myDiv.InnerHtml;
                        }
                        if (myDiv.ClassList.Contains("c4"))
                        {
                            string[] temp = myDiv.InnerHtml.Split(',');

                            flight.City = temp[0].Trim();

                            if (temp.Count() > 1)
                                flight.State = temp[1].Trim();
                        }
                        if (myDiv.ClassList.Contains("c5"))
                        {
                            flight.Status = myDiv.TextContent;
                        }
                        if (myDiv.ClassList.Contains("c6"))
                        {
                            flight.SchedTime = myDiv.TextContent.Replace("\u2605", string.Empty);
                        }
                        if (myDiv.ClassList.Contains("c7"))
                        {
                            flight.ActualTime = myDiv.TextContent.Replace("\u2605", string.Empty);
                        }
                        //if (myDiv.ClassList.Contains("c8"))
                        //{
                        //    flight.Status = myDiv.InnerHtml;
                        //}
                        if (myDiv.ClassList.Contains("c9"))
                        {
                            if (myDiv.InnerHtml.Contains("nbsp"))
                            {
                                flight.Gate = "B9";
                            }
                            else {
                                flight.Gate = myDiv.InnerHtml;
                            }
                        }


                    }
                }

                flightList.Add(flight);
            }

            return flightList;                    

       }

    }

    public class Flight
    {
        public Flight()
        {

        }
        public Flight(bool isArrival)
        {
            IsArrival = isArrival;
        }

        public string ToCSV()
        {
            string line = String.Empty;

            if (IsArrival)
                line = String.Format("A,{0},{1},{2},{3},{4},{5},{6},{7}", Airline, FlightNumber, Status, City, State, SchedTime, ActualTime, Gate);
            else
                line = String.Format("D,{0},{1},{2},{3},{4},{5},{6},{7}", Airline, FlightNumber, Status, City, State, SchedTime, ActualTime, Gate);

            return line; 
        }
        public string ToJSON()
        {
            string line = String.Empty;

            if (IsArrival)
                line = String.Format("[\"A\",\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"],", Airline, FlightNumber, Status, City, State, SchedTime, ActualTime, Gate);
            else
                line = String.Format("[\"D\",\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\"],", Airline, FlightNumber, Status, City, State, SchedTime, ActualTime, Gate);

            return line;
        }

        public bool IsArrival { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string SchedTime { get; set; }

        public string ActualTime { get; set; }
        public string Gate { get; set; }
        
        
    }
}
