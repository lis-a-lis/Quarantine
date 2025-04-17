using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.AssetsManagement
{
    public class AssetsProvider : IAssetsProvider
    {
        public TPrefab LoadPrefab<TPrefab>(string path) where TPrefab : MonoBehaviour
        {
            return Resources.Load<TPrefab>(path);
        }

        public TScriptableObject LoadScriptableObject<TScriptableObject>(string path) where TScriptableObject : ScriptableObject
        {
            return Resources.Load<TScriptableObject>(path);
        }

        public async UniTask<TPrefab> LoadPrefabAsync<TPrefab>(string path) where TPrefab : MonoBehaviour
        {
            ResourceRequest request = Resources.LoadAsync<TPrefab>(path);

            await request.ToUniTask();
            
            if (request.asset is TPrefab asset)
                return asset;

            throw new InvalidOperationException();
        }
    }
}