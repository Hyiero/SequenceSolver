using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Signals;

namespace Services
{
    public class SequenceService : ISequenceService
    {
        [Inject]
        public EndOfSequenceSignal endOfSequence { get; set; }

        [Inject]
        public UpdateCurrentSequenceSignal updateCurrentSequence { get; set; }

        [Inject]
        public UpdateCurrentPositionInSequenceSignal updateCurrentPositionInSequence { get; set; }

        private int[] currentSequence;
        private int currentPositionInSequence;

        public void MoveToNextPositionInSequence()
        {
            //TODO:We should be be hooking into updateCurrentPosition in Sequeunce in order for us to update the GUI when move has been made
            if (currentPositionInSequence < currentSequence.Length)
            {
                currentPositionInSequence++;
                if (currentPositionInSequence == currentSequence.Length)
                    endOfSequence.Dispatch();
                else
                    updateCurrentPositionInSequence.Dispatch(currentPositionInSequence);
            }
        }

        public int GetCurrentValueOfSequence()
        {
            return currentSequence[currentPositionInSequence];
        }

        public void SetCurrentSequence(int currentLevel)
        {
            currentSequence = GenerateSequence();
            updateCurrentSequence.Dispatch(currentSequence);
            currentPositionInSequence = 0;
            updateCurrentPositionInSequence.Dispatch(currentPositionInSequence);
        }

        private int[] GenerateSequence()
        {
            int[] sequence = new int[4] { 1, 1, 1, 3 };
            return sequence;
        }
    }
}