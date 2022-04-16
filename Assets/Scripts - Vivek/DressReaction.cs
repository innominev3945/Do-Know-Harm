using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DressReactionScript
{
    public class DressReaction : MonoBehaviour
    {
        [SerializeField] float wrappage; // Total amount of degrees, signed, that the mouse has gone around the injury
        [SerializeField] float maxWrappage; // Total amount of degrees needed to fully treat the injury 

        // Start is called before the first frame update
        void Start()
        {
            wrappage = 0f;
            maxWrappage = 2880;
        }

        public void Dress(float wrap)
        {
            wrappage += wrap;
            if (Math.Abs(wrappage) >= Math.Abs(maxWrappage))
                this.gameObject.transform.GetChild(0).tag = "Healed";
        }

        public void Unwrap()
        {
            wrappage = 0;
        }

        public float GetWrappage()
        {
            return wrappage;
        }

        public float GetMaxWrappage()
        {
            return maxWrappage;
        }

    }
}