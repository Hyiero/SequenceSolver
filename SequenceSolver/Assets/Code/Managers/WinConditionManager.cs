using UnityEngine;
using System.Collections;
using Signals;
using Views;
using Util;

namespace Managers
{
    public class WinConditionManager : IWinConditionManager
    {

        #region Injections
        //TODO: Seperate anything that may need to come out this class.
        [Inject]
        public SetLocksOnDoorSignal setLocksOnDoor { get; set; }

        [Inject]
        public IMathHelper mathHelper { get; set; }

        [Inject]
        public LoseLevelSignal loseLevel { get; set; }
        #endregion

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
        }

        public void SetPlayersCurrentPosition(Vector3 playerPos)
        {
            playerCurrentPosition = mathHelper.RoundVector3ToNearestTenth(playerPos);
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

        public void DidPlayerWin()
        {
            if ((doorUnlocked && playerIsOutOfMoves) && (playerCurrentPosition == exitDoorPosition))
            {
                Debug.Log("Next Level");
                //TODO: Send Signal to move on.
            }
            else
            {
                Debug.Log("Game Over"); //TODO: LossLifeSignal send out here then restart popup.
                loseLevel.Dispatch();
            }
        }
    }
}