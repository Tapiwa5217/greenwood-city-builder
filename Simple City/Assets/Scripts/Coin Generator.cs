using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinGenerator : MonoBehaviour
{
    public int Coins;
    public TMP_Text coinUI;
    public CoinManager[] CoinManager;
    public GameObject[] PanelsGO;
    public Template[] Panels;
    protected float Timer;
    public int DelayAmount = 1;
    //public RoadManager roadManager[];

    // Start is called before the first frame update
    void Start()
    {
       coinUI.text = "Coins: " + Coins.ToString();
    }

    public void AddCoins()
    {
        Timer += Time.deltaTime;
        if (Timer >= DelayAmount)
		{
			Timer = 0f;
			Coins++; 
            coinUI.text = "Coins: " + Coins.ToString();
        }
        
        Coins++;
        
    }

    //public void CheckPurchaseable()
    //{
        //for (int i = 0; i < CoinManager.Length; i++)
        //{
            //if (coins >= CoinManager[i].baseCost)
                //roadManager[i].interactable = true;
            //else
                //roadManager[i].interactable = false;
       //}
    //}

    // Update is called once per frame
    void Update()
    {

    }

}