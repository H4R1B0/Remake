using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopPanel : MonoBehaviour
{
    public List<GameObject> PurchaseUnitCards; //구매할 유닛카드 리스트
    public List<GameObject> UnitPurchaseButtons; //유닛 구매 버튼 리스트

    private Player player; //플레이어

    void Start()
    {
        this.gameObject.SetActive(false);
        player = Player.instance;

        //상점에 있는 유닛카드가 플레이어가 갖고 있다면 비활성화
        for (int i=0;i< PurchaseUnitCards.Count;i++)
        {
            if (player.UnitCards.Contains(PurchaseUnitCards[i]))
            {
                UnitPurchaseButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "구매 완료";
                UnitPurchaseButtons[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                UnitPurchaseButtons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    //유닛카드 구매
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

            clickObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "구매 완료";
            clickObject.GetComponent<Button>().interactable = false;
        }
        else if (index >= 6 && index <= 10)
        {
            player.Coin -= 2000;
            player.UnitCards.Add(PurchaseUnitCards[index]);

            clickObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "구매 완료";
            clickObject.GetComponent<Button>().interactable = false;
        }
        else if (index >= 11 && index <= 13)
        {
            player.Coin -= 3000;
            player.UnitCards.Add(PurchaseUnitCards[index]);

            clickObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "구매 완료";
            clickObject.GetComponent<Button>().interactable = false;
        }
    }

    //크리스탈 구매 : 1000코인 -> 100크리스탈
    public void PurchaseCrystal()
    {
        //1000 코인 갖고 있는경우
        if (player.Coin >= 1000)
        {
            player.Coin -= 1000;
            player.Crystal += 100;
        }
        
    }
    
    //코인 구매 : 100크리스탈 -> 1000코인
    public void PurchaseCoin()
    {
        //100 크리스탈 갖고 있는 경우
        if (player.Crystal >= 100)
        {
            player.Crystal -= 100;
            player.Coin += 1000;
        }
    }
}
