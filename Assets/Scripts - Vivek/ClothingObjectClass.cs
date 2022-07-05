using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClothingClass
{

    public class ClothingObject : MonoBehaviour
    {
        private GameObject clothingObject;

        public static ClothingObject MakeClothingObject(GameObject ob, GameObject clothing)
        {
            ClothingObject cobj = ob.AddComponent<ClothingObject>();
            cobj.clothingObject = clothing;
            return cobj;
        }

        void Start()
        {

        }

        public void disableClothingObject()
        {

            clothingObject.GetComponent<MaleClothingScript>().disableClothing();
        }
        public void enableClothingObject()
        {
            clothingObject.GetComponent<MaleClothingScript>().enableClothing();
        }

        public void setClothingObject(GameObject ob)
        {
            clothingObject = ob;
        }
        public GameObject getClothingObject()
        {
            return clothingObject;
        }

        public void SetValues()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

