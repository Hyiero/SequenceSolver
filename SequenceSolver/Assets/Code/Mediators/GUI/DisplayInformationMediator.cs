using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;
using Services;
using Signals;
using System;

namespace Mediators
{
    public class DisplayInformationMediator : EventMediator
    {
        private int LocksLeft { get; set; }

        #region Injections
        [Inject]
        public DisplayInformationView view { get; set; }

        [Inject]
        public UpdateCurrentSequenceSignal updateCurrentSequenceSig { get; set; }

        [Inject]
        public UpdateCurrentPositionInSequenceSignal updateCurrentPositionInSequence { get; set; }

        [Inject]
        public SetLocksOnDoorSignal setLocksOnDoor { get; set; }

        [Inject]
        public RemoveLockFromDoorSignal removeLockFromDoor { get; set; }
        #endregion

        public override void OnRegister()
        {
            view.Init();
            updateCurrentSequenceSig.AddListener(SetCurrentSequence);
            updateCurrentPositionInSequence.AddListener(UpdatePosition);
            setLocksOnDoor.AddListener(SetInitialLocksLeftDisplay);
            removeLockFromDoor.AddListener(RemoveALockFromDisplay);
        }

        private void SetCurrentSequence(int[] sequence)
        {
            view.SetCurrentSequence(sequence);
        }

        private void UpdatePosition(int position)
        {
            view.SetCurrentPositionInSequence(position);
        }

        private void SetInitialLocksLeftDisplay(int locksLeft)
        {
            LocksLeft = locksLeft;
            view.UpdateCurrentLocksLeftText(locksLeft);
        }

        private void RemoveALockFromDisplay()
        {
            LocksLeft--;
            view.UpdateCurrentLocksLeftText(LocksLeft);
        }
    }
}