using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script displaying number of player ships shades in right-top corner of a screen 
//corresponding to number of remainig lifes

public class LifesLeftDisplayScript : MonoBehaviour
{
    [SerializeField]
    private float lifesImageOffset = 40;

    [SerializeField]
    private GameObject lifesLeftImageTemplate;

    private GameObject[] lifesLeftImages;



    public void InitializeLifesDisplay(int maxLifesAmount)
    {
        lifesLeftImages = new GameObject[maxLifesAmount];

        Vector2 templatePosition = lifesLeftImageTemplate.GetComponent<RectTransform>().anchoredPosition;

        for (int i = 0; i < maxLifesAmount; i++)
        {

            lifesLeftImages[i] = Instantiate(lifesLeftImageTemplate, transform);
            lifesLeftImages[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(templatePosition.x - lifesImageOffset * i, templatePosition.y);
            lifesLeftImages[i].gameObject.SetActive(true);

        }
    }

    public void ChangeLifesDisplay(int lifesLeft)
    {
        for (int i = 0; i < lifesLeftImages.Length; i++)
        {
            if (i < lifesLeft)
            {

                lifesLeftImages[i].gameObject.SetActive(true);

            }
            else
            {

                lifesLeftImages[i].gameObject.SetActive(false);

            }
        }
    }
}
