using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;
using Signals;
using strange.extensions.injector.api;

namespace Mediators
{
    public class KeyTileMediator : EventMediator
    {
        #region Injections
        [Inject]
        public KeyTileView view { get; set; }

        [Inject]
        public UpdatePlayerCurrentPositionSignal updatePlayerCurrentPosition { get; set; }

        [Inject]
        public IInjectionBinder injectionBinder { get; set; }
        #endregion

        private string activatedSpriteName = "Sprites/PressedActivateDoorFloor";

        public override void OnRegister()
        {
            view.Init(activatedSpriteName);
            view.unlock.AddListener(DispatchUnlockSignal);
            updatePlayerCurrentPosition.AddListener(UpdatePlayersPosition);
        }

        private void UpdatePlayersPosition(Vector3 currentPosition)
        {
            view.UpdatePlayersCurrentPosition(currentPosition);
        }

        private void DispatchUnlockSignal()
        {
            UnlockALockSignal unlockALock = (UnlockALockSignal)injectionBinder.GetInstance<UnlockALockSignal>();
            unlockALock.Dispatch();
        }
    }
}