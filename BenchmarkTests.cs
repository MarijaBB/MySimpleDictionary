using System.Diagnostics;

namespace MySimpleDictionary
{
    public class BenchmarkTests
    {
        private const int SmallSize = 1000;
        private const int MediumSize = 10000;
        private const int LargeSize = 100000;

        public static void Main()
        {
            RunAllBenchmarks(SmallSize, "Small");
            RunAllBenchmarks(MediumSize, "Medium");
            RunAllBenchmarks(LargeSize, "Large");

            /*
             Here you can write your own tests:
             var dict1 = new MySimpleDictionary<int, int>(); 
             dict1.Add(4,5);
             dict1.Count();
             dict1.ContainsValue(5);
             ...
            */
        }

        private static void RunAllBenchmarks(int size, string sizeLabel)
        {
            Console.WriteLine($"\n{sizeLabel} Dataset - {size} elements:");
            Console.WriteLine("----------------------");

            BenchmarkAdd(size);
            BenchmarkLookup(size);
            BenchmarkIteration(size);
            BenchmarkRemove(size);
            BenchmarkGetKeys(size);
            BenchmarkGetValues(size);
            BenchmarkClear(size);
        }
        private static void BenchmarkAdd(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                myDict[keys[i]] = values[i];
            }
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;

            
            var stopwatch2 = Stopwatch.StartNew();
            for (int i = 0; i < size; i++)
            {
                dict[keys[i]] = values[i];
            }
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;

            Console.WriteLine("Add operation:");
            printResults(myDictTime, dictTime);
        }
        private static void BenchmarkLookup(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            for (int i = 0; i < size; i++)
            {
                myDict.Add(keys[i], values[i]);
                dict.Add(keys[i], values[i]);
            }

            var lookupKeys = new string[size];
            for (int i = 0; i < size; i++)
            {
                lookupKeys[i] = i % 10 == 0 ? "something" + i : keys[i % keys.Length];
            }

            int myResults = 0;
            var stopwatch = Stopwatch.StartNew();
            foreach (var key in lookupKeys)
            {
                if (myDict.ContainsKey(key))
                    myResults++;
            }
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;

            int netResults = 0;
            var stopwatch2 = Stopwatch.StartNew();
            foreach (var key in lookupKeys)
            {
                if (dict.ContainsKey(key))
                    netResults++;
            }
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;

            Console.WriteLine("Lookup key operation:");
            printResults(myDictTime, dictTime);
        }
        private static void BenchmarkIteration(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            for (int i = 0; i < size; i++)
            {
                myDict.Add(keys[i], values[i]);
                dict.Add(keys[i], values[i]);
            }

            var stopwatch = Stopwatch.StartNew();
            foreach (var kvp in myDict)
            {          
            }
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;
        
            var stopwatch2 = Stopwatch.StartNew();
            foreach (var pair in dict)
            {
            }
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;

            Console.WriteLine("Iteration:");
            printResults(myDictTime, dictTime);
        }

        private static void BenchmarkRemove(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            for (int i = 0; i < size; i++)
            {
                myDict.Add(keys[i], values[i]);
                dict.Add(keys[i], values[i]);
            }

            var keysToRemove = new List<string>();
            for (int i = 0; i < size; i += 5)
            {
                keysToRemove.Add(keys[i]);
            }

            var stopwatch = Stopwatch.StartNew();
            foreach (var key in keysToRemove)
            {
                myDict.Remove(key);
            }
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;

            var stopwatch2 = Stopwatch.StartNew();
            foreach (var key in keysToRemove)
            {
                dict.Remove(key);
            }
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;


            Console.WriteLine("Remove operation:");
            printResults(myDictTime, dictTime);

        }

        private static void BenchmarkGetKeys(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            for (int i = 0; i < size; i++)
            {
                myDict.Add(keys[i], values[i]);
                dict.Add(keys[i], values[i]);
            }

            var stopwatch = Stopwatch.StartNew();
            _ = myDict.Keys;
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;

            var stopwatch2 = Stopwatch.StartNew();
            _ = dict.Keys;
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;


            Console.WriteLine("Get keys operation:");
            printResults(myDictTime, dictTime);
        }
        private static void BenchmarkGetValues(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            for (int i = 0; i < size; i++)
            {
                myDict.Add(keys[i], values[i]);
                dict.Add(keys[i], values[i]);
            }

            var stopwatch = Stopwatch.StartNew();
            _ = myDict.Values;
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;

            var stopwatch2 = Stopwatch.StartNew();
            _ = dict.Values;
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;


            Console.WriteLine("Get values operation:");
            printResults(myDictTime, dictTime);
        }
        private static void BenchmarkClear(int size)
        {
            var keys = GenerateTestKeys(size);
            var values = GenerateTestValues(size);

            var myDict = new MySimpleDictionary<string, int>();
            var dict = new Dictionary<string, int>();

            for (int i = 0; i < size; i++)
            {
                myDict.Add(keys[i], values[i]);
                dict.Add(keys[i], values[i]);
            }

            var stopwatch = Stopwatch.StartNew();
            myDict.Clear();
            stopwatch.Stop();
            var myDictTime = stopwatch.Elapsed.TotalMilliseconds;

            var stopwatch2 = Stopwatch.StartNew();
            dict.Clear();
            stopwatch2.Stop();
            var dictTime = stopwatch2.Elapsed.TotalMilliseconds;


            Console.WriteLine("Clear operation:");
            printResults(myDictTime, dictTime);
        }

        private static string[] GenerateTestKeys(int count)
        {
            var keys = new string[count];

            for (int i = 0; i < count; i++)
            {
                keys[i] = $"key{i}";
            }

            return keys;
        }

        private static int[] GenerateTestValues(int count)
        {
            var values = new int[count];
            var random = new Random(100);

            for (int i = 0; i < count; i++)
            {
                values[i] = random.Next();
            }

            return values;
        }
        private static void printResults(double myDictTime, double dictTime)
        {
            Console.WriteLine($"MySimpleDictionary: {myDictTime:F2} ms");
            Console.WriteLine($"Dictionary:         {dictTime:F2} ms");
            Console.WriteLine($"Ratio:              {myDictTime / dictTime:F2}x\n");
        }
    }
}