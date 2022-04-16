/*
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
    private const int numCharacters = 4;


    private int whichMC = 0;

    [SerializeField] SpriteRenderer CharacterRenderer_1;
    [SerializeField] SpriteRenderer CharacterRenderer_2;
    [SerializeField] SpriteRenderer CharacterRenderer_3;
    [SerializeField] SpriteRenderer MC;

    public Sprite[] HannahExpressions;
    public Sprite[] EricExpressions;
    public Sprite[] CaptainExpressions;
    public Sprite[] ElderlyLadyExpressions;
    public Sprite[] EricFamilyExpressions;

    //temporary storage to change sprites of a character
    private Sprite[] temp;

    //store who's the MC's sprite array here
    private Sprite[] MCExpressions;
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

    


    private Sprite[] getCharacterExpressionArray(int characterNumber)
    {
        if (characterNumber == 0)
            return HannahExpressions;
        if (characterNumber == 1)
            return EricExpressions;
        if (characterNumber == 2)
            return CaptainExpressions;
        if (characterNumber == 3)
            return ElderlyLadyExpressions;
        else
            return EricFamilyExpressions;
    }

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
        if (currCharacterNum == 1)
        {
            //CharacterRenderer_1.color.a = 0;
            FadeOut(CharacterRenderer_1);
            
            temp = getCharacterExpressionArray(n);
            characterLoads[n] = 0;
            CharacterRenderer_1.sprite = temp[0];
            FadeIn(CharacterRenderer_1);
            loadedCharacters[0] = n;
            
            //loadedCharacter[]
            //GameObject.transform(some position)
        }else if (currCharacterNum == 2)
        {
            CharacterRenderer_1.transform.position += Vector3.left * 100.0f;

            CharacterRenderer_2.transform.position += Vector3.right * 100.0f;

            temp = getCharacterExpressionArray(n);
            characterLoads[n] = 1;
            FadeOut(CharacterRenderer_2);
            CharacterRenderer_2.sprite = temp[0];
            FadeIn(CharacterRenderer_2);
            loadedCharacters[1] = n;

            //CharacterRenderer_1.transform
            //GameObject.transform(some position)
            //GameObject.transform(some position)
        }
        else if (currCharacterNum == 3)
        {
            CharacterRenderer_1.transform.position += Vector3.left * 50.0f;
            CharacterRenderer_2.transform.position += Vector3.left * 100.0f;
            CharacterRenderer_3.transform.position += Vector3.right * 150.0f;

            temp = getCharacterExpressionArray(n);
            characterLoads[n] = 2;
            FadeOut(CharacterRenderer_3);
            CharacterRenderer_3.sprite = temp[0];
            FadeIn(CharacterRenderer_3);
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
                CharacterRenderer_1.transform.position += Vector3.right * 50.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 150.0f;
                CharacterRenderer_3.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[2] = -1;
            }else if (characterLocation == 1)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 50.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 150.0f;
                CharacterRenderer_2.sprite = CharacterRenderer_3.sprite;
                CharacterRenderer_3.sprite = null;
                characterLoads[loadedCharacters[1]] = -1;
                characterLoads[loadedCharacters[2]] = 1;
                loadedCharacters[1] = loadedCharacters[2];
                loadedCharacters[2] = -1;
            }
            else if (characterLocation == 0)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 50.0f;
                CharacterRenderer_2.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_3.transform.position += Vector3.left * 150.0f;
                CharacterRenderer_1.sprite = CharacterRenderer_2.sprite;
                CharacterRenderer_2.sprite = CharacterRenderer_3.sprite;
                CharacterRenderer_3.sprite = null;
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
                CharacterRenderer_1.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_2.transform.position += Vector3.left * 100.0f;
                CharacterRenderer_2.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[1] = -1;
            }else if (characterLocation == 0)
            {
                CharacterRenderer_1.transform.position += Vector3.right * 100.0f;
                CharacterRenderer_2.transform.position += Vector3.left * 100.0f;
                CharacterRenderer_1.sprite = CharacterRenderer_2.sprite;
                CharacterRenderer_2.sprite = null;
                characterLoads[n] = -1;
                loadedCharacters[0] = loadedCharacters[1];
                loadedCharacters[1] = -1;
            }
            return;
        }else if (currCharacterNum == 0)
        {        
            CharacterRenderer_1.sprite = null;
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
            MC.sprite = MCExpressions[expressionNumber];
            return;
        }
        temp = getCharacterExpressionArray(characterNumber);
        int whereCharacter = characterLoads[characterNumber];
        
        if (whereCharacter == -1)
        {
            return;
        }else if (whereCharacter == 0)
        {
            CharacterRenderer_1.sprite = temp[expressionNumber];
        }else if (whereCharacter == 1)
        {
            CharacterRenderer_2.sprite = temp[expressionNumber];
        }else if (whereCharacter == 2)
        {
            CharacterRenderer_3.sprite = temp[expressionNumber];
        }
    }

    public void loadMC(int n)
    {
        whichMC = n;
        if(n == 0)
        {
            MCExpressions = HannahExpressions;
        }else
        {
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

*/