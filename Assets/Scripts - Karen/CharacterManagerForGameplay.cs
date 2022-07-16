using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterManagerForGameplay : MonoBehaviour
{
    //Vector3  movement;
    //float inputX;
    private int currCharacterNum = 0;
    //private int newCharacterNum = 0;

    [SerializeField] private float fadeSpeed;

    //ADJUST AS NEEDED 
    private const int numCharacters = 3;


    private int whichMC = 0;


    [SerializeField] SpriteRenderer MC;
    [SerializeField] SpriteRenderer MCExpr;
    
    

    //public Sprite[] CharacterBases;
    public Sprite[] HannahExpressions;
    public Sprite[] EricExpressions;
    public Sprite[] CaptainExpressions;


//Add in when outfit sprites added
    
    public Sprite[] HannahOutfits;
    public Sprite[] EricOutfits;
    public Sprite[] CaptainOutfits;

    

    //temporary storage to change sprites of a character
    private Sprite[] temp;

    //store who's the MC's sprite array here
    private Sprite[] MCExpressions;

    private Sprite[] MCOutfit;





    // void Awake()
    // {
    //     for (int i = 0; i < numCharacters; i++)
    //     {
    //         characterLoads[i] = -1;
    //     }
    //     //MC.size += new Vector2(1.0f, 1.0f);
    // }




    public void changeExpression(int expressionNumber)
    {

        //MC.sprite = MCExpressions[expressionNumber];
        MCExpr.sprite = MCExpressions[expressionNumber];
        return;
    }


    //Changes the outfit of the character in question (add in when multiple costumes added)
    
    public void changeOutfit(int outfitNumber)
    {
        MC.sprite = MCOutfit[outfitNumber];
        return;
    }

    public void loadMC(int n)
    {
        //MCOutfit = HannahOutfit;
        if (n == 1)
        {
            MCExpressions = HannahExpressions;
            
            MCOutfit = HannahOutfits;
            MCExpr.sprite = MCExpressions[0];
            MC.sprite = MCOutfit[0];
        }
        else if (n == 0)
        {
            MCExpressions = EricExpressions;
            MCOutfit = EricOutfits;
            MCExpr.sprite = MCExpressions[0];
            MC.sprite = MCOutfit[0];
        }
        else
        {
            MCExpressions = CaptainExpressions;
            MCOutfit = CaptainOutfits;
            MCExpr.sprite = MCExpressions[0];
            MC.sprite = MCOutfit[0];
        }
        
    }
    
    IEnumerator FadeOut(SpriteRenderer spriteRenderer){
        while (spriteRenderer.color.a > 0)
        {
            Color objectColor = spriteRenderer.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.color = objectColor;
            yield return null;
        }
    }

    IEnumerator FadeIn(SpriteRenderer spriteRenderer){
        while (spriteRenderer.color.a < 1)
        {
            Color objectColor = spriteRenderer.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.color = objectColor;
            yield return null;
        }
    }
}
