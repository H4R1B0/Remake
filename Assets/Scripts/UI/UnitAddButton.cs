using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAddButton : MonoBehaviour
{
    private Player player; //플레이어

    void Start()
    {
        player = Player.instance;
        this.GetComponent<Button>().onClick.AddListener(player.CallUnitCountAdd);
        if (player.CallUnitCountMax >= 7)
        {
            this.GetComponent<Button>().interactable = false; //소환 최대 증가 버튼 비활성화
        }
        
    }
}
