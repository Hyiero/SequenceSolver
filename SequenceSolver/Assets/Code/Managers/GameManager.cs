using UnityEngine;
using System.Collections;
using System;

namespace Managers
{
    public class GameManager : IGameManager
    {
        private int numberOfRetriesLeft { get; set; }
        private int timeRemainingOnLevel { get; set; }
        private int totalScore { get; set; }
        private int scoreForCurrentLevel { get; set; }
        
        public bool retriesActive { get; set; }

        public void Init()
        {
            //TODO: Check to see if PC, if PC then retries will not be active.
            retriesActive = true;
            if (retriesActive)
            {
                SetNumberOfRetriesLeft(5);
            }
        }

        public void SetNumberOfRetriesLeft(int livesLeft)
        {
            numberOfRetriesLeft = livesLeft;
        }

        public int GetNumberOfRetriesLeft()
        {
            return numberOfRetriesLeft;
        }

        public void RemoveRetry()
        {
            numberOfRetriesLeft--;
        }

        public void AddRetries(int retriesToAdd)
        {

        }
    }
}