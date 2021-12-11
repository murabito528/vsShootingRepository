using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_always : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,1 * speed);
    }
}
