using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;
using Signals;
using System;
using strange.extensions.injector.api;

namespace Mediators
{
    public class ExitTileMediator : EventMediator
    {
        #region Injections
        [Inject]
        public ExitTileView view { get; set; }

        [Inject]
        public UpdateExitDoorPositionSignal updateExitDoorPosition { get; set; }

        [Inject]
        public RemoveLockFromDoorSignal removeLockFromDoor { get; set; }

        [Inject]
        public SetLocksOnDoorSignal setLocksOnDoor { get; set; }

        [Inject]
        public IInjectionBinder injectionBinder { get; set; }
        #endregion

        private string spriteName = "Sprites/UnlockedExit";

        public override void OnRegister()
        {
            view.Init(spriteName);
            view.myPosition.AddListener(SendOutExitDoorPosition);
            view.exitUnlocked.AddListener(UnlockTheExitDoor);
            removeLockFromDoor.AddListener(RemoveOneLockFromDoor);
            setLocksOnDoor.AddListener(SetLocksOnExitDoor);
            //Need To Set Locks on Door Initially
            //SetLocksOnExitDoor(GameObject.FindObjectsOfType<KeyTileView>().Length);
        }

        private void UnlockTheExitDoor()
        {
            UnlockTheExitSignal unlockTheExit = (UnlockTheExitSignal)injectionBinder.GetInstance<UnlockTheExitSignal>();
            unlockTheExit.Dispatch();
        }

        private void SetLocksOnExitDoor(int lockTotal)
        {
            view.SetLocksOnDoor(lockTotal);
        }

        private void RemoveOneLockFromDoor()
        {
            view.RemoveALockOnDoor();
        }

        private void SendOutExitDoorPosition(Vector3 pos)
        {
            updateExitDoorPosition.Dispatch(pos);
        }
    }
}