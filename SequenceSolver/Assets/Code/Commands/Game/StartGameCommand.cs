﻿using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Managers;

namespace Commands
{
    public class StartGameCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("First Level has official started");
        }
    }
}