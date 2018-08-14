using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspRossko.Helper
{
    public class Permutations
    {
        public string PermutationText { get; set; }
        public bool Repeat { get; set; }
        List<string> permutationsList;
        public int CountItems => permutationsList.Count;

        public void AddToPermutationList(IEnumerable<char> array)
        {
            var bufer = new StringBuilder();
            foreach (var c in array)
                bufer.Append(c);

            if (Repeat || !permutationsList.Contains(bufer.ToString()))
                permutationsList.Add(bufer.ToString());
        }

       public char[] RecursionPermutation(char[] array, int n)
        {
            for (var i = 0; i < n; i++)
            {
                var temp = array[n - 1];
                for (var j = n - 1; j > 0; j--)
                    array[j] = array[j - 1];

                array[0] = temp;
                if (i < n - 1)
                    AddToPermutationList(array);
                if (n > 0)
                    RecursionPermutation(array, n - 1);
            }

            return array;
        }

        public Permutations(string text = "")
        {
            PermutationText = text;
            permutationsList = new List<string> {PermutationText};
        }

        public List<string> GetPermutationsList()
        {    
            RecursionPermutation(PermutationText.ToArray(), PermutationText.Length);
            return permutationsList;
        }

        public List<string> GetList() => permutationsList;


        public List<string> GetSortedPermutationsList()
        {
            GetPermutationsList().Sort();
            return permutationsList;
        }
    }
}
