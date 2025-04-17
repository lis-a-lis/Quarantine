using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameStates;
using _Quarantine.Code.Infrastructure.Root.UI;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using _Quarantine.Code.Infrastructure.Services.EntitiesCreation;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.Services.SceneLoading;
using _Quarantine.Code.Infrastructure.Services.UI;
using VContainer;
using VContainer.Unity;

namespace _Quarantine.Code.Infrastructure.Root.Bootstrap
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<UIRootFactory>(Lifetime.Transient);
            builder.Register<UIRoot>(resolver => resolver.Resolve<UIRootFactory>().Create(), Lifetime.Singleton);
            
            builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
            builder.Register<IAssetsProvider, AssetsProvider>(Lifetime.Singleton);
            builder.Register<IItemDatabaseService, ItemDatabaseService>(Lifetime.Singleton);
            builder.Register<IProgressSaveLoadService, JsonUtilityProgressSaveLoadService>(Lifetime.Singleton);
            builder.Register<MainMenuFactory>(Lifetime.Transient);
            builder.Register<IEntitiesFactory, EntitiesFactory>(Lifetime.Transient);
            
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MainMenuState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);
            builder.Register<SetupState>(Lifetime.Singleton);
            builder.Register<ProgressLoadingState>(Lifetime.Singleton);
        }
    }
}