using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public GameObject SettingPanel; //설정 패널
    private bool isOpen = false; //창이 열렸는지

    void Start()
    {
        SettingPanel.gameObject.SetActive(false);
    }

    //설정 창 열때
    public void OpenSetting()
    {
        
        isOpen = true;
        Time.timeScale = 0;
        SettingPanel.gameObject.SetActive(isOpen);
    }

    //설정 창 닫을때
    public void CloseSetting()
    {
        isOpen = false;
        Time.timeScale = 1;
        SettingPanel.gameObject.SetActive(isOpen);
    }

    //홈화면으로 이동
    public void GoLobby()
    {
        //로비로 이동할때 모든 유닛 파괴 및 수정 돌려줌
        List<GameObject> foundUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        foreach(GameObject foundUnit in foundUnits)
        {
            Player.instance.Crystal += foundUnit.GetComponent<Unit>().UnitPrice; //유닛 판매 비용 돌려주기

            //유닛에 해당하는 플레이어의 유닛카드 수정 소모 비용 감소
            GameObject card = Player.instance.UnitCards.Find(x => x.name == foundUnit.GetComponent<Unit>().UnitName + "Card");
            card.GetComponent<UnitCard>().crystal -= 10; //소환 비용 감소
        }


        Time.timeScale = 1;
        Player.instance.CallUnitCount = 0;
        GameManager.instance.IsStart = false;
        SceneManager.LoadScene("Lobby");
    }
}
