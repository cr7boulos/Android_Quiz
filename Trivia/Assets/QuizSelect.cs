using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Game;
using Scoring;
#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

namespace Quiz
{
    public class QuizSelect : MonoBehaviour
    {      
        public const int TRANSITION = 1; //time between scene loads (in seconds)
        private const int ROUND_ONE = 0;
        private const int ROUND_TWO = 1;
        private const int ROUND_THREE = 2;
        private const int ROUND_FOUR = 3;
        private const int FINAL_ROUND = 4;
        private static int ROUND_SELECTED;
        public Text[] rounds = new Text[5];
        public Image[] Images = new Image[5];
       
       
        // Use this for initialization

#if UNITY_ANDROID && !UNITY_EDITOR
        void Start()
        {
            SetVariables();
           
        }

        // Update is called once per frame
        void Update()
        {
            if (OuyaSDK.OuyaInput.GetButton(OuyaController.BUTTON_DPAD_UP))
            {
                Images[0].enabled = false;
                rounds[0].enabled = false;
                ROUND_SELECTED = ROUND_ONE;
                SetUp();
                StartCoroutine(Timer(TRANSITION, "Question_1"));
            }

            if (OuyaSDK.OuyaInput.GetButton(OuyaController.BUTTON_DPAD_RIGHT))
            {
                Images[1].enabled = false;
                rounds[1].enabled = false;
                ROUND_SELECTED = ROUND_TWO;
                SetUp();
                StartCoroutine(Timer(TRANSITION, "Question_1"));
            }

            if (OuyaSDK.OuyaInput.GetButton(OuyaController.BUTTON_DPAD_LEFT))
            {
                Images[3].enabled = false;
                rounds[3].enabled = false;
                ROUND_SELECTED = ROUND_FOUR;
                SetUp();
                StartCoroutine(Timer(TRANSITION, "Question_1"));
            }

            if (OuyaSDK.OuyaInput.GetButton(OuyaController.BUTTON_DPAD_DOWN))
            {
                Images[2].enabled = false;
                rounds[2].enabled = false;
                ROUND_SELECTED = ROUND_THREE;
                SetUp();
                StartCoroutine(Timer(TRANSITION, "Question_1"));
            }

            if (OuyaSDK.OuyaInput.GetButton(OuyaController.BUTTON_MENU))
            {
                Images[4].enabled = false;
                rounds[4].enabled = false;
                ROUND_SELECTED = FINAL_ROUND;
                SetUp();
                StartCoroutine(Timer(TRANSITION, "Question_1"));
            }
           
                
        }
#endif
        public static int GetRound()
        {
            return ROUND_SELECTED;
        }

        public static void SetUp()
        {
            // initializes the variables properly
            GameLogic.SetCTrue(ROUND_SELECTED * GameLogic.WRONG_ANSWERS);
            GameLogic.SetIndexOfCorrectAns(ROUND_SELECTED * GameLogic.QUESTONS_PER_QUIZ);
            GameLogic.SetQuestionArrayIndex(ROUND_SELECTED * GameLogic.QUESTONS_PER_QUIZ);
            ScoreCard.InitScoreCard();
            
        }

        //pauses Wait seconds before calling Scene scene
        public static IEnumerator Timer(float wait, string scene)
        {
            yield return new WaitForSeconds(wait);
            Application.LoadLevel(scene);
        }
        private void SetVariables()
        {
            foreach(Text round in rounds)
            {
                round.enabled = true;
            }
            foreach(Image image in Images)
            {
                image.enabled = true;
            }
        }
        

    }
}
