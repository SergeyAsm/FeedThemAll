using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public enum State
    {
        Appeared,
        Incoming,
        WaitingForMeal,
        GoAway,
        Disappears
    }
    public State state = State.Appeared;

    public Order order;
    private ClientTrail trail;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Appeared:
                break;
            case State.Incoming:
                break;
            case State.WaitingForMeal:
                break;
            case State.GoAway:
//                state = State.GoAway;
                trail.SetFeeded();
                break;
            case State.Disappears:
                GameObject.Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    public void SetTrail(ClientTrail newTrail)
    {
        Debug.Assert(!trail);

        trail = GameObject.Instantiate(newTrail.gameObject).GetComponent<ClientTrail>();
        trail.client = this;
        trail.SetReady();

        if (!TryGetComponent<FollowerScript>(out var followerScript))
        {
            followerScript = gameObject.AddComponent<FollowerScript>();
        }
        followerScript.FollowTo = trail.walkingHelper.gameObject;
        state = State.Incoming;
    }
    public bool TryFeed(MealData meal)
    {
        var result  = order.TryFeed(meal);
        if (result)
        {
            if (order.IsComplete())
            {
                CompleteOrder();
//                state = State.GoAway;
//                trail.SetFeeded();
            }
        }
        return result;
    }
    public void CompleteOrder()
    {
        order.SetComplete();
        state = State.GoAway;
        trail.SetFeeded();
    }
    private void OnDestroy()
    {
        if (trail)
        {
            GameObject.Destroy(trail.gameObject);
        }
    }

}
