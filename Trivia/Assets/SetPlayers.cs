using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
#if UNITY_ANDROID && !UNITY_EDITOR
using tv.ouya.console.api;
#endif

namespace Assets
{
    public class SetPlayers : MonoBehaviour
    {
        public Image[] Player = new Image[4];
        public Text cont;
        
        private static List<int> plrs = new List<int>();
        

        // Use this for initialization
        void Start()
        {
            cont.enabled = false;
            for (int i = 0; i < 4; i++)
            {
                Player[i].enabled = false;
            }

           
        }

        // Update is called once per frame
        void Update()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            Connect();
            if (plrs.Count >= 1)
                cont.enabled = true;
            if (plrs.Count >= 1 && OuyaSDK.OuyaInput.GetButton(OuyaController.BUTTON_R1))
                Application.LoadLevel("Choose_Quiz");
            #endif
        }
       
#if UNITY_ANDROID && !UNITY_EDITOR
        public void Connect()
        {
            
            for (int i = 0; i < OuyaController.MAX_CONTROLLERS; i++)
            {
                if (OuyaSDK.OuyaInput.GetButtonDown(i, OuyaController.BUTTON_MENU))
                {
                    if (Player[i].enabled == false)
                    {
                        plrs.Add(i);
                        Player[i].enabled = true;
                    }
                    
                }
            }


        }
#endif
        // use this function to get which players are connected and how many
        public static List<int> GetPlayers()
        {
            plrs.TrimExcess();
            plrs.Sort(); 
            return plrs;
        }       
    }
}