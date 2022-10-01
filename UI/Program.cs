using Lists;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.InteropServices;

//TestLinked();
//TestMyStack();
//TestMyQueue();
//TestMaze();
//TestMazeManual();
//TestMazeTime();
//TestHashTable();
//TestHashedSet();
TestPhone();
//Dict();
//SeqOfStringsOdd();
//TextText();
void TextText()
{
    Dictionary<string, int> dict = new Dictionary<string, int>();
    string text = "This is the TEXT. Text, text, text - THIS TEXT! Is this the text?";
    string[] bits = text.Replace(".","").Replace(",", "").Replace("-","").Replace("!", "").Replace("?", "").Split(' ');
    foreach (var item in bits)
    {
        if (string.IsNullOrWhiteSpace(item))
            continue;
        if (dict.ContainsKey(item.ToLower()))
            dict[item.ToLower()] += 1;
        else
            dict.Add(item.ToLower(), 1);
    }
    var list = dict.OrderBy(x => x.Value).ToList();
    foreach (var item in list)
    {
        Console.WriteLine(item.Key.ToString() +"->"+item.Value.ToString());
    }
} //3
void SeqOfStringsOdd()
{
    Dictionary<string, int> dict = new Dictionary<string, int>();
    string[] text = new string[] {"C#", "SQL", "PHP", "PHP", "SQL", "SQL"};
    foreach (var item in text)
    {
        if (dict.ContainsKey(item))
            dict[item] += 1;
        else
            dict.Add(item, 1);
    }
    foreach (var item in dict)
    {
        if (item.Value % 2 == 1)
            Console.WriteLine(item.Key.ToString());
    }
} //2
void Dict()
{
    Dictionary<double, int> list = new Dictionary<double, int>();
    double[] test = new double[] { 3, 4, 4, -2.5, 3, 3, 4, 3, -2.5 };
    foreach (var item in test)
    {
        if (list.ContainsKey(item))
            list[item]+= 1;
        else
            list.Add(item, 1);
    }
    Console.WriteLine("");
} //1
void TestPhone()
{
    PhoneBook pb = new PhoneBook("PersonCSV.csv");
    pb.Add("Erik","Karlsson", "Uddevalla", "0000000");
    pb.RunBatchCommands("commands.txt");
    foreach (var item in pb.RunCommand("commands.txt"))
    {
        Console.WriteLine(item);
    }
} //6
void TestHashedSet()
{
    HashedSet<string> set = new HashedSet<string>();
    HashedSet<string> set2 = new HashedSet<string>();
    set.Add("Bike");
    set.Add("Elev");
    set.Add("Teacher");
    set.Add("Cars");

    set2.Add("Bike");
    set2.Add("SQL");
    foreach (var item in set)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine("----------------");
    foreach (var item in set2)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine("--------UNION--------");
    foreach (var item in set.Union(set2))
    {
        Console.WriteLine(item);
    }

    Console.WriteLine("--------INTERSECT--------");
    foreach (var item in set.Intersect(set2))
    {
        Console.WriteLine(item);
    }

} //5

void TestHashTable()
{
    HashTable<int,string> ht = new HashTable<int,string>();
    Random random = new Random();
    for (int i = 0; i < 9000; i+=8)
    {
        ht.Add(i, random.Next(1000).ToString());
    }
    ht.Add(32, "hej");
    ht.Add(320, "hej");
    foreach (KeyValuePair<int,string> item in ht)
    {
        Console.WriteLine(item.Key + " " + item.Value);
    }
} //4

void TestLinked()
{
    Lists.LinkedList<string> lista = new Lists.LinkedList<string>();
    Lists.ListItem<string> one = new Lists.ListItem<string>("First");
    Lists.ListItem<string> two = new Lists.ListItem<string>("Two");
    Lists.ListItem<string> three = new Lists.ListItem<string>("3");
    lista.FirstElement = one;
    lista.FirstElement.NextItem = two;
    lista.FirstElement.NextItem.NextItem = three;
    Console.WriteLine($"{lista.Count} items in list");
    Console.WriteLine(lista.FirstElement.Value);
    Console.WriteLine(lista.FirstElement.NextItem.Value);
    Console.WriteLine(lista.FirstElement.NextItem.NextItem.Value);
}

void TestMyStack()
{
    MyStack list = new MyStack();
    MyStack list2 = new MyStack();
    list2.Pop();
    list2.Push("lista 2 säger hej");
    list.Push("Hej");
    list.Push("Hej2");
    list.Push("Hej3");
    Console.WriteLine(list.ToString());
    list.Pop();
    Console.WriteLine(list.ToString());
    list.Pop();
    list.Pop();
    Console.WriteLine(list.ToString());
    Console.WriteLine(list2.ToString());
}

void TestMyQueue()
{
    MyQueue<object> queue = new MyQueue<object>();
    MyListItem<int> one = new MyListItem<int>(1);
    MyListItem<string> two = new MyListItem<string>("Two");
    queue.Add(one);
    queue.Add(two);
    queue.PrintIt();
    var test = queue.Next;
    queue.PrintIt();

    Console.WriteLine("--------------------");
    ///////////////
    MyQueue<object> queue2 = new MyQueue<object>();
    queue2.Add((int)34);
    queue2.Add("two");
    queue2.Add(3.5);
    bool run = true;
    while(run)
    {
        var item = queue2.Next;
        if (item == null) run = false;
        else
            Console.WriteLine(item);
    }
    Console.WriteLine("--------------------");
    ///////////////
    MyQueue queue3 = new MyQueue();
    queue3.Add("hej");
    queue3.Add(4);
    queue3.Add(new MyListItem<int>(5));
    queue3.PrintIt();
}


void TestMaze()
{
    Labyrinth maze = new Labyrinth(20,40);
    maze.DoEverythingAndPrint();
}

void TestMazeTime()
{
    Labyrinth maze = new Labyrinth(50,50);
    Stopwatch sw = new Stopwatch();
    sw.Start();
    maze.DoEverythingNotPrint();
    sw.Stop();
    Console.WriteLine($"Tiden för 50x50 celler i ms: {sw.ElapsedMilliseconds}");

    maze = new Labyrinth(500, 500);
    sw = new Stopwatch();
    sw.Start();
    maze.DoEverythingNotPrint();
    sw.Stop();
    Console.WriteLine($"Tiden för 500x500 celler i ms: {sw.ElapsedMilliseconds}");

    maze = new Labyrinth(5000, 5000);
    sw = new Stopwatch();
    sw.Start();
    maze.DoEverythingNotPrint();
    sw.Stop();
    Console.WriteLine($"Tiden för 5000x5000 celler i ms: {sw.ElapsedMilliseconds}");

    maze = new Labyrinth(10000, 10000);
    sw = new Stopwatch();
    sw.Start();
    maze.DoEverythingNotPrint();
    sw.Stop();
    Console.WriteLine($"Tiden för 10000x10000 celler i ms: {sw.ElapsedMilliseconds}");
}

void TestMazeManual()
{
    Labyrinth maze = new Labyrinth(20, 20);
    maze.GenerateMaze();
    maze.PlaceStartPoint(10, 7);
    maze.SolveMaze(true);
}
