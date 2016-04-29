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

        private int[] currentSequence;
        private int currentPositionInSequence;

        public SequenceService()
        {
            currentSequence = GenerateSequence();
            currentPositionInSequence = 0;
        }

        public int GetPositionInSequeunce()
        {
            return currentPositionInSequence;
        }

        public void MoveToNextPositionInSequence()
        {
            if (currentPositionInSequence < currentSequence.Length)
            {
                currentPositionInSequence++;
                if (currentPositionInSequence == currentSequence.Length)
                    endOfSequence.Dispatch();
            }
        }

        public int GetCurrentValueOfSequence()
        {
            return currentSequence[currentPositionInSequence];
        }

        private int[] GenerateSequence()
        {
            int[] sequence = new int[4] { 1, 1, 1, 3 };
            return sequence;
        }
    }
}