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
        [Inject]
        public IKeyTileView view { get; set; }

        [Inject]
        public UpdatePlayerCurrentPositionSignal updatePlayerCurrentPosition { get; set; }

        [Inject]
        public IInjectionBinder injectionBinder { get; set; }

        public override void OnRegister()
        {
            view.Init();
            view.unlock.AddListener(DispatchUnlockSignal);
            updatePlayerCurrentPosition.AddListener(UpdatePlayersPosition);
        }

        public override void OnEnabled()
        {
            Debug.Log("On Enabled in Mediator hit");
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