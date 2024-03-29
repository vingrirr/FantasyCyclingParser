﻿using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasyCyclingParser
{



    public class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

            TestRegularDraft();

            //    PDC_Season s = Repository.PDCSeasonGet(2023);
            //PDCTeam dana = s.DraftTeams.Where(d => d.ID == "fc2e7a01-3a31-4aa2-bdcc-1203933932bc").First();
            //PDCTeam tim = s.DraftTeams.Where(d => d.ID == "0b90f656-e1f0-4a9b-af34-5724f126a13b").First();
            //PDCTeam ryan = s.DraftTeams.Where(d => d.ID == "c9c8d30e-6264-4455-b60a-d50b7bac983c").First();

            //int dSum = dana.Riders.Sum(k => k.CurrentYearCost);
            //int tSum = tim.Riders.Sum(k => k.CurrentYearCost);
            //int rSum = ryan.Riders.Sum(k => k.CurrentYearCost);

            //int z = 0;

            #region remove a rider from a draft team
            //PDC_Season s = Repository.PDCSeasonGet(2023);

            //PDCTeam t = s.DraftTeams.Where(d => d.ID == "fc2e7a01-3a31-4aa2-bdcc-1203933932bc").First();

            //Rider temp = t.Riders.Where(r => r.PDC_RiderID == "3975").First();

            //t.Riders.Remove(temp);

            //Repository.PDCSeasonUpdate(s);

            //int v = 0;
            #endregion;

            //3975
            #region copy draft teams from pre-season to season
            //PDC_Season draft = Repository.PDCSeasonGetById("63dc076c479bd474f069fa63");
            //PDC_Season curr = Repository.PDCSeasonGetById("63f37daf479bd479dcaa6837");

            //curr.DraftTeams = draft.DraftTeams;

            //Repository.PDCSeasonUpdate(curr);

            //int g = 0;

            #endregion

            #region create new Fantasy Config by Cloning existing one

            //FantasyYearConfig prev = Repository.FantasyYearConfigGet("621273f698d7278314b8dda7").First();
            //prev.IsDefault = false; 



            //FantasyYearConfig curr = new FantasyYearConfig(2023);
            //curr.ConfigName = "The Rise of ChatGPT_PDC";
            //curr.TeamUIDS = prev.TeamUIDS;
            //curr.IsDefault = true;
            //curr.IsDraft = false;
            //curr.URLToAddPDCTeam = "";


            //Repository.FantasyYearConfigInsert(curr);
            //Repository.FantasyYearConfigUpdate(prev);

            //int c = 0; 

            #endregion

            //FantasyYearConfig config = Repository.FantasyYearConfigGetDefaultDraft();

            #region build a draft fantasy config
            //FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();


            //PDCTeamYear ryan = new PDCTeamYear();
            //ryan.Year = 2023;
            //ryan.Name = "Ryan";
            //ryan.TeamUID = "99901";
            //ryan.Is35Team = false;


            //PDCTeamYear dana = new PDCTeamYear();
            //dana.Year = 2023;
            //dana.Name = "Dana";
            //dana.TeamUID = "99902";
            //dana.Is35Team = false;

            //PDCTeamYear tim = new PDCTeamYear();
            //tim.Year = 2023;
            //tim.Name = "Tim";
            //tim.TeamUID = "99903";
            //tim.Is35Team = false;


            //PDCTeamYear alex = new PDCTeamYear();
            //alex.Year = 2023;
            //alex.Name = "Alex";
            //alex.TeamUID = "99904";
            //alex.Is35Team = false;

            //PDCTeamYear allen = new PDCTeamYear();
            //allen.Year = 2023;
            //allen.Name = "Allen";
            //allen.TeamUID = "99905";
            //allen.Is35Team = false;

            //PDCTeamYear bill = new PDCTeamYear();
            //bill.Year = 2023;
            //bill.Name = "Bill";
            //bill.TeamUID = "99906";
            //bill.Is35Team = false;

            //FantasyYearConfig vm = new FantasyYearConfig();

            //vm.ConfigName = "First Ever Draft Season 2023!";
            //vm.TeamUIDS.Add(ryan);
            //vm.TeamUIDS.Add(dana);
            //vm.TeamUIDS.Add(tim);
            //vm.TeamUIDS.Add(alex);
            //vm.TeamUIDS.Add(allen);
            //vm.TeamUIDS.Add(bill);

            //vm.Year = 2023;
            //vm.IsDraft = true;
            //vm.IsDefault = true; 

            ////Repository.FantasyYearConfigInsert(vm);
            #endregion


            #region snake draft
            //List<string> teams = new List<string>();
            //teams.Add("Ryan");
            //teams.Add("Dana");
            //teams.Add("Tim");
            //teams.Add("Alex");
            //teams.Add("Allen");
            //teams.Add("Bill");

            //SnakeDraft draft = new SnakeDraft(teams, 25);

            #endregion


            #region rider photo stuff

            //Do the matching
            //List<RiderPhoto> photoList = Parser.ParsePCSRiderPhotos();

            //foreach (RiderPhoto p in photoList)
            //{
            //    Repository.RiderPhotoInsert(p);
            //} 

            //List<RiderPhoto> photos = Repository.RiderPhotoGetAll();

            //FantasyYearConfig fyc = Repository.FantasyYearConfigGetDefault();

            //PDC_Season season = Repository.PDCSeasonGet(fyc.Year);
            //string match1 = String.Empty;
            //string match2 = String.Empty;
            //Dictionary<string, string> matches = new Dictionary<string, string>();
            //foreach (Rider r in season.Riders)
            //{
            //    int currScore = Int32.MaxValue;
            //    foreach (RiderPhoto p in photos)
            //    {
            //        string temp = p.Name.Replace("-", " ").Trim().ToLower();

            //        if (String.IsNullOrEmpty(temp))
            //            continue;

            //        int score = FantasyCyclingParser.Helpers.LevenshteinDistance.Compute(r.Name.ToLower(), temp);

            //        if (score < currScore)
            //        {
            //            r.Photo = p; 
            //            currScore = score;
            //        }
            //    }

            //}

            //Repository.PDCSeasonUpdate(season);
            #endregion

            #region photo find nulls
            //List<RiderPhoto> photos = Repository.RiderPhotoGetAll();
            //FantasyYearConfig fyc = Repository.FantasyYearConfigGetDefault();
            //PDC_Season season = Repository.PDCSeasonGet(fyc.Year);
            //int count = 0; 
            //foreach (Rider r in season.Riders)
            //{
            //  if (r.Photo == null || r.Photo.Image == null) 
            //  {
            //        Console.WriteLine(r.Name);
            //        count++; 
            //  }

            //}
            //Console.WriteLine("Count: " + count);
            #endregion

            //BuildSeason(2010);
            //BuildSeason(2011);
            //BuildSeason(2012);
            //BuildSeason(2013);
            //BuildSeason(2014);
            //BuildSeason(2015);
            //BuildSeason(2016);
            //BuildSeason(2017);
            //BuildSeason(2018);
            //BuildSeason(2019);
            //BuildSeason(2020);

            //BuildPreSeason(2023);
            //BuildSeason(2023);


            // Utilities.NationalityList["FRA"] = new Nationality({ Name="France", PDC_URL=})
            //int year = 2022;

            //MockWindowsService(); 


            // WorkerCode(); 

            //IterateSeason(year);

            int x = 0;

        }


        static void TestRegularDraft()
        {

            FantasyYearConfig Config = Repository.FantasyYearConfigGetDefaultDraft();
            PDC_Season Season = Repository.PDCSeasonGet(Config.Year);

            PDCTeam dana = Season.DraftTeams.Where(x => x.ID == "fc2e7a01-3a31-4aa2-bdcc-1203933932bc").First();
            PDCTeam allen = Season.DraftTeams.Where(x => x.ID == "3ab287a5-5a34-4dda-9203-a6bc2404ee15").First();
            PDCTeam alex = Season.DraftTeams.Where(x => x.ID == "7b8e450c-1079-4cc6-bc7a-42479657799d").First();
            PDCTeam tim = Season.DraftTeams.Where(x => x.ID == "0b90f656-e1f0-4a9b-af34-5724f126a13b").First();
            PDCTeam ryan = Season.DraftTeams.Where(x => x.ID == "c9c8d30e-6264-4455-b60a-d50b7bac983c").First();
            PDCTeam bill = Season.DraftTeams.Where(x => x.ID == "1ebb9ae7-0467-4522-b4dc-fe7fc7803806").First();

            List<PDCTeam> initialDraftOrder = new List<PDCTeam>();
            initialDraftOrder.Add(dana);
            initialDraftOrder.Add(allen);
            initialDraftOrder.Add(alex);
            initialDraftOrder.Add(tim);
            initialDraftOrder.Add(ryan);
            initialDraftOrder.Add(bill);

            //Draft = new SnakeDraft(initialDraftOrder, 25);

            var myDraft = new StandardDraft(initialDraftOrder, 25);
            int t = 0;
        }




        static void IterateSeason(int year)
        {
            List<PDCTeamPoints> PDCTeamData = new List<PDCTeamPoints>();
            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            PDC_Season season = Repository.PDCSeasonGet(year);

            List<PDC_Result> results = season.RaceResults;

            List<PDCTeam> configTeams = new List<PDCTeam>();
            foreach (PDCTeamYear ty in config.TeamUIDS)
            {

                PDCTeam team = season.PDCTeams.FirstOrDefault(m => m.PDC_ID == ty.TeamUID);
                configTeams.Add(team);
            }
            List<int> points = new List<int>();

            foreach (PDCTeam t in configTeams)
            {

                foreach (Rider r in t.Riders)
                {
                    //these are all the races they scored points in...but need to figure out how much they actually scored in each one...
                    //List<PDC_Result> resultsForRider = (from res in season.RaceResults
                    //                                    where res.RaceResults.Any(p => p.Rider_PDCID == r.PDC_RiderID)
                    //                                    select res).ToList();

                    int raceResults = season.RaceResults.SelectMany(q => q.RaceResults.Where(p => p.Rider_PDCID == r.PDC_RiderID)).Sum(g => g.Points);
                    points.Add(raceResults);
                }
                PDCTeamPoints ptp = new PDCTeamPoints(t.PDCTeamName, points.Sum());
                PDCTeamData.Add(ptp);
                points.Clear();
            }

            int x = 0;
        }

        static void BuildSeason(int year)
        {
            PDC_Season season = new PDC_Season();
            season.Create(year);
            season.UpdateResults();
            season.LastUpdated = DateTime.Now;
            Repository.PDCSeasonInsert(season);

        }

        /// <summary>
        /// Builds a season but without any race results which dont exist in the pre season
        /// </summary>
        /// <param name="year"></param>
        static void BuildPreSeason(int year)
        {
            PDC_Season season = new PDC_Season();
            season.CreatePreSeason(year);
            season.LastUpdated = DateTime.Now;
            Repository.PDCSeasonInsert(season);

        }

        static void MockWindowsService()
        {
            try
            {
                FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
                PDC_Season season = Repository.PDCSeasonGet(config.Year);
                season.UpdateResults();

                Repository.PDCSeasonUpdate(season);
            }
            catch (Exception ex)
            {
                int x = 0;
            }
        }
        static void FindOptimalPDCTeam(List<Rider> riderList)
        {

            //NOTE: WHEN HE COMES OUT WITH NEW COST OF EACH RIDER, USE THIS TO FIND SUBSTITUTIONS?
            //SIMILAR SCORING RIDERS ALL MOVE UP THE SAME?
            //WHAT ABOUT FINDING PEOPLE HE MAY HAVE MISSED?


            //var selection = new EliteSelection();
            var selection = new TournamentSelection();

            var problem = new FantasyCyclingGAProblem();
            var fitness = new PDCTeamFitness(riderList);

            var crossover = new UniformCrossover();
            //var crossover = new OrderedCrossover();
            //                var mutation = new ReverseSequenceMutation();
            var mutation = new UniformMutation(true);



            var population = new Population(100, 500, problem.CreateChromosome());

            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            //            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
            ga.MutationProbability = 0.4f;

            //ga.Termination = new GenerationNumberTermination(10000);

            //ga.TaskExecutor = new SmartThreadPoolTaskExecutor()
            //{
            //    MinThreads = 25,
            //    MaxThreads = 150
            //};

            //ga.Termination = new FitnessStagnationTermination(500);            
            ga.Termination = new FitnessThresholdTermination(22500);
            //ga.Termination = new OrTermination(new FitnessThresholdTermination(28000), new TimeEvolvingTermination(TimeSpan.FromMinutes(5)));
            //TimeSpan ts = new TimeSpan(0, 0, 30);
            //  ga.Termination = new TimeEvolvingTermination(ts); //done after 30s


            //stopwatch.Stop();
            //Console.WriteLine("Setup For GA Stopwatch Time Elapsed: " + stopwatch.Elapsed);
            //Console.ReadLine();


            ga.TerminationReached += delegate
            {
                double score = 0;
                double budget = 0;
                int above24 = 0;
                int above18 = 0;
                int riderCount = 0;
                var bestChromosome = ga.Population.BestChromosome;

                Console.WriteLine("Generations: {0}", ga.Population.GenerationsNumber);


                Console.WriteLine("Optimal PDCTeam is: ");

                foreach (Gene g in bestChromosome.GetGenes())
                {
                    int i = Convert.ToInt32(g.Value);
                    Rider r = riderList[i];
                    Console.WriteLine(String.Format("Name: {0}, Cost: {1}, Points: {2}", r.Name, r.CurrentYearCost, r.CurrentYearPoints));

                    riderCount += 1;
                    score += r.CurrentYearPoints;
                    budget += r.CurrentYearCost;

                    if (r.CurrentYearCost >= 24)
                        above24 += 1;

                    if (r.CurrentYearCost >= 18)
                        above18 += 1;
                }


                Console.WriteLine("");
                Console.WriteLine("Rider Count: {0}", riderCount);
                Console.WriteLine("Points Scored: {0,10}", bestChromosome.Fitness);
                Console.WriteLine("Budget: {0}", budget);
                Console.WriteLine("Number Above 24: {0}", above24);
                Console.WriteLine("Number Above 18: {0}", above18);


            };


            try
            {
                ga.Start();
            }
            catch (Exception)
            {
            }

            Console.ReadLine();

        }




        //FlightParser fp = new FlightParser();
        //List<Flight> flightList =  fp.Parse("A");

        //using (StreamWriter writer = new StreamWriter("BNA_Arrivals" + ".json"))
        //{
        //    foreach (Flight f in flightList)
        //    {
        //        writer.WriteLine(f.ToJSON());
        //    }
        //}

        //int x = 0; 






        //injured riders and other stats - http://www.procyclingstats.com/statistics.php?c=1&stat_id=224

        //Christopher Lathan of PDCTeam Wiggins, only 22 years old, finished in the top ten once again.


        //Parser.ParsePDCTeam();

        //List<string> urls = ParsePCSRiders();

        //using (StreamWriter writer = new StreamWriter("PCSRiderURLs.txt"))
        //{

        //    foreach(string u in urls)                
        //        writer.WriteLine(u);                
        //}

        //ClusteringKMeans.KMeans.Cluster

        //MongoTest();

        //ParsePDCTeam();





    }








}
