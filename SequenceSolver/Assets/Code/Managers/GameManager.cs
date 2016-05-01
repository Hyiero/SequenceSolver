using UnityEngine;
using System.Collections;
using System;

namespace Managers
{
    public class GameManager : IGameManager
    {
        private int numberOfLivesLeft { get; set; }
        private int timeRemainingOnLevel { get; set; }
        private int totalScore { get; set; }
        private int scoreForCurrentLevel { get; set; }

        public void Init()
        {
            SetNumberOfLivesLeft(5);
        }

        public void SetNumberOfLivesLeft(int livesLeft)
        {
            numberOfLivesLeft = livesLeft;
        }

        public int GetNumberOfLivesLeft()
        {
            return numberOfLivesLeft;
        }
    }
}