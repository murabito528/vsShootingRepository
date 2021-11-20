using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Pool : MonoBehaviour
{
    public GameObject enemy;
    public int delay;
    public int frame;
    Transform tf;
    Quaternion default_rotate;

    Vector3 zero;

    Vector3 vec;
    Vector3 rotate;

    // Start is called before the first frame update
    void Start()
    {
        vec = new Vector3(2.5f, 2.5f,0);
        rotate = new Vector3(0, 0, 1);
        zero = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (frame > delay)
        {
            StartCoroutine(summon());
            frame = 0;
        }
        frame++;
    }

    IEnumerator summon()
    {
        //Debug.Log("summon");
        for (int i = 0; i < 4; i++)
        {
            var go = Pool.Get();
            go.transform.position = vec;
            go.transform.rotation = Quaternion.Euler(rotate * 135);
            for (int j = 0; j < 15; j++) yield return null;
        }
    }



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
        //Debug.Log("Called CreatePooledItem");

        // プールするパーティクルシステムの作成
        //var go = new GameObject($"Pooled Particle System: {_nextId++}");
        var go = Instantiate(enemy, zero, Quaternion.identity);
        go.name = $"Pooled weakenemy: {_nextId++}";
        //var ps = go.AddComponent<GameObject>();
        // パーティクルの終了挙動をエミッター停止 & エミッションのクリアとする
        //ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // パーティクルを1秒のワンショット再生とする
        // (ので約1秒後にパーティクルは停止する)
        //var main = ps.main;
        //main.duration = 1f;
        //main.startLifetime = 1f;
        //main.loop = false;

        // パーティクルが終了したらプールに返却するための
        // 挙動を実装したコンポーネントをアタッチ
        var returnToPool = go.AddComponent<ReturnToPool>();
        returnToPool.Pool = Pool;

        //Debug.Log($"Created {ps.gameObject.name}");

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
        //Debug.Log($"Called OnDestroyPoolObject: ({ps.gameObject.name})");

        // プールされたパーティクルの削除が要求されているので、
        // オブジェクトを破棄する。
        //
        // OnCreatePoolObjectでオブジェクトを生成しているので
        // ここで破棄する責務があるという解釈
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
