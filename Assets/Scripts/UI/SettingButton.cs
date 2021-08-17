using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public GameObject SettingPanel; //���� �г�
    private bool isOpen = false; //â�� ���ȴ���

    void Start()
    {
        SettingPanel.gameObject.SetActive(false);
    }

    //���� â ����
    public void OpenSetting()
    {
        
        isOpen = true;
        Time.timeScale = 0;
        SettingPanel.gameObject.SetActive(isOpen);
    }

    //���� â ������
    public void CloseSetting()
    {
        isOpen = false;
        Time.timeScale = 1;
        SettingPanel.gameObject.SetActive(isOpen);
    }

    //Ȩȭ������ �̵�
    public void GoLobby()
    {
        //�κ�� �̵��Ҷ� ��� ���� �ı� �� ���� ������
        List<GameObject> foundUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        foreach(GameObject foundUnit in foundUnits)
        {
            Player.instance.Crystal += foundUnit.GetComponent<Unit>().UnitPrice; //���� �Ǹ� ��� �����ֱ�

            //���ֿ� �ش��ϴ� �÷��̾��� ����ī�� ���� �Ҹ� ��� ����
            GameObject card = Player.instance.UnitCards.Find(x => x.name == foundUnit.GetComponent<Unit>().UnitName + "Card");
            card.GetComponent<UnitCard>().crystal -= 10; //��ȯ ��� ����
        }


        Time.timeScale = 1;
        Player.instance.CallUnitCount = 0;
        GameManager.instance.IsStart = false;
        SceneManager.LoadScene("Lobby");
    }
}
