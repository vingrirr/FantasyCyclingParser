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
using MongoRepository;

namespace FantasyCyclingParser
{



    public class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {

           // Utilities.NationalityList["FRA"] = new Nationality({ Name="France", PDC_URL=})
            int year = 2022;

               MockWindowsService(); 

            //BuildSeason(year);
            // WorkerCode(); 

            //IterateSeason(year);
            int x = 0; 
            
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
            catch(Exception ex)
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






    public static class LevenshteinDistance
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }

    public static class Extensions
    {
        private static readonly ThreadLocal<Random> RandomThreadLocal =
            new ThreadLocal<Random>(() => new Random());

        public static void Shuffle<T>(this IList<T> list, int seed = -1)
        {

            var r = seed >= 0 ? new Random(seed) : RandomThreadLocal.Value;
            var len = list.Count;
            for (var i = len - 1; i >= 1; --i)
            {
                var j = r.Next(i);
                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }
        }

    }
}
