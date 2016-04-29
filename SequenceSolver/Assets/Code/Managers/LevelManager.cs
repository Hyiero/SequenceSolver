using UnityEngine;
using System.Collections;
using Signals;
using Views;
using Util;

namespace Managers
{
    public class WinConditionManager : IWinConditionManager
    {
        [Inject]
        public SetLocksOnDoorSignal setLocksOnDoor { get; set; }

        [Inject]
        public IMathHelper mathHelper { get; set; }

        private Vector3 exitDoorPosition { get; set; }
        private Vector3 playerCurrentPosition { get; set; }
        private bool playerIsOutOfMoves { get; set; }
        private bool doorUnlocked { get; set; }

        public WinConditionManager()
        {
            exitDoorPosition = Vector3.zero;
            playerIsOutOfMoves = false;
            doorUnlocked = false;
        }

        public void SetExitDoorPosition(Vector3 exitPos)
        {
            exitDoorPosition = exitPos;
            Debug.Log("Exit Position is at: " + exitDoorPosition);
        }

        public void SetPlayersCurrentPosition(Vector3 playerPos)
        {
            playerCurrentPosition = mathHelper.RoundVector3ToNearestTenth(playerPos);
            Debug.Log("Player Position is at: " + playerPos);
        }

        public bool IsPlayerOutOfMoves()
        {
            return playerIsOutOfMoves;
        }

        public void TogglePlayerOutOfMoves()
        {
            playerIsOutOfMoves = !playerIsOutOfMoves;
            Debug.Log("Player is out of moves");
        }
        
        public void ToggleDoorUnlocked()
        {
            doorUnlocked = !doorUnlocked;
            Debug.Log("Door is Unlocked");
        }

        public void SetLocksOnDoor()
        {
            int locksOnDoor = GameObject.FindObjectsOfType<KeyTileView>().Length;
            Debug.Log(locksOnDoor+" lock(s) set");
            setLocksOnDoor.Dispatch(locksOnDoor);
        }

        public bool DidPlayerWin()
        {
            if ((doorUnlocked && playerIsOutOfMoves) && (playerCurrentPosition == exitDoorPosition))
            {
                Debug.Log("Next Level");
                return true;
            }
            else
            {
                Debug.Log("Game Over");
                return false;
            }
        }
    }
}