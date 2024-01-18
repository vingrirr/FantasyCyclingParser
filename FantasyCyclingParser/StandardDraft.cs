using System.Collections.Generic;

namespace FantasyCyclingParser
{
    public class StandardDraft
    {
        public StandardDraft(List<PDCTeam> teams, int numRounds)
        {
            DraftOrder = new List<DraftPick>();
            DraftRounds = new List<DraftRound>();
            Teams = teams;
            numTeams = teams.Count;
            rounds = numRounds;

            //RandomizeTeamAssignment();
            BuildDraftOrder();

        }

        private void BuildDraftOrder()
        {

            var total = numTeams * rounds;
            List<DraftPick> tempList = new List<DraftPick>();
            int pickNum = 1;
            int pointer = 1;
            int draftRound = 1;

            for (int m = 1; m < total; m++)
            {
                DraftPick dp = new DraftPick();
                dp.TeamName = Teams[pointer - 1].PDCTeamName;
                dp.ID = Teams[pointer - 1].ID;
                dp.PickNum = pickNum;
                DraftOrder.Add(dp);
                tempList.Add(dp);

                //dont fk with this, the logic works for 25 rounds.  inclusive yo.
                if (pointer % numTeams == 0)
                {
                    //  Console.WriteLine("pointer: {0}, draftRound: {1}, pickNum: {2},  m: {3}, draftOrderCount: {4}", pointer, draftRound, pickNum, m, DraftOrder.Count.ToString());

                    DraftRound dr = new DraftRound(draftRound, tempList);
                    DraftRounds.Add(dr);

                    //re-set everything
                    pointer = 1;
                    tempList = new List<DraftPick>();
                    draftRound++;

                }
                else
                {
                    pointer++;
                }

                pickNum++;

            }

        }

        public List<PDCTeam> Teams { get; set; }
        private int rounds { get; set; }
        private int numTeams { get; set; }

        public List<DraftPick> DraftOrder { get; set; }
        public List<DraftRound> DraftRounds { get; set; }
    }
}
