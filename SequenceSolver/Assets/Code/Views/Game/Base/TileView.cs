using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using Util;

namespace Views
{
    public class TileView : View
    {
        [Inject]
        public IMathHelper mathHelper { get; set; }

        protected Vector3 playersCurrentPosition;
        protected Vector3 myRoundedPosition;
        protected Sprite activatedSprite { get; set; }
        protected SpriteRenderer spriteRenderer { get; set; }

        public virtual void Init(string spriteName)
        {
            myRoundedPosition = mathHelper.RoundVector3ToNearestTenth(this.gameObject.transform.position);
            activatedSprite = Resources.Load<Sprite>(spriteName);
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        public void UpdatePlayersCurrentPosition(Vector3 currentPosition)
        {
            playersCurrentPosition = mathHelper.RoundVector3ToNearestTenth(currentPosition);
        }
    }
}