using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Controller : MonoBehaviour
{
    public int frame;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        frame = 0;
    }

    // Update is called once per frame
    Color32 color32;
    void Update()
    {
        color32 = sr.color;
        if (frame < 25)
        {
            color32.a += 8;
        }
        if(frame == 25)
        {
            color32.a = 255;
        }
        if (frame > 30)
        {
            transform.localScale = new Vector3(3 * ((40 - frame) * 0.1f), 100, 1);
            color32.a -= 8;
        }
        sr.color = color32;
        if(frame == 40)
        {
            Destroy(this.gameObject);
        }
        frame++;
    }
}
