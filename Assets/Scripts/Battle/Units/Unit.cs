using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : LivingEntity
{
    protected int mana; //마나
    public GameObject HealEffect; //힐 이펙트
    public GameObject StatusUpEffect; //스테이터스 향상 이펙트

    public Slider MPSliderPrefab; //마나 게이지 프리팹
    protected Slider MPSlider; //마나 게이지

    protected int level = 0; //유닛 레벨
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    

    protected string tribe; //종족
    public string Tribe
    {
        get
        {
            return tribe;
        }
    }

    protected string job; //직업
    public string Job
    {
        get
        {
            return job;
        }
    }

    protected int unitPirce = 0; //유닛 생성 비용이자 파는 비용
    public int UnitPrice
    {
        get
        {
            return unitPirce;
        }
        set
        {
            unitPirce = value;
        }
    }    

    //체력 count만큼 회복
    public void HealHP(int count)
    {
        Instantiate(HealEffect, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        GameObject HealText = Instantiate(UIText, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.8f, 0)), Quaternion.identity);
        HealText.GetComponent<UIText>().Number = count;
        HealText.GetComponent<TextMeshProUGUI>().color = Color.green;

        health = maxHealth > health + count ? health + count : maxHealth;
    }

    //마나 count만큼 회복
    public void HealMP(int count)
    {
        mana = 100 > mana + count ? mana + count : 100;
    }

    //몇초간 count만큼 체력 회복하는 코루틴
    public IEnumerator IncreasingHPCoroutine(int time, int count) //시간, 회복하는 체력량
    {
        for (int i = 0; i < time; i++)
        {
            HealHP(count);
            yield return new WaitForSeconds(1);
        }
    }
}
