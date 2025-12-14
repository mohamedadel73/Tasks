using System.Security.Cryptography.X509Certificates;

namespace Problem_Solving
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = new List<int> { 1, 2, 2, 3, 4, 4, 5 };
            var unique = RemoveDuplicates(input);

            Console.WriteLine("Output: [" + string.Join(", ", unique) + "]");
        }

        
        static List<T> RemoveDuplicates<T>(IEnumerable<T> source)
        {
            if (source == null) return new List<T>();

            var seen = new HashSet<T>();
            var result = new List<T>();

            foreach (var item in source)
            {
                if (seen.Add(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
