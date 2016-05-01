﻿using UnityEngine;
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
        [Inject]
        public DisplayInformationView view { get; set; }

        [Inject]
        public UpdateCurrentSequenceSignal updateCurrentSequenceSig { get; set; }

        [Inject]
        public UpdateCurrentPositionInSequenceSignal updateCurrentPositionInSequence { get; set; }

        [Inject]
        public UpdateNumberOfLivesLeftGUISignal updateNumberOfLivesLeftSignal { get; set; }

        public override void OnRegister()
        {
            view.Init();
            updateCurrentSequenceSig.AddListener(SetCurrentSequence);
            updateCurrentPositionInSequence.AddListener(UpdatePosition);
            updateNumberOfLivesLeftSignal.AddListener(UpdateLivesLeft);
        }

        private void UpdateLivesLeft(int lives)
        {
            view.UpdateLivesLeftText(lives);
        }

        private void SetCurrentSequence(int[] sequence)
        {
            view.SetCurrentSequence(sequence);
        }

        private void UpdatePosition(int position)
        {
            view.SetCurrentPositionInSequence(position);
        }
    }
}