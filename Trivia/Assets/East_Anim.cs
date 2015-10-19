using UnityEngine;
using System.Collections;

namespace Animations
{
    public class East_Anim : MonoBehaviour
    {
        public Animation AnsEast;
        private static bool animate = false;
        public AnimationClip EastCenter;
        // Use this for initialization
        public void Start()
        {
            AnsEast = GetComponent<Animation>();
            
        }

        public static void setAnimate(int state) // 0 == false; else true;
        {
            if (state == 0)
            {
                animate = false;
            }
            else
            {
                animate = true;
            }
        }

       
        // Update is called once per frame
        void Update()
        {

            if (animate)
            {
                AnsEast.AddClip(EastCenter, "estcntr");
                AnsEast.Play("estcntr"); 
            }
        }
    }
}
