using System;
using System.IO;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public class JsonUtilityProgressSaveLoadService : IProgressSaveLoadService
    {
        private const string SaveName = "_save_Quarantine";

        private GameProgress _lastLoadedLastLoadedProgress = new GameProgress();
        
        public GameProgress LastLoadedProgress => _lastLoadedLastLoadedProgress;
        
        public async UniTaskVoid Save(GameProgress progress, Action onComplete = null)
        {
            string json = JsonUtility.ToJson(progress);

            StreamWriter writer = new StreamWriter(GetPathToSave(SaveName), false);
         
            await writer.WriteAsync(json);
            
            writer.Close();
            
            onComplete?.Invoke();
        }

        public async UniTaskVoid Load(Action<GameProgress> onComplete)
        {
            string path = GetPathToSave(SaveName);

            if (!File.Exists(path))
            {
                Debug.Log($"File {path} does not exist");
                onComplete?.Invoke(new GameProgress());
                return;
            }
            
            StreamReader reader = new StreamReader(path);       
            
            string json = await reader.ReadToEndAsync();
            
            reader.Close();
            
            _lastLoadedLastLoadedProgress = JsonUtility.FromJson<GameProgress>(json);
            Debug.Log(_lastLoadedLastLoadedProgress);
            Debug.Log("Progress loaded");
            onComplete?.Invoke(LastLoadedProgress);
        }
        
        private string GetPathToSave(string saveName) =>
            $"{Application.persistentDataPath}/{saveName}.json";
    }
}