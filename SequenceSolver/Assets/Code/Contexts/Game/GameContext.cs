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
using Util;
using Managers;

namespace Contexts
{
    public class GameContext : MVCSContext
    {
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
            #region Singletons and Signals that are disptached from anywhere
            injectionBinder.Bind<PlayersTargetPositionResponseSignal>().ToSingleton();
            injectionBinder.Bind<PlayerIsOutOfMovesSignal>().ToSingleton(); 
            injectionBinder.Bind<UpdatePlayerCurrentPositionSignal>().ToSingleton();
            injectionBinder.Bind<RemoveLockFromDoorSignal>().ToSingleton();
            injectionBinder.Bind<EndOfSequenceSignal>().ToSingleton();
            injectionBinder.Bind<SetLocksOnDoorSignal>().ToSingleton();

            injectionBinder.Bind<ISequenceService>().To<SequenceService>().ToSingleton();
            injectionBinder.Bind<IMathHelper>().To<MathHelper>().ToSingleton();
            injectionBinder.Bind<IGameManager>().To<GameManager>().ToSingleton();
            injectionBinder.Bind<IWinConditionManager>().To<WinConditionManager>().ToSingleton();
            #endregion

            //TODO: Take out binding to value in this part. Instead load the resources in a gameService and then bind them to value when we have loaded the "map".
            //TODO: We will also unbind them upon loading the next map and then rebind them when we load up the next level. To make sure the objects come in fresh.
            //Check to see if tovalue will work if we have multiple keytile views but we put each one in one at a time with a foreach loop.


            mediationBinder.Bind<PlayerView>().To<PlayerMediator>();
            mediationBinder.Bind<KeyTileView>().To<KeyTileMediator>();
            mediationBinder.Bind<ExitTileView>().To<ExitTileMediator>();
            //mediationBinder.Bind<DisplayInformationView>().To<DisplayInformationMediator>();

            commandBinder.Bind<StartFirstLevelSignal>().To<StartGameCommand>();
            commandBinder.Bind<MovePlayerSignal>().To<MovePlayerCommand>();
            commandBinder.Bind<RequestPlayersTargetPositionSignal>().To<FetchPlayersTargetPositionCommand>();
            commandBinder.Bind<UnlockALockSignal>().To<UnlockALockCommand>();
            commandBinder.Bind<UpdateExitDoorPositionSignal>().To<UpdateExitDoorPositionCommand>();
            commandBinder.Bind<UpdatePlayerCurrentPositionSignal>().To<UpdatePlayersCurrentPositionCommand>();
            commandBinder.Bind<EndOfSequenceSignal>().To<EndOfSequenceCommand>();
            commandBinder.Bind<UnlockTheExitSignal>().To<UnlockExitDoorCommand>();
        }
    }
}