using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsController : MonoBehaviour
{
    private ClientTrail[] trails;
    private Client[] clientsByTrails;
    private List<Client> clientsByOrder = new List<Client>();

    public GameObject clients;
    public ClientData clientDescriptor;

    private void Start()
    {
        trails = GetComponentsInChildren<ClientTrail>();
        //clients = GetComponentInChildren<Clients>();
        clientsByTrails = new Client[trails.Length];
    }
    private void Update()
    {
        for (int i = 0; i < clientsByTrails.Length; i++)
        {
            if (clientsByTrails[i]) continue;
            if (!GameController.instance.level.TryGetNextClientOrderData(out var clientOrderData)) continue;

            var newClient = clientDescriptor.Instantiate(clients.transform);
            newClient.order.meals = clientOrderData;
            clientsByTrails[i] = newClient;
            newClient.SetTrail(trails[i]);
            clientsByOrder.Add(newClient);
        }
        ClearOneDeleted();
    }

    public void CompleteFirstOrder()
    {
        foreach (var client in clientsByOrder)
        {
            if (!client) continue;
            if (client.state != Client.State.WaitingForMeal) continue;
            client.CompleteOrder();
            ClearByTrail(client);
            return;
        }
    }
    private void ClearByTrail(Client client)
    {
        for (int i = 0; i < clientsByTrails.Length; i++)
        {
            if (clientsByTrails[i] == client)
            {
                clientsByTrails[i] = default;
                return;
            }
        }
    }
    public bool TryFeed(MealData meal)
    {
        foreach (var client in clientsByOrder)
        {
            if (!client) continue;
            if (client.state != Client.State.WaitingForMeal) continue;

            var result = client.TryFeed(meal);
            if (result)
            {
                if (((int)client.state) > ((int)Client.State.WaitingForMeal))
                {
//                    clientsByOrder.Remove(client);
                    ClearByTrail(client);
                }
                return result;
            }
        }
        return false;
    }
    private void ClearOneDeleted()
    {
        for (int i = 0; i < clientsByOrder.Count; i++)
        {
            if (!clientsByOrder[i])
            {
                clientsByOrder.RemoveAt(i);
                return;
            }
        }
    }
    public bool IsNoneClients()
    {
        foreach (var client in clientsByOrder)
        {
            if (client && ((int)client.state)<=((int)Client.State.WaitingForMeal))
            {
                return false;
            }
        }
        return true;
//        return clientsByOrder.Count == 0;
    }
    public bool IsWaitingClients()
    {
        foreach (var client in clientsByOrder)
        {
            if (client && client.state == Client.State.WaitingForMeal)
            {
                return true;
            }
        }
        return false;
        //        return clientsByOrder.Count == 0;
    }
    public void Clear()
    {
        foreach (var client in clientsByOrder)
        {
            if (client)
            {
                GameObject.Destroy(client.gameObject);
            }
        }
        clientsByOrder.Clear();
        for (int i = 0; i < clientsByTrails.Length; i++)
        {
            clientsByTrails[i] = default;
        }
    }
}
