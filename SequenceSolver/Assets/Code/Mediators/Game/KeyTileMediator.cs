using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class KeyTileMediator : EventMediator
    {
        [Inject]
        public IKeyTileView view { get; set; }

        public override void OnRegister()
        {
            view.Init();
        }

        public override void OnEnabled()
        {
            Debug.Log("On Enabled in Mediator hit");
        }
    }
}