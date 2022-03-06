using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotBullets : MonoBehaviour
{
    public ObjectPool<GameObject> BulletPool;
    //public GameObject bullets;
    public int delay;
    [SerializeField] private int frame;
    Transform tf;
    Quaternion default_rotate;
    GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");
        BulletPool = MainCamera.GetComponent<Bullet_weakt1_Pool>().Pool;
        default_rotate = Quaternion.Euler(new Vector3(0, 0, 1));
        tf = this.transform;
        
    }

    void Update()
    {
        if(GameManager.game_end > 0)
        {
            return;
        }
        if (delay <= frame &&( (tf.position.z == 1 || Input.touchCount == 1) || Input.GetKey(KeyCode.Z)))
        {
            var go = BulletPool.Get();
            go.transform.position = this.tf.position + tf.right * 0.15f;
            go.transform.rotation = Quaternion.Euler(default_rotate.eulerAngles * -0);

            if (tf.position.z == 0)
            {
                go.tag = "P1bullet";
                //Debug.Log("shotp1");
            }
            else
            {
                go.tag = "P2bullet";
                //Debug.Log("shotp2");
            }

            go = BulletPool.Get();
            go.transform.position = this.tf.position + tf.right * -0.15f;
            go.transform.rotation = Quaternion.Euler(default_rotate.eulerAngles * 0);

            if(tf.position.z == 0)
            {
                go.tag = "P1bullet";
                //Debug.Log("shotp1");
            }
            else
            {
                go.tag = "P2bullet";
                //Debug.Log("shotp2");
            }

            frame = 0;
        }
        frame++;
    }
    /*
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
        var go = Instantiate(bullets, tf.position, Quaternion.identity);
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

    IEnumerator make()
    {
        yield return null;
        var vec = new Vector3(0, -200, 0);
        for (int i = 0; i < 40; i++)
        {
            var go = Pool.Get();
            go.transform.position = vec;
            go.transform.rotation = Quaternion.Euler(default_rotate.eulerAngles * 0);
            //Debug.Log("初期");
        }
    }
    */
}
