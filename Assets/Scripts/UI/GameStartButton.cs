using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private GameManager gamemanager; //���ӸŴ���

    void Start()
    {
        gamemanager = GameManager.instance;
        this.GetComponent<Button>().onClick.AddListener(gamemanager.CreateMonster);

    }
}
