using UnityEngine;
using System.Collections;
using strange.extensions.signal.impl;
using Models;

namespace Views
{
    public interface IPlayerView
    {
        Signal<Movement> movePlayer { get; set; }
        Signal<PlayerTargetPositionInput> requestTargetPosition { get; set; }
        Signal<Vector3> updateCurrentPosition { get; set; }

        void Init();
        void UpdateTargetPosition(Vector3 updatedTargetPosition);
        void SetOutOfMoves();
    }
}