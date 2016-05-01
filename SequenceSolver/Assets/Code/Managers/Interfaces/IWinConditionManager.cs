using UnityEngine;
using System.Collections;

namespace Managers
{
    public interface IWinConditionManager
    {
        void SetExitDoorPosition(Vector3 exitPos);
        void SetPlayersCurrentPosition(Vector3 playerPos);
        void SetLocksOnDoor();
        bool IsPlayerOutOfMoves();
        void TogglePlayerOutOfMoves();
        void ToggleDoorUnlocked();
        bool DidPlayerWin();
    }
}