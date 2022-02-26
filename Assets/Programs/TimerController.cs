using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    float start_time;
    TextMeshProUGUI tmpgui;
    void Start()
    {
        start_time = Time.time;
        tmpgui = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.game_end > 0)
        {
            return;
        }
        float time = Time.time - start_time;
        StringBuilder str_tmp = new StringBuilder("Time:");
        str_tmp.Append(((int)Mathf.Floor(time / 60)).ToString("D2"));
        str_tmp.Append(":");
        str_tmp.Append(((int)Mathf.Floor(time % 60)).ToString("D2"));
        tmpgui.text = str_tmp.ToString();
    }
}
