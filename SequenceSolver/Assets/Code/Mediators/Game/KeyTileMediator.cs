using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;
using Signals;

namespace Mediators
{
    public class KeyTileMediator : EventMediator
    {
        [Inject]
        public IKeyTileView view { get; set; }

        [Inject]
        public PlayersTargetPositionResponseSignal playersTargetPositionResponse { get; set; }

        public override void OnRegister()
        {
            view.Init();
            playersTargetPositionResponse.AddListener(UpdatePlayersTargetPosition);
        }

        public override void OnEnabled()
        {
            Debug.Log("On Enabled in Mediator hit");
        }

        //TODO: Recieve the signal for the players current position, not his target position
        private void UpdatePlayersTargetPosition(Vector3 targetPosition)
        {
            view.UpdatePlayersTargetPosition(targetPosition);
        }
    }
}