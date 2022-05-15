using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSettings 
{
    public int defaultBoosters;

    public int maxMeals;
    public int maxClients;
    public int levelTimeInSeconds;

    [Range(1,4)]
    public int maxOrderSize = 3;

    [HideInInspector]
    public List<OrderDataStrings> ordersSerialize;
}
