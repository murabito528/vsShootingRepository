using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Count_Controller : MonoBehaviour
{
    int frame;
    int count;
    Transform tf;
    TextMeshProUGUI tmpgui;
    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        count = 3;
        tf = transform;
        tmpgui = GetComponent<TextMeshProUGUI>();
        tmpgui.text = "3";
    }

    // Update is called once per frame
    void Update()
    {
        if (frame > 20)
        {
            tf.Rotate(Vector3.forward * 45);
            tf.localScale -= Vector3.one * 0.2f;
        }
        if (frame == 30)
        {
            switch (count)
            {
                case 3:
                    tmpgui.text = "2";
                    break;
                case 2:
                    tmpgui.text = "1";
                    break;
                case 1:
                    tmpgui.text = "Start!";
                    break;
                case 0:
                    GameManager.game_now = true;
                    this.gameObject.SetActive(false);
                    break;
            }
            tf.rotation = Quaternion.Euler(Vector3.zero);
            tf.localScale = Vector3.one;
            count--;
            frame = 0;
        }
        frame++;
    }
}
