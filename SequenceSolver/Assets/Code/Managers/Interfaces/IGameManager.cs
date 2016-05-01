using UnityEngine;
using System.Collections;

namespace Managers
{
    public interface IGameManager
    {
        void Init();
        void SetNumberOfLivesLeft(int livesLeft);
        int GetNumberOfLivesLeft();
    }
}