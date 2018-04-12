using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    [Serializable]
    class FileSystem
    {
        public static string ignoreMessagesLog = "";
        private static string log = "";
        static BinaryFormatter formatter = new BinaryFormatter();

        private static StreamWriter logWriter = new StreamWriter("log.txt");

        public static Dictionary<long, String> userChoices = new Dictionary<long, string>();

        public static void LoadFiles()
        {
            string temp = "";
            int tempCount = -1;
            String[] tempArray;

            StreamReader streamReader;
            Console.WriteLine("Loading files...");

            using (streamReader = new StreamReader("mainImgs.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    temp = streamReader.ReadLine();
                    MyBot.mainImgs.Add(new FileToSend(temp));
                }
            }

            using (streamReader = new StreamReader("questionImgs.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    tempCount++;
                    MyBot.questionImgs.Add(new List<FileToSend>());
                    tempArray = streamReader.ReadLine().Split(' ');
                    for (int t = 0; t < tempArray.Length; t++)
                    {
                        MyBot.questionImgs[tempCount].Add(new FileToSend(tempArray[t]));
                    }
                }
            }

            using (streamReader = new StreamReader("ignoreMessagesLog.txt"))
            {
                ignoreMessagesLog = streamReader.ReadToEnd();
            }

                Console.WriteLine("All files successful loaded!");
        }

        public static void GetLogs(string date, string nickname, string textMessage)
        {
            log += date + " : " + nickname + " : " + textMessage + "\n";
            if (log.Length >= 25)
            {
                logWriter.WriteLine(log);
                logWriter.Flush();
                log = "";
            }
        }

        public static void SaveUserChoices()
        {
            using (FileStream userChoicesSaving = new FileStream("userChoices.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(userChoicesSaving, userChoices);
                Console.WriteLine("Object userChoices was Serialized!");
            }
        }

        public static Dictionary<long, String> LoadUserChoices()
        {
            using (FileStream userChoicesLoading = new FileStream("userChoices.dat", FileMode.OpenOrCreate))
            {
                userChoices.Clear();
                try
                {
                    userChoices = (Dictionary<long, string>)formatter.Deserialize(userChoicesLoading);
                    Console.WriteLine("Object userChoices was Deserialized!");
                }
                catch(SerializationException)
                {
                    Console.WriteLine("Object userChoices was not Deserialized! (Maybe file is empty)");
                }
            }
            return userChoices;
        }
    }
}
