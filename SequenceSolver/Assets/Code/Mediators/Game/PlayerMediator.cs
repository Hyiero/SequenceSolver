using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;
using Models;
using Signals;
using strange.extensions.injector.api;

namespace Mediators
{
    public class PlayerMediator : EventMediator
    {

        #region Injectors
        [Inject]
        public PlayerView view { get; set; }

        [Inject]
        public IInjectionBinder injectionBinder { get; set; }

        [Inject]
        public PlayersTargetPositionResponseSignal playersTargetPositionResponse { get; set; }

        [Inject]
        public UpdatePlayerCurrentPositionSignal updatePlayerCurrentPosition { get; set; }

        [Inject]
        public EndOfSequenceSignal endOfSequence { get; set; }
        #endregion

        private bool notToldAnyonePlayerIsDoneMoving;

        public override void OnRegister()
        {
            view.Init();
            view.movePlayer.AddListener(MovePlayer);
            view.requestTargetPosition.AddListener(RequestTargetPositionForPlayer);
            view.updateCurrentPosition.AddListener(DispatchCurrentPosition);
            view.doneMoving.AddListener(PlayerIsAtMoveLimit);
            view.fellOff.AddListener(PlayerLostALife);
            playersTargetPositionResponse.AddListener(UpdatePlayersTargetPosition);
            endOfSequence.AddListener(SquenceIsAtTheEnd);
            notToldAnyonePlayerIsDoneMoving = true;
        }

        private void MovePlayer(Movement playerMovement)
        {
            playerMovement.MovementLeft += (Time.deltaTime * playerMovement.PlayerSpeed);
            MovePlayerSignal movePlayer = (MovePlayerSignal)injectionBinder.GetInstance<MovePlayerSignal>();
            movePlayer.Dispatch(playerMovement);
        }

        private void RequestTargetPositionForPlayer(PlayerTargetPositionInput playerInfo)
        {
            RequestPlayersTargetPositionSignal requestPlayersTargetPosition = (RequestPlayersTargetPositionSignal)injectionBinder.GetInstance<RequestPlayersTargetPositionSignal>();
            requestPlayersTargetPosition.Dispatch(playerInfo);
        }

        private void UpdatePlayersTargetPosition(Vector3 targetPosition)
        {
            view.UpdateTargetPosition(targetPosition);
        }

        private void SquenceIsAtTheEnd()
        {
            view.SetOutOfMoves();
        }
        
        private void DispatchCurrentPosition(Vector3 currentPosition)
        {
            updatePlayerCurrentPosition.Dispatch(currentPosition);
        }

        private void PlayerIsAtMoveLimit()
        {
            if(notToldAnyonePlayerIsDoneMoving)
            {
                notToldAnyonePlayerIsDoneMoving = false;
                Debug.Log("Find out if congratz or game over");
            }
        }

        private void PlayerLostALife()
        {
            LoseLevelSignal loseLife = (LoseLevelSignal)injectionBinder.GetInstance<LoseLevelSignal>();
            loseLife.Dispatch();
        }
    }
}