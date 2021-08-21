using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : LivingEntity
{
    protected int mana; //����
    public GameObject HealEffect; //�� ����Ʈ
    public GameObject StatusUpEffect; //�������ͽ� ��� ����Ʈ

    public Slider MPSliderPrefab; //���� ������ ������
    protected Slider MPSlider; //���� ������

    protected int unitLevel = 1; //���� ����
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

    protected int starLevel = 0; //��Ÿ ����
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

    protected int unitPirce = 0; //���� ���� ������� �Ĵ� ���
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
    public List<string> Tribe; //����
    public List<string> Job; //����

    protected int memelSynergyHP = 0; //�޸� �ó����� ��� ü��
    public int MemelSynergyHP
    {
        set
        {
            memelSynergyHP = value;
        }
    }

    protected int fossilSynergyPower = 0; //ȭ�� �ó����� ��� ���ݷ�
    public int FossilSynergyPower
    {
        set
        {
            fossilSynergyPower = value;
        }
    }

    protected int fossilSynergyHP = 0; //ȭ�� �ó����� ��� ü��
    public int FossilSynergyHP
    {
        set
        {
            fossilSynergyHP = value;
        }
    }

    protected int beastSynergyHealHPPercent = 0; //��Ʈ �ó����� Ÿ���� �׾����� ��� ü�� �ۼ�Ʈ
    public int BeastSynergyHealHPPercent
    {
        set
        {
            beastSynergyHealHPPercent = value;
        }
    }

    protected Coroutine runningRaptorCoroutine = null; //�������� ���� �ڷ�ƾ
    protected int raptorSynergyHealHP = 0; //���� �ó����� �ʴ� ȸ���ϴ� ü��
    public int RaptorSynergyHealHP
    {
        set
        {
            raptorSynergyHealHP = value;
        }
    }
    protected int raptorSynergyHealMana = 0; //���� �ó����� �ʴ� ȸ���ϴ� ����
    public int RaptorSynergyHealMana
    {
        set
        {
            raptorSynergyHealMana = value;
        }
    }

    protected int insectSynergyReducedMana = 0; //�μ�Ʈ �ó����� ��ų�� ���Ǵ� ���� ����
    public int InsectSynergyReducedMana
    {
        set
        {
            insectSynergyReducedMana = value;
        }
    }

    protected int fishSynergyAvoid = 0; //�ǽ� �ó����� ȸ���� ����
    public int FishSynergyAvoid
    {
        set
        {
            fishSynergyAvoid = value;
        }
    }

    protected int birdSynergyCritical = 0; //���� �ó����� ũ��Ƽ�� Ȯ�� ����
    public int BirdSynergyCritical
    {
        set
        {
            birdSynergyCritical = value;
        }
    }

    protected int shellSynergyReducedDamagePercent = 0; //�� �ó����� ���ط� ����
    public int ShellSynergyReducedDamagePercent
    {
        set
        {
            shellSynergyReducedDamagePercent = value;
        }
    }

    //ü�� count��ŭ ȸ��
    public void HealHP(int count)
    {
        Instantiate(HealEffect, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        GameObject HealText = Instantiate(UIText, Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.8f, 0)), Quaternion.identity);
        HealText.GetComponent<UIText>().Number = count;
        HealText.GetComponent<TextMeshProUGUI>().color = Color.green;

        health = maxHealth > health + count ? health + count : maxHealth;
    }

    //���� count��ŭ ȸ��
    public void HealMP(int count)
    {
        mana = 100 > mana + count ? mana + count : 100;
    }

    public void SetHealthSynergy()
    {
        Debug.Log(this.unitName+"ü�� �ó��� ����");
        //ü�� �ó���
        health = originHealth + memelSynergyHP + fossilSynergyHP;
        maxHealth = health;
    }

    public void SetPowerSynergy()
    {
        Debug.Log("���ݷ� �ó��� ����");
        //���ݷ� �ó���
        power = originPower + fossilSynergyPower;
    }


    //���ʰ� count��ŭ ü�� ȸ���ϴ� �ڷ�ƾ
    public IEnumerator IncreasingHPCoroutine(int time, int count) //�ð�, ȸ���ϴ� ü�·�
    {
        for (int i = 0; i < time; i++)
        {
            HealHP(count);
            yield return new WaitForSeconds(1);
        }
    }

    //���� �ó��� ȿ��
    protected IEnumerator RaptorSynergyCoroutine()
    {
        while (true)
        {
            Debug.Log("���� �ó��� ����");
            //���� ���� �ó��� ��ŭ ü��, ���ݷ� ȸ��
            HealHP(raptorSynergyHealHP);
            HealMP(raptorSynergyHealMana);
            yield return new WaitForSeconds(1);
        }
    }
}
