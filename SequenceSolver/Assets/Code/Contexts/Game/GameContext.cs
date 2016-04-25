using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using Signals;
using Commands;
using Views;
using Mediators;

namespace Contexts
{
    public class GameContext : MVCSContext
    {
        private PlayerView player { get; set; }

        public GameContext(MonoBehaviour contextView, bool autoMap) : base(contextView,autoMap) { }

        #region Needed for the user of Signals
        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }

        public override void Launch()
        {
            base.Launch();
            StartFirstLevelSignal startFirstLevelSig = (StartFirstLevelSignal)injectionBinder.GetInstance<StartFirstLevelSignal>();
            startFirstLevelSig.Dispatch();
        }
        #endregion

        protected override void mapBindings()
        {
            GetGameObjectReferences();

            injectionBinder.Bind<IPlayerView>().ToValue(player);

            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();

            commandBinder.Bind<StartFirstLevelSignal>().To<StartGameCommand>();
        }

        private void GetGameObjectReferences()
        {
            player = GameObject.FindObjectOfType<PlayerView>();
        }
    }
}