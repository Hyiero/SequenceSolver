using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using Signals;
using Commands;
using Views;
using Mediators;
using Services;

namespace Contexts
{
    public class GameContext : MVCSContext
    {
        #region To be removed when we load the map in a game service
        private PlayerView player { get; set; }
        private KeyTileView keyTile { get; set; }
        #endregion


        private SequenceService sequenceService { get; set; }

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

            #region Singletons and Signals that are disptached from anywhere
            injectionBinder.Bind<MovePlayerSignal>().ToSingleton();
            injectionBinder.Bind<RequestPlayersTargetPositionSignal>().ToSingleton();
            injectionBinder.Bind<PlayersTargetPositionResponseSignal>().ToSingleton();
            injectionBinder.Bind<PlayerIsOutOfMovesSignal>().ToSingleton();
            injectionBinder.Bind<ISequenceService>().ToValue(sequenceService).ToSingleton();
            #endregion

            //TODO: Take out binding to value in this part. Instead load the resources in a gameService and then bind them to value when we have loaded the "map".
            //TODO: We will also unbind them upon loading the next map and then rebind them when we load up the next level. To make sure the objects come in fresh.
            //Check to see if tovalue will work if we have multiple keytile views but we put each one in one at a time with a foreach loop.
            injectionBinder.Bind<IPlayerView>().ToValue(player);
            injectionBinder.Bind<IKeyTileView>().ToValue(keyTile);


            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
            mediationBinder.Bind<KeyTileView>().To<KeyTileMediator>();

            commandBinder.Bind<StartFirstLevelSignal>().To<StartGameCommand>();
            commandBinder.Bind<MovePlayerSignal>().To<MovePlayerCommand>();
            commandBinder.Bind<RequestPlayersTargetPositionSignal>().To<FetchPlayersTargetPositionCommand>();
        }

        private void GetGameObjectReferences()
        {
            player = GameObject.FindObjectOfType<PlayerView>();
            sequenceService = GameObject.FindObjectOfType<SequenceService>();
            keyTile = GameObject.FindObjectOfType<KeyTileView>();
        }
    }
}