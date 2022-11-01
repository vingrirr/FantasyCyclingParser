using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyCyclingParser
{
    public static class SnakeDraft
    {       
        public static List<List<int>> BuildDraftOrder( int numTeams, int rounds)
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
    }
}
