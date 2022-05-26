using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestReactionScript
{
    public class ChestReaction : MonoBehaviour
    {
        SpriteRenderer sprender;
        [SerializeField] int numCompressions;
        [SerializeField] int maxCompressions;
        private bool raising;

        // Start is called before the first frame update
        void Start()
        {
            numCompressions = 0;
            maxCompressions = 30;
            sprender = this.GetComponent<SpriteRenderer>();
            raising = false;
        }

        private void Update()
        {
            if (raising)
            {
                sprender.color = new Color(sprender.color.r, sprender.color.g, sprender.color.b, sprender.color.a + 0.01f);
                if (sprender.color.a > 0.98f)
                {
                    raising = false;
                }
            }
            else
            {
                sprender.color = new Color(sprender.color.r, sprender.color.g, sprender.color.b, sprender.color.a - 0.01f);
                if (sprender.color.a < 0.02f)
                {
                    raising = true;
                }
            }
        }

        public void Compress()
        {
            numCompressions++;
            if (numCompressions >= maxCompressions)
                this.gameObject.transform.GetChild(0).tag = "Healed";
        }

        public int GetNumCompressions()
        {
            return numCompressions;
        }

        public int GetMaxNumCompressions()
        {
            return maxCompressions;
        }
    }
}