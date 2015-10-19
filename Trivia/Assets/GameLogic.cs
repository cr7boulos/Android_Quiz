using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets;
using Animations;
using Quiz;
#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;				
#endif


namespace Game
{
   
    public class GameLogic : MonoBehaviour
    {
        #region VariableDeclarations
      
        public const int NUMBER_OF_QUIZZES = 5;
        public const int QUESTONS_PER_QUIZ = 10;

        //ratio of wrong answers to right answers is 3:1
        public const int WRONG_ANSWERS = QUESTONS_PER_QUIZ * 3;
        

        public string[] QuestionArray = new string[NUMBER_OF_QUIZZES * QUESTONS_PER_QUIZ]; 
        public static int length;
        public string[] Correct_Ans = new string[NUMBER_OF_QUIZZES * QUESTONS_PER_QUIZ]; 


        //elements 0-2 correspond to Question 1; elements 3-5 correspond to Question 2, etc.
        public string[] Wrong_Ans = new string[NUMBER_OF_QUIZZES * WRONG_ANSWERS]; 

        //these variables are used in the AnswerRandomizer method
        //IndexOfCorrectAns should never be reset to zero while the game is running!
        private static int IndexOfCorrectAns; 
        private static int questionArrayIndex; 

        //Used in the CheckTrue() function
        //ctrue should never be reset to zero while the game is running!
        private static int ctrue; 

        private static bool NorthFilled = false;
        private static bool SouthFilled = false;
        private static bool WestFilled = false;
        private static bool EastFilled = false;
        private const int NORTH = 0;
        private const int SOUTH = 1;
        private const int EAST = 2;
        private const int WEST = 3;
       

        private static List<int> plrs;           

        //if false, listen for input from P1, ..., P4; if true, ignore all input from said player.
        //element 0 maps to players 1's choice ... element 3 maps to player 4's choice
        private static bool[] Choices = new bool[4];

        //stores the player's selection P1 == 0, ... , P4 == 3
        private static int[] PlayerSelections = new int[4];
   
        //can hold the values of { NORTH, SOUTH, EAST, WEST }
        private static int CorrectAnswerLocation;

        
        public Text Question;
        public Text Ans_East;
        public Text Ans_West;
        public Text Ans_North;
        public Text Ans_South;
        #endregion


        // Use this for initialization
        void Start()
        {
            length = QuestionArray.Length; 
            plrs = SetPlayers.GetPlayers();
            plrs.TrimExcess();
            for (int i = 0; i < 4; i++)
                Choices[i] = false;
            ResetVariables();
            if(questionArrayIndex < QuestionArray.Length)
                Question.text = QuestionArray[questionArrayIndex++];         
            CorrectAnswerLocation = Answer_Randomizer();
           
        }

        // Update is called once per frame
        void Update()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
                        Select();
                        NextQuestion();
                       
            #endif           
     
        }

        //The value returned is the location of the correct answer
       public int Answer_Randomizer()
        {
            int i = Random.Range(0, 3);
            switch (i)
            {
                case NORTH:
                    Ans_North.text = Correct_Ans[IndexOfCorrectAns];
                    IndexOfCorrectAns++;
                    NorthFilled = true;
                    break;
                case SOUTH:
                    Ans_South.text = Correct_Ans[IndexOfCorrectAns];
                    SouthFilled = true;
                    IndexOfCorrectAns++;
                    break;
                case WEST:
                    Ans_West.text = Correct_Ans[ IndexOfCorrectAns];
                    WestFilled = true;
                    IndexOfCorrectAns++;
                    break;
                case EAST:
                    Ans_East.text = Correct_Ans[IndexOfCorrectAns];
                    EastFilled = true;
                    IndexOfCorrectAns++;
                    break;
            }
            for (int j = 0; j < 3; j++)
            {
                CheckTrue();
            }

            return i;


        }

        private void CheckTrue()
        {

            if (!NorthFilled)
            {
                NorthFilled = true;
                Ans_North.text = Wrong_Ans[ctrue];
                ctrue++;
            }
            else if (!SouthFilled)
            {
                SouthFilled = true;
                Ans_South.text = Wrong_Ans[ctrue];
                ctrue++;
            }
            else if (!WestFilled)
            {
                WestFilled = true;
                Ans_West.text = Wrong_Ans[ctrue];
                ctrue++;
            }
            else
            {
                EastFilled = true;
                Ans_East.text = Wrong_Ans[ctrue];
                ctrue++;
            }
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        //This function will run in the update function
       
        public void Select()
        {
            //Player 1 input handler     
            if (!Choices[0] && Contains(0))
            {

                if (OuyaSDK.OuyaInput.GetButtonDown(0, OuyaController.BUTTON_Y))
                {
                    Choices[0] = true;
                    PlayerSelections[0] = NORTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(0, OuyaController.BUTTON_A))
                {
                    Choices[0] = true;
                    PlayerSelections[0] = EAST;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(0, OuyaController.BUTTON_O))
                {
                    Choices[0] = true;
                    PlayerSelections[0] = SOUTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(0, OuyaController.BUTTON_U))
                {
                    Choices[0] = true;
                    PlayerSelections[0] = WEST;
                }
            }
            //Player 2 input handler
            if (!Choices[1] && Contains(1))
            {

                if (OuyaSDK.OuyaInput.GetButtonDown(1, OuyaController.BUTTON_Y))
                {
                    Choices[1] = true;
                    PlayerSelections[1] = NORTH;

                }
                if (OuyaSDK.OuyaInput.GetButtonDown(1, OuyaController.BUTTON_A))
                {
                    Choices[1] = true;
                    PlayerSelections[1] = EAST;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(1, OuyaController.BUTTON_O))
                {
                    Choices[1] = true;
                    PlayerSelections[1] = SOUTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(1, OuyaController.BUTTON_U))
                {
                    Choices[1] = true;
                    PlayerSelections[1] = WEST;
                }
            }
            //Player 3 input handler
            if (!Choices[2] && Contains(2))
            {

                if (OuyaSDK.OuyaInput.GetButtonDown(2, OuyaController.BUTTON_Y))
                {
                    Choices[2] = true;
                    PlayerSelections[2] = NORTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(2, OuyaController.BUTTON_A))
                {
                    Choices[2] = true;
                    PlayerSelections[2] = EAST;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(2, OuyaController.BUTTON_O))
                {
                    Choices[2] = true;
                    PlayerSelections[2] = SOUTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(2, OuyaController.BUTTON_U))
                {
                    Choices[2] = true;
                    PlayerSelections[2] = WEST;
                }
            }
            //Player 4 input handler
            if (!Choices[3] && Contains(3))
            {

                if (OuyaSDK.OuyaInput.GetButtonDown(3, OuyaController.BUTTON_Y))
                {
                    Choices[3] = true;
                    PlayerSelections[3] = NORTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(3, OuyaController.BUTTON_A))
                {
                    Choices[3] = true;
                    PlayerSelections[3] = EAST;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(3, OuyaController.BUTTON_O))
                {
                    Choices[3] = true;
                    PlayerSelections[3] = SOUTH;
                }
                if (OuyaSDK.OuyaInput.GetButtonDown(3, OuyaController.BUTTON_U))
                {
                    Choices[3] = true;
                    PlayerSelections[3] = WEST;
                   
                }
            }
        }
#endif

        public void NextQuestion()
        {
            //every player has made their selection
            if (Ready())
            {
                switch (CorrectAnswerLocation)
                {
                    case NORTH:
                        Ans_East.enabled = false;
                        Ans_South.enabled = false;
                        Ans_West.enabled = false;
                        break;

                    case SOUTH:
                        Ans_East.enabled = false;
                        Ans_North.enabled = false;
                        Ans_West.enabled = false;
                        break;

                    case WEST:
                        Ans_East.enabled = false;
                        Ans_South.enabled = false;
                        Ans_North.enabled = false;
                        break;

                    case EAST:
                        Ans_North.enabled = false;
                        Ans_South.enabled = false;
                        Ans_West.enabled = false;
                        break;
                }
                //adds a 3 second delay before calling next scene               
                StartCoroutine(QuizSelect.Timer(QuizSelect.TRANSITION, "Scores")); 
                                                                        
            }
        }

        public void ResetVariables()
        {
            NorthFilled = false;
            SouthFilled = false;
            WestFilled = false;
            EastFilled = false;
            for (int i = 0; i < 4; i++)
                Choices[i] = false;
            for (int i = 0; i < 4; i++)
            {
                //assigning them to -1 to ensure they never touch the real answer
                PlayerSelections[i] = -1;
            }
            Ans_East.enabled = true;
            Ans_South.enabled = true;
            Ans_North.enabled = true;
            Ans_West.enabled = true;
        }

        private bool Contains(int i)
        {
            foreach (int player in plrs)
            {
                if (player == i)
                    return true;
            }
            return false;
        }

        private bool Ready()
        {
            foreach (int player in plrs)
            {
                if (Choices[player] == false)
                    return false;
            }
            return true;
        }

        public static int GetCorrectAnswerLocation()
        {
            return CorrectAnswerLocation;
        }

        //returns -1 if there is an error
        public static int GetPlayerSelection(int i)
        {
            if (i >= 0 && i < 4)
                return PlayerSelections[i];
            else
                return -1;
        }

    /**     
        public IEnumerator Timer(float wait, string scene)
        {
            yield return new WaitForSeconds(wait);
            Application.LoadLevel(scene);
        }
     **/
        public static int GetQuestionArrayLength()
        {
            return length;
        }
     
        public static int GetQuestionArrayIndex()
        {
            return questionArrayIndex;
        }

        public static void SetQuestionArrayIndex(int variable)
        {
            questionArrayIndex = variable;
        }

        public static void SetIndexOfCorrectAns(int variable)
        {
            IndexOfCorrectAns = variable;
        }

        public static void SetCTrue(int variable)
        {
             ctrue = variable;
        }       
    }
}
