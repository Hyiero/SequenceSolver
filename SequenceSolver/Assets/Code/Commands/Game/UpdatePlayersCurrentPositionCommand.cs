using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Managers;

namespace Commands
{
    public class UpdatePlayersCurrentPositionCommand : Command
    {
        [Inject]
        public Vector3 playersCurrentPos { get; set; }

        [Inject]
        public IWinConditionManager winConditionManager { get; set; }

        public override void Execute()
        {
            winConditionManager.SetPlayersCurrentPosition(playersCurrentPos);
            if (winConditionManager.IsPlayerOutOfMoves())
                winConditionManager.DidPlayerWin();
        }
    }
}
