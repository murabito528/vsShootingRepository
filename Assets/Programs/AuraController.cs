using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour
{
    Transform tf;
    Vector3 rotation_speed;
    SpriteRenderer sr;
    int frame;
    void Start()
    {
        tf = transform;
        sr = GetComponent<SpriteRenderer>();
        float tmp = Random.Range(50, 35);
        if (Random.Range(1, 100) < 50)
        {
            tmp *= -1;
        }
        rotation_speed = new Vector3(0,0,tmp);
        tmp = Random.Range(0.1f, 1.0f);
        tf.localScale = new Vector3(tmp,tmp,1);
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frame < 10)
        {
            tf.Rotate(rotation_speed);
            tf.localScale += Vector3.one*0.5f;
        }
        else if(frame < 40)
        {
            tf.Rotate(rotation_speed);
            tf.localScale += Vector3.one*0.05f;
        }else if (frame < 50)
        {
            tf.Rotate(rotation_speed);
            tf.localScale += Vector3.one;
            Color32 color32 = sr.color;
            color32.a-=25;
            sr.color = color32;
        }
        else
        {
            Destroy(this.gameObject);
        }
        frame++;
    }
}
