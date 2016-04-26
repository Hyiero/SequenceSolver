using UnityEngine;
using System.Collections;

namespace Views
{
    public interface IKeyTileView
    {
        void Init();
        void UpdatePlayersTargetPosition(Vector3 targetPosition);
    }
}