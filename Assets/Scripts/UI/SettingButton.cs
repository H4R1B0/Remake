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
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
    }
}
