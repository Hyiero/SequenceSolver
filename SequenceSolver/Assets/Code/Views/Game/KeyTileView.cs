using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace Views
{
    public class KeyTileView : View,IKeyTileView
    {
        public void Init()
        {
            Debug.Log("We have a key tile in the map");
        }

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("Awake hit in the KeyTileView");
        }
    }
}