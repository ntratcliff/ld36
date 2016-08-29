using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateTextByRound : MonoBehaviour
{
    public string[] RoundText;
    
    // Update is called once per frame
    void Update()
    {
        RoundManager rm = FindObjectOfType<RoundManager>();
        if (rm != null)
            GetComponent<Text>().text = RoundText[rm.CurrentRound - 2];
        else
            GetComponent<Text>().text = "";

    }
}
