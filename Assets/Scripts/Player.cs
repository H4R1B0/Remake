using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance; //플레이어 인스턴스
    public List<GameObject> UnitCards; //소환할 유닛카드들

    private int coin = 1000000; //코인
    private int crystal = 10000; //크리스탈

    private int callUnitCount = 0; //현재 소환한 유닛
    private int callUnitCountMax = 2; //유닛 소환 최대수
    private int callUnitCountAddPrice = 40; //유닛 소환 최대수 증가 비용

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

    public int CallUnitCount
    {
        get
        {
            return callUnitCount;
        }
        set
        {
            callUnitCount = value;
        }
    }

    public int CallUnitCountMax
    {
        get
        {
            return callUnitCountMax;
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

    //유닛 소환 최대수 증가
    public void CallUnitCountAdd()
    {
        callUnitCountMax++; //소환 최대수 1증가
        crystal -= callUnitCountAddPrice; //소환 최대수 증가시 크리스탈 감소

        //유닛 소환 최대수 비용 증가
        if(callUnitCountAddPrice == 40)
        {
            callUnitCountAddPrice = 80; //40*2
        }
        else if(callUnitCountAddPrice == 80)
        {
            callUnitCountAddPrice = 200; //80*2.5
        }
        else if (callUnitCountAddPrice == 200)
        {
            callUnitCountAddPrice = 500;
        }
        else if (callUnitCountAddPrice == 500)
        {
            callUnitCountAddPrice = 1500;
        }
        else
        {
            GameObject.Find("UnitAddButton").GetComponent<Button>().interactable = false; //소환 최대 증가 버튼 비활성화
            
        }

        //GameObject.Find("CallUnitCountText").GetComponent<CallUnitCountText>().RenewText(); //유닛 소환 텍스트 갱신
    }
}
