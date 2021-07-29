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
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
    }
}
