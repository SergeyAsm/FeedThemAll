using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public ClientsController clients;
    public FoodCounter counter;
    public LevelController level;
    public InterfaceController interfaceController;
    public SettingsData settingsData;

    public enum State
    {
        Play,
        Victory,
        Defeat
    }
    public State state;
    void Awake()
    {
        if (instance)
        {
            throw new System.Exception("GameController double init!");
        }
        instance = this;
        Init();
    }
    private void Init()
    {

        level.ApplySettings(settingsData);
        state = State.Play;
        interfaceController.SetGameState();
    }

    public void Feed(int mealIndex)
    {
        var meal = counter.MealByIndex(mealIndex);
        clients.TryFeed(meal);
    }
    public void Defeat()
    {
        state = State.Defeat;
        interfaceController.SetGameOverState();
        clients.Clear();
        //Pause();
    }
    public void Victory()
    {
        state = State.Victory;
        interfaceController.SetGameDoneState();
        clients.Clear();
        //Pause();
    }
    public void RestartLevel()
    {
        clients.Clear();
        Resume();
        state = State.Play;
        interfaceController.SetGameState();
        level.ApplySettings(settingsData);
    }
    public void UseBooster()
    {
        if (clients.IsWaitingClients())
        {
            if (level.TryUseBooster())
            {
                clients.CompleteFirstOrder();
            }
            else
            {
                Pause();
                interfaceController.SetBuyBoosterState();
            }
        }
    }
    public void BuyBooster()
    {
        level.BuyBooster();
        Resume();
        interfaceController.SetGameState();
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Play:
                if (level.IsDone() && clients.IsNoneClients())
                {
                    Victory();
                }
                else if(level.IsTimeExpiried())
                {
                    Defeat();
                }
                break;
            case State.Victory:
                break;
            case State.Defeat:
                break;
            default:
                break;
        }
    }
}
