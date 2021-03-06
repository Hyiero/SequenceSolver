﻿using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Views
{
    public interface IKeyTileView : ITileView
    {
        Signal unlock { get; set; }
    }
}