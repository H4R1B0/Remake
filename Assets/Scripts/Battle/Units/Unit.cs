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

    protected int unitLevel = 1; //유닛 레벨
    public int UnitLevel
    {
        get
        {
            return unitLevel;
        }
        set
        {
            unitLevel = value;
        }
    }

    protected int starLevel = 0; //스타 레벨
    public int StarLevel
    {
        get
        {
            return starLevel;
        }
        set
        {
            starLevel = value;
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

    protected Coroutine runningRaptorCoroutine = null; //실행중인 랩터 코루틴
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

    protected bool warriorSynergyExtraAttackBool = false; //전사 시너지 여부
    public bool WarriorSynergyExtraAttackBool
    {
        set
        {
            warriorSynergyExtraAttackBool = value;
        }
    }
    protected int warriorSynergyExtraAttackCountMax = 0; //전사 시너지로 6/4번째마다 추가 공격 
    public int WarriorSynergyExtraAttackCountMax
    {
        set
        {
            warriorSynergyExtraAttackCountMax = value;
        }
    }
    protected int warriorSynergyExtraAttackCount = 0; //전사 시너지로 현재 공격 횟수

    protected int wizardSynergyHPPercent = 0; //마법사 시너지 얻는 체력 퍼센트
    public int WizardSynergyHPPercent
    {
        set
        {
            wizardSynergyHPPercent = value;
        }
    }
    protected int wizardSynergyPowerPercent = 0; //마법사 시너지 얻는 공격력 퍼센트
    public int WizardSynergyPowerPercent
    {
        set
        {
            wizardSynergyPowerPercent = value;
        }
    }

    protected bool assassinSynergyBool = false; //암살자 시너지 여부
    public bool AssassinSynergyBool
    {
        set
        {
            assassinSynergyBool = value;
        }
    }    
    protected int assassinSynergyExecutionCondition = 0; //암살자 시너지로 처형할 타겟의 체력 조건
    public int AssassinSynergyExecutionCondition
    {
        set
        {
            assassinSynergyExecutionCondition = value;
        }
    }

    protected int guardiansSynergyReducedDamage = 0; //수호자 시너지로 피해량 감소
    public int GuardiansSynergyReducedDamage
    {
        set
        {
            guardiansSynergyReducedDamage = value;
        }
    }    

    //체력 count만큼 회복
    public void HealHP(int count)
    {
        Instantiate(HealEffect, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        GameObject HealText = Instantiate(UIText, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.8f, 0)), Quaternion.identity);
        HealText.GetComponent<UIText>().Content = count.ToString();
        HealText.GetComponent<TextMeshProUGUI>().color = Color.green;

        health = maxHealth > health + count ? health + count : maxHealth;
    }

    //마나 count만큼 회복
    public void HealMP(int count)
    {
        mana = 100 > mana + count ? mana + count : 100;
    }

    public void SetHealthSynergy()
    {
        Debug.Log(this.unitName + "체력 시너지 적용");
        //체력 시너지
        health = originHealth * (100 + wizardSynergyHPPercent) / 100 + memelSynergyHP + fossilSynergyHP;
        maxHealth = health;
    }

    public void SetPowerSynergy()
    {
        Debug.Log("공격력 시너지 적용");
        //공격력 시너지
        power = originPower * (100 + wizardSynergyPowerPercent) / 100 + fossilSynergyPower;
    }

    public void SetCriticalRateSynergy()
    {
        Debug.Log("치명타율 시너지 적용");
        //치명타율 시너지
        criticalRate = originCriticalRate + birdSynergyCritical;
    }

    public void SetAvoidRateSynergy()
    {
        Debug.Log("회피율 시너지 적용");
        //회피율 시너지
        avoidRate = fishSynergyAvoid;
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

    //랩터 시너지 효과
    protected IEnumerator RaptorSynergyCoroutine()
    {
        while (true)
        {
            Debug.Log("랩터 시너지 실행");
            //매초 랩터 시너지 만큼 체력, 공격력 회복
            HealHP(raptorSynergyHealHP);
            HealMP(raptorSynergyHealMana);
            yield return new WaitForSeconds(1);
        }
    }
}
