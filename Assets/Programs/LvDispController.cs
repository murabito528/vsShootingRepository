using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class LvDispController : MonoBehaviour
{
    public bool P2;
    TextMeshProUGUI tmpgui;
    // Start is called before the first frame update
    void Start()
    {
        tmpgui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (P2 == true)
        {
            StringBuilder str_tmp = new StringBuilder("Enemy:Lv");
            str_tmp.Append(GameManager.p1lv);
            tmpgui.text = str_tmp.ToString();
        }
        else
        {
            StringBuilder str_tmp = new StringBuilder("Lv");
            str_tmp.Append(GameManager.p1lv);
            str_tmp.Append(":");
            tmpgui.text = str_tmp.ToString();
        }
    }
}
