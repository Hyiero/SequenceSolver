using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System;
using Models;
using strange.extensions.signal.impl;

namespace Views
{
    public class PlayerView : View,IPlayerView
    {
        private float playerSpeed { get; set; }
        private Movement playerMovement { get; set; }
        private PlayerTargetPositionInput playerInfo { get; set; }

        [SerializeField]
        private Vector3 currentPositon;
        [SerializeField]
        private Vector3 targetPosition;

        [SerializeField]
        private float movementLeft;

        private bool onFloor { get; set; }
        private bool haveNotWon { get; set; }

        public Signal<Movement> movePlayer { get; set; }
        public Signal<PlayerTargetPositionInput> requestTargetPosition { get; set; }
        public bool outOfMoves { get; set; }

        public void Init()
        {
            playerSpeed = .25f;
            playerMovement = new Movement()
            {
                CurrentGameObject = this.gameObject,
                MovementLeft = 0f,
                PlayerSpeed = playerSpeed
            };
            playerInfo = new PlayerTargetPositionInput()
            {
                CurrentPosition = this.gameObject.transform.position
            };
            movePlayer = new Signal<Movement>();
            requestTargetPosition = new Signal<PlayerTargetPositionInput>();
            targetPosition = this.gameObject.transform.position;
            outOfMoves = false;
            haveNotWon = false; //This will get updated if the player hits the door with the shard, signal will send out to update this. The door will know about the players last move
            //due to the lastmovesignal and if the player has not interacted with the door then it will continue on to the game over signal to be dispatched here.
        }

        void Update()
        {
            if (onFloor && !haveNotWon)
            {
                if (targetPosition == this.gameObject.transform.position && outOfMoves)
                    Debug.Log("Send out Game Over Signal");
                else if (targetPosition == this.gameObject.transform.position) //TODO: Send out the players position once when he reaches his target position
                {
                    GetPlayerInput();
                    Debug.Log(this.gameObject.transform.position);
                }
                else
                    MovePlayerToDesiredPosition();
            }
            else
            {
                Debug.Log("Send out falling animation");
                Debug.Log("Send out Game Over signal");
            }

        }

        #region Triggers to determine if fallen off the tile floor
        void OnTriggerStay(Collider col)
        {
            onFloor = true;
        }

        void OnTriggerExit(Collider col)
        {
           // onFloor = false;
        }
        #endregion

        private void GetPlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpdatePlayerInfo(Vector3.up);
                requestTargetPosition.Dispatch(playerInfo);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                UpdatePlayerInfo(Vector3.right);
                requestTargetPosition.Dispatch(playerInfo);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UpdatePlayerInfo(Vector3.left);
                requestTargetPosition.Dispatch(playerInfo);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                UpdatePlayerInfo(Vector3.down);
                requestTargetPosition.Dispatch(playerInfo);
            }
        }

        private void UpdatePlayerInfo(Vector3 direction)
        {
            playerInfo.CurrentPosition = this.gameObject.transform.position;
            playerInfo.DirectionOfTravel = direction;
        }

        private void MovePlayerToDesiredPosition()
        {
            playerMovement.TargetPosition = targetPosition;
            movePlayer.Dispatch(playerMovement);
        }

        public void UpdateTargetPosition(Vector3 updatedTargetPosition)
        {
            targetPosition = updatedTargetPosition;
            playerMovement.MovementLeft = 0;
        }
    }
}