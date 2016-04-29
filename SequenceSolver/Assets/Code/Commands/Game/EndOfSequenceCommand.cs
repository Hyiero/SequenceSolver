﻿using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Managers;

namespace Commands
{
    public class EndOfSequenceCommand : Command
    {
        [Inject]
        public IWinConditionManager levelManager { get; set; }

        public override void Execute()
        {
            levelManager.TogglePlayerOutOfMoves();
        }
    }
}