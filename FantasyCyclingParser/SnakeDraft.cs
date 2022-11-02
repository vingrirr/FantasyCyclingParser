﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyCyclingParser
{
    public class SnakeDraft
    {
        public SnakeDraft(List<string> teams, int numRounds)
        {
            DraftOrder = new List<DraftRound>();
            Teams = teams;  
            numTeams = teams.Count;
            rounds = numRounds;
            
            RandomizeTeamAssignment();
            BuildDraftOrder();

        }
        private void RandomizeTeamAssignment()
        {
            Teams.Shuffle();            
        }        
        private void BuildDraftOrder()
        {
        
            List<List<int>> allDraftPositions = new List<List<int>>();

            for (int j=0; j< numTeams; j++)
            {
                allDraftPositions.Add(new List<int>()); 
            }

            var total = numTeams * rounds;
            var count = 0;
            

            for (var POS = 1; POS <= numTeams; POS++)
            {

                List<int> draftPosition = new List<int>(); 
                for (var i = 1; i <= total; i++)
                {

             
                    if ((i % (2 * numTeams)) == ((2 * numTeams) - (POS - 1)) % (2 * numTeams)
                    || (i % (2 * numTeams)) == POS)
                    {
                        //isMyTurn = true;
                        draftPosition.Add(i);
                    }

                    //var turnText = (isMyTurn) ? i + " My Turn!" : i;
                    
                }
                    allDraftPositions[POS - 1] = draftPosition;
            }

            for (int m = 0; m < allDraftPositions.Count; m++)
            {
                for (int n = 0; n < allDraftPositions[m].Count; n++)
                {
                    DraftRound dr = new DraftRound();
                    dr.Name = Teams[m];
                    dr.PickNum = allDraftPositions[m][n];
                    DraftOrder.Add(dr);
                }

            }
            DraftOrder = DraftOrder.OrderBy( x=> x.PickNum ).ToList();

        }

        public List<string> Teams { get; set; }
        private int rounds { get; set; }
        private int numTeams { get; set; }
        
        public List<DraftRound> DraftOrder { get; set; }
    }
    
    public class DraftRound
    {
        public DraftRound()
        {

        }
        public int PickNum { get; set; }
        public string Name { get; set; }
    }



    
}
