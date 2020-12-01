using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public static class ConfigLoader
    {
        //Main Token and Prefix variables
        public static string Token { get; private set; } = "";
        public static string Prefix { get; private set; } = "";

        //Path to config file
        public static string path;

        //Called when the program is first loaded
        public static void Load()
        {
            //Sets path string to the Current Directory with a file named "configs.json"
            path = Directory.GetCurrentDirectory() + "/config.json";
            //Prints the path
            Console.WriteLine($"Path: {path}");

            //If the file does not exist then...
            if (!File.Exists(path))
            {
                //Inform the developer that the config doesn't exist
                Console.WriteLine("Config file does not exist. Creating config...");
                //And create a new empty JSON file
                SaveData(new JSONWrapper());

                return;
            }

            //Create a variable that holds the loaded config
            JSONWrapper wrapper = LoadData();

            //Set the Token and Prefix in the Config Loader
            Token = wrapper.data.token;
            Prefix = wrapper.data.prefix;
        }

        //Method that saves inputed JSONWrapper to json file
        public static void SaveData(JSONWrapper wrapper)
        {
            //Create a string of the class passed in
            string content = JsonConvert.SerializeObject(wrapper);
            //Write the string to the path
            File.WriteAllText(path, content);

            //If the prefix from the file is different from the current prefix reset it
            if (wrapper.data.prefix != Prefix)
            {
                Prefix = wrapper.data.prefix;
            }
        }

        //Returns JSONwrapper from fil path
        public static JSONWrapper LoadData()
        {
            //If the file doesn't exists return null 
            if (!File.Exists(path))
            {
                return null;
            }

            //Create a string from the file path
            string content = File.ReadAllText(path);
            //Turn the string into a JSONWrapper class and returns it 
            return JsonConvert.DeserializeObject<JSONWrapper>(content);
        }
    }

    //JSONWrapper class 
    [System.Serializable] //Any class that will be serialized into JSON requires this attribute 
    public class JSONWrapper
    {
        public Data data = new Data();
    }

    //Data class that is held in the JSONWrapper
    [System.Serializable]
    public class Data
    {
        public string token = "";
        public string prefix = "";
    }
}
