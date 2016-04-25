using UnityEngine;
using System.Collections;

namespace Services
{
    public interface ISequenceService
    {
        int GetPositionInSequeunce();
        void MoveToNextPositionInSequence();
        int GetCurrentValueOfSequence();
    }
}