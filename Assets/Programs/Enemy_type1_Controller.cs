using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_type1_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> Pool;
    Rigidbody2D rb;
    Transform tf;
    public float speed;
    int frame;
    int waittime;

    int MaxHP;
    int HP;

    // Start is called before the first frame update
    void Start()
    {
        Pool = GameObject.FindWithTag("P1player").GetComponent<ShotBullets>().Pool;
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
    }

    void OnEnable()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.up * speed);
        
        if(frame > waittime)
        {
            frame = 0;
        }
        frame++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.position.z == this.gameObject.transform.position.z&&collision.gameObject.tag == "P1bullet")
        {
            HP--;
            Pool.Release(collision.gameObject);
            if (HP <= 0)
            {

            }
        }
    }
}
