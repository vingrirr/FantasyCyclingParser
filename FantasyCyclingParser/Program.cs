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

namespace FantasyCyclingParser
{



    public class Program
    {

        static void Main(string[] args)
        {


            //Rider r = new Rider();

            //FantasyYearConfig config = new FantasyYearConfig();


            //config.ConfigName = "Wout van der Poel you talkin' about?";
            //config.Year = 2019;
            //config.IsDefault = true;
            //config.TeamUIDS.Add(new TeamYear("2483"));
            //config.TeamUIDS.Add(new TeamYear("2534"));
            //config.TeamUIDS.Add(new TeamYear("1881"));
            //config.TeamUIDS.Add(new TeamYear("1191"));
            //config.TeamUIDS.Add(new TeamYear("37"));
            //config.TeamUIDS.Add(new TeamYear("2176"));
            //config.TeamUIDS.Add(new TeamYear("2211"));
            //config.TeamUIDS.Add(new TeamYear("2216"));
            //config.TeamUIDS.Add(new TeamYear("2843"));
            //config.TeamUIDS.Add(new TeamYear("2059"));



            //Repository.FantasyYearConfigInsert(config);
            //needs to a team name or URL as parameter...needs to at least return their current score. 

            //Parser.ParseTeam("2534");
            //Parser.GetTeamPoints(1882);


            //List<int> TeamUIDs = new List<int>();



            //Team Krtecek_35 = Parser.ParseTeam("2211");

            //double fuglsangPts = Krtecek_35.Riders.Find(x => x.Name == "Jakob Fuglsang").CurrentYearPoints;

            //int year = DateTime.Now.Year;
            //try {

            //    MongoRepository<PDC_Result> db = new MongoRepository<PDC_Result>();

            int year = 2021;
            List<PDC_Result> results = Parser.ParsePDCResults(year);


            FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
            List<Team> Teams = new List<Team>();
            foreach (TeamYear ty in config.TeamUIDS)
            {
                Team t = Parser.ParseTeam(ty.TeamUID, ty.Year);
                Teams.Add(t);
            }


            //    List<PDC_Result> results = db.ToList();
            //Team KamnaChameleon = Parser.ParseTeam("2483", year);
            //Team TheBauhausMovement = Parser.ParseTeam("2534", year);
            //Team Plaidstockings = Parser.ParseTeam("1881", year);
            //Team Rubicon = Parser.ParseTeam("1191", year);
            //Team Zauzage = Parser.ParseTeam("37", year);
            //Team Cowboys = Parser.ParseTeam("1882", year);
            //int sumPoints = 0;
            //using (StreamWriter writer =
            //          new StreamWriter("2018_FantasyCyclingResults.csv"))
            //{

            List<FantasyResult> fr = new List<FantasyResult>();
            int sumPoints = 0; 

            foreach (PDC_Result r in results)
            {

                FantasyResult f = new FantasyResult();

                f.Race = r;

                foreach (Team tm in Teams)
                {
                    TeamPoints t = r.CompareTeamToRace(tm);
                    t.Name = tm.PDCTeamName;
                    f.Points.Add(t);

                    fr.Add(f);
                    sumPoints += t.Points;

                }
            }

            int x = 0;
            //t = r.CompareTeamToRace(TheBauhausMovement);
            //t.Name = "BauhauseMovement";
            //f.Points.Add(t);

            //t = r.CompareTeamToRace(Plaidstockings);
            //t.Name = "Plaidstockings";
            //f.Points.Add(t);

            //t = r.CompareTeamToRace(Rubicon);
            //t.Name = "Rubicon";
            //f.Points.Add(t);

            //t = r.CompareTeamToRace(Zauzage);
            //t.Name = "Zauzage";
            //f.Points.Add(t);

            //t = r.CompareTeamToRace(Cowboys);
            //t.Name = "Cowboys";
            //f.Points.Add(t);


            //db.Add(r);

            //Console.WriteLine(r.ToCSV());
            //Console.WriteLine("\r\n");

            //writer.WriteLine(r.ToCSV());

            //}



            //MongoRepository<Rider> db = new MongoRepository<Rider>();

            //List<Rider> list = Parser.ParseAllRiders(true);
            //Team t = new Team();

            //foreach (Rider r in list)
            //{
            //    db.Add(r);
            //    t.AddRider(r);
            //}

            //t.ToCSVFile("PDCRiderList2016_2");


            ////"5810e76ec0f64e44a842431d"

            //Rider rd = db.GetById("5810e76ec0f64e44a842431d");
            //rd.Name = "Peter Sagan";

            //db.Update(rd);


            //List<Rider> list = db.ToList();





            //t.ToCSVFile("PDCRiders2016_2");



            //var r = db.Where(x => x.CurrentYearPoints > 100 && x.Age == 26).ToList();
            int z = 0;

                //foreach (Rider r in list)
                //{
                //    db.Add(r);
                //}
            //}
            //catch(Exception ex)
            //{
              //  int z = 0; 
            //}
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            //FindOptimalTeam(list);
            //stopwatch.Stop();


            //Console.WriteLine("Main Stopwatch Time Elapsed: " + stopwatch.Elapsed);
            //Console.ReadLine();
            //ParseTeamList();

        }

        //public static void MongoTest()
        //{

        //    try {

        //        MongoRepository<Customer> customerrepo = new MongoRepository<Customer>();

        //        customerrepo.DeleteAll();

        //        var john = new Customer() { FirstName = "John", LastName = "Doe" };
        //        var jane = new Customer() { FirstName = "Jane", LastName = "Doe" };
        //        var jerry = new Customer() { FirstName = "Jerry", LastName = "Maguire" };
        //        customerrepo.Add(new[] { john, jane, jerry });


        //        john.FirstName = "Johnny";  //John prefers Johnny
        //        customerrepo.Update(john);

        //        jane.LastName = "Maguire";  //Jane divorces John and marries Jerry
        //        customerrepo.Update(jane);

        //        //Delete customers
        //        customerrepo.Delete(jerry.Id);  //Jerry passes away

        //        //Add some products to John and Jane
        //        john.Products.AddRange(new[] {
        //        new Product() { Name = "Fony DVD Player XY1299", Price = 35.99M },
        //        new Product() { Name = "Big Smile Toothpaste", Price = 1.99M }
        //});
        //        jane.Products.Add(new Product() { Name = "Life Insurance", Price = 2500 });
        //        customerrepo.Update(john);
        //        customerrepo.Update(jane);
        //        //Or, alternatively: customerrepo.Update(new [] { john, jane });

               

        //        //Finally; demonstrate GetById and First
        //        var mysterycustomer1 = customerrepo.GetById(john.Id);
        //        Customer mysterycustomer2 = customerrepo.First(c => c.FirstName == "Jane");

        //        Console.WriteLine("Mystery customer 1: {0} (having {1} products)",
        //                mysterycustomer1.FirstName, mysterycustomer1.Products.Count);
        //        Console.WriteLine("Mystery customer 2: {0} (having {1} products)",
        //                mysterycustomer2.FirstName, mysterycustomer2.Products.Count);

        //        //Delete all customers
        //        //customerrepo.DeleteAll();

        //        //Halt for user
        //        Console.WriteLine("Press any key...");
        //        Console.ReadKey();
                
                
        //         int x = 0;
        //    }
        //    catch(Exception ex) {
        //        int z = 0;
        //    }
        //    //var server = MongoServer.Create(ConnectionString);
        //    //var blog = server.GetDatabase("blog");


        //}

        
        static void FindOptimalTeam(List<Rider> riderList)
        {

            //NOTE: WHEN HE COMES OUT WITH NEW COST OF EACH RIDER, USE THIS TO FIND SUBSTITUTIONS?
            //SIMILAR SCORING RIDERS ALL MOVE UP THE SAME?
            //WHAT ABOUT FINDING PEOPLE HE MAY HAVE MISSED?


            //var selection = new EliteSelection();
            var selection = new TournamentSelection();

            var problem = new FantasyCyclingGAProblem();
            var fitness = new TeamFitness(riderList);

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


                Console.WriteLine("Optimal Team is: ");

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
            catch (Exception ex)
            {
                int z = 0;
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

        //Christopher Lathan of Team Wiggins, only 22 years old, finished in the top ten once again.


        //Parser.ParseTeam();

        //List<string> urls = ParsePCSRiders();

        //using (StreamWriter writer = new StreamWriter("PCSRiderURLs.txt"))
        //{

        //    foreach(string u in urls)                
        //        writer.WriteLine(u);                
        //}

        //ClusteringKMeans.KMeans.Cluster

        //MongoTest();

        //ParseTeam();





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
