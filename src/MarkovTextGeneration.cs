using ListSymbolTable;
using SymbolTable;
using System.Diagnostics;

/*
 * If the story is really long (500+ n-length words) it is more likely to encounter "key not found"
 */

// Settings I know this works with: 

/*
"drseuss.txt dickens_christmas_carol.txt"
1
100
*/

/*
 * drseuss.txt
 * 3
 * 200
 */


class Program {

    public static void Main(string[] args) {

        // string[] lines = File.ReadAllLines(args[0]);
        // string word = "";
        Stopwatch num1 = new Stopwatch();
        Stopwatch num2 = new Stopwatch();
        Stopwatch num3 = new Stopwatch();

        
        Debug.Assert(args.Length == 3, "USAGE: dotnet run <training text filepaths> <n-length> <wordcount>");





num1.Start();
        Console.WriteLine("Sorted Dict: ");
        SortedD.Story1(args);
        num1.Stop();
        Console.WriteLine(num1.ElapsedMilliseconds + " milliseconds");

        num2.Start();
        Console.WriteLine("Tree Table: ");
        TreeTable.Story2(args);
        num2.Stop();
        Console.WriteLine(num2.ElapsedMilliseconds + " milliseconds");

        num3.Start();
        Console.WriteLine("Sorted List: ");
        SortedList.Story3(args);
        num3.Stop();
        Console.WriteLine(num3.ElapsedMilliseconds + " milliseconds");

    }
}

public class SortedD {

    public static void Story1(string[] args) {

        string[] files = args[0].Split();
        string word = "";

        for (int i = 0; i < files.Length; i++) {
            string[] part = File.ReadAllLines(files[i]);
            foreach (string line in part) {
                word += line + " ";
            }

        }

        // Get key value pair length
        int N = Convert.ToInt32(args[1]);


        // Define the objects of things
        SortedDictionary<string, MarkovEntry> sd_model = new SortedDictionary<string, MarkovEntry>();
        List<string> for_random = new List<string>();
        Random rnd = new Random();
        int count = 0;

        word.Replace("  ", " ");
        string[] words = word.Split(" ");

        for (int i = 0; i < words.Length - 2 * N - 1; i++) {

            // Join function for n-length
            string MarkovKey = string.Join(" ", words.Skip(i).Take(N));

            if (!sd_model.ContainsKey(MarkovKey)) {
                sd_model[MarkovKey] = new MarkovEntry(MarkovKey);
            }
            string addValue = "";

            // 
            for (int k = 0; k < N; k++) {
                if (k < N - 1) {
                    addValue += words[i + k + N] + " ";
                }
                else {
                    addValue += words[i + k + N];
                }

            }
            sd_model[MarkovKey].Add(addValue);
            count++;
        }


        // Add the capital lettered words for the start of the stories.

        foreach (string key in sd_model.Keys) {
            string item = key;
            if (item.Length > 0) {
                char things = item[0];

                if (char.ToLower(things) != things) {
                    for_random.Add(key);
                }
            }
        }


        string story = "";
        string randomword = for_random[rnd.Next(0, for_random.Count)];
        story += randomword + " ";
        int length = Convert.ToInt32(args[2]);
        int j = 0;
        bool thing = true;

        while (j < length / 2 || thing) {
            j++;
            string to_add = sd_model[randomword].RandomWord();
            // The keys "back.", "HAM", "CHRISTMAS!" and others do not work?
            randomword = to_add;
            story += to_add + " ";
            // if (j % 15 == 0 && j > 5) {  story += "\n";}
            if (length - j < 2 && (to_add.Contains(".") || to_add.Contains("!") || to_add.Contains("?") || to_add.Contains("..."))) {
                break;
            }
            if (j > length && !(to_add.Contains(".") || to_add.Contains("!") || to_add.Contains("?") || to_add.Contains("..."))) {
                to_add = sd_model[randomword].RandomWord();
                // The keys "back.", "HAM", "CHRISTMAS!" and others do not work?
                randomword = to_add;
                story += to_add + " ";
            }
            else {

            }
        }



        Console.WriteLine(story); Console.WriteLine(story.Split(" ").Length);


        // foreach (string item in sd_model.Keys) { Console.WriteLine(sd_model[item]); }


    }
}

class TreeTable {
    public static void Story2(string[] args) {

        // Get key value pair length
        int N = Convert.ToInt32(args[1]);

        string[] files = args[0].Split();
        string word = "";

        for (int i = 0; i < files.Length; i++) {
            string[] part = File.ReadAllLines(files[i]);
            foreach (string line in part) {
                word += line + " ";
            }

        }
        word.Replace("  ", " ");
        string[] words = word.Split(" ");

        // Define the objects of things
        TreeSymbolTable<string, MarkovEntry> sd_model = new TreeSymbolTable<string, MarkovEntry>();
        List<string> for_random = new List<string>();
        Random rnd = new Random();
        int count = 0;

        // Add all words

        for (int i = 0; i < words.Length - 2 * N - 1; i++) {

            // Join function for n-length
            string MarkovKey = string.Join(" ", words.Skip(i).Take(N));

            if (!sd_model.ContainsKey(MarkovKey)) {
                sd_model.Add(MarkovKey);
                sd_model[MarkovKey] = new MarkovEntry(MarkovKey);
            }
            string addValue = "";

            // 
            for (int k = 0; k < N; k++) {
                if (k < N - 1) {
                    addValue += words[i + k + N] + " ";
                }
                else {
                    addValue += words[i + k + N];
                }

            }
            sd_model[MarkovKey].Add(addValue);
            count++;
        }


        // Add the capital lettered words for the start of the stories.

        foreach (string key in sd_model) {
            if (key == "") {
                continue;
            }
            char[] item = key.ToCharArray();
            if (char.ToLower(item[0]) != item[0]) {
                for_random.Add(key);
            }
        }


        string story = "";
        string randomword = for_random[rnd.Next(0, for_random.Count)];
        story += randomword + " ";
        int length = Convert.ToInt32(args[2]);
        int j = 0;
        bool thing = true;

        while (j < length / 2 || thing) {
            j++;
            string to_add = sd_model[randomword].RandomWord();
            randomword = to_add;
            story += to_add + " ";
            // if (j % 15 == 0 && j > 5) {  story += "\n";}
            if (length - j < 2 && (to_add.Contains(".") || to_add.Contains("!") || to_add.Contains("?") || to_add.Contains("..."))) {
                break;
            }
            if (j > length && !(to_add.Contains(".") || to_add.Contains("!") || to_add.Contains("?") || to_add.Contains("..."))) {
                to_add = sd_model[randomword].RandomWord();
                randomword = to_add;
                story += to_add + " ";
            }
            else {

            }
        }



        Console.WriteLine(story); Console.WriteLine(story.Split(" ").Length);


        // foreach (string item in sd_model.Keys) { Console.WriteLine(sd_model[item]); }


    }
}

class SortedList {

    public static void Story3(string[] args) {

        // string[] files = args[0].Split();

        // Get key value pair length
        int N = Convert.ToInt32(args[1]);
        string[] files = args[0].Split();
        string word = "";

        for (int i = 0; i < files.Length; i++) {
            string[] part = File.ReadAllLines(files[i]);
            foreach (string line in part) {
                word += line + " ";
            }

        }

        // Define the objects of things
        ListSymbolTable<string, MarkovEntry> sd_model = new ListSymbolTable<string, MarkovEntry>();
        List<string> for_random = new List<string>();
        Random rnd = new Random();
        int count = 0;

        // Add all words
        word.Replace("  ", " ");
        string[] words = word.Split(" ");

        for (int i = 0; i < words.Length - 2 * N - 1; i++) {

            // Join function for n-length
            string MarkovKey = string.Join(" ", words.Skip(i).Take(N));

            if (!sd_model.ContainsKey(MarkovKey)) {
                MarkovEntry broo = new MarkovEntry(MarkovKey);
                sd_model.Add(MarkovKey, broo);
            }
            string addValue = "";

            // 
            for (int k = 0; k < N; k++) {
                if (k < N - 1) {
                    addValue += words[i + k + N] + " ";
                }
                else {
                    addValue += words[i + k + N];
                }

            }
            sd_model[MarkovKey].Add(addValue);
            count++;
        }


        // Add the capital lettered words for the start of the stories.

        foreach (string key in sd_model) {
            if (key == "") {
                continue;
            }
            char[] item = key.ToCharArray();
            if (char.ToLower(item[0]) != item[0]) {
                for_random.Add(key);
            }
        }


        string story = "";
        string randomword = for_random[rnd.Next(0, for_random.Count)];
        story += randomword + " ";
        int length = Convert.ToInt32(args[2]);
        int j = 0;
        bool thing = true;

        while (j < length / 2 || thing) {
            j++;
            string to_add = sd_model[randomword].RandomWord();
            // The keys "back.", "HAM", "CHRISTMAS!" and others do not work?
            randomword = to_add;
            story += to_add + " ";
            // if (j % 15 == 0 && j > 5) {  story += "\n";}
            if (length - j < 2 && (to_add.Contains(".") || to_add.Contains("!") || to_add.Contains("?") || to_add.Contains("..."))) {
                break;
            }
            if (j > length && !(to_add.Contains(".") || to_add.Contains("!") || to_add.Contains("?") || to_add.Contains("..."))) {
                to_add = sd_model[randomword].RandomWord();
                // The keys "back.", "HAM", "CHRISTMAS!" and others do not work?
                randomword = to_add;
                story += to_add + " ";
            }
            else {

            }
        }



        Console.WriteLine(story); Console.WriteLine(story.Split(" ").Length);


        // foreach (string item in sd_model.Keys) { Console.WriteLine(sd_model[item]); }


    }
}

class MarkovEntry {

    private SortedDictionary<string, int> nextWords;
    public int count;
    public string root;
    private List<string> strings = new List<string>();

    private Random rnd;

    public int Count() {
        return count;
    }

    public MarkovEntry(string root) {
        this.root = root;
        this.count = 0;
        nextWords = new SortedDictionary<string, int>();
        this.rnd = new Random();
    }

    public void Add(string word) {

        if (!nextWords.ContainsKey(word)) {
            nextWords.Add(word, 0);
        }

        nextWords[word]++;
        count++;
        strings.Add(word);

    }

    public string RandomWord() {
        return strings[rnd.Next(0, strings.Count)];
    }

    public override string ToString() {
        string words = "";
        foreach (string word in nextWords.Keys) {
            words += "  " + word + " " + nextWords[word] + "\n";
        }
        return root + $" - {count};\n{words}";
    }
}


