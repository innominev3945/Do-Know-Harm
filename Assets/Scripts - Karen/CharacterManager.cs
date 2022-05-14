using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //Vector3  movement;
    //float inputX;
    private int currCharacterNum = 0;
    //private int newCharacterNum = 0;

    [SerializeField] private float fadeSpeed;

    //ADJUST AS NEEDED 
    private const int numCharacters = 5;


    private int whichMC = 0;

    [SerializeField] SpriteRenderer CharacterRenderer_1;
    [SerializeField] SpriteRenderer CharacterRenderer_2;
    [SerializeField] SpriteRenderer CharacterRenderer_3;
    [SerializeField] SpriteRenderer CharacterExpression_1;
    [SerializeField] SpriteRenderer CharacterExpression_2;
    [SerializeField] SpriteRenderer CharacterExpression_3;

    [SerializeField] SpriteRenderer MC;
    [SerializeField] SpriteRenderer MCExpr;
    
    

    public Sprite[] CharacterBases;
    public Sprite[] HannahExpressions;
    public Sprite[] EricExpressions;
    public Sprite[] CaptainExpressions;
    public Sprite[] ElderlyLady1Expressions;
    public Sprite[] AmphiteranMan1Expressions;

//Add in when outfit sprites added
    /*
    public Sprite[] HannahOutfits;
    public Sprite[] EricOutfits;
    public Sprite[] CaptainOutfits;
    public Sprite[] ElderlyLady1Outfits;
    public Sprite[] AmphiteranMan1Outfits;
    */

    //temporary storage to change sprites of a character
    private Sprite[] temp;

    //store who's the MC's sprite array here
    private Sprite[] MCExpressions;

    private Sprite[] MCOutfit;
    //array of sprite arrays for the sprites?

    //for each character, if they aren't loaded, stores -1
    //if they are loaded, stores their position
    private int[] characterLoads = new int[numCharacters];

    
    //private int[] loadedLocations = new int[3];

    //element number corresponds to which characters loaded there
    private int[] loadedCharacters = new int[3];
    // Start is called before the first frame update
    //private int numCharacters = 0;
    //private new List <int> activeCharacters = new <int>;

    public void replaceCharacter (int toReplace, int newCharacter)
    {
        //checks if the character to replace is part of t
        bool isToReplaceThere = false;
        int replaceLoaded = -1;
        for (int  i = 0; i < currCharacterNum; i++)
        {
            if(loadedCharacters[i] == toReplace)
            {
                isToReplaceThere = true;
                replaceLoaded = i;
            }
        }
        if (!isToReplaceThere)
        {
            return;
        }
        //replaces toReplace character in loadedCharacters array with newCharacter
        loadedCharacters[replaceLoaded] = newCharacter;
        //changes the location of the toReplace character with -1 to show they aren't loaded
        characterLoads[toReplace] = -1;
        

        //where the loaded character is (ie sprite renderer 1, 2, or 3)
        int whichToChange = 0;

        
        //checks which sprite the loaded character is at
        for (int i = 0; i < currCharacterNum; i++)
        {
            if(characterLoads[i] == toReplace)
            {
                whichToChange = i;
            }
        }

        //changes that sprite to show the character loaded there
        characterLoads[newCharacter] = whichToChange;

        //changes character sprite to the new character
        if(whichToChange == 0)
        {
            FadeOut(CharacterRenderer_1);
            temp = getCharacterExpressionArray(newCharacter);
            CharacterRenderer_1.sprite = temp[0];
            FadeIn(CharacterRenderer_2);
        }else if (whichToChange == 1)
        {
            FadeOut(CharacterRenderer_2);
            temp = getCharacterExpressionArray(newCharacter);
            CharacterRenderer_2.sprite = temp[0];
            FadeIn(CharacterRenderer_2);
        }else
        {
            FadeOut(CharacterRenderer_3);
            temp = getCharacterExpressionArray(newCharacter);
            CharacterRenderer_3.sprite = temp[0];
            FadeIn(CharacterRenderer_3);
        }
    
    }

    
    public int getCharacterNumber()
    {
        return numCharacters;
    }

    private Sprite[] getCharacterExpressionArray(int characterNumber)
    {
        if (characterNumber == 0)
            return HannahExpressions;
        if (characterNumber == 1)
            return EricExpressions;
        if (characterNumber == 2)
            return CaptainExpressions;
        if (characterNumber == 3)
            return ElderlyLady1Expressions;
        else
            return AmphiteranMan1Expressions;
    }

    /*
    private Sprite[] getCharacterOutfitArray(int characterNumber)
    {
        if (characterNumber == 0)
            return HannahOutfits;
        if (characterNumber == 1)
            return EricOutfits;
        if (characterNumber == 2)
            return CaptainOutfits;
        if (characterNumber == 3)
            return ElderlyLady1Outfits;
        else
            return AmphiteranMan1Outfits;
    }*/

    void Awake()
    {
        for (int i = 0; i < numCharacters; i++)
        {
            characterLoads[i] = -1;
        }
        MC.size += new Vector2(1.0f, 1.0f);
    }


    public void loadCharacter(int n)
    {
        currCharacterNum++;
        //If there aren't any characters, load in character to center
        if (currCharacterNum == 1)
        {
            //Load in new character and their default expression from their expression array
            
            //fade out to hide what's happening
            FadeOut(CharacterRenderer_1);
            FadeOut(CharacterExpression_1);
            
            //get the character's default base visual (remove later)
            CharacterRenderer_1.sprite = CharacterBases[n];

            //To add once we get different costumes
            /*
            temp = getCharacterOutfitArray(n);
            characterLoads[n] = 0;
            CharacterRenderer_1.sprite = temp[0];
            */


            //Loads the expression array of the character
            temp = getCharacterExpressionArray(n);
            characterLoads[n] = 0;
            CharacterExpression_1.sprite = temp[0];
            FadeIn(CharacterExpression_1);
            FadeIn(CharacterRenderer_1);
            loadedCharacters[0] = n;
            
            //loadedCharacter[]
            //GameObject.transform(some position)
        }
        //If there's already a character in the scene, move that character over to the left
        //and load the new character in
        else if (currCharacterNum == 2)
        {
            CharacterRenderer_1.transform.position += Vector3.left * 500.0f;

            CharacterRenderer_2.transform.position += Vector3.right * 500.0f;


            

            //hide all the nasty deets from the player
            FadeOut(CharacterRenderer_2);
            FadeOut(CharacterExpression_2);
            
            //load the base default sprite (TO REMOVE)
            CharacterRenderer_2.sprite = CharacterBases[n];

            //TO ADD
            /*
            temp = getCharacterOutfitArray(n);
            characterLoads[n] = 1;
            CharacterRenderer_2.sprite = temp[0];
            */

            temp = getCharacterExpressionArray(n);
            characterLoads[n] = 1;
            CharacterExpression_2.sprite = temp[0];


            FadeIn(CharacterRenderer_2);
            FadeIn(CharacterExpression_2);

            loadedCharacters[1] = n;

            //CharacterRenderer_1.transform
            //GameObject.transform(some position)
            //GameObject.transform(some position)
        }
        else if (currCharacterNum == 3)
        {
            CharacterRenderer_1.transform.position += Vector3.left * 100.0f;
            CharacterRenderer_2.transform.position += Vector3.left * 500.0f;
            CharacterRenderer_3.transform.position += Vector3.right * 600.0f;


            FadeOut(CharacterRenderer_3);
            FadeOut(CharacterExpression_3);

            //TO REMOVE
            CharacterRenderer_3.sprite = CharacterBases[n];

            //TO ADD
            /*
            temp = getCharacterOutfitArray(n);
            characterLoads[n] = 0;
            CharacterRenderer_1.sprite = temp[0];
            */


            temp = getCharacterExpressionArray(n);
            characterLoads[n] = 2;            
            CharacterRenderer_3.sprite = temp[0];


            FadeIn(CharacterRenderer_3);
            FadeIn(CharacterExpression_3);


            loadedCharacters[2] = n;
            //GameObject.transform(some position)
            //GameObject.transform(some position)
            //GameObject.transform(some position)
        }
        return;
    }


    //To implement later: make characters slide across screen instead of popping around
    //also fade characters
    public void unloadCharacter(int n)
    {
        int characterLocation = characterLoads[n];
        if (characterLocation == -1)
        {
            return;
            
        }

        currCharacterNum--;

        if(currCharacterNum == 2)
        {
            if (characterLocation == 2)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 500.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 600.0f;
                CharacterRenderer_3.sprite = null;
                CharacterExpression_3.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[2] = -1;
            }else if (characterLocation == 1)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 500.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 600.0f;
                CharacterRenderer_2.sprite = CharacterRenderer_3.sprite;
                CharacterRenderer_3.sprite = null;
                CharacterExpression_3.sprite = null;
                characterLoads[loadedCharacters[1]] = -1;
                characterLoads[loadedCharacters[2]] = 1;
                loadedCharacters[1] = loadedCharacters[2];
                loadedCharacters[2] = -1;
            }
            else if (characterLocation == 0)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 500.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 600.0f;
                CharacterRenderer_1.sprite = CharacterRenderer_2.sprite;
                CharacterRenderer_2.sprite = CharacterRenderer_3.sprite;
                CharacterRenderer_3.sprite = null;
                CharacterExpression_3.sprite = null;
                characterLoads[loadedCharacters[0]] = -1;
                characterLoads[loadedCharacters[1]] = 0;
                characterLoads[loadedCharacters[2]] = 1;
                loadedCharacters[0] = loadedCharacters[1];
                loadedCharacters[1] = loadedCharacters[2];
                loadedCharacters[2] = -1;
            }
            return;
        }else if (currCharacterNum == 1)
        {
            if (characterLocation == 1)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 500.0f;
                CharacterRenderer_2.transform.position += Vector3.left * 500.0f;
                CharacterRenderer_2.sprite = null;
                CharacterExpression_2.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[1] = -1;
            }else if (characterLocation == 0)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 500.0f;
                CharacterRenderer_2.transform.position += Vector3.left * 500.0f;
                CharacterRenderer_1.sprite = CharacterRenderer_2.sprite;
                CharacterRenderer_2.sprite = null;
                CharacterExpression_2.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[0] = loadedCharacters[1];
                loadedCharacters[1] = -1;
            }
            return;
        }else if (currCharacterNum == 0)
        {        
            CharacterRenderer_1.sprite = null;
            CharacterExpression_1.sprite = null;
            characterLoads[n] = -1;
            loadedCharacters[0] = -1;
            return;
        }
        return;
    }

    public void changeExpression(int characterNumber, int expressionNumber)
    {
        
        if (characterNumber == whichMC)
        {
            //MC.sprite = MCExpressions[expressionNumber];
            MCExpr.sprite = MCExpressions[expressionNumber];
            return;
        }
        temp = getCharacterExpressionArray(characterNumber);
        int whereCharacter = characterLoads[characterNumber];
        
        if (whereCharacter == -1)
        {
            return;
        }else if (whereCharacter == 0)
        {
            CharacterExpression_1.sprite = temp[expressionNumber];
        }else if (whereCharacter == 1)
        {
            CharacterExpression_2.sprite = temp[expressionNumber];
        }else if (whereCharacter == 2)
        {
            CharacterExpression_3.sprite = temp[expressionNumber];
        }
    }


    //Changes the outfit of the character in question (add in when multiple costumes added)
    /*
    public void changeOutfit(int characterNumber, int outfitNumber)
    {
        
        if (characterNumber == whichMC)
        {
            MC.sprite = MCOutfits[outfitNumber];
            return;
        }
        temp = getCharacterOutfitArray(characterNumber);
        int whereCharacter = characterLoads[characterNumber];
        
        if (whereCharacter == -1)
        {
            return;
        }else if (whereCharacter == 0)
        {
            CharacterRenderer_1.sprite = temp[outfitNumber];
        }else if (whereCharacter == 1)
        {
            CharacterRenderer_2.sprite = temp[outfitNumber];
        }else if (whereCharacter == 2)
        {
            CharacterRenderer_3.sprite = temp[outfitNumber];
        }
    }*/

    public void loadMC(int n)
    {
        whichMC = n;
        if(n == 0)
        {
            //MCOutfit = HannahOutfit;
            MCExpressions = HannahExpressions;
        }else
        {
            //MCOutfit = EricOutfit;
            MCExpressions = EricExpressions;
        }

        FadeOut(MC);
        MC.sprite = MCExpressions[0];
        
        FadeIn(MC);
        return;
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

