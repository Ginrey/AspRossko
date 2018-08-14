using AspRossko.Helper;
using Xunit;

namespace AspRossko.Tests
{
    public class PermutationsTests
    {
        [Theory]
        [InlineData("46", new[] {"46", "64" })]   
        [InlineData("aba", new[] {"aab", "aba", "baa" })]   
        [InlineData("123", new[] {"123", "132", "213", "231", "321", "312" })]   
        public void RecursionPermutationTest(string text, string [] textResults)
        {
            Permutations p = new Permutations(text);
            Assert.Equal(text, p.PermutationText);
            Assert.Equal(1, p.CountItems);
           
            var result = p.RecursionPermutation(text.ToCharArray(), text.Length);
            Assert.Equal(textResults.Length, p.CountItems);
            Assert.Equal(text, result);
            foreach (var t in textResults)
            {
                Assert.Contains(t, p.GetList());
            }    
        }

        [Theory]
        [InlineData("46", new[] {"46", "64"})]
        [InlineData("aba", new[] {"aab", "aba", "baa"})]
        [InlineData("123", new[] {"123", "132", "213", "231", "312", "321"})]
        public void GetSortedPermutationsListTest(string text, string[] textResults)
        {
            Permutations p = new Permutations(text);
            Assert.Equal(text, p.PermutationText);
            Assert.Equal(1, p.CountItems);  
            p.GetSortedPermutationsList();
            Assert.Equal(textResults.Length, p.CountItems);
            for (var index = 0; index < textResults.Length; index++)
            {
                var t = textResults[index];
                Assert.Equal(t, p.GetList()[index]);
            }
        }
    }
}
