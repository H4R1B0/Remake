using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitAddButton : MonoBehaviour
{
    private Player player; //�÷��̾�

    void Start()
    {
        player = Player.instance;

        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = player.CallUnitCountAddPrice.ToString();

        this.GetComponent<Button>().onClick.AddListener(player.CallUnitCountAdd);
        if (player.CallUnitCountMax >= 7)
        {
            this.GetComponent<Button>().interactable = false; //��ȯ �ִ� ���� ��ư ��Ȱ��ȭ
        }
        
    }
}
