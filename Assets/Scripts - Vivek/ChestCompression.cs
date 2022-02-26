using System.Collections;
using System.Collections.Generic;
using TreatmentClass;
using InjuryClass;
using UnityEngine;

namespace ChestCompressionClass
{
    public class ChestCompression : Treatment
    {
        private int maxCompressions;
        private int numCompressions;
        private float maxTimeInterval;
        private float minTimeInterval;
        private float timeElapsed;
        private GameObject cursor;

        public static ChestCompression MakeChestCompressionObject(GameObject ob, Injury injury, int maxCompressions, float maxTimeInterval, float minTimeInterval)
        {
            ChestCompression ret = ob.AddComponent<ChestCompression>();
            ret.treatmentStarted = false;
            ret.vitalSpike = false;
            ret.injury = injury;
            ret.maxCompressions = maxCompressions;
            ret.numCompressions = 0;
            ret.maxTimeInterval = maxTimeInterval;
            ret.minTimeInterval = minTimeInterval;
            ret.timeElapsed = 0;
            return ret;
        }

        // Start is called before the first frame update
        void Start()
        {
            cursor = GameObject.Find("Cursor");
        }

        // Update is called once per frame
        void Update()
        {
            if (treatmentStarted && cursor.GetComponent<NewCursor>().getSelected())
            {
                if (injury.IsSelected(cursor.transform.position))
                {
                    if (timeElapsed >= minTimeInterval && timeElapsed <= maxTimeInterval)
                    {
                        numCompressions++;
                        if (numCompressions >= maxCompressions)
                        {
                            vitalSpike = false;
                            injury.RemoveTreatment();
                            Destroy(this);
                        }
                    }
                    timeElapsed = 0;
                }
                else
                    timeElapsed += Time.deltaTime;
                Debug.Log(numCompressions);
            }
            else
                timeElapsed += Time.deltaTime;
        }
    }
}