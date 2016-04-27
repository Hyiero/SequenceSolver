using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Signals;
using Models;
using Services;

namespace Commands
{
    public class MovePlayerCommand : Command
    {
        [Inject]
        public Movement playerMovement { get; set; }

        [Inject]
        public ISequenceService sequenceService { get; set; }

        public override void Execute()
        {          
            Vector3 playerPosition = playerMovement.CurrentGameObject.transform.position;
            float lerpTime = playerMovement.MovementLeft;
            playerMovement.CurrentGameObject.transform.position = Vector3.Lerp(playerPosition, playerMovement.TargetPosition, lerpTime);
        }
    }
}