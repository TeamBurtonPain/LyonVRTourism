using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Service
{
    class FileHelper
    {
        public void WriteStringToFile(string str, string filename)
        {
            string path = PathForDocumentsFile(filename);
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(str);

            sw.Close();
            file.Close();
        }

        public string PathForDocumentsFile(string filename)
        {
                string path = Application.persistentDataPath;
                path = path.Substring(0, path.LastIndexOf('/'));
                return Path.Combine(path, filename);
        }

        public string ReadStringFromFile(string filename)
        {
            string path = PathForDocumentsFile(filename);

            if (File.Exists(path))
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(file);

                string str = null;
                str = sr.ReadLine();

                sr.Close();
                file.Close();

                return str;
            }

            else
            {
                return null;
            }
        }
    }
}
