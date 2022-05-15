using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MealData", menuName = "Game/MealData", order = 1)]
public class MealData: ScriptableObject
{
    public GameObject model;
    public string Letter;
    public GameObject Instantiate(Transform parent)
    {
        if (!model)
            throw new System.Exception("Undefined model in " + name);

        var newObject = GameObject.Instantiate(model, parent);
        var text = newObject.GetComponentInChildren<TMPro.TMP_Text>();
        if (text)
        {
            text.text = Letter;
        }
        else
        {
            Debug.LogWarningFormat("Cannot find text component in " + model.name);
        }
        return newObject;
    }
    public override string ToString()
    {
        return name;
    }
}
