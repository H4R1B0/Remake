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

    protected int level = 0; //���� ����
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

    

    protected string tribe; //����
    public string Tribe
    {
        get
        {
            return tribe;
        }
    }

    protected string job; //����
    public string Job
    {
        get
        {
            return job;
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

    //���ʰ� count��ŭ ü�� ȸ���ϴ� �ڷ�ƾ
    public IEnumerator IncreasingHPCoroutine(int time, int count) //�ð�, ȸ���ϴ� ü�·�
    {
        for (int i = 0; i < time; i++)
        {
            HealHP(count);
            yield return new WaitForSeconds(1);
        }
    }
}
