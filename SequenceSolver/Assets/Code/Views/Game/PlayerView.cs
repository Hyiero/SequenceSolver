using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System;

namespace Views
{
    public class PlayerView : View,IPlayerView
    {
        private float playerSpeed = .25f;
        private CharacterController playerCharController { get; set; }
        [SerializeField]
        private Vector3 currentPositon;
        [SerializeField]
        private Vector3 targetPosition;

        private int[] mySequence;
        private int positionInSequence { get; set; }
        [SerializeField]
        private float movementLeft;

        private bool onFloor { get; set; }

        public void Init()
        {
            Debug.Log("My player is alive and good to go");
            playerCharController = this.gameObject.GetComponent<CharacterController>();
            targetPosition = this.gameObject.transform.position;
            mySequence = new int[4] { 1, 1, 3, 4 };
            positionInSequence = 0;
        }

        void Update()
        {
            if (onFloor)
            {
                if (positionInSequence == 4)
                    Debug.Log("GameOver");
                if (targetPosition == this.gameObject.transform.position)
                    GetPlayerInput();
                else if (movementLeft < 0.4f)
                    MovePlayerToDesiredPosition();
            }
            else
            {
                Debug.Log("GameOver");
                Debug.Log("Play Death Animation here");
            }

        }

        void OnTriggerStay(Collider col)
        {
            onFloor = true;
        }

        void OnTriggerExit(Collider col)
        {
            onFloor = false;
        }

        private void GetPlayerInput()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                movementLeft = 0;
                targetPosition += Vector3.up * mySequence[positionInSequence];
                positionInSequence++;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                movementLeft = 0;
                targetPosition += Vector3.right * mySequence[positionInSequence];
                positionInSequence++;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movementLeft = 0;
                targetPosition += Vector3.left * mySequence[positionInSequence];
                positionInSequence++;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                movementLeft = 0;
                targetPosition += Vector3.down * mySequence[positionInSequence];
                positionInSequence++;
            }
        }

        private void MovePlayerToDesiredPosition()
        {
            transform.position = Vector3.Lerp(this.gameObject.transform.position, targetPosition, movementLeft);
            movementLeft += (Time.deltaTime * playerSpeed) / positionInSequence;
        }
    }
}