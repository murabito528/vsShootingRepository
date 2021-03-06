using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Pool_t1 : MonoBehaviour
{
    public GameObject enemy;
    //public int delay;
    //public int frame;
    Transform tf;
    Quaternion default_rotate;

    int before_rnd;

    Vector3 zero;

    Vector3 vec;
    Vector3 position;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        vec = new Vector3(0, 0,0);
        //rotate = new Vector3(0, 0, 0);
        zero = new Vector3(0, 0, 0);

        //epc = GameObject.FindWithTag("MainCamera").GetComponent<EffectPoolController>();
    }

    /*
    void Update()
    {
        
        if (frame > delay)
        {
            int rnd = Random.Range(0, 4);
            while(rnd == before_rnd)
            {
                rnd = Random.Range(0, 4);
            }

            switch (rnd)
            {
                case 0:
                    vec.x = 2.5f;
                    vec.y = 2.5f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 135;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 1:
                    vec.x = -2.5f;
                    vec.y = 2.5f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = -135;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 2:
                    vec.x = -2f;
                    vec.y = -5f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 0;
                    rotation = Quaternion.Euler(vec);
                    break;
                case 3:
                    vec.x = 2f;
                    vec.y = -5f;
                    vec.z = 0;
                    position = vec;
                    vec.x = 0;
                    vec.y = 0;
                    vec.z = 0;
                    rotation = Quaternion.Euler(vec);
                    break;
            }
            StartCoroutine(summon(position,rotation));
            frame = 0;
        }
        frame++;
    }

    IEnumerator summon(Vector3 pos,Quaternion qua)
    {
        //Debug.Log("summon");
        for (int i = 0; i < 4; i++)
        {
            var go = Pool.Get();
            go.transform.position = pos;
            go.transform.rotation = qua;//Quaternion.Euler(rotate * 135);
            for (int j = 0; j < 15; j++) yield return null;
        }
    }
    */


    //????????pool

    // ???????v?[???T?C?Y
    public int DefaultCapacity = 10;
    // ?v?[???T?C?Y??????????????????????????
    public int MaxSize = 100;

    public ObjectPool<GameObject> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<GameObject>(OnCreatePoolObject, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, DefaultCapacity, MaxSize);
            }

            return _pool;
        }
    }

    GameObject OnCreatePoolObject()
    {
        //Debug.Log("Called CreatePooledItem");

        // ?v?[???????p?[?e?B?N???V?X?e????????
        //var go = new GameObject($"Pooled Particle System: {_nextId++}");
        var go = Instantiate(enemy, zero, Quaternion.identity);
        go.name = $"Pooled weakenemy: {_nextId++}";
        //var ps = go.AddComponent<GameObject>();
        // ?p?[?e?B?N?????I?????????G?~?b?^?[???~ & ?G?~?b?V???????N???A??????
        //ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // ?p?[?e?B?N????1?b???????V???b?g??????????
        // (??????1?b?????p?[?e?B?N???????~????)
        //var main = ps.main;
        //main.duration = 1f;
        //main.startLifetime = 1f;
        //main.loop = false;

        // ?p?[?e?B?N?????I?????????v?[???????p??????????
        // ???????????????R???|?[?l???g???A?^?b?`
        //var returnToPool = go.AddComponent<ReturnToPool>();
        //returnToPool.Pool = Pool;

        //Debug.Log($"Created {ps.gameObject.name}");

        return go;
    }

    void OnTakeFromPool(GameObject ps)
    {
        //Debug.Log($"Called OnTakeFromPool: ({ps.gameObject.name})");

        // ?v?[???????p?[?e?B?N???V?X?e????????????????
        // ?????I?u?W?F?N?g???A?N?e?B?u??ON??????
        ps.gameObject.SetActive(true);
    }

    void OnReturnedToPool(GameObject ps)
    {
        //Debug.Log($"Called OnReturnedToPool: ({ps.gameObject.name})");

        // ?t???v?[?????p?[?e?B?N???V?X?e???????p??????????
        // ?????I?u?W?F?N?g???A?N?e?B?u??OFF??????
        ps.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject ps)
    {
        //Debug.Log($"Called OnDestroyPoolObject: ({ps.gameObject.name})");

        // ?v?[?????????p?[?e?B?N???????????v?????????????????A
        // ?I?u?W?F?N?g???j???????B
        //
        // OnCreatePoolObject???I?u?W?F?N?g??????????????????
        // ???????j??????????????????????????
        Destroy(ps.gameObject);
    }

    void ClearPool()
    {
        // ?v?[?????j??????
        if (_pool != null)
        {
            _pool.Clear();
            _pool = null;
        }
    }

    private ObjectPool<GameObject> _pool = null;

    private int _nextId = 1;
}
