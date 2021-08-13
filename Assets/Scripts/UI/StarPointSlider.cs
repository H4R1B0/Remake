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
            //�÷��̾��� ��Ÿ����Ʈ������ 5���� ������ 100(+50)��ŭ ���� ���� 
            if (Player.instance.StarPointLevel < 5)
            {
                Player.instance.Crystal += 100 + (Player.instance.StarPointLevel - 1) * 50;
            }
            else
            {
                Player.instance.Crystal += 300;
            }
            Player.instance.StarPoint -= 10;
        }
    }
}
