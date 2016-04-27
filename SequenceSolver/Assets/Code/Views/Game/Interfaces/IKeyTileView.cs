using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;

namespace Views
{
    public interface IKeyTileView
    {
        Signal unlock { get; set; }

        void Init();
        void UpdatePlayersCurrentPosition(Vector3 currentPosition);
    }
}