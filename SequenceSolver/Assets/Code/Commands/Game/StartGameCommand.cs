using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Managers;
using Services;
using Signals;

namespace Commands
{
    public class StartGameCommand : Command
    {
        [Inject]
        public ISequenceService sequenceService { get; set; }

        [Inject]
        public IGameManager GameManager { get; set; }

        public override void Execute()
        {
            Debug.Log("Setting up service");
            GameManager.Init();
            sequenceService.SetCurrentSequence(0);
        }
    }
}