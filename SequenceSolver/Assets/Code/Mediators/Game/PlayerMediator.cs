using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;
using Models;
using Signals;

namespace Mediators
{
    public class PlayerMediator : EventMediator
    {
        [Inject]
        public IPlayerView view { get; set; }

        [Inject]
        public MovePlayerSignal movePlayer { get; set; }

        [Inject]
        public RequestPlayersTargetPositionSignal requestPlayersTargetPosition { get; set; }

        [Inject]
        public PlayersTargetPositionResponseSignal playersTargetPositionResponse { get; set; }

        [Inject]
        public PlayerIsOutOfMovesSignal outOfMoves { get; set; }

        public override void OnRegister()
        {
            view.Init();
            view.movePlayer.AddListener(MovePlayer);
            view.requestTargetPosition.AddListener(RequestTargetPositionForPlayer);
            playersTargetPositionResponse.AddListener(UpdatePlayersTargetPosition);
            outOfMoves.AddListener(PlayerIsOutOfMoves);
        }

        private void MovePlayer(Movement playerMovement)
        {
            playerMovement.MovementLeft += (Time.deltaTime * playerMovement.PlayerSpeed);
            movePlayer.Dispatch(playerMovement);
        }

        private void RequestTargetPositionForPlayer(PlayerTargetPositionInput playerInfo)
        {
            requestPlayersTargetPosition.Dispatch(playerInfo);
        }

        private void UpdatePlayersTargetPosition(Vector3 targetPosition)
        {
            view.UpdateTargetPosition(targetPosition);
        }

        private void PlayerIsOutOfMoves()
        {
            view.outOfMoves = true;
        }
    }
}