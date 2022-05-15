using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientsDisplay : MonoBehaviour
{
    public TMPro.TMP_Text cur;
    public TMPro.TMP_Text max;

    // Update is called once per frame
    void Update()
    {
        var gc = GameController.instance;
        cur.text =  gc.level.clients.ToString();
        max.text = gc.settingsData.settings.maxClients.ToString();
    }
}
