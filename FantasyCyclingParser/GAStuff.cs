using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;

namespace FantasyCyclingParser
{

    public class FantasyCyclingGAProblem 
    {
        
        public FantasyCyclingGAProblem()
        {

        }

        public IChromosome CreateChromosome()
        {
            return new PDCTeamChromosome(25);
        }
    }
    public class PDCTeamFitness : IFitness
    {
        List<Rider> _allRiders;
        public PDCTeamFitness()
        {
            _allRiders = new List<Rider>();
        }
        public PDCTeamFitness(List<Rider> riders)
        {
            _allRiders = riders;
        }

        public double Evaluate(IChromosome chromosome)
        {
            double score = 0;


            double budget = 0;
            int above24Count = 0;
            int above18Count = 0;
            List<int> ids = new List<int>();
            foreach(Gene g in chromosome.GetGenes())
            {
                int i = Convert.ToInt32(g.Value);
                Rider r = _allRiders[i];
                score += r.CurrentYearPoints;
                budget += r.CurrentYearCost;

                if (r.CurrentYearCost >= 24)
                    above24Count += 1;

                if (r.CurrentYearCost >= 18)
                    above18Count += 1;

                ids.Add(i);
            }


            if (budget > 150)
                score = Int32.MinValue;

            if (above24Count > 1)
                score = Int32.MinValue;

            if (above18Count > 3)
                score = Int32.MinValue;


            //not sure if this is the best way to handle this?  could lose good chormos due to crossover or mutation??

            var q = ids.GroupBy(x => x)
            .Select(g => new { Value = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count);
            if (q.Any(x => x.Count > 1))
            {
                score = Int32.MinValue;
            }
            //

            return score;
        }
    }

    public class PDCTeamChromosome : ChromosomeBase
    {
        int[] uindexes = null;
        int _length = 0;
        List<Rider> _riders;
        public PDCTeamChromosome(int length)
            : base(length)
        {
            _length = length;
           

            uindexes = RandomizationProvider.Current.GetUniqueInts(25, 0, 341);
            for (int i = 0; i < length; i++)
            {
                ReplaceGene(i, new Gene(uindexes[i]));
            }
        }

        public override Gene GenerateGene(int geneIndex)
        {
            int i = RandomizationProvider.Current.GetInt(0, 341);
            return new Gene(i);
        }

        public override IChromosome CreateNew()
        {
            return new PDCTeamChromosome(_length);
        }
    }
}
