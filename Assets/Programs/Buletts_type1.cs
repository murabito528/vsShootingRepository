using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Pool;

public class Buletts_type1 : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    Transform tf;
    Vector3 rotate_tmp;
    Vector3 bounce_tmp;
    SpriteRenderer spriterenderer;
    Color32 color32;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        tf = transform;
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.position += tf.up * speed;

        if(tf.position.z == 0)
        {
            color32 = spriterenderer.color;
            color32.g = 255;
            spriterenderer.material.color = color32;
        }
        else
        {
            color32 = spriterenderer.color;
            color32.g = 64;
            spriterenderer.material.color = color32;
        }

        if (tf.position.y > 5)
        {
            rotate_tmp.x = 0;
            rotate_tmp.y = 0;
            rotate_tmp.z = 180 - tf.rotation.eulerAngles.z;
            tf.rotation = Quaternion.Euler(rotate_tmp);
            bounce_tmp.y = 5;
            bounce_tmp.x = tf.position.x;
            bounce_tmp.z = 1;
            tf.position = bounce_tmp;
            //color32 = spriterenderer.color;
            //color32.g = 64;
            //spriterenderer.material.color = color32;
        }
        if(Mathf.Abs(tf.position.x) > 2.8)
        {
            if(tf.position.x > 2.8)
            {
                rotate_tmp.x = 0;
                rotate_tmp.y = 0;
                rotate_tmp.z = -1 * tf.rotation.eulerAngles.z;
                tf.rotation = Quaternion.Euler(rotate_tmp);
                bounce_tmp.y = tf.position.y;
                bounce_tmp.x = 2.8f;
                bounce_tmp.z = 1;
                tf.position = bounce_tmp;
                //color32 = spriterenderer.color;
                //color32.g = 64;
                //spriterenderer.material.color = color32;
            }
            else
            {
                rotate_tmp.x = 0;
                rotate_tmp.y = 0;
                rotate_tmp.z = -1 * tf.rotation.eulerAngles.z;
                tf.rotation = Quaternion.Euler(rotate_tmp);
                bounce_tmp.y = tf.position.y;
                bounce_tmp.x = -2.8f;
                bounce_tmp.z = 1;
                tf.position = bounce_tmp;
                //color32 = spriterenderer.color;
                //color32.g = 64;
                //spriterenderer.material.color = color32;
            }
        }
    }
}
