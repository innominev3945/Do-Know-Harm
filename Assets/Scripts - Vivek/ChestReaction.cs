using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestReactionScript
{
    public class ChestReaction : MonoBehaviour
    {
        [SerializeField] int numCompressions;
        [SerializeField] int maxCompressions;

        // Start is called before the first frame update
        void Start()
        {
            numCompressions = 0;
            maxCompressions = 30;
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