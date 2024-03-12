using System;
using System.IO;
using UnityEngine;

namespace FirePaw
{
    namespace SaveSystem
    {
        [Serializable]
        public class Saver<T>
        {
            public T data;

            public static void TryLoad(string filename, ref T m_CompletitionData)
            {
                var path = FileHandler.Path(filename);
                if (File.Exists(path))
                {
                    var datastring = File.ReadAllText(path);
                    var saver = JsonUtility.FromJson<Saver<T>>(datastring);
                    m_CompletitionData = saver.data;
                }
            }

            public static void Save(string filename, T data)
            {
                var wrapper = new Saver<T> { data = data };
                var dataString = JsonUtility.ToJson(wrapper);
                File.WriteAllText(FileHandler.Path(filename), dataString);
            }
        }

        public static class FileHandler
        {
            public static void Reset(string filename)
            {
                var path = FileHandler.Path(filename);
                if (File.Exists(path)) File.Delete(path);
            }

            public static string Path(string filename) { return $"{Application.persistentDataPath}/{filename}"; }
            public static bool HasFile(string filename) { return File.Exists(Path(filename)); }
        }
    }
}