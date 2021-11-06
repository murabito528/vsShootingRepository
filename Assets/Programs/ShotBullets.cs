using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotBullets : MonoBehaviour
{
    public GameObject bullets;
    public int delay;
    private int flame;

    // Start is called before the first frame update
    void Start()
    {
        var vec = new Vector3(0, 200, 0);
        for (int i = 0; i < 40; i++)
        {
            var go = Pool.Get();
            //go.GetComponent<ReturnToPool>().returnToPool();
            go.transform.position = vec;
            //Debug.Log("初期");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delay <= flame)
        {
            //Instantiate(bullets, transform.position + new Vector3(0.1f, 0, 0), Quaternion.identity);
            //Instantiate(bullets, transform.position + new Vector3(-0.1f, 0, 0), Quaternion.identity);

            var go = Pool.Get();
            go.transform.position = this.transform.position + transform.right * 0.15f;
            var go2 = Pool.Get();
            go2.transform.position = this.transform.position + transform.right * -0.15f;
            flame = 0;
        }
        flame++;
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
        var go = Instantiate(bullets, transform.position, Quaternion.identity);
        go.name = $"Pooled type1Bullet: {_nextId++}";
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
