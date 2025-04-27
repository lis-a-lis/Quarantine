using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Infrastructure.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string name, Func<bool> activationCondition, Action<float> onProgressChanged, Action onLoaded)
        {
            Debug.Log("Loading Scene " + name);
            LoadSceneAsync(name, activationCondition, onProgressChanged, onLoaded).Forget();
        }

        private async UniTaskVoid LoadSceneAsync(string name, Func<bool> activationCondition,
            Action<float> onProgressChanged, Action onLoaded)
        {
            await UniTask.Yield();

            AsyncOperation loading = SceneManager.LoadSceneAsync(name);
            
            if (loading == null)
                throw new NullReferenceException();
                
            loading.allowSceneActivation = false;

            //await UniTask.Delay(2000);
            
            while (!loading.isDone)
            {
                if (loading.progress <= 0.9f)
                    onProgressChanged?.Invoke(loading.progress / 0.9f);

                if (loading.progress >= 0.9f)
                {
                    if (activationCondition.Invoke())
                    {
                        loading.allowSceneActivation = true;
                        
                        await UniTask.Yield();
                        
                        onLoaded?.Invoke();
                    }
                }

                Debug.Log(loading.progress);
                
                await UniTask.Yield();
            }
        }
    }
}