using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Views;

namespace Mediators
{
    public class PlayerMediator : EventMediator
    {
        [Inject]
        public IPlayerView view { get; set; }

        public override void OnRegister()
        {
            view.Init();
        }
    }
}