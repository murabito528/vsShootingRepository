using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Pool;

public class Buletts_type1 : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector2(this.transform.position.x,this.transform.position.y+speed);
        transform.position += transform.up * speed;
        //if (this.transform.position.y > 5) Destroy(this.gameObject);
    }
}
