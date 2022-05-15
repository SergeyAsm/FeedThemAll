using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientTrail : MonoBehaviour
{
    public Animator walkingHelper;
    public Client client;
    public void SetReady()
    {
        walkingHelper.SetBool("IsReady", true);
    }
    public void SetFeeded()
    {
        walkingHelper.SetBool("IsFeeded", true);
    }
}
