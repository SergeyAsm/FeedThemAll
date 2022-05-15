using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{

    public MealData[] meals;
    public GameObject[] requests;
    public MealRequestPoint[] requestPoints;
    private void Start()
    {
        requestPoints = GetComponentsInChildren<MealRequestPoint>();
        requests = new GameObject[requestPoints.Length];
        GenerateRequests();
    }
    private void GenerateRequests()
    {
        for (int i = 0; i < meals.Length; i++)
        {
            if (i<requestPoints.Length)
            {
                requests[i] = meals[i].Instantiate(requestPoints[i].transform);
            }
        }
    }
    public bool TryFeed(MealData meal)
    {
        for (int i = 0; i < meals.Length; i++)
        {
            if (meals[i] == meal)
            {
                GameObject.Destroy(requests[i]);
                meals[i] = default;
                requests[i] = default;

                return true;
            }
        }
        return false;
    }

    public bool IsComplete()
    {
        foreach (var request in requests)
        {
            if (request)
            {
                return false;
            }
        }
        return true;
    }
    public void SetComplete()
    {
        requests = new GameObject[0];
    }


}
