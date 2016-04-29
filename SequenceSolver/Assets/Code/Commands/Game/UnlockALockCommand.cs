using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using Signals;

namespace Commands
{
    public class UnlockALockCommand : Command
    {
        [Inject]
        public RemoveLockFromDoorSignal removeLockFromDoor { get; set; }

        public override void Execute()
        {
            Debug.Log("Unlocking a lock on the door");
            //TODO: Update the number of locks left in the gui as well as update the door with how many locks have been unlocked to this point
            removeLockFromDoor.Dispatch();
        }
    }
}