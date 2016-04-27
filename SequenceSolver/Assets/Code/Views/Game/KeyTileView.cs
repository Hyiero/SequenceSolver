using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Util;
using strange.extensions.signal.impl;

namespace Views
{
    public class KeyTileView : View,IKeyTileView
    {
        [Inject]
        public IMathHelper mathHelper { get; set; }

        public Signal unlock { get; set; }

        [SerializeField]
        private Vector3 playersCurrentPosition;

        [SerializeField]
        private Vector3 myRoundedPosition;
        private Sprite activatedSprite { get; set; }
        private SpriteRenderer spriteRenderer { get; set; }

        public void Init()
        {
            Debug.Log("We have a key tile in the map");
            myRoundedPosition = mathHelper.RoundVector3ToNearestTenth(this.gameObject.transform.position);
            activatedSprite = Resources.Load<Sprite>("Sprites/PressedActivateDoorFloor");
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            unlock = new Signal();
        }

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Awake hit in the KeyTileView");
        }

        void OnTriggerStay(Collider col)
        {
            Debug.Log("Your on me");
            if (playersCurrentPosition == myRoundedPosition && spriteRenderer.sprite != activatedSprite)
                ActivateFloorSwitch();
        }

        public void UpdatePlayersCurrentPosition(Vector3 currentPosition)
        {
            playersCurrentPosition = mathHelper.RoundVector3ToNearestTenth(currentPosition);
        }

        private void ActivateFloorSwitch()
        {
            unlock.Dispatch();
            spriteRenderer.sprite = activatedSprite;
        }
    }
}