namespace FantasyDraftBlazor
{

    // C# program for the above approach
    using System;
    using System.Collections.Generic;
    using System.Collections;
    public class PointCombinations
    {
        public PointCombinations(List<int> pointArray, int targetSum, int numSlots)
        {

            Combos = new List<List<int>>(); 

            List<List<int>> ans
                = GFG.combinationSum(pointArray, targetSum);

            foreach(List<int> c in ans)
            {
                if (c.Count <= numSlots)
                    Combos.Add(c);
            }
            if (Combos.Count > 0)
            {
                Combos = Combos.OrderByDescending(x => x.Count).ToList();
            }

            
        }

        public  List<List<int>> Combos { get; set; }
    }

    public static class GFG
    {

        public static List<List<int>> combinationSum(List<int> arr, int sum)
        {
            List<List<int>> ans
                = new List<List<int>>();
            List<int> temp = new List<int>();

            // first do hashing since hashset does not always
            // sort
            // removing the duplicates using HashSet and
            // Sorting the List

            HashSet<int> set = new HashSet<int>(arr);
            arr.Clear();
            arr.AddRange(set);
            arr.Sort();

            findNumbers(ans, arr, sum, 0, temp);
            return ans;
        }

        private static void findNumbers(List<List<int>> ans,
                    List<int> arr, int sum, int index,
                    List<int> temp)
        {

            if (sum == 0)
            {

                // Adding deep copy of list to ans

                ans.Add(new List<int>(temp));
                return;
            }

            for (int i = index; i < arr.Count; i++)
            {

                // checking that sum does not become negative

                if ((sum - arr[i]) >= 0)
                {

                    // Adding element which can contribute to
                    // sum

                    temp.Add(arr[i]);

                    findNumbers(ans, arr, sum - arr[i], i,
                                temp);

                    // removing element from list (backtracking)
                    temp.Remove(arr[i]);
                }
            }
        }

    
    }

    // This code is contributed by ShubhamSingh10


}
