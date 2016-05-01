using UnityEngine;
using System.Collections;

namespace Services
{
    public interface ISequenceService
    {
        int GetCurrentValueOfSequence();
        void MoveToNextPositionInSequence();
        void SetCurrentSequence(int currentLevel);
    }
}