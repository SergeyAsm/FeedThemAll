using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookControl : MonoBehaviour
{
    public int index = 0;
    public void Feed()
    {
        GameController.instance.Feed(index);
    }
}
