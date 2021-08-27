using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopPanel : MonoBehaviour
{
    public List<GameObject> PurchaseUnitCards; //������ ����ī�� ����Ʈ
    public List<GameObject> UnitPurchaseButtons; //���� ���� ��ư ����Ʈ

    private Player player; //�÷��̾�

    void Start()
    {
        this.gameObject.SetActive(false);
        player = Player.instance;

        //������ �ִ� ����ī�尡 �÷��̾ ���� �ִٸ� ��Ȱ��ȭ
        for (int i=0;i< PurchaseUnitCards.Count;i++)
        {
            if (player.UnitCards.Contains(PurchaseUnitCards[i]))
            {
                UnitPurchaseButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "���� �Ϸ�";
                UnitPurchaseButtons[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                UnitPurchaseButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    //����ī�� ����
    public void PurchaseUnitCard()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(clickObject);
        //Debug.Log(UnitPurchaseButtons.IndexOf(clickObject));
        int index = UnitPurchaseButtons.IndexOf(clickObject);
        if(index>=0 && index <= 5)
        {
            player.Coin -= 1000;
            player.UnitCards.Add(PurchaseUnitCards[index]);

            clickObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "���� �Ϸ�";
            clickObject.GetComponent<Button>().interactable = false;
        }
        else if (index >= 6 && index <= 10)
        {
            player.Coin -= 2000;
            player.UnitCards.Add(PurchaseUnitCards[index]);

            clickObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "���� �Ϸ�";
            clickObject.GetComponent<Button>().interactable = false;
        }
        else if (index >= 11 && index <= 13)
        {
            player.Coin -= 3000;
            player.UnitCards.Add(PurchaseUnitCards[index]);

            clickObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "���� �Ϸ�";
            clickObject.GetComponent<Button>().interactable = false;
        }
    }

    //ũ����Ż ���� : 1000���� -> 100ũ����Ż
    public void PurchaseCrystal()
    {
        //1000 ���� ���� �ִ°��
        if (player.Coin >= 1000)
        {
            player.Coin -= 1000;
            player.Crystal += 100;
        }
        
    }
    
    //���� ���� : 100ũ����Ż -> 1000����
    public void PurchaseCoin()
    {
        //100 ũ����Ż ���� �ִ� ���
        if (player.Crystal >= 100)
        {
            player.Crystal -= 100;
            player.Coin += 1000;
        }
    }
}
