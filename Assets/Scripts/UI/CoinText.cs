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
        player = Player.instance; //Player �ν��Ͻ� �ҷ�����
        coinText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //���� �� �� �ڸ����� , ǥ��
        coinText.text = GetThousandCommaText(player.Coin);
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
