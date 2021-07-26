using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinText : MonoBehaviour
{
    private GameObject Player;
    private TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        coinText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //코인 수 세 자리마다 , 표시
        coinText.text = GetThousandCommaText(Player.GetComponent<Player>().Coin);
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
