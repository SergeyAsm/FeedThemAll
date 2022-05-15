using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCounterControl : MonoBehaviour
{
    public TMPro.TMP_Text text;
    // Update is called once per frame
    void Update()
    {
        text.text = GameController.instance.level.boosters.ToString();
    }
}
