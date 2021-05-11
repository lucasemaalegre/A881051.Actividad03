using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace A881051.Actividad03.Helper
{
    public class Document
    {
        public static string ReadTextFromFile(string fileName)
        {
            string filePath = System.IO.Path.Combine(
                System.IO.Directory.GetCurrentDirectory(),
                "data", fileName);

            return System.IO.File.ReadAllText(filePath);
        }

        public static void AppendTextToFile(
            string fileName,
            string text
        ) {
            string filePath = System.IO.Path.Combine(
                System.IO.Directory.GetCurrentDirectory(),
                "data", fileName);

            if (File.Exists(filePath))
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.Write(text);
                }	
            }
        }

        public static void CreateTextFileIfNotExists(
            string fileName,
            string text = ""
        ) {
            string filePath = System.IO.Path.Combine(
                System.IO.Directory.GetCurrentDirectory(),
                "data", fileName);

            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.Write(text);
                }
            }
        }
    }
}
