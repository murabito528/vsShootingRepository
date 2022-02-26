using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_type1_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> EBulletPool_t1;
    public ObjectPool<GameObject> BulletPool;
    public ObjectPool<GameObject> EnemyPool;//this
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
    GameObject P2Player;

    Vector3 tf_tmp;

    Vector3 vec_tmp;

    SpriteRenderer spriterenderer;
    Color32 color32;

    // Start is called before the first frame update
    void Start()
    {
        P1Player = GameObject.FindWithTag("P1player");
        P2Player = GameObject.FindWithTag("P2player");
        MainCamera = GameObject.FindWithTag("MainCamera");

        EBulletPool_t1 = MainCamera.GetComponent<EnemyBulletpool_t1>().Pool;
        BulletPool = MainCamera.GetComponent<Bullet_weakt1_Pool>().Pool;
        EnemyPool = MainCamera.GetComponent<Enemy_Pool_t1>().Pool;

        epc = MainCamera.GetComponent<EffectPoolController>();

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        spriterenderer = GetComponent<SpriteRenderer>();

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
        //Œ‚‚¿•Ô‚µ(”½‘ÎŒü‚«‚Ì)
        var go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        vec_tmp.z = 0;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, -vec_tmp);

        go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        vec_tmp.z = 0;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.up, -vec_tmp);
        go.transform.Rotate(0, 0, 5);

        go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        vec_tmp.z = 0;
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
            color32.r = 64;
            color32.g = 72;
            color32.b = 64;
            spriterenderer.color = color32;
        }

        rb.MovePosition(transform.position + transform.up * speed);
        
        if(shotwait > waittime)
        {
            if (tf.position.z == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    var go = EBulletPool_t1.Get();
                    go.transform.position = transform.position;
                    go.transform.Translate(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                    vec_tmp = P1Player.transform.position - transform.position;
                    vec_tmp.z = 0;
                    go.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec_tmp);
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    var go = EBulletPool_t1.Get();
                    go.transform.position = transform.position;
                    go.transform.Translate(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 1);
                    vec_tmp = P2Player.transform.position - transform.position;
                    vec_tmp.z = 0;
                    go.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec_tmp);
                }
            }
            shotwait = 0;
        }

        //if(frame == 20)
        if(turn == false && Mathf.Abs(tf.transform.position.y - 1) <= 0.4)
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

        if(Mathf.Abs(this.transform.position.x) >= 3.5 || Mathf.Abs(this.transform.position.y) >= 6)
        {
            if(tf.position.z == 0)
            {
                GameManager.p1chain = 0;
            }
            else
            {
                GameManager.p2chain = 0;
            }
            EnemyPool.Release(this.gameObject);
        }

        if (tf.position.z != 0)
        {
            tf_tmp = tf.position;
            for (int i = 0; i < 5; i++)
            {
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5), 0, 49)].risk += 5;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5) + 1, 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5), 0, 49)].risk += 3 * (35 - i) / 35;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5) - 1, 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5), 0, 49)].risk += 3 * (35 - i) / 35;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5) + 1, 0, 49)].risk += 3 * (35 - i) / 35;
                P2Controller.node[(int)Mathf.Clamp(Mathf.Round((tf_tmp.x + 3) * 5), 0, 29)][(int)Mathf.Clamp((int)Mathf.Round((tf_tmp.y + 5) * 5) - 1, 0, 49)].risk += 3 * (35 - i) / 35;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tf.position.z == 0 && collision.gameObject.transform.position.z == 0 && collision.CompareTag("P1bullet"))//P1‘¤
        {
            HP--;
            if (HP <= 0)
            {
                PlayerController.p1score += 1000;
                GameManager.p1exp++;
                epc.BurstEffect(transform.position, transform.rotation);
                EnemyPool.Release(this.gameObject);
                GameManager.p1chain++;
            }
            BulletPool.Release(collision.gameObject);
        }
        if (tf.position.z == 1 && collision.gameObject.transform.position.z == 1 && collision.CompareTag("P2bullet"))//P2‘¤
        {
            HP--;
            if (HP <= 0)
            {
                P2Controller.p2score += 1000;
                GameManager.p2exp++;
                epc.BurstEffect(transform.position, transform.rotation);
                GameManager.p2chain++;
                EnemyPool.Release(this.gameObject);
            }
            BulletPool.Release(collision.gameObject);
        }
    }
}
