using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CellPhoneDict
{
    class Program
    {
        /*
         * string words([2, 2, 8]) -> ["bat", "cat"]
         * string words([6, 6, 8, 7, 3]) -> ["mouse"]
         * string words([2, 2, 8, 8])-> []
        */
        static List<string> dictionary = new List<string>()
        {
            "cat", "bat", "elephant", "mouse", "horse", "dog", "zebra"
        };

        static Dictionary<int, string> letters = new Dictionary<int, string>()
        {
            {2,"abc"},{3, "def"}, {4, "ghi"},
            {5, "jkl"}, {6, "mno"}, {7, "pqrs"},
            {8, "tuv"}, {9, "wxyz"}
        };

        static bool contains(string word)
        {
            return dictionary.Contains(word);
        }

        
        private void expandstr(ref List<string> str, string letter)
        {
            //// expand the str letter.Length times
            // high space complexity

            if (str.Count == 0)
                return;

            for (int i = 0; i <= letter.Length; i++)
            {
                str.Add(str[i]);
            }
        }

        private void append(ref List<string> str, int[] numbers, int offset)
        {
            if (offset >= numbers.Length) // last time
            {
                return;
            }

            if (str == null) // first time
            {
                string firstletter = letters[numbers[offset]];

                str = new List<string>(firstletter.Length);

                for (int i = 0; i < firstletter.Length; i++)
                {
                    str.Add(string.Format("{0}", firstletter[i]));
                }

                str.RemoveAll(s => !dictionary.Exists(w => w.StartsWith(s)));
                
                if(str.Count == 0)
                    return;

                offset++;
            }

            string letter = letters[numbers[offset]];

            expandstr(ref str, letter);

            for (int i = 0; i < str.Count; i++)
            {
                str[i] = string.Format("{0}{1}", str[i], letter[i % letter.Length]);
            }

            str.RemoveAll(s => !dictionary.Exists(w => w.StartsWith(s)));

            if (str.Count == 0)
                return;

            append(ref str, numbers, offset + 1);
        }

        private string[] words(int[] numbers)
        {
            //// main logic write here
            //
            List<string> str = null;

            append(ref str, numbers, 0);

            if (str.Count > 1) // eliminate duplicated words
            {
                // high space complexity, use another list
                List<string> temp = new List<string>();

                foreach (var s in str)
                    if (!temp.Contains(s))
                        temp.Add(s);
                str = temp;
            }
            return str.ToArray();
        }

        private void WriteAllWords()
        {
            foreach(var word in dictionary)
                Console.Write(word + " ");
            Console.WriteLine("\n");
        }

        private void Start()
        {
            //// run test cases

            int[][] numbers = new int[][]
            {
                new int[]{2, 2, 8},
                new int[]{6, 6, 8, 7, 3}, 
                new int[]{2, 2, 8, 8}
            };

            WriteAllWords();

            Console.WriteLine("Numbers:");
            int id = 0;
            
            var sw = new System.Diagnostics.Stopwatch();

            foreach (var n in numbers)
            {
                Console.Write("{0,2}: [", id++);
                foreach (var i in n)
                    Console.Write("{0} ", i);
                Console.CursorLeft--;
                Console.Write("] -> [");
                int oldPosition = Console.CursorLeft;

                sw.Start();
                string[] results = words(n);
                sw.Stop();
                
                foreach (string w in results)
                    Console.Write("{0} ", w);

                if(Console.CursorLeft-oldPosition>1)
                    Console.CursorLeft--;
                Console.WriteLine("]");
            }

            Console.WriteLine("Total time spent: {0} msec", sw.ElapsedMilliseconds);
        }

        static void Main(string[] args)
        {
            new Program().Start();

            Console.ReadLine();
        }
    }
}
