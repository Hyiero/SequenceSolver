using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace Contexts
{
    public class GuiContextView : ContextView
    {
        // Use this for initialization
        void Awake()
        {
            context = new GuiContext(this, true);
            context.Start();
        }
    }
}
