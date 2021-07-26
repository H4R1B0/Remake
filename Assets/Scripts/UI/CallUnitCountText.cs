using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallUnitCountText : MonoBehaviour
{
    private Player player;
    private TextMeshProUGUI callUnitCountText;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.instance; //Player �ν��Ͻ� �ҷ�����
        callUnitCountText = GetComponent<TextMeshProUGUI>();
    }

    public void RenewText()
    {
        int unitcnt = GameObject.FindGameObjectsWithTag("Unit").Length;
        callUnitCountText.text = unitcnt+ " / " + player.CallUnitCountMax;
    }
}
