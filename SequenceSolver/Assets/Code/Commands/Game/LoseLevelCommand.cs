using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Managers;
using Signals;

namespace Commands
{
    public class LoseLevelCommand : Command
    {
        [Inject]
        public IGameManager gameManager { get; set; }

        public override void Execute()
        {
            if(gameManager.retriesActive)
            {
                gameManager.RemoveRetry();
                if(gameManager.GetNumberOfRetriesLeft() != 0)
                {
                    Debug.Log("You lost. You have " + gameManager.GetNumberOfRetriesLeft() + " retries left");
                }
            }
            else
            {
                Debug.Log("Pop up retry message because we can retry forever!");
            }
        }
    }
}