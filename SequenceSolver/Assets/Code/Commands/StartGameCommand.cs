using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;

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