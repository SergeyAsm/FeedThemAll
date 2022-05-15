using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCounter : MonoBehaviour
{
    public MealData[] meals;
    public Dish[] dishes;
    // Start is called before the first frame update
    void Start()
    {
        dishes = GetComponentsInChildren<Dish>();

        if (meals.Length == 0 || dishes.Length == 0) return;

        for (int i = 0; i < meals.Length; i++)
        {
            if (i < dishes.Length)
            {
                dishes[i].SetMeal(meals[i]);
            }
        }
    }

    public MealData MealByIndex(int index)
    {
        return meals[index];
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
