
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoRepository;

namespace FantasyCyclingParser
{
    public static class Repository
    {

        public static void FantasyYearConfigInsert(FantasyYearConfig config)
        {
            (new MongoRepository<FantasyYearConfig>()).Add(config);
        }

        public static List<FantasyYearConfig> FantasyYearConfigGet(string configID)
        {
            MongoRepository<FantasyYearConfig> fyConfig = new MongoRepository<FantasyYearConfig>();
            List<FantasyYearConfig> items = fyConfig.Where(x => x.Id == configID).ToList();

            return items;
        }

        public static FantasyYearConfig FantasyYearConfigGetDefault()
        {
            MongoRepository<FantasyYearConfig> fyConfig = new MongoRepository<FantasyYearConfig>();
            FantasyYearConfig item = fyConfig.Where(x => x.IsDefault == true).FirstOrDefault();

            return item;
        }

   

        //race results now stored and updated as part of the season
        //public static List<PDC_Result> RaceResultsAll()
        //{
        //    MongoRepository<PDC_Result> db = new MongoRepository<PDC_Result>();

        //    return db.ToList();
        //}

        public static PDC_Season PDCSeasonGet(int year)
        {
            MongoRepository<PDC_Season> season = new MongoRepository<PDC_Season>();
            PDC_Season item = season.Where(x => x.Year == year).FirstOrDefault();

            return item;
        }

        public static void PDCSeasonInsert(PDC_Season season)
        {
            (new MongoRepository<PDC_Season>()).Add(season);
        }
        public static void PDCSeasonUpdate(PDC_Season season)
        {
            (new MongoRepository<PDC_Season>()).Update(season);
        }

        public static void PDCSeasonDelete(int year)
        {
            MongoRepository<PDC_Season> db = new MongoRepository<PDC_Season>();
            PDC_Season season = db.Where(x => x.Year == year).FirstOrDefault();
            db.Delete(season);
        }    

        //public static void ParseSeasonToDB(int year)
        //{
        //    MongoRepository<PDC_Result> db = new MongoRepository<PDC_Result>();

        //    List<PDC_Result> results = Parser.ParsePDCResults(year);

        //    foreach (PDC_Result r in results)
        //    {
        //        db.Add(r);
        //    }
        //}

    }
}
