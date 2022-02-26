using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar_Controller : MonoBehaviour
{
    public bool p2;
    public int maxhp;
    public int minhp;

    Transform tf;
    Slider slider;
    Image img;
    Color32 color32_one;
    Color32 color32_64;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        tf = GetComponent<Transform>();
        color32_one = new Color32(255,255,255,255);
        color32_64 = new Color32(64,64,64,255);
        img = GetComponentInChildren<Image>();
        slider.maxValue = maxhp;
        slider.minValue = minhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (p2 == true)
        {
            if (P2Controller.p2hp <= maxhp && P2Controller.p2hp > minhp)
            {
                slider.value = P2Controller.p2hp;

                tf.localScale = Vector3.one * 1.1f;
                img.color = color32_one;
            }
            else
            {
                tf.localScale = Vector3.one;
                img.color = color32_64;
            }
        }
        else
        {
            if (PlayerController.p1hp <= maxhp && PlayerController.p1hp > minhp)
            {
                slider.value = PlayerController.p1hp;

                tf.localScale = Vector3.one * 1.1f;
                img.color = color32_one;
            }
            else
            {
                tf.localScale = Vector3.one;
                img.color = color32_64;
            }
        }
    }
}
