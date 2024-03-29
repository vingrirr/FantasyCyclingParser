﻿using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using System.Reflection;
using MongoRepository;

namespace FantasyCyclingParser
{

    //public static class Utilities
    //{
    //    public static Nationality GetNationality(string key)
    //    {
    //        if (NationalityList.ContainsKey(key))
    //        {
    //            return NationalityList[key];
    //        }
    //    } 

    //    private static Dictionary<string, Nationality> NationalityList = new Dictionary<string, Nationality>()
    //    {
    //        { "BEL", new Nationality {Name = "Belgium", PDC_URL=String.Format("https://pdcvds.com/riders.php?mw=1&y={0}&nat=BEL",DateTime.Now.Year) }},
    //        { "COL", new Nationality {Name = "Colombia", PDC_URL=String.Format("https://pdcvds.com/riders.php?mw=1&y={0}&nat=COL",DateTime.Now.Year) } }

    //    };
    //}


    public class FantasyYearConfig : Entity
    {
        public FantasyYearConfig()
        {
            
            TeamUIDS = new List<PDCTeamYear>();
            Year = DateTime.Now.Year;
            
        }
        public FantasyYearConfig(int year)
        {
          
            TeamUIDS = new List<PDCTeamYear>(); 
            Year = year;
            
        }

        public string ConfigName { get; set; }
        public int Year { get; set; }
        public List<PDCTeamYear> TeamUIDS { get; set; }
        public bool IsDefault { get; set; }

        public bool IsDraft { get; set; }

        public string URLToAddPDCTeam { get; set; }
              
    }

    public class PDCTeamYear
    {
        public PDCTeamYear()
        {
            GUID = Guid.NewGuid().ToString();
        }
        public PDCTeamYear(string id, int year, string name, bool is35team =false)
                : this()
        {
            TeamUID = id;
            Year = year;
            Name = name;
            Is35Team = is35team;
        }
        public PDCTeamYear(string id)
            : this()
        {
            TeamUID = id;
            Year = DateTime.Now.Year;
        }
        public string TeamUID { get; set; }
        public int Year { get; set; }

        public string Name { get; set; }
        public string GUID { get; set; }
        
        public bool Is35Team { get; set; }

    }
    public class Rider : Entity
    {

        //[BsonElement("Id")]
        //public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }

        public string PDC_RiderURL { get; set; }

        public string PDC_RiderID { get; set; }

        public int CurrentYearPoints { get; set; }
        public int CurrentYearCost { get; set; }
       // public int PdcRankCurrentYear { get; set; }

        public int Year { get; set; }

        /// <summary>
        /// pro world tour, pro conti, conti etc
        /// </summary>
        public string PDCTeamStatus { get; set; }
        public string PDCTeam { get; set; }

        public double Age { get; set; }
        public double YearBorn { get; set; }
        public String Birthday { get; set; }


        public double PreviousPoints { get; set; }
        public double PreviousCost { get; set; }

        public RiderPhoto Photo { get; set; }
    
        public Rider()
        {
            Photo = new RiderPhoto(); 
        }
        
        public void CalculatePoints(List<PDC_Result> results)
        {
            CurrentYearPoints = results.SelectMany(q => q.RaceResults.Where(p => p.Rider_PDCID == PDC_RiderID)).Sum(g => g.Points);
            //CurrentYearPoints = results.SelectMany(q => q.RaceResults.Where(p => p.Name == Name)).Sum(g => g.Points);
        }

        #region old stuff
        //public double[] ToVector()
        //{
        //    double[] vector = new double[10];

        //    vector = new double[] { this.Age, this.CurrentYearCost, this.CurrentYearPoints, this.PreviousCost, this.PreviousPoints,
        //                               this.PointsDiff, this.SprintPoints, this.TTPoints, this.GCPoints, this.OneDayPoints };

        //    return vector;

        //}
        //public void LoadProCyclingStats()
        //{
        //    var parser = new HtmlParser();

        //    //string fname = this.Name.Split(null)[0];
        //    //string lname = this.Name.Split(null)[1];


        //    List<string> _pcsriderurls = File.ReadAllLines("PCSRiderURLs.txt").ToList();
        //    Dictionary<string, double> scores = new Dictionary<string, double>();

        //    foreach (string s in _pcsriderurls)
        //    {
        //        //string d = s.Replace("rider/", "").Trim();

        //        scores.Add(s, LevenshteinDistance.Compute(s, this.Name));
        //    }
        //    string r = scores.OrderBy(x => x.Value).Select(x => x.Key).First().ToString();


        //    using (WebClient client = new WebClient())
        //    {

        //        string url = String.Format("http://www.procyclingstats.com/{0}", r);

        //        string htmlCode = client.DownloadString(url);

        //        var document = parser.Parse(htmlCode);

        //        Parse(document);
        //    }


        //}

        //public void LoadProCyclingStats(string url)
        //{
        //    var parser = new HtmlParser();
        //    var document = parser.Parse(url);
        //    Parse(document);
        //}


        //public void GetPCDInfoForYear(int year)
        //{
        //    string url = String.Format(PDC_RiderURL, year);
        //    PDC_AnnualData data = Parser.ParseHistoricPDCStats(url);
        //    data.Year = year;

        //    HistoricPDCData.Add(data);

        //}
        //public void GetPCSSeasonStats(string href)
        //{
        //    using (WebClient client = new WebClient())
        //    {
        //        try
        //        {
        //            var parser = new HtmlParser();

        //            string url = String.Format("http://www.procyclingstats.com/rider.php{0}", href);

        //            string htmlCode = client.DownloadString(url);

        //            var document = parser.Parse(htmlCode);

        //            //var table = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth - child(4) > table > tbody");
        //            var currSeason = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(4) > table > tbody > tr:nth-child(1)").First();
        //            var prevSeason = document.QuerySelectorAll("body > div.wrapper > div.content > div:nth-child(4) > table > tbody > tr:nth-child(2)").First();

        //            PCS_SeasonStat current = new PCS_SeasonStat();

        //            PCS_CurrentSeasonStats.Year = Convert.ToInt32(currSeason.ChildNodes[0].TextContent);
        //            PCS_CurrentSeasonStats.PCSPoints = Convert.ToInt32(currSeason.ChildNodes[1].TextContent);
        //            PCS_CurrentSeasonStats.RaceDays = Convert.ToInt32(currSeason.ChildNodes[2].TextContent);
        //            PCS_CurrentSeasonStats.KMs = Convert.ToInt32(currSeason.ChildNodes[3].TextContent);
        //            PCS_CurrentSeasonStats.Wins = Convert.ToInt32(currSeason.ChildNodes[4].TextContent);
        //            PCS_CurrentSeasonStats.Top10s = Convert.ToInt32(currSeason.ChildNodes[5].TextContent);


        //            PCS_SeasonStat prev = new PCS_SeasonStat();

        //            PCS_PreviousSeasonStats.Year = Convert.ToInt32(prevSeason.ChildNodes[0].TextContent);
        //            PCS_PreviousSeasonStats.PCSPoints = Convert.ToInt32(prevSeason.ChildNodes[1].TextContent);
        //            PCS_PreviousSeasonStats.RaceDays = Convert.ToInt32(prevSeason.ChildNodes[2].TextContent);
        //            PCS_PreviousSeasonStats.KMs = Convert.ToInt32(prevSeason.ChildNodes[3].TextContent);
        //            PCS_PreviousSeasonStats.Wins = Convert.ToInt32(prevSeason.ChildNodes[4].TextContent);
        //            PCS_PreviousSeasonStats.Top10s = Convert.ToInt32(prevSeason.ChildNodes[5].TextContent);
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //}
        //private void Parse(AngleSharp.Dom.Html.IHtmlDocument document)
        //{

        //    try {

        //        string seasonStatsLink = String.Empty;

        //        if (TryParse.TryToParse(document, QS.SeasonStatsLink))
        //            seasonStatsLink = document.QuerySelectorAll(QS.SeasonStatsLink).First().Attributes[0].Value;

        //        GetPCSSeasonStats(seasonStatsLink);

        //        if (TryParse.TryToParse(document, QS.Age))
        //        {
        //            var age = document.QuerySelectorAll(QS.Age).First().NextSibling.TextContent.Trim();
        //            Age = Convert.ToInt32(age);
        //        }

        //        string specialToUse = "";


        //        if (TryParse.TryToParse(document, QS.Weight))
        //        {

        //            var weight = document.QuerySelectorAll(QS.Weight).First().NextSibling.TextContent.Trim();
        //            if (weight.Contains("kg"))
        //                Weight = Convert.ToInt32(weight.Replace("kg", "").Trim());
        //            else if (weight.Contains("m"))
        //                Height = Convert.ToDouble(weight.Replace("m", "").Trim()); //they fucked up

        //            specialToUse = QS.Specialty;
        //        }
        //        else
        //        {
        //            specialToUse = QS.Specialty2;
        //        }

        //        if (TryParse.TryToParse(document, QS.Height))
        //        {
        //            string height = document.QuerySelectorAll(QS.Height).First().NextSibling.TextContent;
        //            if (height.Contains("m"))
        //                Height = Convert.ToDouble(height.Replace("m", "").Trim());
        //            else if (height.Contains("kg"))
        //                Weight = Convert.ToInt32(height.Replace("kg", "").Trim()); //they fucked up
        //        }


        //        if (TryParse.TryToParse(document, specialToUse))
        //        {
        //            var specialtyPts = document.QuerySelectorAll(specialToUse);
        //            var oneDay = specialtyPts.First().NextSibling.ChildNodes[0].ChildNodes[1].TextContent;
        //            OneDayPoints = Convert.ToInt32(oneDay);


        //            var GC = specialtyPts.First().NextSibling.ChildNodes[1].ChildNodes[1].TextContent;
        //            GCPoints = Convert.ToInt32(GC);

        //            var TT = specialtyPts.First().NextSibling.ChildNodes[2].ChildNodes[1].TextContent;
        //            TTPoints = Convert.ToInt32(TT);

        //            var Sprint = specialtyPts.First().NextSibling.ChildNodes[3].ChildNodes[1].TextContent;
        //            SprintPoints = Convert.ToInt32(Sprint);
        //        }
        //    }
        //    catch (Exception)
        //    {                
        //    }

        //}

        #endregion

    }


    public class RiderSeason
    {
        public int Year { get; set; }
        public Team Team { get; set; }
        public int Cost { get; set; }
        public int PointsScored { get; set; }
        public int OwnedByCount { get; set; }

        public List<PDC_Result> Results { get; set; }
        public RiderSeason()
        {
            Results = new List<PDC_Result>();
            Team = new Team(); 
        }

    }

    public class Team
    {
        public Team()
        {

        }
        public string Name { get; set; }
        public string Status { get; set; }
        public string PDC_URL { get; set; }
    }

    public class Nationality 
    {
        public Nationality()
        {
           
        }
        public string Name { get; set; }        
        public string PDC_URL { get; set; }        
    }

  


    public class PDCTeam : Entity
    {
        //[BsonElement("Id")]
        //public ObjectId Id { get; set; }
        public List<Rider> Riders { get; set; }
        public int TotalPointsScored { get; set; }
  

        public string PDC_ID { get; set; }

        public string ID { get; set; }

        public bool IsDraftPDCTeam { get; set; }
        
        public bool Is35Team { get; set; }

        public string PDCTeamName { get; set; }
        public string PDCTeamURL { get; set; }
        public int Rank { get; set; }

        #region stats
        //public double AveragePointsScored { get; set; }
        //public double AverageCostOfRiders { get; set; }

        //public double AveragePreviousPointsScored { get; set; }
        //public double AveragePreviousCostOfRiders { get; set; }

        //public double AverageRiderAge { get; set; }
        //public double AverageRiderHeight { get; set; }
        //public double AverageRiderWeight { get; set; }
        //public double AverageOneDayPoints { get; set; }
        //public double AverageGCPoints { get; set; }
        //public double AverageTTPoints { get; set; }
        //public double AverageSprintPoints { get; set; }
        

        //public double CostVariance { get; set; }

        //public double CostKurtosis { get; set; }  
        //public double CostSkew { get; set; }

        #endregion
        public List<string> MissingStatsForRiders { get; set; }
        public PDCTeam()
        {
            ID = Guid.NewGuid().ToString();
            Riders = new List<Rider>();
            MissingStatsForRiders = new List<string>();
            IsDraftPDCTeam = false; 
        }

        public PDCTeam(List<Rider> riders)
            : this()
        {
            Riders = riders;
            IsDraftPDCTeam = false;
        }
        public PDCTeam(PDCTeamYear team, bool? isDraftTeam)
            : this()
        {            
            IsDraftPDCTeam = isDraftTeam.HasValue ? isDraftTeam.Value : false;
            Is35Team = team.Is35Team;
            PDCTeamName = team.Name;
            

        }

        public void AddRider(Rider r)
        {
            //todo: calculate stats when a rider is added.
            Riders.Add(r);
            TotalPointsScored += r.CurrentYearPoints;

            #region old stuff
            //CostVariance = MathNet.Numerics.Statistics.ArrayStatistics.Variance(Riders.Select(x => x.CurrentYearCost).ToArray());


            //CostKurtosis = MathNet.Numerics.Statistics.Statistics.Kurtosis(Riders.Select(x => x.CurrentYearCost).ToArray());
            //CostSkew = MathNet.Numerics.Statistics.Statistics.Skewness(Riders.Select(x => x.CurrentYearCost).ToArray());

            //// MathNet.Numerics.Statistics.Histogram h = new MathNet.Numerics.Statistics.Histogram(Riders.Select(x => x.CurrentYearCost).ToArray(), 1);

            //AverageCostOfRiders = Riders.Average(x => x.CurrentYearCost);
            //AveragePointsScored = Riders.Average(x => x.CurrentYearPoints);

            //AveragePreviousCostOfRiders = Riders.Average(x => x.PreviousCost);
            //AveragePreviousPointsScored = Riders.Average(x => x.PreviousPoints);


            //AverageRiderAge = Riders.Average(x => x.Age);
            //AverageRiderHeight = Riders.Average(x => x.Height);
            //AverageRiderWeight = Riders.Average(x => x.Weight);


            ////from pro cycling stats
            //AverageOneDayPoints = Riders.Average(x => x.OneDayPoints);
            //AverageGCPoints = Riders.Average(x => x.GCPoints);
            //AverageTTPoints = Riders.Average(x => x.TTPoints);
            //AverageSprintPoints = Riders.Average(x => x.SprintPoints);
            #endregion

        }

        public void CalculatePoints(List<PDC_Result> results)
        {
            List<int> points = new List<int>();
            foreach (Rider r in Riders)
            {
                //int raceResults = results.SelectMany(q => q.RaceResults.Where(p => p.Rider_PDCID == r.PDC_RiderID)).Sum(g => g.Points);
                //points.Add(raceResults);
                //r.CurrentYearPoints = raceResults;

                r.CalculatePoints(results);
                points.Add(r.CurrentYearPoints);
                
            }
            TotalPointsScored = points.Sum(); 
        }

        public List<KeyValuePair<double, int>> GetCostFrequency()
        {
            List<KeyValuePair<double, int>> cf = new List<KeyValuePair<double, int>>();
            foreach (var grp in this.Riders.OrderBy(x => x.CurrentYearCost).GroupBy(i => i.CurrentYearCost))
            {
                cf.Add(new KeyValuePair<double, int>(grp.Key, grp.Count()));
                Console.WriteLine("{0} : {1}", grp.Key, grp.Count());
            }

            return cf;
        }
        //public double[][] ToVector()
        //{
        //    double[][] vector = new double[Riders.Count][];


        //    for (int x = 0; x < Riders.Count; x++)
        //    {
        //        vector[x] = Riders[x].ToVector();
        //    }

        //    return vector;

        //}

   
        public override string ToString()
        {
            string d = String.Empty;
            d = this.PDCTeamName;
            d += "\r\n__________________________\r\n\r\n";
            foreach (Rider r in Riders.OrderByDescending(x => x.CurrentYearPoints).ToList())
            {
                d += String.Format("{0}....{1}....{2}\r\n", r.Name, r.CurrentYearCost, r.CurrentYearPoints);
            }

            return d;
        }

       

    }



    public class PDC_AnnualData
    {
        public PDC_AnnualData()
        {

        }

        public int Year { get; set; }
        public int Cost { get; set; }
        public int PointsScored { get; set; }
    }

    public class PDCTeamPoints
    {
        public PDCTeamPoints()
        {

        }
        public PDCTeamPoints(string n, int p)
        {
            Name = n;
            Points = p;
        }

        public string Name { get; set; }
        public int Points { get; set; }
        public int RunningTotalPoints { get; set; }

    }


    public class PDC_Result : Entity
    {
        public PDC_Result()
        {
            RaceResults = new List<PDC_RaceResult>();
        }

        public string Month { get; set; }
        public string DayNum { get; set; }
        public string Country { get; set; }
        public string Category { get; set; }
        public string EventName { get; set; }
        public string RiderCount { get; set; }
        public string Points { get; set; }

        public string URL { get; set; }

        public List<PDC_RaceResult> RaceResults { get; set; }

        public PDCTeamPoints ComparePDCTeamToRace(PDCTeam t)
        {
            PDCTeamPoints tp = new PDCTeamPoints();

            foreach (PDC_RaceResult r in RaceResults)
            {
                //if a rider in the result exists in the PDCTeam, add up the points. 
                if (t.Riders.Exists(x => x.PDC_RiderID == r.Rider_PDCID))  //BETTER WAY TO COMPARE 
                //if (t.Riders.Exists(x => x.Name == r.Name))  ......OLD, BAD WAY OF CALCULATING!
                {
                    tp.Points += r.Points;
                }
            }
            return tp;
        }

        //public int ComparePDCTeamToRace(PDCTeam t)
        //{
        //    int total = 0;

        //    foreach (PDC_RaceResult r in RaceResults)
        //    {
        //        //if a rider in the result exists in the PDCTeam, add up the points. 
        //        if (t.Riders.Exists(x => x.Name == r.Name))
        //        {
        //            total += r.Points;
        //        }
        //    }
        //    return total;
        //}

        public string ToCSV()
        {
            string r = String.Empty;
            try {
                PropertyInfo[] properties = typeof(PDC_Result).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType.Name != typeof(List<PDC_RaceResult>).Name  && property.Name != "Id") //iterate the race result list after
                    {

                        r += property.GetValue(this);
                        r += ",";
                    }
                   
                }

                r += Environment.NewLine;
                foreach (PDC_RaceResult rr in RaceResults)
                {
                    r += rr.ToCSV();
                }
            }
            catch(Exception)
            {                
            }

            return r;
        }
    }

    public class PDC_RaceResult : Entity
    {
        public PDC_RaceResult()
        {

        }

        public string Place { get; set; }
        public string PDCTeam { get; set; }
        public string Name { get; set; }
        public string Rider_PDCID { get; set; }
        public int Points { get; set; }

        public override string ToString()
        {
            string s = String.Empty;

            s = String.Format("{0} {1} {2} {3}", this.Place, this.PDCTeam, this.Name, this.Points);
            return s; 
        }
        public string ToCSV()
        {
            string s = String.Empty;

            s = String.Format("{0},{1},{2},{3}", this.Place, this.PDCTeam, this.Name, this.Points);
            s += Environment.NewLine;
            return s;
        }


    }

    public class PDC_Event
    {
        public string PDC_ID { get; set; }
        public DateTime Date { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int StageCount { get; set; }
        public string ResultsURL { get; set; }
        public PDC_Event()
        {

        }

    }


    public class FantasyResult
    {
        public FantasyResult()
        {
            Race = new PDC_Result();
            Points = new List<PDCTeamPoints>();
        }
        public FantasyResult(PDC_Result r, List<PDCTeamPoints> p)
        {
            Race = r;
            Points = p; 
        }
        public PDC_Result Race { get; set; }
        public List<PDCTeamPoints> Points { get; set; }

        public int TempID { get; set; }
    }


    public class HallOfFameItem
    {
        public HallOfFameItem()
        {

        }

        public int Year { get; set; }
        public string PDCTeamName { get; set; }

        public int TotalPointsScored { get; set; }

        public PDCTeam PDCTeam { get; set; }
    }


    /// <summary>
    /// This will hold the race calendar, 
    /// A list of all the riders and their salary/points scored
    /// A list of all the results 
    /// for a given year
    /// </summary>
    public class PDC_Season : Entity
    {
        public PDC_Season()
        {
            Year = DateTime.Now.Year;
            DraftTeams = new List<PDCTeam>();
            IsDev = false;
        }
        
        public void Create(int year)
        {
            Year = year;
            DraftTeams = new List<PDCTeam>();
            //we should only need to do this one time to start off the season.  Teams, Riders, and Calendar wont change
            Riders = Parser.ParseAllRiders(Year);
            //this grabs all teams and their riders.
            PDCTeams = Parser.ParsePDCTeamList(Year);
            RaceCalendar = Parser.ParsePDCCalendar(Year);
            
        }
        public void CreatePreSeason(int year)
        {
            Year = year;
            DraftTeams = new List<PDCTeam>();
            //we should only need to do this one time to start off the season.  Teams, Riders, and Calendar wont change
            Riders = Parser.ParseAllRiders(Year);
            //this grabs all teams and their riders.         
            RaceCalendar = Parser.ParsePDCCalendar(Year);

        }
        public void UpdateResults()
        {
                        
            RaceResults = Parser.ParsePDCResults(Year);                                                            
            LastUpdated = DateTime.Now;
            
        }

        public void UpdateRiderPoints()
        {
            foreach(Rider r in Riders)
            {
                r.CalculatePoints(RaceResults);
            }
        }

        public void MatchRiderPhotos()
        {
            List<RiderPhoto> photos = Repository.RiderPhotoGetAll();

            if (photos != null & photos.Count() > 0)
            {
                FantasyYearConfig fyc = Repository.FantasyYearConfigGetDefault();

                PDC_Season season = Repository.PDCSeasonGet(fyc.Year);
                string match1 = String.Empty;
                string match2 = String.Empty;
                Dictionary<string, string> matches = new Dictionary<string, string>();
                foreach (Rider r in season.Riders)
                {
                    int currScore = Int32.MaxValue;
                    foreach (RiderPhoto p in photos)
                    {
                        string temp = p.Name.Replace("-", " ").Trim().ToLower();

                        if (String.IsNullOrEmpty(temp))
                            continue;

                        int score = FantasyCyclingParser.Helpers.LevenshteinDistance.Compute(r.Name.ToLower(), temp);

                        if (score < currScore)
                        {
                            r.Photo = p;
                            currScore = score;
                        }
                    }

                }

                Repository.PDCSeasonUpdate(season);
            }
            else
            {
                throw new Exception("No rider photos availabe in the DB");
            }
        }
        
        public int Year{ get; set; }
        public List<PDC_Result> RaceResults { get; set; }

        public List<Rider> Riders { get; set; }

        public List<PDCTeam> PDCTeams { get; set; }

        public List<PDCTeam> DraftTeams { get; set; }

        //public List<Team> Teams { get; set; }

        public List<PDC_Event> RaceCalendar { get; set; }

        public DateTime LastUpdated { get; set; }
        public bool IsDev { get; set; }
    }

    public class RiderPhoto : Entity
    {
        public RiderPhoto()
        {
            //todo: have a stand in image if none is available
        }
        public RiderPhoto(byte[] photo)
        {
            Image = photo; 
        }

        public string Name { get; set; }
        public string PCS_RiderURL { get; set; }
        public byte[] Image { get; set; }
    }

    public class DraftLogEntry : Entity
    {
        public DraftLogEntry(int round, int pickNum, string team, string riderName, string riderId)
        {
            ID = Guid.NewGuid().ToString(); 
            DraftTime = DateTime.Now.ToString();
            Year = DateTime.Now.Year; 

            DraftRound = round;
            PickNumber = pickNum;
            DraftingTeam = team;
            RiderDrafted = riderName;
            RiderID = riderId; 

        }

        public string ID { get; set; }
        public int Year { get; set; }
        public int DraftRound { get; set; }
        public int PickNumber { get; set; }
        public string DraftTime { get; set; }
        public string DraftingTeam { get; set; }
        public string RiderDrafted { get; set; }
        public string RiderID { get; set; }
    }

}
