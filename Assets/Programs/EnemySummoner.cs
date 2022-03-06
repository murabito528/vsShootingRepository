using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySummoner : MonoBehaviour
{
    GameObject MainCamera;

    public ObjectPool<GameObject> Enemy_t1Pool;
    public ObjectPool<GameObject> Enemy_t2Pool;

    public float delay,delay2;
    public float frame,frame2;

    int before_rnd;

    Vector3 vec;
    Vector3 position;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
        Enemy_t1Pool = MainCamera.GetComponent<Enemy_Pool_t1>().Pool;
        Enemy_t2Pool = MainCamera.GetComponent<Enemy_Pool_t2>().Pool;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.game_end > 0)
        {
            return;
        }

        if (frame > delay)
        {
            int rnd = Random.Range(0, 4);
            while (rnd == before_rnd)
            {
                rnd = Random.Range(0, 4);
            }

            switch (rnd)
            {
                case 0:
                    vec.x = 2.8f;
                    vec.y = 2.8f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 135;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 1:
                    vec.x = -2.8f;
                    vec.y = 2.8f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = -135;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 2:
                    vec.x = -2f;
                    vec.y = -5.5f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 0;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 3:
                    vec.x = 2f;
                    vec.y = -5.5f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 0;
                    rotation = Quaternion.Euler(vec);
                    break;
            }
            StartCoroutine(summon(position, rotation));
            frame = 0;
            delay = 45 + 50 * (1 - GameManager.p1lv * 0.1f);
            if (GameManager.p1chain >= 99)
            {
                delay -= 20;
            }
        }
        frame++;

        if (frame2 > delay2)
        {
            int rnd = Random.Range(0, 4);
            while (rnd == before_rnd)
            {
                rnd = Random.Range(0, 4);
            }

            switch (rnd)
            {
                case 0:
                    vec.x = 2.8f;
                    vec.y = 2.8f;
                    vec.z = 1;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 135;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 1:
                    vec.x = -2.8f;
                    vec.y = 2.8f;
                    vec.z = 1;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = -135;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 2:
                    vec.x = -2f;
                    vec.y = -5.5f;
                    vec.z = 1;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 0;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 3:
                    vec.x = 2f;
                    vec.y = -5.5f;
                    vec.z = 1;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 0;
                    rotation = Quaternion.Euler(vec);
                    break;
            }
            StartCoroutine(summon(position, rotation));
            frame2 = 0;
            delay2 = 45 + 50 * (1 - GameManager.p2lv * 0.1f);
            if (GameManager.p2chain >= 99)
            {
                delay2 -= 20;
            }
        }
        frame2++;
    }

    IEnumerator summon(Vector3 pos, Quaternion qua)
    {
        GameObject go;
        for (int i = 0; i < 3; i++)
        {
            go = Enemy_t1Pool.Get();
            go.transform.position = pos;
            go.transform.rotation = qua;//Quaternion.Euler(rotate * 135);
            for (int j = 0; j < 15; j++) yield return null;
        }
        go = Enemy_t2Pool.Get();
        go.transform.position = pos;
        go.transform.rotation = qua;
    }
}
