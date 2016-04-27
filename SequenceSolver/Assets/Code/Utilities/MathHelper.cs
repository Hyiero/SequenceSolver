using UnityEngine;
using System.Collections;

namespace Util
{
    public class MathHelper : IMathHelper
    {
        public Vector3 RoundVector3ToNearestTenth(Vector3 position)
        {
            Vector3 roundedPosition = new Vector3(Mathf.Round(position.x * 10) / 10, Mathf.Round(position.y * 10) / 10, Mathf.Round(position.z * 10) / 10);
            return roundedPosition;
        }
    }
}