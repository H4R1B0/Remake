using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    private Player player;
    private TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance; //Player 인스턴스 불러오기
        coinText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //코인 수 세 자리마다 , 표시
        coinText.text = GetThousandCommaText(player.Coin);
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
