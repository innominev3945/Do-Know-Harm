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

    [SerializeField] private float CharacterFadeInSpeed;

    //ADJUST AS NEEDED 
    private const int numCharacters = 5;


    private int whichMC = 0;

    [SerializeField] SpriteRenderer CharacterRenderer_1;
    [SerializeField] SpriteRenderer CharacterRenderer_2;
    [SerializeField] SpriteRenderer CharacterRenderer_3;
    [SerializeField] SpriteRenderer CharacterExpression_1;
    [SerializeField] SpriteRenderer CharacterExpression_2;
    [SerializeField] SpriteRenderer CharacterExpression_3;
    [SerializeField] SpriteRenderer CharacterAccessory_1;
    [SerializeField] SpriteRenderer CharacterAccessory_2;
    [SerializeField] SpriteRenderer CharacterAccessory_3;

    [SerializeField] SpriteRenderer MC;
    [SerializeField] SpriteRenderer MCExpr;
    [SerializeField] SpriteRenderer MCAccessory;
    
    

    //public Sprite[] CharacterBases;
    public Sprite[] HannahExpressions;
    public Sprite[] EricExpressions;
    public Sprite[] CaptainExpressions;
    public Sprite[] ElderlyLady1Expressions;
    public Sprite[] AmphiteranMan1Expressions;

//Add in when outfit sprites added
    
    public Sprite[] HannahOutfits;
    public Sprite[] EricOutfits;
    public Sprite[] CaptainOutfits;
    public Sprite[] ElderlyLady1Outfits;
    public Sprite[] AmphiteranMan1Outfits;

    public Sprite[] HannahAccessories;
    public Sprite[] EricAccessories;
    public Sprite[] CaptainAccessories;
    public Sprite[] ElderlyLady1Accessories;
    public Sprite[] AmphiteranMan1Accessories;
    
    

    //temporary storage to change sprites of a character
    private Sprite[] temp;

    //store who's the MC's sprite array here
    private Sprite[] MCExpressions;

    private Sprite[] MCOutfit;

    private Sprite[] MCAccessories;
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

    //************************************************************************************
    //TODO: Add in expressions
    //***********************************************************************************
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
            StartCoroutine(ExitFade(CharacterRenderer_1, CharacterExpression_1, CharacterAccessory_1));
            temp = getCharacterOutfitArray(newCharacter);
            CharacterRenderer_1.sprite = temp[0];
            temp = getCharacterExpressionArray(newCharacter);
            CharacterExpression_1.sprite = temp[0];
            temp = getCharacterAccessoryArray(newCharacter);
            StartCoroutine(EnterFade(CharacterRenderer_1, CharacterExpression_1, CharacterAccessory_1));
        }else if (whichToChange == 1)
        {
            StartCoroutine(ExitFade(CharacterRenderer_2, CharacterExpression_2, CharacterAccessory_2));
            temp = getCharacterOutfitArray(newCharacter);
            CharacterRenderer_2.sprite = temp[0];
            temp = getCharacterExpressionArray(newCharacter);
            CharacterExpression_2.sprite = temp[0];
            temp = getCharacterAccessoryArray(newCharacter);
            CharacterAccessory_2.sprite = temp[0];
            StartCoroutine(EnterFade(CharacterRenderer_2, CharacterExpression_2, CharacterAccessory_2));
        }else
        {
            StartCoroutine(ExitFade(CharacterRenderer_3, CharacterExpression_3, CharacterAccessory_3));
            temp = getCharacterOutfitArray(newCharacter);
            CharacterRenderer_3.sprite = temp[0];
            temp = getCharacterExpressionArray(newCharacter);
            CharacterExpression_3.sprite = temp[0];
            temp = getCharacterAccessoryArray(newCharacter);
            CharacterAccessory_3.sprite = temp[0];
            StartCoroutine(EnterFade(CharacterRenderer_3, CharacterExpression_3, CharacterAccessory_3));
        }
    
    }

    
    public int getCharacterNumber()
    {
        return numCharacters;
    }

    private Sprite[] getCharacterExpressionArray(int characterNumber)
    {
        if (characterNumber == 1)
            return HannahExpressions;
        if (characterNumber == 0)
            return EricExpressions;
        if (characterNumber == 2)
            return CaptainExpressions;
        if (characterNumber == 3)
            return ElderlyLady1Expressions;
        else
            return AmphiteranMan1Expressions;
    }

    
    private Sprite[] getCharacterOutfitArray(int characterNumber)
    {
        if (characterNumber == 1)
            return HannahOutfits;
        if (characterNumber == 0)
            return EricOutfits;
        if (characterNumber == 2)
            return CaptainOutfits;
        if (characterNumber == 3)
            return ElderlyLady1Outfits;
        else
            return AmphiteranMan1Outfits;
    }

     private Sprite[] getCharacterAccessoryArray(int characterNumber)
    {
        if (characterNumber == 1)
            return HannahAccessories;
        if (characterNumber == 0)
            return EricAccessories;
        if (characterNumber == 2)
            return CaptainAccessories;
        if (characterNumber == 3)
            return ElderlyLady1Accessories;
        else
            return AmphiteranMan1Accessories;
    }

    void Awake()
    {
        for (int i = 0; i < numCharacters; i++)
        {
            characterLoads[i] = -1;
        }
        //MC.size += new Vector2(1.0f, 1.0f);
    }


    public void loadCharacter(int n)
    {
        //Debug.Log("Tryna load a character: " + n);
        currCharacterNum++;
        //If there aren't any characters, load in character to center
        if (currCharacterNum == 1)
        {
            //Load in new character and their default expression from their expression array
            
            //fade out to hide what's happening
            StartCoroutine(FadeOut(CharacterRenderer_1));
            StartCoroutine(FadeOut(CharacterExpression_1));
            StartCoroutine(FadeOut(CharacterAccessory_1));
            
            //get the character's default base visual (remove later)
            //CharacterRenderer_1.sprite = CharacterBases[n];

            //To add once we get different costumes
            
            temp = getCharacterOutfitArray(n);
            characterLoads[n] = 0;
            CharacterRenderer_1.sprite = temp[0];
        
            


            //Loads the expression array of the character
            temp = getCharacterExpressionArray(n);
            //characterLoads[n] = 0;
            CharacterExpression_1.sprite = temp[0];
            
            temp = getCharacterOutfitArray(n);
            CharacterAccessory_1.sprite = temp[0];
            
            StartCoroutine(EnterFade(CharacterRenderer_1, CharacterExpression_1, CharacterAccessory_1));
            // FadeIn(CharacterExpression_1);
            // FadeIn(CharacterRenderer_1);
            loadedCharacters[0] = n;
            
            //loadedCharacter[]
            //GameObject.transform(some position)
        }
        //If there's already a character in the scene, move that character over to the left
        //and load the new character in
        else if (currCharacterNum == 2)
        {
            CharacterRenderer_1.transform.position += Vector3.left * 400.0f;

            CharacterRenderer_2.transform.position += Vector3.right * 400.0f;


            

            //hide all the nasty deets from the player
            StartCoroutine(FadeOut(CharacterRenderer_2));
            StartCoroutine(FadeOut(CharacterExpression_2));
            StartCoroutine(FadeOut(CharacterAccessory_2));
            
            //load the base default sprite (TO REMOVE)
            //CharacterRenderer_2.sprite = CharacterBases[n];

            //TO ADD
            
            temp = getCharacterOutfitArray(n);
            characterLoads[n] = 1;
            CharacterRenderer_2.sprite = temp[0];
           

            temp = getCharacterExpressionArray(n);
            //characterLoads[n] = 1;
            CharacterExpression_2.sprite = temp[0];

            temp = getCharacterAccessoryArray(n);
            CharacterAccessory_2.sprite = temp[0];

            StartCoroutine(EnterFade(CharacterRenderer_2, CharacterExpression_2, CharacterAccessory_2));
            // FadeIn(CharacterRenderer_2);
            // FadeIn(CharacterExpression_2);

            loadedCharacters[1] = n;

            //CharacterRenderer_1.transform
            //GameObject.transform(some position)
            //GameObject.transform(some position)
        }
        else if (currCharacterNum == 3)
        {
            CharacterRenderer_1.transform.position += Vector3.left * 200.0f;
            CharacterRenderer_2.transform.position += Vector3.left * 400.0f;
            CharacterRenderer_3.transform.position += Vector3.right * 600.0f;


            StartCoroutine(FadeOut(CharacterRenderer_3));
            StartCoroutine(FadeOut(CharacterExpression_3));
            StartCoroutine(FadeOut(CharacterAccessory_3));


            characterLoads[n] = 2;   
            temp = getCharacterOutfitArray(n);
            CharacterRenderer_1.sprite = temp[0];
            


            temp = getCharacterExpressionArray(n);
                     
            CharacterExpression_3.sprite = temp[0];


            temp = getCharacterAccessoryArray(n);
            CharacterAccessory_3.sprite = temp[0];


            StartCoroutine(EnterFade(CharacterRenderer_3, CharacterExpression_3, CharacterAccessory_3));

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
                CharacterRenderer_1.transform.position += Vector3.right * 200.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 400.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 600.0f;
                CharacterRenderer_3.sprite = null;
                CharacterExpression_3.sprite = null;
                CharacterAccessory_3.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[2] = -1;
            }else if (characterLocation == 1)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 200.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 400.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 600.0f;
                CharacterRenderer_2.sprite = CharacterRenderer_3.sprite;
                CharacterExpression_2.sprite = CharacterExpression_3.sprite;
                CharacterAccessory_2.sprite = CharacterAccessory_3.sprite;
                CharacterRenderer_3.sprite = null;
                CharacterExpression_3.sprite = null;
                CharacterAccessory_3.sprite = null;
                characterLoads[n] = -1;
                characterLoads[loadedCharacters[2]] = 1;
                loadedCharacters[1] = loadedCharacters[2];
                loadedCharacters[2] = -1;
            }
            else if (characterLocation == 0)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 200.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 400.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 600.0f;
                CharacterRenderer_1.sprite = CharacterRenderer_2.sprite;
                CharacterExpression_1.sprite = CharacterExpression_2.sprite;
                CharacterAccessory_1.sprite = CharacterAccessory_2.sprite;
                CharacterRenderer_2.sprite = CharacterRenderer_3.sprite;
                CharacterExpression_2.sprite = CharacterExpression_3.sprite;
                CharacterAccessory_2.sprite = CharacterAccessory_3.sprite;
                CharacterRenderer_3.sprite = null;
                CharacterExpression_3.sprite = null;
                CharacterAccessory_3.sprite = null;
                characterLoads[n] = -1;
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
                CharacterRenderer_1.transform.position += Vector3.right * 400.0f;
                CharacterRenderer_2.transform.position += Vector3.left * 400.0f;
                CharacterRenderer_2.sprite = null;
                CharacterExpression_2.sprite = null;
                CharacterAccessory_2.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[1] = -1;
            }else if (characterLocation == 0)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 400.0f;
                CharacterRenderer_2.transform.position += Vector3.left * 400.0f;
                CharacterRenderer_1.sprite = CharacterRenderer_2.sprite;
                CharacterExpression_1.sprite = CharacterExpression_2.sprite;
                CharacterAccessory_1.sprite = CharacterAccessory_2.sprite;
                CharacterRenderer_2.sprite = null;
                CharacterExpression_2.sprite = null;
                CharacterAccessory_2.sprite = null;
                characterLoads[n] = -1;
                characterLoads[loadedCharacters[1]] = 0;
                loadedCharacters[0] = loadedCharacters[1];
                loadedCharacters[1] = -1;
            }
            return;
        }else if (currCharacterNum == 0)
        {
            //Debug.Log("Removed Char 0");        
            CharacterRenderer_1.sprite = null;
            CharacterExpression_1.sprite = null;
            CharacterAccessory_1.sprite = null;
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
    
    public void changeOutfit(int characterNumber, int outfitNumber)
    {
        
        if (characterNumber == whichMC)
        {
            MC.sprite = MCOutfit[outfitNumber];
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
    }

    public void changeAccessory(int characterNumber, int accessoryNumber)
    {
        
        if (characterNumber == whichMC)
        {
            MCAccessory.sprite = MCAccessories[accessoryNumber];
            return;
        }
        temp = getCharacterAccessoryArray(characterNumber);
        int whereCharacter = characterLoads[characterNumber];
        
        if (whereCharacter == -1)
        {
            return;
        }else if (whereCharacter == 0)
        {
            CharacterAccessory_1.sprite = temp[accessoryNumber];
        }else if (whereCharacter == 1)
        {
            CharacterAccessory_2.sprite = temp[accessoryNumber];
        }else if (whereCharacter == 2)
        {
            CharacterAccessory_3.sprite = temp[accessoryNumber];
        }
    }

    public void loadMC(int n)
    {
        whichMC = n;
        if(n == 1)
        {
            //MCOutfit = HannahOutfit;
            MCExpressions = HannahExpressions;
            MCOutfit = HannahOutfits;
            MCAccessories = HannahAccessories;
        }else
        {
            //MCOutfit = EricOutfit;
            MCExpressions = EricExpressions;
            MCOutfit = EricOutfits;
            MCAccessories = EricAccessories;
        }

        FadeOut(MC);
        FadeOut(MCExpr);
        MC.sprite = MCOutfit[0];
        MCExpr.sprite = MCExpressions[0];
        MCAccessory.sprite = MCAccessories[0];
        
        // FadeIn(MC);
        // FadeIn(MCExpr);
        StartCoroutine(EnterFade(MC, MCExpr, MCAccessory));
        return;
    }


    public void clearCharacters()
    {
        if (loadedCharacters[2] != -1)
        {
            unloadCharacter(loadedCharacters[2]);
        }
        if (loadedCharacters[1] != -1)
        {
            unloadCharacter(loadedCharacters[1]);
        }
        if (loadedCharacters[0] != -1)
        {
            unloadCharacter(loadedCharacters[0]);
        }
        if (MCExpr.sprite != null)
        {
            MC.sprite = null;
            MCExpr.sprite = null;
            MCAccessory.sprite = null;
        }
    }

    
    IEnumerator FadeOut(SpriteRenderer spriteRenderer){
        Color objectColor = spriteRenderer.color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        spriteRenderer.color = objectColor;
        yield return null;
        /*while (spriteRenderer.color.a > 0)
        {
            Color objectColor = spriteRenderer.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            spriteRenderer.color = objectColor;
            yield return null;
        }*/
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

    IEnumerator EnterFade(SpriteRenderer outfitRenderer, SpriteRenderer expressionRenderer, SpriteRenderer accessoryRenderer){
        // Color objectColor = outfitRenderer.color;
        // objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        // outfitRenderer.color = objectColor;
        // //outfitRenderer.a = 1;
        // objectColor = expressionRenderer.color;
        // objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        // expressionRenderer.color = objectColor;


        // objectColor = accessoryRenderer.color;
        // objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        // accessoryRenderer.color = objectColor;
        // //expressionRenderer.color = objectColor;
        // //outfitRenderer.color = objectColor;
        // //accessoryColor.a = 1;

        // // outfitRenderer.color = outfitColor;
        // // expressionRenderer.color = expressionColor;
        // // accessoryRenderer.color = accessoryColor;
        // yield return null;
        // //Debug.Log("NYAAAAAAAAAH Attempted Fade");
        while (expressionRenderer.color.a < 1)
        {
            Color outfitColor = outfitRenderer.color;
            Color expressionColor = expressionRenderer.color;
            Color accessoryColor = accessoryRenderer.color;
            float outfitFadeAmount = outfitColor.a + (CharacterFadeInSpeed * Time.deltaTime);
            float expressionFadeAmount = expressionColor.a + (CharacterFadeInSpeed * Time.deltaTime);
            float accessoryFadeAmount = accessoryColor.a + (CharacterFadeInSpeed * Time.deltaTime);

            outfitColor = new Color(outfitColor.r, outfitColor.g, outfitColor.b, outfitFadeAmount);
            expressionColor = new Color(expressionColor.r, expressionColor.g, expressionColor.b, expressionFadeAmount);
            accessoryColor = new Color(accessoryColor.r, accessoryColor.g, accessoryColor.b, accessoryFadeAmount);
            outfitRenderer.color = outfitColor;
            expressionRenderer.color = expressionColor;
            accessoryRenderer.color = accessoryColor;
            yield return null;
        }
    }
    IEnumerator ExitFade(SpriteRenderer outfitRenderer, SpriteRenderer expressionRenderer, SpriteRenderer accessoryRenderer){
        //Debug.Log("NYAAAAAAAAAH Attempted Fade");
        while (outfitRenderer.color.a > 0)
        {
            Color outfitColor = outfitRenderer.color;
            Color expressionColor = expressionRenderer.color;
            Color accessoryColor = accessoryRenderer.color;
            float outfitFadeAmount = outfitColor.a - (CharacterFadeInSpeed * Time.deltaTime);
            float expressionFadeAmount = expressionColor.a - (CharacterFadeInSpeed * Time.deltaTime);
            float accessoryFadeAmount = accessoryColor.a - (CharacterFadeInSpeed * Time.deltaTime);

            outfitColor = new Color(outfitColor.r, outfitColor.g, outfitColor.b, outfitFadeAmount);
            expressionColor = new Color(expressionColor.r, expressionColor.g, expressionColor.b, expressionFadeAmount);
            accessoryColor = new Color(accessoryColor.r, accessoryColor.g, accessoryColor.b, accessoryFadeAmount);
            outfitRenderer.color = outfitColor;
            expressionRenderer.color = expressionColor;
            accessoryRenderer.color = accessoryColor;
            yield return null;
        }
    }
}

