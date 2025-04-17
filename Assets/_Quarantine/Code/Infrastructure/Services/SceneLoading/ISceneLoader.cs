using System;

namespace _Quarantine.Code.Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        public void LoadScene(string name, Func<bool> activationCondition, Action<float> onProgressChanged, Action onLoaded);
    }
}