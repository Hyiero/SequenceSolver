using UnityEngine;
using System.Collections;

namespace Models
{
    public class Movement
    {
        public GameObject CurrentGameObject { get; set; }
        public Vector3 TargetPosition { get; set; }
        public float MovementLeft { get; set; }
        public float PlayerSpeed { get; set; }
    }
}