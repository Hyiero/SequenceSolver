using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System;
using Models;
using strange.extensions.signal.impl;

namespace Views
{
    public class PlayerView : View
    {

        private float playerSpeed { get; set; }
        private Movement playerMovement { get; set; }
        private PlayerTargetPositionInput playerInfo { get; set; }
        private bool outOfMoves { get; set; }
        private bool isCurrentPositionUpdated { get; set; }

        [SerializeField]
        private Vector3 targetPosition;

        private int onFloor { get; set; }

        public Signal<Movement> movePlayer { get; set; }
        public Signal<PlayerTargetPositionInput> requestTargetPosition { get; set; }
        public Signal<Vector3> updateCurrentPosition { get; set; }
        public Signal doneMoving { get; set; }
        public Signal fellOff { get; set; }

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
            updateCurrentPosition = new Signal<Vector3>();
            doneMoving = new Signal();
            fellOff = new Signal();
            targetPosition = this.gameObject.transform.position;
            isCurrentPositionUpdated = false;
            outOfMoves = false;
        }

        void Update()
        {
            if (onFloor == 0)
            {
                //TODO: Possibly take this out of the player or put code in here and not the door or create Level Manager to do this? player and door shouldn't be responsible if player failed to reach the objective or not
                if (targetPosition == this.gameObject.transform.position && !outOfMoves)
                {
                    GetPlayerInput();
                    SendOutCurrentPositionUpdate();
                }
                else if(targetPosition == this.gameObject.transform.position && outOfMoves)
                {
                    SendOutCurrentPositionUpdate();
                    doneMoving.Dispatch();
                }
                else if(targetPosition != this.gameObject.transform.position)
                    MovePlayerToDesiredPosition();
            }
            else if(onFloor == 1)
            {
                Debug.Log("Send out falling animation");
                onFloor = 2;
                fellOff.Dispatch();
            }

        }

        #region Triggers to determine if fallen off the tile floor
        void OnTriggerStay(Collider col)
        {
            onFloor = 0;
        }

        void OnTriggerExit(Collider col)
        {
            onFloor = 1;
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
            isCurrentPositionUpdated = false;
            movePlayer.Dispatch(playerMovement);
        }

        public void UpdateTargetPosition(Vector3 updatedTargetPosition)
        {
            targetPosition = updatedTargetPosition;
            playerMovement.MovementLeft = 0;
        }

        public void SetOutOfMoves()
        {
            outOfMoves = true;
        }

        private void SendOutCurrentPositionUpdate()
        {
            if (!isCurrentPositionUpdated)
            {
                updateCurrentPosition.Dispatch(this.gameObject.transform.position);
                isCurrentPositionUpdated = true;
            }
        }

        private void ResetPlayerToStart()
        {
            onFloor = 0;
        }
    }
}