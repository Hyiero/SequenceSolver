using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Views
{
    public class KeyTileView : View,IKeyTileView
    {
        [SerializeField]
        private Vector3 playersTargetPosition;

        [SerializeField]
        private Vector3 myRoundedPosition;

        public void Init()
        {
            Debug.Log("We have a key tile in the map");
            myRoundedPosition = new Vector3(Mathf.Round(this.gameObject.transform.position.x * 10) / 10, Mathf.Round(this.gameObject.transform.position.y * 10) / 10, 0);
        }

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Awake hit in the KeyTileView");
        }

        void OnTriggerStay(Collider col)
        {
            Debug.Log("Your on me");
            if (playersTargetPosition == myRoundedPosition)
                Debug.Log("Players destination is me");
        }

        public void UpdatePlayersTargetPosition(Vector3 targetPosition)
        {
            playersTargetPosition = new Vector3(Mathf.Round(targetPosition.x * 10) / 10, Mathf.Round(targetPosition.y * 10), 0);
        }
    }
}