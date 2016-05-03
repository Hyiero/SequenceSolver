using UnityEngine;
using System.Collections;

namespace Managers
{
    public interface IGameManager
    {
        bool retriesActive { get; set; }

        void Init();
        void SetNumberOfRetriesLeft(int livesLeft);
        int GetNumberOfRetriesLeft();
        void RemoveRetry();
        void AddRetries(int retriesToAdd);
    }
}