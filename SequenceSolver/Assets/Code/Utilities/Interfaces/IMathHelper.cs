using UnityEngine;
using System.Collections;

namespace Util
{
    public interface IMathHelper
    {
        Vector3 RoundVector3ToNearestTenth(Vector3 position);
    }
}