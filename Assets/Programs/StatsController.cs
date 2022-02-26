using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class StatsController : MonoBehaviour
{
    TextMeshProUGUI tmpgui;
    public bool P2;
    float dispscore;
    // Start is called before the first frame update
    void Start()
    {
        tmpgui = GetComponent<TextMeshProUGUI>();
        dispscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder str_tmp = new StringBuilder();
        if (P2 == false)
        {
            dispscore += (PlayerController.p1score - dispscore) * 0.1f;
            if (Mathf.Abs(PlayerController.p1score - dispscore) < 5) dispscore = PlayerController.p1score;
            str_tmp.Append(((int)Mathf.Floor(dispscore)).ToString("D8"));
            str_tmp.Append("\nChain:");
            str_tmp.Append((Mathf.Min(GameManager.p1chain, 99)).ToString("D2"));
        }
        else
        {
            dispscore += (P2Controller.p2score - dispscore) * 0.1f;
            if (Mathf.Abs(P2Controller.p2score - dispscore) < 5) dispscore = P2Controller.p2score;
            str_tmp.Append(((int)Mathf.Floor(dispscore)).ToString("D8"));
            str_tmp.Append("\nChain:");
            str_tmp.Append((Mathf.Min(GameManager.p2chain,99)).ToString("D2"));
        }
        tmpgui.text = str_tmp.ToString();
    }
}
