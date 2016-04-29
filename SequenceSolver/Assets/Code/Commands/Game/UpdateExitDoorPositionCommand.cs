using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Managers;

namespace Commands
{
    public class UpdateExitDoorPositionCommand : Command
    {
        [Inject]
        public Vector3 exitDoorPosition { get; set; }

        [Inject]
        public IWinConditionManager levelManager { get; set; }

        public override void Execute()
        {
            levelManager.SetExitDoorPosition(exitDoorPosition);
        }
    }
}
