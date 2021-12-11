using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Pool_t2 : MonoBehaviour
{
    public GameObject enemy;

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
        var go = Instantiate(enemy, Vector3.zero, Quaternion.identity);
        go.name = $"Pooled weakenemy: {_nextId++}";
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
