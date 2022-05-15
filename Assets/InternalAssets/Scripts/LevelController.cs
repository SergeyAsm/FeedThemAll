using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public int clients;
    public int meals;
    public float time;

    public int boosters;
    public int maxOrderSize;

    private List<OrderData> ordersQuery = new List<OrderData>();
    private int LastOrderId;

    public MealData[] avalableMeals;

    private void Update()
    {
        time -= Time.deltaTime;
    }
    public bool IsDone()
    {
        return clients <= 0 || meals <= 0;
    }
    public bool IsTimeExpiried()
    {
        return time <= 0;
    }
    public bool TryGetNextClientOrderData(out MealData[] orderMeals)
    {
        orderMeals = default;
        if (clients <= 0) return false;
        if (meals <= 0) return false;

        if (ordersQuery.Count==0)
        {
            if (!TryGenerateRandomOrder(out orderMeals))
            {
                return false;
            }
        }
        else
        {
            if (!TryGenerateAssignedOrder(out orderMeals))
            {
                return false;
            }
        }
        meals -= orderMeals.Length;
        clients--;
        return true;
    }
    private bool TryGenerateRandomOrder(out MealData[] orderMeals)
    {
        orderMeals = default;
        var nextMealsCount = Mathf.Min(Random.Range(1, maxOrderSize), meals);

        orderMeals = new MealData[nextMealsCount];
        for (int i = 0; i < nextMealsCount; i++)
        {
            orderMeals[i] = RandomMeal();
        }
        return true;
    }

    public bool TryUseBooster()
    {
        if (boosters>0)
        {
            boosters--;
            return true;
        }
        return false;
    }
    public void BuyBooster()
    {
        boosters++;
    }

    private OrderData GetNextAssignedOrder()
    {
        LastOrderId++;
        if (LastOrderId >= ordersQuery.Count) LastOrderId = 0;
        return ordersQuery[LastOrderId];
    }
    private bool TryGenerateAssignedOrder(out MealData[] orderMeals)
    {
        orderMeals = default;
        var nextOrder = GetNextAssignedOrder();

        orderMeals = nextOrder.meals.ToArray();
        return true;
    }

    private MealData RandomMeal()
    {
        return avalableMeals[Random.Range(0, avalableMeals.Length)];
    }
    public void ApplySettings(SettingsData settingsData)
    {
        var settings = settingsData.settings;
        clients = settings.maxClients;
        meals = settings.maxMeals;
        time = settings.levelTimeInSeconds;
        maxOrderSize = settings.maxOrderSize;
        ordersQuery = settingsData.orders;
        LastOrderId = 0;

        boosters = settings.defaultBoosters;

    }
}
