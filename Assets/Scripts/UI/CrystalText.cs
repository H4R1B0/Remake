using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrystalText : MonoBehaviour
{
    private Player player;
    private TextMeshProUGUI crystalText;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.instance; //Player 인스턴스 불러오기
        crystalText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //코인 수 세 자리마다 , 표시
        crystalText.text = GetThousandCommaText(player.Crystal);
    }
    public string GetThousandCommaText(int data)
    {
        return string.Format("{0:#,###}", data);
    }
}
