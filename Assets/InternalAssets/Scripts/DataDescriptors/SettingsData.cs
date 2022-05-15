using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[CreateAssetMenu(fileName = "Settings", menuName = "Game/Settings", order = 1)]
public class SettingsData : ScriptableObject
{
    public string storageJson;
    public bool autoloadJson = false;
    public LevelSettings settings = new LevelSettings();

    [HideInInspector]
    public List<MealData> avalableMeals;

    public List<OrderData> orders;

    private void OnEnable()
    {

        //Тут по-хорошему стоило бы юзать addresables,но в ТЗ про них ни чего не было
#if UNITY_EDITOR
        avalableMeals = new List<MealData>();
        var meals = AssetDatabase.FindAssets("t:MealData", null);
        foreach (var meal in meals)
        {
            avalableMeals.Add(
            (MealData)AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(meal)));
        }
#endif
        if (autoloadJson)
        {
            LoadFromJson();
        }
    }

    private bool TryFindMealByName(string mealName,out MealData mealData)
    {
        foreach (var meal in avalableMeals)
        {
            if (meal.name == mealName)
            {
                mealData = meal;
                return true;
            }
        }
        mealData = default;
        return false;
    }
    private void OrdersFromJsonData()
    {
        orders = new List<OrderData>();
        foreach (var order in settings.ordersSerialize)
        {
            var newOrder = new OrderData();
            orders.Add(newOrder);
            foreach (var mealName in order.meals)
            {
                if (TryFindMealByName(mealName,out var meal))
                {
                    newOrder.meals.Add(meal);
                }
            }
        }
    }

    public void LoadFromJson()
    {
        try
        {
            var reader = new StreamReader(storageJson);
            var json = reader.ReadToEnd();
            settings = JsonUtility.FromJson<LevelSettings>(json);
            reader.Close();
            OrdersFromJsonData();
        }
        catch (System.IO.FileNotFoundException)
        {
            Debug.LogErrorFormat("Not found settings {0} data json {1}", name, storageJson);
            //use defaults
            //throw;
        }
    }
    private void OrdersToJsonData()
    {
        settings.ordersSerialize = new List<OrderDataStrings>();
        foreach (var order in orders)
        {
            var newOrder = new OrderDataStrings();
            settings.ordersSerialize.Add(newOrder);
            foreach (var meal in order.meals)
            {
                newOrder.meals.Add(meal.name);
            }
        }
    }


    public void SaveToJson()
    {
        OrdersToJsonData();
        var json = JsonUtility.ToJson(settings);

        StreamWriter writer = new StreamWriter(storageJson, false);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

}
