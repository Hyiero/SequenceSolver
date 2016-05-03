using UnityEngine;
using System.Collections;
using Signals;
using Commands;
using Views;
using Mediators;
using Services;
using Util;
using Managers;
using strange.extensions.context.impl;

namespace Contexts
{
    public class GuiContext : MVCSContext
    {
        public GuiContext(MonoBehaviour contextView,bool autoMap) : base(contextView,autoMap) { }

        public override void Launch()
        {
            base.Launch();
            Debug.Log("Gui Context is Launched");
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<UpdateCurrentSequenceSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<UpdateCurrentPositionInSequenceSignal>().ToSingleton().CrossContext();

            mediationBinder.Bind<DisplayInformationView>().To<DisplayInformationMediator>();
        }

    }
}