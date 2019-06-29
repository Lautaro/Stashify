using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;


namespace StashifyQuickStorage
{
    public class Stashify
    {
        static string FolderName = "Stashify";
        static string FolderPath() => $"c:/{FolderName}";
        static string GetFullFilePath(string identifier) => $"{FolderPath()}/{identifier}.txt";

        public static bool Save<T>(string identifier, Object Obj)
        {
            //DoTheThing();

            var xs = new XmlSerializer(typeof(T));
            var folderPath = FolderPath();
            var fullPath = GetFullFilePath(identifier);

            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            using (TextWriter sw = new StreamWriter(fullPath))
            {

                xs.Serialize(sw, Obj);
            }

            return File.Exists(identifier) ? true: false; ;
        }

        public static T Load<T>(string identifier)
        {
            Object rslt;
            if (File.Exists(GetFullFilePath(identifier)))
            {
                var xs = new XmlSerializer(typeof(T));
                using (var sr = new StreamReader(GetFullFilePath(identifier)))
                {
                    rslt = (T)xs.Deserialize(sr);

                    return (T)rslt;
                }
            }
            else return default(T);
            
        }
    }
}
