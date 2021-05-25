using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain;
using System.Threading;
using GeneticSharp.Infrastructure.Threading;
using System.Diagnostics;
using System.Net;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoRepository;
using System.Text.RegularExpressions;

namespace FantasyCyclingParser
{

    public static class QS
    {
        public const string SeasonStatsLink = "body > div.wrapper > div.content > div:nth-child(1) > ul > li:nth-child(3) > ul > li:nth-child(3) > a";
        public const string Age = "body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > b";
        public const string Weight = "body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > span:nth-child(5) > b";
        public const string Height = "body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > span:nth-child(5) > span > b:nth-child(1)";
        public const string Specialty = "body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > span:nth-child(5) > span > b:nth-child(3)";
        public const string Specialty2 = "body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > b:nth-child(5)";

        public const string PDCHistoric = "#content > table:nth-child(5)";

        public const string TeamTotalPoints = "#content > table:nth-child(4) > tbody > tr:nth-child(27) > th:nth-child(5)";

      //  public const string TeamName = "#content > h2"; ...not being used, probably should be.  Used in ParseTeam

        //public const string PDCHistoricCost = "#content > table:nth-child(5) > tbody > tr:nth-child(10) > td.tar";
        //public const string PDCHistoricPoints = "#content > table:nth-child(5) > tbody > tr:nth-child(9) > td.tar";
    }
    public static class TryParse
    {
        public static bool TryToParse(AngleSharp.Dom.Html.IHtmlDocument document, string path)
        {
            //IHtmlCollection<IElement> val = null;
            try
            {
                var item = document.QuerySelectorAll(path).First();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
    public static class Parser
    {
        public static List<string> ParsePCSRiders()
        {

            List<string> riderURLs = new List<string>();
            using (WebClient client = new WebClient())
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;


                var parser = new HtmlParser();


                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        string url = String.Format("http://www.procyclingstats.com/rankings.php?c=1&filter=1&id=18732&index={0}&nation=&team=&compare_to_id=0&younger_than=&older_than=", i);

                        string htmlCode = client.DownloadString(url);

                        var document = parser.Parse(htmlCode);

                        var table = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(5) > table > tbody").First();
                        foreach (var row in table.ChildNodes)
                        {
                            AngleSharp.Dom.Html.IHtmlAnchorElement name = (AngleSharp.Dom.Html.IHtmlAnchorElement)row.ChildNodes[3].ChildNodes[2];
                            string riderURL = name.Attributes[1].Value.ToString();

                            riderURLs.Add(riderURL);

                            int z = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;

                    }
                }

                return riderURLs;
            }
        }


        public static List<string> ParsePCSRider(int riderID, int year)
        {

            List<string> riderURLs = new List<string>();
            using (WebClient client = new WebClient())
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;


                var parser = new HtmlParser();                
                    try
                    {
                        string url = String.Format("https://pdcvds.com/riders.php?mw=1&y={0}&pid={1}", year, riderID);

                        string htmlCode = client.DownloadString(url);

                        var document = parser.Parse(htmlCode);

                        var table = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(5) > table > tbody").First();
                        foreach (var row in table.ChildNodes)
                        {
                            AngleSharp.Dom.Html.IHtmlAnchorElement name = (AngleSharp.Dom.Html.IHtmlAnchorElement)row.ChildNodes[3].ChildNodes[2];
                            string riderURL = name.Attributes[1].Value.ToString();

                            riderURLs.Add(riderURL);

                            int z = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        //continue;

                    }
                
                return riderURLs;
            }
        }


        public static PDC_AnnualData ParseHistoricPDCStats(string url, int year)
        {
            string u = String.Format(url, year);

            return ParseHistoricPDCStats(u);
        }
        
        public static PDC_AnnualData ParseHistoricPDCStats(string url) 
        {
            PDC_AnnualData data = new PDC_AnnualData(); 

            using (WebClient client = new WebClient())
            {

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;


                try
                {
                    var parser = new HtmlParser();                    
                    string htmlCode = client.DownloadString(url);

                    var document = parser.Parse(htmlCode);
                    if (TryParse.TryToParse(document, QS.PDCHistoric))
                    {
                        var tbl = document.QuerySelectorAll(QS.PDCHistoric).First();
                        string[] items = tbl.TextContent.Trim().Replace("\n", "").Split('\t');

                        int count = 0;
                        foreach(string s in items)
                        {
                            if (s == "Score 2015")
                            {
                                data.PointsScored = Convert.ToInt32(items[count + 2]);
                            }

                            if (s == "Salary 2015")
                            {
                                data.Cost = Convert.ToInt32(items[count + 2]);
                            }
                            count++;
                        }
                    }


                    //if (TryParse.TryToParse(document, QS.PDCHistoricCost))
                    //    data.Cost = Convert.ToInt32(document.QuerySelectorAll(QS.PDCHistoricCost).First().InnerHtml);

                    //if (TryParse.TryToParse(document, QS.PDCHistoricPoints))
                    //    data.PointsScored = Convert.ToInt32(document.QuerySelectorAll(QS.PDCHistoricPoints).First().InnerHtml);


                }
                catch (Exception ex)
                {

                }
            }

            return data; 
        }
        public static List<Rider> ParseAllRiders(int year, bool loadPCSstats =false)
        {
            string url = "http://pdcvds.com/riders.php?mw=1&y="+year+"&n=0";            
            var parser = new HtmlParser();
            //var document = parser.Parse(path);
            List<Rider> failList = new List<Rider>();

            List<Rider> riderList = new List<Rider>();
            //string path = "C:\\Code\\FantasyCycling\\FantasyCyclingParser\\FantasyCyclingParser\\Pages\\FSA DS __ Men 2016 __ Riders_All.html";
            //FileStream fs = File.Open(path, FileMode.Open);
            //var parser = new HtmlParser();

            //var document = parser.Parse(fs);

            using (WebClient client = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;


                string htmlCode = client.DownloadString(url);

                var document = parser.Parse(htmlCode);

                var tbl = document.QuerySelectorAll("#content > table.cell > tbody");


                //
                int count = 0;
                foreach (var rider in tbl[0].ChildNodes)
                {
                    Rider r = new Rider();
                    try
                    {

                        string[] rd = rider.TextContent.Trim().Replace("\n", "").Split('\t');

                        AngleSharp.Dom.Html.IHtmlAnchorElement anc2 = (AngleSharp.Dom.Html.IHtmlAnchorElement)rider.ChildNodes[9].ChildNodes[0];
                        string riderlink = anc2.Href;

                        riderlink = Regex.Replace(riderlink, @"y=\d{2,}", "y={0}");
                        if (rd.Count() == 17)
                        {


                            r.PdcRankCurrentYear = Convert.ToInt32(rd[0].Trim().Replace(".", ""));
                            r.Team = rd[4].Trim();
                            r.TeamStatus = rd[6].Trim();
                            r.PDC_RiderURL = riderlink;
                            r.Name = rd[8].Trim();
                            r.YearBorn = Convert.ToInt32(rd[10]);
                            r.CurrentYearCost = Convert.ToInt32(rd[12]);
                            r.CurrentYearPoints = Convert.ToInt32(rd[16]);
                            PDC_AnnualData adata = Parser.ParseHistoricPDCStats(riderlink, DateTime.Now.Year - 1);

                            r.PreviousCost = adata.Cost;
                            r.PreviousPoints = adata.PointsScored;

                            r.PointsDiff = r.CurrentYearPoints - r.PreviousPoints;

                            r.CostDiff = r.CurrentYearCost - r.PreviousCost;

                            r.CostDiffPercent = Math.Round((r.CostDiff / r.PreviousCost), 2) * 100.0;
                            r.PointsDiffPercent = Math.Round((r.PointsDiff / r.PreviousPoints), 2) * 100.0;

                            if (loadPCSstats)
                                r.LoadProCyclingStats();



                            //r.ToCSV();

                            Console.WriteLine(count + ". Adding Rider: " + r.Name);
                            riderList.Add(r);  //unique points given price only...

                            count++;
                        }
                        int zz = 0;
                    }
                    catch (Exception ex)
                    {
                        failList.Add(r);
                        continue;

                        int d = 0;
                    }

                }//end foreach

            }
            return riderList;
        }

        //public static List<Rider> ParseEvent(string url)
        //{
        //}
        public static List<PDC_Result> ParsePDCResults(int year)
        {
            using (WebClient client = new WebClient())
            {
                string url = String.Format("https://pdcvds.com/results.php?mw=1&y={0}", year);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;



                string htmlCode = client.DownloadString(url);
                var parser = new HtmlParser();

                var document = parser.Parse(htmlCode);

                string currentMonth = "";

                ///var document = parser.Parse(fs);

                
                var rows = document.QuerySelectorAll("table.cell").First().QuerySelectorAll("tr");

                var els = rows.ToArray();


                List<PDC_Result> results = new List<PDC_Result>(); 
                foreach (IElement el in rows.ToList().Skip(1))
                {
                    try
                    {
                        string[] rd = el.TextContent.Trim().Replace("\n", "").Split('\t');


                        if (rd.Count() == 13) //13 means a stage of a race
                        {
                            AngleSharp.Dom.Html.IHtmlAnchorElement country = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[5].ChildNodes[0];
                            AngleSharp.Dom.Html.IHtmlAnchorElement resultURL = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[9].LastChild;

                            PDC_Result result = new PDC_Result();

                            currentMonth = rd[0];

                            result.Month = currentMonth;
                            result.DayNum = rd[2];
                            result.Country = country.Attributes[1].Value;
                            result.Category = rd[6];
                            result.EventName = rd[8];
                            result.RiderCount = rd[10];
                            result.Points = rd[12];
                            result.URL = "https://pdcvds.com/results.php" + resultURL.Attributes[0].Value;

                            result.RaceResults = Parser.ParsePDCEventResult(result.URL);
                            Console.WriteLine(result.EventName);

                            results.Add(result);
                        }

                        if (rd.Count() == 11) //11 means a single race
                        {

                       
                            AngleSharp.Dom.Html.IHtmlAnchorElement country = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[5].ChildNodes[0];
                            AngleSharp.Dom.Html.IHtmlAnchorElement resultURL = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[9].LastChild;

                            PDC_Result result = new PDC_Result();

                            result.Month = currentMonth;
                            result.DayNum = rd[0];
                            result.Country = country.Attributes[1].Value;
                            result.Category = rd[5];
                            result.EventName = rd[6];
                            result.RiderCount = rd[8];
                            result.Points = rd[10];
                            result.URL = "https://pdcvds.com/results.php" + resultURL.Attributes[0].Value;

                            result.RaceResults = Parser.ParsePDCEventResult(result.URL);
                            Console.WriteLine(result.EventName);

                            results.Add(result);
                        }
                      
                        //AngleSharp.Dom.Html.IHtmlAnchorElement anc = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[3].ChildNodes[0];

                        //AngleSharp.Dom.Html.IHtmlAnchorElement anc2 = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[9].ChildNodes[0];



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("failed");
                        
                        continue;
                    }
                    //finally
                    //{
                    //    Console.WriteLine("Adding: " + result);

                    //}

                }

                return results;      
            }
        }

        public static List<PDC_RaceResult> ParsePDCEventResult(string url)
        {

            List<PDC_RaceResult> raceResults = new List<PDC_RaceResult>(); 
            using (WebClient client = new WebClient())
            {
                

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;



                string htmlCode = client.DownloadString(url);
                var parser = new HtmlParser();

                var document = parser.Parse(htmlCode);

                IHtmlCollection<IElement> rows = null;

                if (document.QuerySelectorAll("table").Count() == 2)
                {
                    rows = document.QuerySelectorAll("table").Last().QuerySelectorAll("tr");
                }
                if (document.QuerySelectorAll("table").Count() == 3)
                {
                    rows = document.QuerySelectorAll("table")[1].QuerySelectorAll("tr"); //"last" is the DSTeams, not the results
                }


                var els = rows.ToArray();

                //results page - <tr> with a <th> means it is just info, we want trs that are just the results and should be able to 
                //ignore the heading...points are points, don't care what category they got them from. 

                 
                foreach (IElement el in rows.ToList())
                {
                    try
                    {

                        if (el.QuerySelectorAll("th").Count() == 0)
                        {
                            PDC_RaceResult r = new PDC_RaceResult();
                           
                            string[] rd = el.TextContent.Trim().Replace("\n", "").Split('\t');
                            if (rd.Count() == 9) //avoid empty or "Scoring Teams" results which pass above filters.  
                            {
                                r.Place = rd[0];
                                r.Team = rd[4];
                                r.Name = rd[6];
                                r.Points = Convert.ToInt32(rd[8]);

                                raceResults.Add(r);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("failed");

                        continue;
                    }
             

                }
            }

            return raceResults;
        }


        public static List<Rider> ParseUniqueCostPriceRiders()
        {
            //string path = "http://pdcvds.com/riders.php?mw=1&y=2016&n=0";            
            //var parser = new HtmlParser();
            //var document = parser.Parse(path);

            List<Rider> riderList = new List<Rider>();
            string path = "C:\\Code\\FantasyCycling\\FantasyCyclingParser\\FantasyCyclingParser\\Pages\\FSA DS __ Men 2016 __ Riders_All.html";
            FileStream fs = File.Open(path, FileMode.Open);
            var parser = new HtmlParser();

            var document = parser.Parse(fs);



            var tbl = document.QuerySelectorAll("#content > table.cell > tbody");


            List<KeyValuePair<double, double>> priceScore = new List<KeyValuePair<double, double>>();

            foreach (var rider in tbl[0].ChildNodes)
            {
                try
                {
                    string[] rd = rider.TextContent.Trim().Replace("\n", "").Split('\t');
                    KeyValuePair<double, double> kvp;



                    if (rd.Count() == 17)
                    {
                        Rider r = new Rider();

                        r.PdcRankCurrentYear = Convert.ToInt32(rd[0].Trim().Replace(".", ""));
                        r.Team = rd[4].Trim();
                        r.TeamStatus = rd[6].Trim();
                        r.Name = rd[8].Trim();
                        r.YearBorn = Convert.ToInt32(rd[10]);
                        r.CurrentYearCost = Convert.ToInt32(rd[12]);
                        r.CurrentYearPoints = Convert.ToInt32(rd[16]);

                        kvp = new KeyValuePair<double, double>(r.CurrentYearPoints, r.CurrentYearCost);


                        if (!priceScore.Contains(kvp))
                        {
                            priceScore.Add(kvp);
                            riderList.Add(r);  //unique points given price only...
                        }

                    }
                    int zz = 0;
                }
                catch (Exception ex)
                {
                    continue;

                    int d = 0;
                }

            }//end foreach


            return riderList;
        }
        public static void ParseTeamList()
        {
            string path = "C:\\Code\\FantasyCycling\\FantasyCyclingParser\\FantasyCyclingParser\\Pages\\FSA DS __ Men 2016 __ Teams.html";
            FileStream fs = File.Open(path, FileMode.Open);
            var parser = new HtmlParser();

            var document = parser.Parse(fs);

            var tbl = document.QuerySelectorAll("#content > table");
            int z = 0;

        }

        public static int GetTeamPoints(int teamUID, int year)
        {
            int points = -1; 

            using (WebClient client = new WebClient())
            {
                string url = String.Format("https://pdcvds.com/teams.php?mw=1&y={1}&uid={0}", teamUID, year);


                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;


                string htmlCode = client.DownloadString(url);
                var parser = new HtmlParser();

                var document = parser.Parse(htmlCode);

                IElement lastRow = document.QuerySelectorAll("table.cell").First().QuerySelectorAll("tr").Last();

                points = Convert.ToInt32(lastRow.ChildNodes[9].TextContent);

               
            }


            return points; 
        }

        public static Team ParseTeam(string teamUID, int year, bool getPCSstats = false)
        {
            // MongoRepository<Team> db = new MongoRepository<Team>();
            Team t = new Team();
            //string path = "C:\\Code\\FantasyCycling\\FantasyCyclingParser\\FantasyCyclingParser\\Pages\\FSA DS __ Men 2016 __ Teams __ il vaut mieux pomper.html";
            //FileStream fs = File.Open(path, FileMode.Open);
            //var parser = new HtmlParser();

            using (WebClient client = new WebClient())
            {
                string url = String.Format("https://pdcvds.com/teams.php?mw=1&y={1}&uid={0}", teamUID, year);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (sender, x509Certificate, chain, sslPolicyErrors) => true;



                string htmlCode = client.DownloadString(url);
                var parser = new HtmlParser();

                var document = parser.Parse(htmlCode);

                ///var document = parser.Parse(fs);

                t.PDCTeamName = document.QuerySelectorAll("#content > h2").First().TextContent.Trim().Replace("Teams :: ", "").Trim();
                var rows = document.QuerySelectorAll("table.cell").First().QuerySelectorAll("tr");

                var els = rows.ToArray();
                string name = String.Empty;
                Rider r = new Rider();

                foreach (IElement el in rows.ToList())
                {
                    try
                    {
                        string[] rd = el.TextContent.Trim().Replace("\n", "").Split('\t');
                        AngleSharp.Dom.Html.IHtmlAnchorElement anc = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[3].ChildNodes[0];

                        AngleSharp.Dom.Html.IHtmlAnchorElement anc2 = (AngleSharp.Dom.Html.IHtmlAnchorElement)el.ChildNodes[9].ChildNodes[0];

                        if (rd.Count() == 15  && !String.IsNullOrEmpty(rd[8])) //make sure they have a name...
                        {
                            r = new Rider();

                            r.PdcRankCurrentYear = Convert.ToInt32(rd[0].Trim().Replace(".", ""));
                            r.Team = rd[4];
                            r.TeamStatus = rd[6];
                            r.Name = rd[8];
                            name = r.Name;
                            r.Nationality = anc.Attributes[1].Value;
                            r.PDC_RiderURL = anc.Href.ToString();
                            // anc2.Href.ToString().Replace("http://pdcvds.com/riders.php?mw=1&y=2016&pid=", "").Trim();
                            //   r.PDC_RiderURL = anc2.Attributes[1].Value;
                            r.CurrentYearCost = Convert.ToInt32(rd[10]);

                            r.PreviousPoints = Convert.ToInt32(rd[12]);
                            r.CurrentYearPoints = Convert.ToInt32(rd[14]);
                            r.PointsDiff = r.CurrentYearPoints - r.PreviousPoints;

                            if (getPCSstats)
                                r.LoadProCyclingStats();

                            t.AddRider(r);

                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("failed to get stats for: " + name);
                        t.MissingStatsForRiders.Add(name);
                        continue;
                    }
                    //finally
                    //{
                    //    Console.WriteLine("Adding: " + name);
                       
                    //}

                }
                Console.WriteLine(t.ToString());
                //t.ToCSVFile();

              //  int[] test = ClusteringKMeans.KMeans.Cluster(t.ToVector(), 3);


                //db.Add(t);

                //t.PrintCostFrequency();
                return t;

            }
        }

        public static void Testing_ParseProStatsRider()
        {
            string path = "C:\\Code\\FantasyCycling\\FantasyCyclingParser\\FantasyCyclingParser\\Pages\\PeterSagan.html";
            FileStream fs = File.Open(path, FileMode.Open);
            var parser = new HtmlParser();

            var document = parser.Parse(fs);
            //var document = parser.Parse("http://www.procyclingstats.com/rider/Peter_Sagan");

            var age = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > b").First().NextSibling.TextContent.Trim();
            var weight = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > span:nth-child(5) > b").First().NextSibling.TextContent.Trim();
            var height = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > span:nth-child(5) > span > b:nth-child(1)").First().NextSibling.TextContent;

            var specialtyPts = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(5) > div:nth-child(3) > span > span > span:nth-child(5) > span > b:nth-child(3)");
            var oneDay = specialtyPts.First().NextSibling.ChildNodes[0].ChildNodes[1].TextContent;
            var GC = specialtyPts.First().NextSibling.ChildNodes[1].ChildNodes[1].TextContent;
            var TT = specialtyPts.First().NextSibling.ChildNodes[2].ChildNodes[1].TextContent;
            var Sprint = specialtyPts.First().NextSibling.ChildNodes[3].ChildNodes[1].TextContent;

            int x = 0;
        }

    }
}
