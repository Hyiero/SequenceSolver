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
        public IPlayerView view { get; set; }

        [Inject]
        public IInjectionBinder injectionBinder { get; set; }

        [Inject]
        public PlayersTargetPositionResponseSignal playersTargetPositionResponse { get; set; }

        [Inject]
        public UpdatePlayerCurrentPositionSignal updatePlayerCurrentPosition { get; set; }

        [Inject]
        public PlayerIsOutOfMovesSignal outOfMoves { get; set; }
        #endregion

        public override void OnRegister()
        {
            view.Init();
            view.movePlayer.AddListener(MovePlayer);
            view.requestTargetPosition.AddListener(RequestTargetPositionForPlayer);
            view.updateCurrentPosition.AddListener(DispatchCurrentPosition);
            playersTargetPositionResponse.AddListener(UpdatePlayersTargetPosition);
            outOfMoves.AddListener(PlayerIsOutOfMoves);
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

        private void PlayerIsOutOfMoves()
        {
            view.SetOutOfMoves();
        }
        
        private void DispatchCurrentPosition(Vector3 currentPosition)
        {
            updatePlayerCurrentPosition.Dispatch(currentPosition);
        }
    }
}