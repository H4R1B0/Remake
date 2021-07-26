using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance; //플레이어 인스턴스
    public List<GameObject> UnitCards; //소환할 유닛카드들

    private int coin = 1000000; //코인
    private int crystal = 10000; //크리스탈

    public int Coin 
    {
        get
        {
            return coin; 
        }
        set 
        {
            coin = value;
        }
    }

    public int Crystal
    {
        get
        {
            return crystal;
        }
        set
        {
            crystal = value;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
