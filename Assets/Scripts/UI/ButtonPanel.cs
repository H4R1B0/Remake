using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPanel : MonoBehaviour
{
    //홈화면으로 이동
    public void GoBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
