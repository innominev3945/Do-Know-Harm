using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UB.Simple2dWeatherEffects.Standard;
using UnityEngine.UI;

public class FogManager : MonoBehaviour
{
    private D2FogsPE fogScript;
    private bool gasMaskWorn;
    private Image bloodEffectImage;
    [SerializeField] private GameObject bloodEffect;
    private float setTime;
    private bool gasActive;
    private float startAlpha;
    private bool inTransition;
    [SerializeField] private GameObject gasMask;

    void Start()
    {
        fogScript = this.gameObject.GetComponent<D2FogsPE>();
        fogScript.enabled = false;
        bloodEffectImage = bloodEffect.GetComponent<Image>();
        setTime = -1; // negative num (or -1) is set as a null-like value
        inTransition = false;
        gasMask.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gasActive && !gasMaskWorn)
        {
            if (setTime < 0)
            {
                setTime = Time.time;
                startAlpha = bloodEffectImage.color.a;
            }
            else if (Time.time > (setTime + 15f))
            {
                Debug.Log("game over, failed to put on gas mask");
                // patient manager game over code blah
            }
            else
            {
                float alpha = (Time.time - setTime) / 15f;
                if (alpha > 1)
                {
                    alpha = 1;
                }
                Color temp2 = bloodEffectImage.color;
                bloodEffectImage.color = new Color(temp2.r, temp2.g, temp2.b, alpha);
            }
        }
        else
        {
            setTime = -1;
        }
    }

    public void ActivateFog()
    {
        StartCoroutine(ActivateFogSeq());
    }

    public IEnumerator WearGasMaskSeq()
    {
        Debug.Log("wearseq started");
        gasMask.SetActive(true); // add transition effects here
        float bloodAlpha = bloodEffectImage.color.a;
        Color temp1 = bloodEffectImage.color;
        while (bloodAlpha > 0)
        {
            bloodAlpha -= 0.005f;
            if (bloodAlpha <= 0)
            {
                bloodAlpha = 0;
                gasMaskWorn = true;
            }
            bloodEffectImage.color = new Color(temp1.r, temp1.g, temp1.b, bloodAlpha);
            yield return null;
            /*if ((Time.time) - tempTime > 2f)
            {
                break;
            }
            float fraction = 1 - ((Time.time - tempTime) / 2f);
            float tempAlpha = bloodAlpha * fraction;
            Color temp1 = bloodEffectImage.color;
            bloodEffectImage.color = new Color(temp1.r, temp1.g, temp1.b, tempAlpha);*/

        }
        Debug.Log("Wearseq concluded");
        inTransition = false;
    }

    public IEnumerator ActivateFogSeq()
    {
        fogScript.Density = 0;
        fogScript.enabled = true;
        while (fogScript.Density < 1.2f)
        {
            fogScript.Density += 0.01f;
            yield return null;
        }
        gasActive = true;
        /*float time = Time.time;
        while (Time.time < (time + 15f))
        {
            if (gasMaskWorn)
            {
                Color temp1 = bloodEffectImage.color;
                bloodEffectImage.color = new Color(temp1.r, temp1.g, temp1.b, 0);
                break;
            }
            float alpha = ((time + 15f) - Time.time) / 15f;
            if (alpha > 1)
            {
                alpha = 1;
            }
            Color temp2 = bloodEffectImage.color;
            bloodEffectImage.color = new Color(temp2.r, temp2.g, temp2.b, alpha); 
        }*/
    }

    public void WearGasMask()
    {
        if (gasActive && !inTransition)
        {
            inTransition = true;
            StartCoroutine(WearGasMaskSeq());
        }
    }

    public void RemoveGasMask()
    {
        if (gasMaskWorn && !inTransition)
        {
            gasMask.SetActive(false);
            gasMaskWorn = false;
        }
    }
    //bruh
    public void ToggleGasMask()
    {
        if (gasMaskWorn && !inTransition)
        {
            RemoveGasMask();
        }
        else if (!gasMaskWorn && !inTransition)
        {
            WearGasMask();
        }
    }
}
