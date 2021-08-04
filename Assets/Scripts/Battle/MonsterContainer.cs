using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterContainer : MonoBehaviour
{
    public static MonsterContainer instance; //���������̳� �ν��Ͻ�

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    //���͵��� ����Ǿ��ִ� �����̳�
    [System.Serializable]
    public class MonsterArray
    {
        public List<GameObject> Monster;
    }
    public List<MonsterArray> MonsterPrefabs;
}
