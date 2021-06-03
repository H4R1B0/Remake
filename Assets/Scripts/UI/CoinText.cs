using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        this.GetComponent<Text>().text = GetThousandCommaText(Player.GetComponent<Player>().Coin);
    }

    string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
