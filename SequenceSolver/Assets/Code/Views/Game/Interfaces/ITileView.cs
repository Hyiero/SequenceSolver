using UnityEngine;
using System.Collections;

namespace Views
{
    public interface ITileView
    {
        void Init();
        void UpdatePlayersCurrentPosition(Vector3 currentPosition);
    }
}