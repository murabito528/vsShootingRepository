using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Touch myTouch;
    Vector2 ClickPos;
    Vector2 Goalpos;
    Vector2 Cursorpos;
    Vector2 Lastpos;

    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
        Goalpos = tf.position;
    }

    // Update is called once per frame
    void Update()
    {
        int touchcount = Input.touchCount;

        if (touchcount > 0)
        {
            for (int i = 0; i < touchcount; i++)
            {
                myTouch = Input.GetTouch(i);
                switch (i)
                {
                    case 0:
                        ClickPos = Camera.main.ScreenToWorldPoint(myTouch.position);
                        if (myTouch.phase == TouchPhase.Began)
                        {
                            Lastpos = ClickPos;
                        }
                        Goalpos = Goalpos + (ClickPos - Lastpos);
                        Lastpos = ClickPos;

                        tf.position = Goalpos;
                        break;
                    case 1:

                        break;
                }

            }
        }
    }
}
