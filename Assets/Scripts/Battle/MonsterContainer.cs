using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterContainer : MonoBehaviour
{
    public static MonsterContainer instance; //몬스터컨테이너 인스턴스

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    //몬스터들이 저장되어있는 컨테이너
    [System.Serializable]
    public class MonsterArray
    {
        public List<GameObject> Monster;
    }
    public List<MonsterArray> MonsterPrefabs;
}
