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
    public int waittime;

    public int MaxHP;
    int HP;

    GameObject MainCamera;
    GameObject P1Player;

    Vector3 vec_tmp;

    // Start is called before the first frame update
    void Start()
    {
        P1Player = GameObject.FindWithTag("P1player");
        MainCamera = GameObject.FindWithTag("MainCamera");

        EBulletPool_t1 = MainCamera.GetComponent<EnemyBulletpool_t1>().Pool;
        BulletPool = P1Player.GetComponent<ShotBullets>().Pool;
        EnemyPool = MainCamera.GetComponent<Enemy_Pool>().Pool;

        epc = MainCamera.GetComponent<EffectPoolController>();

        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();

        HP = MaxHP;
        shotwait = 260;
        frame = 0;
    }

    /*
    void OnEnable()
    {
        HP = MaxHP;
    }
    */
    void OnDisable()
    {
        //epc.BurstEffect(transform.position,transform.rotation);
        HP = MaxHP;
        shotwait = 260;
        frame = 0;

        var go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.down, vec_tmp);

        go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.down, vec_tmp);
        go.transform.Rotate(0, 0, 5);

        go = EBulletPool_t1.Get();
        go.transform.position = transform.position;
        vec_tmp = P1Player.transform.position - transform.position;
        go.transform.rotation = Quaternion.FromToRotation(Vector3.down, vec_tmp);
        go.transform.Rotate(0, 0, -5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.up * speed);
        
        if(shotwait > waittime)
        {
            var go = EBulletPool_t1.Get();
            go.transform.position = transform.position;
            vec_tmp = P1Player.transform.position - transform.position;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec_tmp);

            go = EBulletPool_t1.Get();
            go.transform.position = transform.position;
            vec_tmp = P1Player.transform.position - transform.position;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec_tmp);
            go.transform.Rotate(0,0,5);

            go = EBulletPool_t1.Get();
            go.transform.position = transform.position;
            vec_tmp = P1Player.transform.position - transform.position;
            go.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec_tmp);
            go.transform.Rotate(0, 0, -5);
            shotwait = 0;
        }

        if(frame == 20)
        {
            if(transform.rotation.eulerAngles.z == 135)
            {
                transform.rotation = Quaternion.Euler(Vector3.forward * 90);
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.forward * -90);
            }
        }

        frame++;
        shotwait++;

        if(Mathf.Abs(this.transform.position.x) >= 3.5 || Mathf.Abs(this.transform.position.y) >= 6)
        {
            EnemyPool.Release(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.z == this.gameObject.transform.position.z&&collision.gameObject.tag == "P1bullet")
        {
            HP--;
            BulletPool.Release(collision.gameObject);
            if (HP <= 0)
            {
                //Debug.Log("HP:" + HP);
                epc.BurstEffect(transform.position, transform.rotation);
                EnemyPool.Release(this.gameObject);
            }
        }
    }
}
