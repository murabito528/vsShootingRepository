using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Pool;

public class Buletts_type1 : MonoBehaviour
{
    public float speed;
    Transform tf;
    Vector3 rotate_tmp;
    Vector2 bounce_tmp;

    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        rotate_tmp = new Vector3(0, 0, 180);
        bounce_tmp = new Vector2(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        tf.position += tf.up * speed;

        if(tf.position.y > 5)
        {
            rotate_tmp.x = 0;
            rotate_tmp.y = 0;
            rotate_tmp.z = 180 - tf.rotation.eulerAngles.z;
            tf.rotation = Quaternion.Euler(rotate_tmp);
            bounce_tmp.y = 5;
            bounce_tmp.x = tf.position.x;
            tf.position = bounce_tmp;
        }
    }
}
