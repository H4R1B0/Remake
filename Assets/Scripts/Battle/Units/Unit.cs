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

    protected string unitName = "";
    public string UnitName
    {
        get
        {
            return unitName;
        }
        set
        {
            unitName = value;
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
    public List<string> Tribe; //종족
    public List<string> Job; //직업

    protected int memelSynergyHP = 0; //메멀 시너지로 얻는 체력
    public int MemelSynergyHP
    {
        set
        {
            memelSynergyHP = value;
        }
    }

    protected int fossilSynergyPower = 0; //화석 시너지로 얻는 공격력
    public int FossilSynergyPower
    {
        set
        {
            fossilSynergyPower = value;
        }
    }

    protected int fossilSynergyHP = 0; //화석 시너지로 얻는 체력
    public int FossilSynergyHP
    {
        set
        {
            fossilSynergyHP = value;
        }
    }

    protected int beastSynergyHealHPPercent = 0; //비스트 시너지로 타겟이 죽었을때 얻는 체력 퍼센트
    public int BeastSynergyHealHPPercent
    {
        set
        {
            beastSynergyHealHPPercent = value;
        }
    }

    protected int raptorSynergyHealHP = 0; //랩터 시너지로 초당 회복하는 체력
    public int RaptorSynergyHealHP
    {
        set
        {
            raptorSynergyHealHP = value;
        }
    }
    protected int raptorSynergyHealMana = 0; //랩터 시너지로 초당 회복하는 마나
    public int RaptorSynergyHealMana
    {
        set
        {
            raptorSynergyHealMana = value;
        }
    }

    protected int insectSynergyReducedMana = 0; //인섹트 시너지로 스킬에 사용되는 마나 감소
    public int InsectSynergyReducedMana
    {
        set
        {
            insectSynergyReducedMana = value;
        }
    }

    protected int fishSynergyAvoid = 0; //피쉬 시너지로 회피율 증가
    public int FishSynergyAvoid
    {
        set
        {
            fishSynergyAvoid = value;
        }
    }

    protected int birdSynergyCritical = 0; //버드 시너지로 크리티컬 확률 증가
    public int BirdSynergyCritical
    {
        set
        {
            birdSynergyCritical = value;
        }
    }

    protected int shellSynergyReducedDamagePercent = 0; //쉘 시너지로 피해량 감소
    public int ShellSynergyReducedDamagePercent
    {
        set
        {
            shellSynergyReducedDamagePercent = value;
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
