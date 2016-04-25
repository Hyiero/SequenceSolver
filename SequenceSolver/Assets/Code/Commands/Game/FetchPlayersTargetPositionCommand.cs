using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Services;
using Signals;
using Models;

namespace Commands
{
    public class FetchPlayersTargetPositionCommand : Command
    {
        [Inject]
        public ISequenceService sequenceService { get; set; }

        [Inject]
        public PlayerTargetPositionInput playerInfo { get; set; }

        [Inject]
        public PlayersTargetPositionResponseSignal playersTargetPositionResponse { get; set; }

        public override void Execute()
        {
            Vector3 targetPosition = playerInfo.CurrentPosition;
            targetPosition += playerInfo.DirectionOfTravel * sequenceService.GetCurrentValueOfSequence();
            sequenceService.MoveToNextPositionInSequence();
            playersTargetPositionResponse.Dispatch(targetPosition);
        }
    }
}