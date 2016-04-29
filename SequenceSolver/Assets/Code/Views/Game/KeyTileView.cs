using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Util;
using strange.extensions.signal.impl;

namespace Views
{
    public class KeyTileView : TileView
    {
        public Signal unlock { get; set; }

        public override void Init(string spriteName)
        {
            base.Init(spriteName);
            unlock = new Signal();
        }

        void OnTriggerStay(Collider col)
        {
            if (playersCurrentPosition == myRoundedPosition && spriteRenderer.sprite != activatedSprite)
                ActivateFloorSwitch();
        }

        private void ActivateFloorSwitch()
        {
            unlock.Dispatch();
            spriteRenderer.sprite = activatedSprite;
        }
    }
}