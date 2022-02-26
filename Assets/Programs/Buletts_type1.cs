using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
        if (tf.position.y > 5)
        {
            rotate_tmp.x = 0;
            rotate_tmp.y = 0;
            rotate_tmp.z = 180 - tf.rotation.eulerAngles.z;
            tf.rotation = Quaternion.Euler(rotate_tmp);
            bounce_tmp.y = 5;
            bounce_tmp.x = tf.position.x;
            if (tf.position.z == 0)
            {
                bounce_tmp.z = 1;
            }
            else
            {
                bounce_tmp.z = 0;
            }
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

    void LateUpdate()
    {
        color32.a = 255;
        if (this.tag == "P1bullet")
        {
            if (tf.position.z == 0)
            {
                color32.r = 0;
                color32.g = 255;
                color32.b = 0;
            }
            else
            {
                color32.r = 0;
                color32.g = 64;
                color32.b = 0;
            }
        }
        else
        {
            if (tf.position.z == 0)
            {
                color32.r = 255;
                color32.g = 0;
                color32.b = 0;
            }
            else
            {
                color32.r = 128;
                color32.g = 0;
                color32.b = 0;
            }
        }
        spriterenderer.color = color32;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tf.position.z == 0 && collision.gameObject.transform.position.z == 0 && collision.CompareTag("P1player") && this.CompareTag("P2bullet"))//P1‘¤
        {
            PlayerController.p1hp--;
            P2Controller.p2score += 10;
            tf.position = Vector3.one * -100;
        }
        if (tf.position.z == 1 && collision.gameObject.transform.position.z == 1 && collision.CompareTag("P2player") && this.CompareTag("P1bullet"))//P2‘¤
        {
            P2Controller.p2hp--;
            PlayerController.p1score += 10;
            tf.position = Vector3.one * -100;
        }
    }
}
