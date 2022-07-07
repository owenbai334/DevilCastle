using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite[] ButtonSprites;
    public Image[] ButtonRealSprites;
    public GameObject[] Grids; //0 裝備 ,2武器,4藥水,6其他
    int CountGrid = 0;
    int Count = -1;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if(CountGrid==0)
        {
            Grids[0].SetActive(true);
            Grids[0].SetActive(false);
            Grids[1].SetActive(true);
            Grids[1].SetActive(false);
            Grids[2].SetActive(true);
            Grids[2].SetActive(false);
            Grids[0].SetActive(true);
            CountGrid++;
            if(CountGrid==1)
            {
                Grids[0].SetActive(false);
                Grids[4].SetActive(true);
                Grids[4].SetActive(false);
                Grids[0].SetActive(true);
                CountGrid=2;
                if(CountGrid==2)
                {
                    Grids[0].SetActive(false);
                    Grids[6].SetActive(true);
                    Grids[6].SetActive(false);
                    Grids[0].SetActive(true);
                }
                CountGrid=-1;
            }
        }
        
    }
    public void OnClickEquipButton()
    {
        Count = 0 ;
        Buttonsprite();
    }
    public void OnClickWeaponButton()
    {
        Count = 1 ;
        Buttonsprite();
    }
    public void OnClickPotionButton()
    {
        Count = 2 ;
        Buttonsprite();
    }
    public void OnClickOtherButton()
    {
        Count = 3 ;
        Buttonsprite();
    }
    void Buttonsprite()
    {
        ButtonRealSprites[0].sprite = ButtonSprites[0];
        ButtonRealSprites[1].sprite = ButtonSprites[0];
        ButtonRealSprites[2].sprite = ButtonSprites[0];
        ButtonRealSprites[3].sprite = ButtonSprites[0];
        Grids[0].SetActive(false);
        Grids[2].SetActive(false);
        Grids[4].SetActive(false);
        Grids[6].SetActive(false);
        ButtonRealSprites[Count].sprite = ButtonSprites[1];
        Grids[Count*2].SetActive(true);
        Count = -1;
    }
    
}
