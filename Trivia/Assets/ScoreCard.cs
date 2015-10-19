using UnityEngine;
using System.Collections;
using System;
using Game;
using UnityEngine.UI;
using Assets;
using Quiz;

namespace Scoring
{
    public class ScoreCard : MonoBehaviour
    {

        private int correctAns;

        const int YPOS_PLAYER_POINTS = -76;
        const int YPOS_PLAYER_NAMES = -125;
        public Text[] PlayerPoints = new Text[4];
        public Image[] PlayerNames = new Image[4];
        int participants;
       
        //private static bool firstSceneLoad = true;
        private static int[] scoreCard = new int[4];// element 0 refers to Player 1's score; element 1 to Player 2's score, and so on. 

        // Use this for initialization
        void Start()
        {
           
            SetVariables();
            participants = SetPlayers.GetPlayers().Count;
            SetScorePostions(participants);
           
            correctAns = GameLogic.GetCorrectAnswerLocation();
            foreach (int player in SetPlayers.GetPlayers())
            {
                if (correctAns == GameLogic.GetPlayerSelection(player))
                {
                    scoreCard[player]++;
                }
            }
            ComputePoints();

            if (endOfQuiz(GameLogic.GetQuestionArrayIndex()))
            {
                StartCoroutine(QuizSelect.Timer(QuizSelect.TRANSITION, "Choose_Quiz"));
            }
            //makes sure there is another question in the array before proceding
            else if (GameLogic.GetQuestionArrayIndex() < GameLogic.GetQuestionArrayLength())
            {
                StartCoroutine(QuizSelect.Timer(QuizSelect.TRANSITION * 5, "Question_1"));
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void InitScoreCard()
        {
            for (int i = 0; i < 4; i++)
            {
                scoreCard[i] = 0;
            }
        }
        
        private void ComputePoints()
        {
            foreach (int player in SetPlayers.GetPlayers())
            {
                PlayerPoints[player].text = scoreCard[player].ToString();
            }
        }
       
      
        private void SetScorePostions(int numberOfPlayers)
        {
            switch (numberOfPlayers)
            {
                //assume that all text fields start at position: x = 0, y = 0, z = 0.
                //the transform.Translate method adds the parameter values to the gameObject's current position;
               

                case 1:
                    PlayerPoints[SetPlayers.GetPlayers()[0]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[0]].enabled = true;
                    PlayerPoints[SetPlayers.GetPlayers()[0]].transform.Translate(0, YPOS_PLAYER_POINTS, 0);
                    PlayerNames[SetPlayers.GetPlayers()[0]].transform.Translate(0, YPOS_PLAYER_NAMES, 0);

                    break;
                case 2:
                    PlayerPoints[SetPlayers.GetPlayers()[0]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[0]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[1]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[1]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[0]].transform.Translate(-181, YPOS_PLAYER_POINTS, 0);
                    PlayerPoints[SetPlayers.GetPlayers()[1]].transform.Translate(181, YPOS_PLAYER_POINTS, 0);
                    PlayerNames[SetPlayers.GetPlayers()[0]].transform.Translate(-181, YPOS_PLAYER_NAMES, 0);
                    PlayerNames[SetPlayers.GetPlayers()[1]].transform.Translate(181, YPOS_PLAYER_NAMES, 0);

                    break;
                case 3://fix these settings
                    PlayerPoints[SetPlayers.GetPlayers()[0]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[0]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[1]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[1]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[2]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[2]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[0]].transform.Translate(-330, YPOS_PLAYER_POINTS, 0);
                    PlayerPoints[SetPlayers.GetPlayers()[1]].transform.Translate(0, YPOS_PLAYER_POINTS, 0);
                    PlayerPoints[SetPlayers.GetPlayers()[2]].transform.Translate(330, YPOS_PLAYER_POINTS, 0);
                    PlayerNames[SetPlayers.GetPlayers()[0]].transform.Translate(-330, YPOS_PLAYER_NAMES, 0);
                    PlayerNames[SetPlayers.GetPlayers()[1]].transform.Translate(0, YPOS_PLAYER_NAMES, 0);
                    PlayerNames[SetPlayers.GetPlayers()[2]].transform.Translate(330, YPOS_PLAYER_NAMES, 0);

                    break;
                case 4:

                    PlayerPoints[SetPlayers.GetPlayers()[0]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[0]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[1]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[1]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[2]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[2]].enabled = true;

                    PlayerPoints[SetPlayers.GetPlayers()[3]].enabled = true;
                    PlayerNames[SetPlayers.GetPlayers()[3]].enabled = true;

                    PlayerPoints[0].transform.Translate(-436, YPOS_PLAYER_POINTS, 0);
                    PlayerPoints[1].transform.Translate(-181, YPOS_PLAYER_POINTS, 0);
                    PlayerPoints[2].transform.Translate(181, YPOS_PLAYER_POINTS, 0);
                    PlayerPoints[3].transform.Translate(436, YPOS_PLAYER_POINTS, 0);
                    PlayerNames[0].transform.Translate(-436, YPOS_PLAYER_NAMES, 0);
                    PlayerNames[1].transform.Translate(-181, YPOS_PLAYER_NAMES, 0);
                    PlayerNames[2].transform.Translate(181, YPOS_PLAYER_NAMES, 0);
                    PlayerNames[3].transform.Translate(436, YPOS_PLAYER_NAMES, 0);
                    break;
            }
        }

       

        private void SetVariables()
        {
            foreach (Image im in PlayerNames)
                im.enabled = false;
            foreach (Text txt in PlayerPoints)
                txt.enabled = false;

        }

        private bool endOfQuiz(int questionIndex)
        {

            if (questionIndex % GameLogic.QUESTONS_PER_QUIZ == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //To-do: work out a way to detect who won
        private static string winner(int [] scores)
        {
            int max = (Math.Max(scores[0], scores[1]) > Math.Max(scores[2], scores[3])) ? Math.Max(scores[0], scores[1]) : Math.Max(scores[2], scores[3]);
            int ties = -1;
            for (int i = 0; i < 4; i++)
            {
                if (scores[i] == max)
                {
                    ties++;
                }
            }
            for (int i = 0; i < 4; i++ )
            {
                if( ties > 0)
                {
                    return "Draw";
                }
                else if (scores[i] == max)
                {  
                    // add one to i so the player number is correct
                    return "Congradulations, Player" + (i + 1) + ", you won!";
                }
            }
            return "Error";
        }
    }
}
    