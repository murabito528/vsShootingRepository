using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletpool_t1 : MonoBehaviour
{
    public GameObject bullet;
    Transform tf;
    Quaternion default_rotate;

    Vector3 zero;

    Vector3 vec;
    Vector3 position;
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        vec = new Vector3(0, 0, 0);
        //rotate = new Vector3(0, 0, 0);
        zero = new Vector3(0, 0, 0);

        //epc = GameObject.FindWithTag("MainCamera").GetComponent<EffectPoolController>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //ここからpool

    // 初期のプールサイズ
    public int DefaultCapacity = 100;
    // プールサイズを最大どれだけ大きくするか
    public int MaxSize = 500;

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
        var go = Instantiate(bullet, zero, Quaternion.identity);
        go.name = $"Pooled Ebullet_t1: {_nextId++}";
        return go;
    }

    void OnTakeFromPool(GameObject ps)
    {
        ps.gameObject.SetActive(true);
    }

    void OnReturnedToPool(GameObject ps)
    {
        ps.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject ps)
    {
        Destroy(ps.gameObject);
    }

    void ClearPool()
    {
        // プールを破棄する
        if (_pool != null)
        {
            _pool.Clear();
            _pool = null;
        }
    }

    private ObjectPool<GameObject> _pool = null;

    private int _nextId = 1;
}
