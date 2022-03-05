using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MouseScriptClass;

namespace MouseReactionScript
{
    public class MouseReaction : MonoBehaviour
    {
        private bool selected;

        public bool GetSelected() { return selected; }
        public void ResetSelected() { selected = false; }

        void Start()
        {
            selected = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "BaseMouse")
            {
                Mouse_Script mse = collision.gameObject.GetComponent<Mouse_Script>();
                if (mse.GetClicking())
                {
                    selected = true;
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }
}
