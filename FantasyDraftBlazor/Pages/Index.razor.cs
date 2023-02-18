using FantasyCyclingParser;
using FantasyDraftBlazor.ViewModels;
using Microsoft.AspNetCore.Http;

namespace FantasyDraftBlazor.Pages
{
    public partial class Index
    {
        protected override void OnInitialized()
        {             
            Config = Repository.FantasyYearConfigGetDefaultDraft();
            Season = Repository.PDCSeasonGet(Config.Year);

            DraftRound = 1;
            PickNumber = 1;


            List<int> distinctPoints = new List<int>();
            distinctPoints.Add(1);
            distinctPoints.Add(2);
            distinctPoints.Add(4);
            distinctPoints.Add(6);

            //for testing only...
            //Combinations = new PointCombinations(distinctPoints, 14, 4);


            if (Season.DraftTeams.Count() == 0)
            {
                //build initial draft teams
                foreach (PDCTeamYear t in Config.TeamUIDS)
                {
                    PDCTeam temp = new PDCTeam(t, true);
                    Season.DraftTeams.Add(temp);                    
                }
                Repository.PDCSeasonUpdate(Season);
            }
            
            DraftTeams = new List<DraftTeamViewModel>();

                                    
            BuildSnakeDraft();

            #region for testing only - clear all riders 

            //foreach (PDCTeam t in Season.DraftTeams)
            //{
            //    t.Riders.Clear();
            //}

            //Repository.PDCSeasonUpdate(Season);

            #endregion

            DraftPick currPick = Draft.DraftOrder[0];
            DraftPick nextPick = Draft.DraftOrder[1];
            CurrentTeam = DraftTeams.Where(x => x.ID == currPick.ID).First();
            NextTeam = DraftTeams.Where(x => x.ID == nextPick.ID).First();

            //note: Must load available riders after draft teams have been made so we filter out
            //any already selected riders
            LoadAvailableRiders();
            Visibility = "visible";

        }



        void HandleStatusUpdated(Rider updatedRider)
        {
            AvailableRiders.Remove(updatedRider);
        }
        void HandleRiderUndo(Rider addRider)
        {
            AvailableRiders.Add(addRider);
        }
        void HandleSaveChanges(DraftTeamViewModel team)
        {

            try
            {
                // everytime we add a rider, we save the entire updated draft team to the season
                if (Season.DraftTeams.Where(x => x.ID == team.ID).Count() > 0)
                {
                    PDCTeam temp = Season.DraftTeams.First(x => x.ID == team.ID);
                    Season.DraftTeams.Remove(temp);

                    Season.DraftTeams.Add(team.Model);
                }
                else
                {
                    Season.DraftTeams.Add(team.Model);
                }

                Repository.PDCSeasonUpdate(Season);

                if (team.RiderToDraft != null)
                {
                    DraftLogEntry log = new DraftLogEntry(DraftRound, PickNumber, team.TeamName, team.RiderToDraft.Name, team.RiderToDraft.PDC_RiderID);
                    Repository.DraftLogInsert(log);
                }

                team.RiderToDraft = null;
                
                Draft.DraftOrder.RemoveAt(0); //basically this is pop

                if (Draft.DraftOrder.Count() > 1) 
                { 
                    
                    DraftPick currPick = Draft.DraftOrder[0];
                    DraftPick nextPick = Draft.DraftOrder[1];
                    CurrentTeam = DraftTeams.Where(x => x.ID == currPick.ID).First();
                    NextTeam = DraftTeams.Where(x => x.ID == nextPick.ID).First();

                    PickNumber++;

                    if (((PickNumber - 1) % 6) == 0)
                        DraftRound++;
                }
                else if (Draft.DraftOrder.Count() == 1)
                {
                    DraftPick currPick = Draft.DraftOrder[0];
                    
                    CurrentTeam = DraftTeams.Where(x => x.ID == currPick.ID).First();
                    NextTeam = null;
                }
                else
                {
                    Visibility = "hidden";
                }

                if (DraftRound >= 20)
                {
                    List<Rider> distinctRiders = AvailableRiders.DistinctBy(x => x.CurrentYearCost).ToList();
                    List<int> distinctPoints = new List<int>();
                    foreach (Rider r in distinctRiders.OrderByDescending(y => y.CurrentYearCost).ToList())
                    {
                        distinctPoints.Add(r.CurrentYearCost);
                    }

                    Combinations = new PointCombinations(distinctPoints, CurrentTeam.BudgetAvailable, (25 - CurrentTeam.RiderCount));
                }

                
            }
            catch(Exception ex)
            {
                DraftLogEntry err = new DraftLogEntry(0,0,"", ex.Message, ex.StackTrace);
                Repository.DraftLogInsert(err);

            }
        }

        void HandleRemoveExistingRider(DraftTeamViewModel team)
        {
            try
            {
                // everytime we add a rider, we save the entire updated draft team to the season
                if (Season.DraftTeams.Where(x => x.ID == team.ID).Count() > 0)
                {
                    PDCTeam temp = Season.DraftTeams.First(x => x.ID == team.ID);
                    Season.DraftTeams.Remove(temp);

                    Season.DraftTeams.Add(team.Model);
                }
                else
                {
                    Season.DraftTeams.Add(team.Model);
                }
                Repository.PDCSeasonUpdate(Season);

                AvailableRiders.Add(team.RiderToAddBackToDraft);

                
                //todo: add "action" to the draft log then log the removal of rider
                
                //DraftLogEntry log = new DraftLogEntry(DraftRound, PickNumber, team.TeamName, team.RiderToDraft.Name, team.RiderToDraft.PDC_RiderID);
                //Repository.DraftLogInsert(log);

            }
            catch (Exception ex)
            {
                DraftLogEntry err = new DraftLogEntry(0, 0, "", ex.Message, ex.StackTrace);
                Repository.DraftLogInsert(err);

            }
        }

        void HandleTimerUpdated(int timer)
        {
           // CurrentTeam = DanasTeam;
            
        }

        private void BuildSnakeDraft()
        {
            //Dana - fc2e7a01-3a31-4aa2-bdcc-1203933932bc
            //Allen - 3ab287a5-5a34-4dda-9203-a6bc2404ee15
            //Alex - 7b8e450c-1079-4cc6-bc7a-42479657799d
            //Tim - 0b90f656-e1f0-4a9b-af34-5724f126a13b
            //Ryan - c9c8d30e-6264-4455-b60a-d50b7bac983c
            //Bill - 1ebb9ae7-0467-4522-b4dc-fe7fc7803806

            PDCTeam dana = Season.DraftTeams.Where(x => x.ID == "fc2e7a01-3a31-4aa2-bdcc-1203933932bc").First();
        //    PDCTeam allen = Season.DraftTeams.Where(x => x.ID == "3ab287a5-5a34-4dda-9203-a6bc2404ee15").First();
          //  PDCTeam alex = Season.DraftTeams.Where(x => x.ID == "7b8e450c-1079-4cc6-bc7a-42479657799d").First();
            PDCTeam tim = Season.DraftTeams.Where(x => x.ID == "0b90f656-e1f0-4a9b-af34-5724f126a13b").First();
            PDCTeam ryan = Season.DraftTeams.Where(x => x.ID == "c9c8d30e-6264-4455-b60a-d50b7bac983c").First();
            //PDCTeam bill = Season.DraftTeams.Where(x => x.ID == "1ebb9ae7-0467-4522-b4dc-fe7fc7803806").First();


            List<PDCTeam> initialDraftOrder = new List<PDCTeam>();
            initialDraftOrder.Add(dana);
            //initialDraftOrder.Add(allen);
            //initialDraftOrder.Add(alex);
            initialDraftOrder.Add(tim);
            initialDraftOrder.Add(ryan);
            //initialDraftOrder.Add(bill);

            Draft = new SnakeDraft(initialDraftOrder, 25);

            DraftTeamViewModel d = new DraftTeamViewModel(dana);
            DraftTeams.Add(d);

            //DraftTeamViewModel al = new DraftTeamViewModel(allen);
            //DraftTeams.Add(al);

            //DraftTeamViewModel ax = new DraftTeamViewModel(alex);
            //DraftTeams.Add(ax);

            DraftTeamViewModel t = new DraftTeamViewModel(tim);
            DraftTeams.Add(t);

            DraftTeamViewModel r = new DraftTeamViewModel(ryan);
            DraftTeams.Add(r);

            //DraftTeamViewModel b = new DraftTeamViewModel(bill);
            //DraftTeams.Add(b);

        }

        private void LoadAvailableRiders()
        {
            AvailableRiders = new List<Rider>(Season.Riders);

            //remove any riders which may have already been drafted
            foreach (DraftTeamViewModel team in DraftTeams)
            {
                foreach (Rider r in team.Model.Riders)
                {
                    Rider temp = AvailableRiders.Where(x => x.PDC_RiderID == r.PDC_RiderID).First();
                    AvailableRiders.Remove(temp);
                }
            }
        }

        public FantasyYearConfig Config { get; set; }

        public PDC_Season Season { get; set; }

        public List<Rider> AvailableRiders { get; set; }

        public List<DraftTeamViewModel> DraftTeams { get; set; }
        public DraftTeamViewModel CurrentTeam { get; set; }
        public DraftTeamViewModel NextTeam { get; set; }
        public SnakeDraft Draft { get; set; }

        public PointCombinations Combinations { get; set; }
        public int DraftRound { get; set; }
        public int PickNumber { get; set; }

        public string Visibility { get; set; }
    }
}
