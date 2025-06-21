using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private bool _shouldSaveOnDispose = true;

        private static string SavePath =>
#if UNITY_EDITOR
            Path.Combine(Application.dataPath, SAVE_FILE_NAME);
#else
            Path.Combine(Application.persistentDataPath, SaveFileName);
#endif

        public PlayerData PlayerData { private set; get; }
        public InventoryData InventoryData { private set; get; }
        public DropItemData DropItemData { private set; get; }

        public ProjectDatasContainer()
        {
            ReinitializeData();

            LoadGame();
        }

        private void ReinitializeData()
        {
            PlayerData = new PlayerData();
            InventoryData = new InventoryData();
            DropItemData = new DropItemData();

            Register(PlayerData);
            Register(InventoryData);
            Register(DropItemData);
        }

        private void Register<T>(ProgressData<T> component) where T : class, new()
        {
            _components[typeof(T)] = component;
        }

        private void RemoveNullValues()
        {
            List<Type> keysToRemove = _components
                .Where(pair => pair.Value.GetProgressModel() == null)
                .Select(pair => pair.Key)
                .ToList();

            foreach (Type key in keysToRemove)
            {
                _components.Remove(key);
            }
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
                Debug.Log("No save file found. New Game");
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

        public void ClearSaveFile()
        {
            if (File.Exists(SavePath))
            {
                string metaPath = SavePath + ".meta";
                File.Delete(SavePath);
                File.Delete(metaPath);
            }

            _shouldSaveOnDispose = false;
        }

        public void Dispose()
        {
            if (_shouldSaveOnDispose)
            {
                RemoveNullValues();
                SaveGame();
            }
        }
    }
}