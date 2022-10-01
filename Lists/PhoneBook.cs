using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Globalization;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using System.Xml;

namespace Lists
{
    public sealed class PhoneBook
    {
        private List<RecordEntry> list = new List<RecordEntry>();

        public PhoneBook(string pathToTextFile)
        {
            string[] text = File.ReadAllLines(pathToTextFile);
            PopulateList(text);
        }
        /// <summary>
        /// Add a new person to the list, based on the textfile
        /// </summary>
        /// <param name="text"></param>
        private void PopulateList(string[] text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                string[] bits = text[i].Split(",");
                Add(bits[0], bits[1], bits[2], bits[3]);
            }
        }
        private List<RecordEntry> Find(Filter f)
        {
            List<RecordEntry> result = new List<RecordEntry>();
            List<RecordEntry> filtered = new List<RecordEntry>();
            if (f.Firstname != null)
                result.AddRange(FindFirstname(f.Firstname));
            if (f.Lastname != null)
                result.AddRange(FindLastname(f.Lastname));
            if (f.Town != null)
                result.AddRange(FindTown(f.Town));
            if (f.Phone != null)
                result.AddRange(FindPhoneNumber(f.Phone));

            foreach (var item in list.Intersect(result))
                filtered.Add(item);

            foreach (var item in filtered.ToList())
            {
                if (f.Firstname != null && item.Firstname != f.Firstname)
                    filtered.Remove(item);
                if (f.Lastname != null && item.Lastname != f.Lastname)
                    filtered.Remove(item);
                if (f.Town != null && item.Town != f.Town)
                    filtered.Remove(item);
                if (f.Phone != null && item.PhoneNumber != f.Phone)
                    filtered.Remove(item);
            }
            return filtered;
        }
        /// <summary>
        /// Manually adds a Person to the list during runtime.
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="town"></param>
        public void Add(string firstname,string lastname , string phoneNumber, string town)
         =>   list.Add(new RecordEntry(firstname, lastname, town, phoneNumber));

        /// <summary>
        /// Will run first command in the command.txt file
        /// </summary>
        /// <param name="pathToCommandFile"></param>
        /// <returns></returns>
        public List<RecordEntry> RunCommand(string pathToCommandFile)
        {
            string[] text = File.ReadAllLines(pathToCommandFile);
            string[] bits = text[0].Replace("(", ";").Replace(")", ";").Split(";"); //0=Command,1=keywords
            string[] parts = text[1].Split(","); //keywords for search
            string[] filter = bits[1].Split(","); //filter = values to add to filter.
            Filter f = MakeFilterForSearch(filter, parts);
            return Find(f).ToList();
        }
        /// <summary>
        /// Will do all the commands in the file. 
        /// Each find command till be saved into a result txt file.
        /// </summary>
        /// <param name="pathToCommandFile"></param>
        /// <returns></returns>
        public void RunBatchCommands(string pathToCommandFile)
        {
            string[] text = File.ReadAllLines(pathToCommandFile);
            for (int i = 0; i < text.Length; i+=2)
            {
                string[] bits = text[i].Replace("(", ";").Replace(")", ";").Split(";"); //0=Command,1=keywords
                string[] parts = text[i+1].Split(","); //keywords for search
                string[] filter = bits[1].Split(","); //filter = values to add to filter.
                Filter f = new Filter();
                f = MakeFilterForSearch(filter, parts);
                SaveResult(Find(f));
            }
        }
        private Filter MakeFilterForSearch(string[] keywords, string[] values)
        {
            Filter f = new Filter();
            for (int i = 0; i < keywords.Length; i++)
            {
                switch (keywords[i].ToLower())
                {
                    case "firstname":
                        f.Firstname = values[i];
                        break;
                    case "lastname":
                        f.Lastname = values[i];
                        break;
                    case "town":
                        f.Town = values[i];
                        break;
                    case "phone":
                        f.Phone = values[i];
                        break;
                    default:
                        break;
                }
            }
            return f;
        }
        private void SaveResult(List<RecordEntry> list)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            //Spara hela listan i en string,
            foreach (RecordEntry item in list)
            {
                sb.AppendLine(item.ToString());
            }
            string[] saveArr = sb.ToString().Split("\r\t");
            File.WriteAllLines($"result_"+DateTime.Now.Ticks.ToString()+"_"+rnd.Next(100000)+".txt",saveArr);
        }
        private List<RecordEntry> FindFirstname(string firstname) => list.FindAll(x => x.Firstname == firstname).ToList();
        private List<RecordEntry> FindLastname(string lastname) => list.FindAll(x => x.Lastname == lastname).ToList();
        private List<RecordEntry> FindTown(string town) => list.FindAll(x => x.Town == town).ToList();
        private List<RecordEntry> FindPhoneNumber(string phone) => list.FindAll(x => x.PhoneNumber == phone).ToList();
    }
}
