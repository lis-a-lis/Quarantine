using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.AssetsManagement
{
    public interface IAssetsProvider
    {
        public TPrefab LoadPrefab<TPrefab>(string path) where TPrefab : MonoBehaviour;
        public TScriptableObject LoadScriptableObject<TScriptableObject>(string path) where TScriptableObject : ScriptableObject;
        public UniTask<TPrefab> LoadPrefabAsync<TPrefab>(string path) where TPrefab : MonoBehaviour;
    }
}