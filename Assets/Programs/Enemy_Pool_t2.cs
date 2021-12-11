using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Pool_t2 : MonoBehaviour
{
    public GameObject enemy;

    //ここからpool

    // 初期のプールサイズ
    public int DefaultCapacity = 10;
    // プールサイズを最大どれだけ大きくするか
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
        var go = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        go.name = $"Pooled weakenemy: {_nextId++}";
        return go;
    }

    void OnTakeFromPool(GameObject ps)
    {
        //Debug.Log($"Called OnTakeFromPool: ({ps.gameObject.name})");

        // プールからパーティクルシステムを借りるときに
        // そのオブジェクトのアクティブをONにする
        ps.gameObject.SetActive(true);
    }

    void OnReturnedToPool(GameObject ps)
    {
        //Debug.Log($"Called OnReturnedToPool: ({ps.gameObject.name})");

        // 逆にプールにパーティクルシステムを返却するときに
        // そのオブジェクトのアクティブをOFFにする
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
