using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_type2_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> EBulletPool_t1;
    public ObjectPool<GameObject> BulletPool;
    public ObjectPool<GameObject> Enemy_t2Pool;//this
    EffectPoolController epc;

    Rigidbody2D rb;
    Transform tf;
    public float speed;
    [SerializeField] int shotwait;
    [SerializeField] int frame;
    bool turn;
    public int waittime;

    public int MaxHP;
    int HP;

    GameObject MainCamera;
    GameObject P1Player;

    Vector3 vec_tmp;
    SpriteRenderer spriterenderer;
    Color32 color32;

    // Start is called before the first frame update
    void Start()
    {
        P1Player = GameObject.FindWithTag("P1player");
        MainCamera = GameObject.FindWithTag("MainCamera");

        EBulletPool_t1 = MainCamera.GetComponent<EnemyBulletpool_t1>().Pool;
        BulletPool = MainCamera.GetComponent<Bullet_weakt1_Pool>().Pool;
        Enemy_t2Pool = MainCamera.GetComponent<Enemy_Pool_t2>().Pool;

        epc = MainCamera.GetComponent<EffectPoolController>();
        spriterenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        HP = MaxHP;
        shotwait = 260;
        frame = 0;
        turn = false;
    }


    void OnDisable()
    {
        //epc.BurstEffect(transform.position,transform.rotation);
        HP = MaxHP;
        shotwait = 260;
        frame = 0;
        turn = false;

        var go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, -vec_tmp);

        go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, -vec_tmp);
        go.transform.Rotate(0, 0, 5);

        go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, -vec_tmp);
        go.transform.Rotate(0, 0, -5);
        shotwait = 0;
    }

    void OnEnable()
    {
        turn = false;
        StartCoroutine("Enablenext");
    }

    IEnumerator Enablenext()
    {
        yield return null;
        if (transform.rotation.eulerAngles.z == 0)
        {
            shotwait = 245;
        }
        else
        {
            shotwait = 260;
        }
    }

    IEnumerator Shot()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 4; j++)
            {
            var go = EBulletPool_t1.Get();
            go.transform.position = transform.position;
            go.transform.rotation = Quaternion.Euler(Vector3.zero);
            go.transform.Rotate(0,0,i * 15 + j*90);
            }
            for (int j = 0; j < 2; j++) yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tf.position.z == 0)
        {
            color32 = spriterenderer.color;
            color32.r = 255;
            color32.g = 255;
            color32.b = 255;
            spriterenderer.color = color32;
        }
        else
        {
            color32 = spriterenderer.color;
            color32.r = 72;
            color32.g = 64;
            color32.b = 64;
            spriterenderer.color = color32;
        }

        rb.MovePosition(transform.position + transform.up * speed);

        if (shotwait > waittime)
        {
            StartCoroutine(Shot());
            shotwait = 0;
        }

        //if(frame == 20)
        if (turn == false && Mathf.Abs(tf.transform.position.y - 1) <= 0.4)
        {
            //Debug.Log(tf.rotation.eulerAngles.z);
            turn = true;
            switch (transform.rotation.eulerAngles.z)
            {
                case 135:
                    tf.rotation = Quaternion.Euler(Vector3.forward * 90);
                    break;
                case 225:
                    tf.rotation = Quaternion.Euler(Vector3.forward * -90);
                    break;
                case 0:
                    if (tf.position.x < 0)
                    {
                        tf.rotation = Quaternion.Euler(Vector3.forward * -45);
                    }
                    else
                    {
                        tf.rotation = Quaternion.Euler(Vector3.forward * 45);
                    }
                    break;
            }
        }

        frame++;
        shotwait++;

        if (Mathf.Abs(this.transform.position.x) >= 3.5 || Mathf.Abs(this.transform.position.y) >= 6)
        {
            Enemy_t2Pool.Release(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tf.position.z == 0 && collision.gameObject.transform.position.z == 0 && collision.CompareTag("P1bullet"))
        {
            HP--;
            BulletPool.Release(collision.gameObject);
            if (HP <= 0)
            {
                //Debug.Log("HP:" + HP);
                epc.BurstEffect(transform.position, transform.rotation);
                Enemy_t2Pool.Release(this.gameObject);
            }
        }
        if(tf.position.z == 1 && collision.gameObject.transform.position.z == 1 && collision.CompareTag("P2bullet"))
        {
            HP--;
            BulletPool.Release(collision.gameObject);
            if (HP <= 0)
            {
                epc.BurstEffect(transform.position, transform.rotation);
                Enemy_t2Pool.Release(this.gameObject);
            }
        }
    }
}
