using _Quarantine.Code.Infrastructure.Root.Bootstrap;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Quarantine.Code.Infrastructure.Root
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Start()
        {
            GameRunningMode mode = GameRunningMode.PlayerAccessMode;
            
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name != Scenes.Menu)
                mode = GameRunningMode.ActiveSceneDebugMode;
#endif
            
            IBootstrapper bootstrapper = new VContainerBootstrapper();
            bootstrapper.Run(mode);
        }
    }
}