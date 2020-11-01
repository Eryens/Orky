using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour {

    [HideInInspector]
    public int displayedHealth;
    [HideInInspector]
    public int numbOfTotalHearts;

    public Image[] heartsImages;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
	
	// Update is called once per frame
	void Update () {

        if (fullHeartSprite != null && emptyHeartSprite != null)
        {
            for (int i = 0; i < heartsImages.Length; i++)
            {
                if (heartsImages[i] != null)
                {
                    if (displayedHealth > numbOfTotalHearts)
                    {
                        displayedHealth = numbOfTotalHearts;
                    }

                    if (i < displayedHealth)
                        heartsImages[i].sprite = fullHeartSprite;
                    else
                        heartsImages[i].sprite = emptyHeartSprite;

                    if (i < numbOfTotalHearts)
                        heartsImages[i].enabled = true;
                    else
                        heartsImages[i].enabled = false;
                }
            }
        }
	}
}
