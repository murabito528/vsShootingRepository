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



    //��������pool

    // �����̃v�[���T�C�Y
    public int DefaultCapacity = 10;
    // �v�[���T�C�Y���ő�ǂꂾ���傫�����邩
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

        // �v�[������p�[�e�B�N���V�X�e���̍쐬
        //var go = new GameObject($"Pooled Particle System: {_nextId++}");
        var go = Instantiate(enemy, zero, Quaternion.identity);
        go.name = $"Pooled weakenemy: {_nextId++}";
        //var ps = go.AddComponent<GameObject>();
        // �p�[�e�B�N���̏I���������G�~�b�^�[��~ & �G�~�b�V�����̃N���A�Ƃ���
        //ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // �p�[�e�B�N����1�b�̃����V���b�g�Đ��Ƃ���
        // (�̂Ŗ�1�b��Ƀp�[�e�B�N���͒�~����)
        //var main = ps.main;
        //main.duration = 1f;
        //main.startLifetime = 1f;
        //main.loop = false;

        // �p�[�e�B�N�����I��������v�[���ɕԋp���邽�߂�
        // ���������������R���|�[�l���g���A�^�b�`
        var returnToPool = go.AddComponent<ReturnToPool>();
        returnToPool.Pool = Pool;

        //Debug.Log($"Created {ps.gameObject.name}");

        return go;
    }

    void OnTakeFromPool(GameObject ps)
    {
        //Debug.Log($"Called OnTakeFromPool: ({ps.gameObject.name})");

        // �v�[������p�[�e�B�N���V�X�e�����؂��Ƃ���
        // ���̃I�u�W�F�N�g�̃A�N�e�B�u��ON�ɂ���
        ps.gameObject.SetActive(true);
    }

    void OnReturnedToPool(GameObject ps)
    {
        //Debug.Log($"Called OnReturnedToPool: ({ps.gameObject.name})");

        // �t�Ƀv�[���Ƀp�[�e�B�N���V�X�e����ԋp����Ƃ���
        // ���̃I�u�W�F�N�g�̃A�N�e�B�u��OFF�ɂ���
        ps.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject ps)
    {
        //Debug.Log($"Called OnDestroyPoolObject: ({ps.gameObject.name})");

        // �v�[�����ꂽ�p�[�e�B�N���̍폜���v������Ă���̂ŁA
        // �I�u�W�F�N�g��j������B
        //
        // OnCreatePoolObject�ŃI�u�W�F�N�g�𐶐����Ă���̂�
        // �����Ŕj������Ӗ�������Ƃ�������
        Destroy(ps.gameObject);
    }

    void ClearPool()
    {
        // �v�[����j������
        if (_pool != null)
        {
            _pool.Clear();
            _pool = null;
        }
    }

    private ObjectPool<GameObject> _pool = null;

    private int _nextId = 1;
}
