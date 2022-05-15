using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public GameObject GameScreen;
    public GameObject GameOverScreen;
    public GameObject GameDoneScreen;
    public GameObject BuyBoosterScreen;

    public void SetGameState()
    {
        GameScreen.SetActive(true);
        GameOverScreen.SetActive(false);
        GameDoneScreen.SetActive(false);
        BuyBoosterScreen.SetActive(false);

    }
    public void SetGameOverState()
    {
        GameScreen.SetActive(false);
        GameOverScreen.SetActive(true);
        GameDoneScreen.SetActive(false);
        BuyBoosterScreen.SetActive(false);
    }
    public void SetGameDoneState()
    {
        GameScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GameDoneScreen.SetActive(true);
        BuyBoosterScreen.SetActive(false);
    }
    public void SetBuyBoosterState()
    {
        GameScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GameDoneScreen.SetActive(false);
        BuyBoosterScreen.SetActive(true);
    }
}
