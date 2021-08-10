using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarPointSlider : MonoBehaviour
{
    private Slider starPointSlider; //��Ÿ����Ʈ �����̴�

    void Start()
    {
        starPointSlider = GameObject.Find("StarPointSlider").GetComponent<Slider>();
    }

    void Update()
    {
        starPointSlider.value = Player.instance.StarPoint;

        //�÷��̾��� ��Ÿ����Ʈ�� 10�� �̻� �ִ°��
        if (Player.instance.StarPoint >= 10)
        {
            Player.instance.Crystal += 100; //ũ����Ż 100����
            Player.instance.StarPoint -= 10;
        }
    }
}
