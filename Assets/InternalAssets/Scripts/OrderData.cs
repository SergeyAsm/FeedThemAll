using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class OrderData 
{
    public List<MealData> meals = new List<MealData>();
}


[Serializable]
public class OrderDataStrings
{
    public List<string> meals = new List<string>();
}