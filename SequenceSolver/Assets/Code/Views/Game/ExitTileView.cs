using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;
using System;

namespace Views
{
    public class ExitTileView : TileView
    {
        public Signal exitUnlocked { get; set; }
        public Signal<Vector3> myPosition { get; set; }

        //TODO: Fetch the number of keytiles and send them in a singal for this to get populated
        private int totalLocksOnDoor { get; set; }

        public override void Init(string spriteName)
        {
            base.Init(spriteName);
            myPosition = new Signal<Vector3>();
            exitUnlocked = new Signal();
        }

        protected override void Start()
        {
            base.Start();
            myPosition.Dispatch(myRoundedPosition);
        }

        public void SetLocksOnDoor(int locks)
        {
            totalLocksOnDoor = locks;
        }

        public void RemoveALockOnDoor()
        {
            totalLocksOnDoor--;
            if (totalLocksOnDoor == 0)
                UnlockDoor();
        }

        private void UnlockDoor()
        {
            spriteRenderer.sprite = activatedSprite;
            exitUnlocked.Dispatch();
        }
    }
}