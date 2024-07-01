using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinGenerator : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public CoinManager[] CoinManager;
    public GameObject[] PanelsGO;
    public Template[] Panels;

    // Start is called before the first frame update
    void Start()
    {
       for (int i = 0; i < CoinManager.Length; i++)
          PanelsGO[i].SetActive(true);
       coinUI.text = "Coins: " + coins.ToString();
        //LoadPanels();
    }

    public void AddCoins()
    {
        coins++;
        coinUI.text = "Coins: " + coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

}