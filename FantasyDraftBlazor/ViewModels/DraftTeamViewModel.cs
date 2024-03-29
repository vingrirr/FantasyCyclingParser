﻿using FantasyCyclingParser;

namespace FantasyDraftBlazor.ViewModels
{
    public class DraftTeamViewModel
    {
        public DraftTeamViewModel()
        {
            // Riders = new List<DraftRiderViewModel>();
        }
        public DraftTeamViewModel(PDCTeam team)
        {
            Model = team;
            team.Riders.RemoveAll(x => x == null);

            ID = team.ID;
            TeamName = team.PDCTeamName;
            CanUseOverride = true;
            IsUsingOverride = false;
            Calculate();
        }

        public void Calculate()
        {


            if (RiderToDraft != null)
            {
                TeamBudget = Model.Riders.Sum(x => x.CurrentYearCost) + RiderToDraft.CurrentYearCost;
                RiderCount = Model.Riders.Count() + 1;
                BudgetAvailable = 150 - TeamBudget;
                Rider24ptCount = RiderToDraft.CurrentYearCost >= 24 ? Model.Riders.Count(x => x.CurrentYearCost >= 24) + 1 : Model.Riders.Count(x => x.CurrentYearCost >= 24);
                Rider18ptCount = RiderToDraft.CurrentYearCost >= 18 ? Model.Riders.Count(x => x.CurrentYearCost >= 18) + 1 : Model.Riders.Count(x => x.CurrentYearCost >= 18);
            }
            else
            {
                TeamBudget = Model.Riders.Sum(x => x.CurrentYearCost);
                RiderCount = Model.Riders.Count();
                BudgetAvailable = 150 - TeamBudget;
                Rider24ptCount = Model.Riders.Count(x => x.CurrentYearCost >= 24);
                Rider18ptCount = Model.Riders.Count(x => x.CurrentYearCost >= 18);
            }



        }

        public bool Validate()
        {
            return (RiderCount <= 25 &&
                    TeamBudget >= 150 &&
                    Rider24ptCount <= 1 &&
                    Rider18ptCount <= 3
            );
        }
        public string ID { get; set; }

        public string TeamName { get; set; }
        public PDCTeam Model { get; set; }
        public Rider RiderToDraft { get; set; }

        public Rider RiderToAddBackToDraft { get; set; }

        public bool CanUseOverride { get; set; }
        public bool IsUsingOverride { get; set; }

        public int RiderCount { get; set; }

        public int TeamBudget { get; set; }
        public int BudgetAvailable { get; set; }

        public int Rider24ptCount { get; set; }

        public int Rider18ptCount { get; set; }

    }
}
