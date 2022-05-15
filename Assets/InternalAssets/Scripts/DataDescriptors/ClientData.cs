using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ClientData", menuName = "Game/ClientData", order = 1)]
public class ClientData : ScriptableObject
{
    public GameObject model;
    public Client Instantiate(Transform parent)
    {
        if (!model)
            throw new System.Exception("Undefined model in " + name);
        var newObject = GameObject.Instantiate(model, parent);
        return newObject.GetComponent<Client>();
    }
}
