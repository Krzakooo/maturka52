using System;
using System.Collections.Generic;
using System.IO;

namespace maturka52
{
    class Program
    {
        static String[][] readFile()
        {
            List<String[]> list = new List<String[]>();
            using (StreamReader reader = new StreamReader("pogoda.txt"))
            {
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine();
                    String[] values = line.Split(';');
                    list.Add(values);
                }
            }
            return list.ToArray();
        }
        static void Main(string[] args)
        {
            String[][] data = readFile();
            Console.WriteLine(data.Length);

            //int daysNotWatered = 0;
            //int daysWatered = 0;
            //int daysWateredTwice = 0;
            DateTime dt = new DateTime(2015, 4, 1);
            double volume = 25000;
            foreach (String[] row in data)
            {
                
                int watered;
                int temp = Int32.Parse(row[0]);
                double rain = Double.Parse(row[1]);
                volume += 700 * rain;
                if(volume > 25000)
                {
                    volume = 25000;
                }

                if(rain == 0)
                {
                    volume -= Math.Ceiling(0.03 * 0.01 * Math.Pow(temp, 1.5) * volume);
                }
                
                if (temp <= 15 || rain > 0.6)
                {
                    //daysNotWatered++;
                    watered = 0;
                } 
                else if(temp > 30)
                {
                    //daysWateredTwice++;
                    volume -= 24000;
                    watered = 2;
                }
                else
                {
                    //daysWatered++;
                    volume -= 12000;
                    watered = 1;
                }
                
                if(volume < 0)
                {
                
                    double refilled = 25000 - (volume + 12000 * watered);
                    var d = dt.ToShortDateString();
                    Console.WriteLine("Data pierwszego dolania: " + d + "\nIlość dolanej wody: " + refilled);
                    break;
                }
                dt = dt.AddDays(1);
            }
        }
    }
}

