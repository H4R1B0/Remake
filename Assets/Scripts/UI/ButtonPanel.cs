using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPanel : MonoBehaviour
{
    //Ȩȭ������ �̵�
    public void GoBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
