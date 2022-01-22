using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletCont_t1 : MonoBehaviour
{
    Transform tf;
    public float speed;
    [SerializeField] float speeddown;
    Color32 color32;
    SpriteRenderer spriterenderer;
    Vector3 rotate_tmp;
    Vector3 bounce_tmp;

    Vector3 tf_tmp;

    public Vector3 defaultscale;

    GameObject MainCamera;
    public ObjectPool<GameObject> EBulletPool;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        spriterenderer = GetComponent<SpriteRenderer>();
        MainCamera = GameObject.FindWithTag("MainCamera");
        EBulletPool = MainCamera.GetComponent<EnemyBulletpool_t1>().Pool;

        speeddown = speed;
    }

    void OnDisable()
    {
        speeddown = speed;
    }

    // Update is called once per frame
    void Update()
    {
        tf.position += tf.up * (speed-speeddown);
        speeddown *= 0.85f;

        transform.localScale = defaultscale + defaultscale * 30 * speeddown;
        //color32.a = (byte)(255 - 255 * speeddown);

        if (tf.position.z == 0)
        {
            color32 = spriterenderer.color;
            color32.r = 255;
            color32.g = 255;
            color32.b = 255;
            spriterenderer.material.color = color32;
        }
        else
        {
            color32 = spriterenderer.color;
            color32.r = 64;
            color32.g = 64;
            color32.b = 64;
            spriterenderer.material.color = color32;
        }

        if (tf.position.y > 5)
        {
            if (tf.position.z == 0)
            {
                rotate_tmp.x = 0;
                rotate_tmp.y = 0;
                rotate_tmp.z = 180 - tf.rotation.eulerAngles.z;
                tf.rotation = Quaternion.Euler(rotate_tmp);
                bounce_tmp.y = 5;
                bounce_tmp.x = tf.position.x;
                bounce_tmp.z = 1;
                tf.position = bounce_tmp;
            }
            else
            {
                rotate_tmp.x = 0;
                rotate_tmp.y = 0;
                rotate_tmp.z = 180 - tf.rotation.eulerAngles.z;
                tf.rotation = Quaternion.Euler(rotate_tmp);
                bounce_tmp.y = 5;
                bounce_tmp.x = tf.position.x;
                bounce_tmp.z = 0;
                tf.position = bounce_tmp;
            }
        }

        if (tf.position.y < -5 || Mathf.Abs(tf.position.x) > 3)
        {
            EBulletPool.Release(this.gameObject);
        }
        //Debug.Log((int)Mathf.Round((tf.position.x + 3) * 5) + "," + (int)Mathf.Round((tf.position.y + 5) * 5));

        if (tf.position.z != 0)
        {
            tf_tmp = tf.position;
            for (int i = 0; i < 10; i++)
            {
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5), 0, 49)].risk += 5;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5) + 1, 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5), 0, 49)].risk += 3 * (35 - i) / 35;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5) - 1, 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5), 0, 49)].risk += 3 * (35 - i) / 35;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5) + 1, 0, 49)].risk += 3 * (35 - i) / 35;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5) - 1, 0, 49)].risk += 3 * (35 - i) / 35;
                tf_tmp += tf.up * speed;
            }
        }
    }
}
