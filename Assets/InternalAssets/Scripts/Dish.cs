using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    public GameObject meal;
    public void SetMeal(MealData newMeal)
    {
        if (meal)
        {
            GameObject.Destroy(meal);
        }
        meal = newMeal.Instantiate(transform);
    }
}
