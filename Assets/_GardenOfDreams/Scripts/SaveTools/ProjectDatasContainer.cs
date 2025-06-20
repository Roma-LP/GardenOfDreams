using System;
using System.Collections.Generic;
using System.IO;
using _GardenOfDreams.Scripts.Interfaces;
using _GardenOfDreams.Scripts.SaveTools.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace _GardenOfDreams.Scripts.SaveTools
{
    public class ProjectDatasContainer : IDisposable
    {
        private const string SAVE_FILE_NAME = "save.json";
        
        private Dictionary<Type, IProgressData> _components = new();
        private static string SavePath => 
            #if UNITY_EDITOR
            Path.Combine(Application.dataPath, SAVE_FILE_NAME);
            #else 
            Path.Combine(Application.persistentDataPath, SaveFileName);
        #endif

        public PlayerData PlayerData { private set; get; }

        public void Init()
        {
            ReinitializeData();

            LoadGame();
        }

        private void ReinitializeData()
        {
            PlayerData = new PlayerData();

            Register(PlayerData);
        }

        private void Register<T>(ProgressData<T> component) where T : class, new()
        {
            _components[typeof(T)] = component;
        }

        private void Unregister<T>(ProgressData<T> component) where T : class, new()
        {
            _components.Remove(typeof(T));
        }

        private void SaveGame()
        {
            Dictionary<string, object> saveData = new Dictionary<string, object>();

            foreach (var pair in _components)
            {
                object data = pair.Value.GetProgressModel();
                saveData[pair.Key.AssemblyQualifiedName] = data;
            }

            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(SavePath, json);
            Debug.Log("Game saved to: " + SavePath);
        }

        private void LoadGame()
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("No save file found.");
                return;
            }

            string json = File.ReadAllText(SavePath);
            Dictionary<string, JObject> rawData = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);

            foreach (var pair in _components)
            {
                if (rawData.TryGetValue(pair.Key.AssemblyQualifiedName, out JObject jObj))
                {
                    var data = jObj.ToObject(pair.Key);
                    pair.Value.SetProgressModel(data);
                }
            }

            Debug.Log("Game loaded.");
        }

        public void Dispose()
        {
            SaveGame();
        }
    }
}