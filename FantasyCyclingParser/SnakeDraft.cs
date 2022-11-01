using System;
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
            Teams = teams;  
            numTeams = teams.Count;
            rounds = numRounds; 

        }
        public void RandomizeTeamAssignment()
        {
            Teams.Shuffle();
            


        }
        
        public List<List<int>> BuildDraftOrder()
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

            return allDraftPositions; 
            
        }

        public List<string> Teams { get; set; }
        private int rounds { get; set; }
        private int numTeams { get; set; }
        
    }


    
}
