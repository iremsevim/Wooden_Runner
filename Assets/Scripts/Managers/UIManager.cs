using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
        [Header("Game Screen")]
        public Text levelName;
        public GameObject gameComplatedScreen;
        public GameObject gameOverScreen;
        public GameObject inGameScreen;
        public Text cointext;
       public Image complatedBar;

        [Header("Menu Screen")]
        public GameObject menuScreen;
        [Header("Loading Screen")]
        public GameObject loadingScreen;
        

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    public void UpdatelevelNameText(string title)
    {
        levelName.text = title;
    }
    public void UpdateCoinAmount(int coin)
    {
        cointext.text = coin.ToString();
    }
    public void UpdateComplatedBar(float amount)
    {
        complatedBar.fillAmount = amount;
    }
    public  void InGameToMenu()
    { 
        inGameScreen.SetActive(false);
      menuScreen.SetActive(true);
    }
    public  void MenuToIngame()
    {
      menuScreen.SetActive(false);
      inGameScreen.SetActive(true);
    }
    public void ShowHideGameFailedWindow(bool status)
    {
        gameOverScreen.SetActive(status);
    }
    public void ShowHideGameComplatedScreen(bool status)
    {
        gameComplatedScreen.SetActive(status);
    }

    public  void ShowHideLoading(bool status)
    {
        loadingScreen.SetActive(status);
    }

}
