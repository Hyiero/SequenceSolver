using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
using strange.extensions.signal.impl;

namespace Views
{
    public class DisplayInformationView : View
    {
        public Text locksLeftText;
        public Text currentSequenceText;
        public int positionInSequeunce { get; set; }
        public int[] currentSequence { get; set; }

        public void Init()
        {
            currentSequenceText = GameObject.FindGameObjectWithTag("Current_Sequence_Text").GetComponent<Text>();
            locksLeftText = GameObject.FindGameObjectWithTag("Locks_Left_Text").GetComponent<Text>();
            positionInSequeunce = 0;
        }

        public void SetCurrentPositionInSequence(int position)
        {
            positionInSequeunce = position;
            Debug.Log("We will move " + currentSequence[position] + " tile next");
        }

        public void SetCurrentSequence(int[] sequence)
        {
            currentSequence = sequence;
            UpdateCurrentSequenceText();
        }

        public void UpdateCurrentSequenceText()
        {
            foreach(var number in currentSequence)
            {
                currentSequenceText.text += number.ToString() + " ";
            }
        }

        public void UpdateCurrentLocksLeftText(int locksLeft)
        {
            locksLeftText.text = locksLeft.ToString();
        }
    }
}