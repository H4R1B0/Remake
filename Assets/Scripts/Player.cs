using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player instance; //플레이어 인스턴스
    public List<GameObject> UnitCards; //소환할 유닛카드들

    private int coin = 1000000; //코인
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

    private int crystal = 10000; //크리스탈
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

    private int callUnitCount = 0; //현재 소환한 유닛
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

    private int callUnitCountMax = 2; //유닛 소환 최대수
    public int CallUnitCountMax
    {
        get
        {
            return callUnitCountMax;
        }
        set
        {
            callUnitCountMax = value;
        }
    }

    private int callUnitCountAddPrice = 40; //유닛 소환 최대수 증가 비용
    public int CallUnitCountAddPrice
    {
        get
        {
            return callUnitCountAddPrice;
        }
        set
        {
            callUnitCountAddPrice = value;
        }
    }

    private int starPoint=0;
    public int StarPoint
    {
        get
        {
            return starPoint;
        }
        set
        {
            starPoint = value;
        }
    }

    private int starPointLevel = 1; //스타포인트 지급 레벨 (최대 5)
    public int StarPointLevel
    {
        get
        {
            return starPointLevel;
        }
        set
        {
            starPointLevel = value;
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
        //게임 시작 전에만 유닛 최대 수 증가 가능
        if (GameManager.instance.IsStart == false)
        {
            callUnitCountMax++; //소환 최대수 1증가
            crystal -= callUnitCountAddPrice; //소환 최대수 증가시 크리스탈 감소
            //DOTween.To(() => crystal, x => crystal = x, crystal -= callUnitCountAddPrice, 1);

            //유닛 소환 최대수 비용 증가
            if (callUnitCountAddPrice == 40)
            {
                callUnitCountAddPrice = 80; //40*2
            }
            else if (callUnitCountAddPrice == 80)
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
            GameObject.Find("UnitAddButton").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = callUnitCountAddPrice.ToString();
        }

        //GameObject.Find("CallUnitCountText").GetComponent<CallUnitCountText>().RenewText(); //유닛 소환 텍스트 갱신
    }
}
