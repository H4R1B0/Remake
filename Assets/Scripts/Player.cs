using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player instance; //�÷��̾� �ν��Ͻ�
    public List<GameObject> UnitCards; //��ȯ�� ����ī���

    private int coin = 1000000; //����
    private int crystal = 10000; //ũ����Ż

    private int callUnitCount = 0; //���� ��ȯ�� ����
    private int callUnitCountMax = 2; //���� ��ȯ �ִ��
    private int callUnitCountAddPrice = 40; //���� ��ȯ �ִ�� ���� ���

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

    //���� ��ȯ �ִ�� ����
    public void CallUnitCountAdd()
    {
        callUnitCountMax++; //��ȯ �ִ�� 1����
        //crystal -= callUnitCountAddPrice; //��ȯ �ִ�� ������ ũ����Ż ����
        DOTween.To(() => crystal, x => crystal = x, crystal -= callUnitCountAddPrice, 1);

        //���� ��ȯ �ִ�� ��� ����
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
            GameObject.Find("UnitAddButton").GetComponent<Button>().interactable = false; //��ȯ �ִ� ���� ��ư ��Ȱ��ȭ
        }

        //GameObject.Find("CallUnitCountText").GetComponent<CallUnitCountText>().RenewText(); //���� ��ȯ �ؽ�Ʈ ����
    }
}
