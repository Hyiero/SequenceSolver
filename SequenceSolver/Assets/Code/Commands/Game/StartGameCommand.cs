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

        [Inject]
        public UpdateNumberOfLivesLeftGUISignal updateNumberOfLivesLeftSignal { get; set; }

        public override void Execute()
        {
            Debug.Log("Setting up service");
            GameManager.Init();
            updateNumberOfLivesLeftSignal.Dispatch(GameManager.GetNumberOfLivesLeft());
            sequenceService.SetCurrentSequence(0);
        }
    }
}