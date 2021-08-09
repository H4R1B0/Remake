using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarPoint : MonoBehaviour
{
    private int point=0; //������ ���� == ��Ÿ����Ʈ
    public int Point
    {
        set
        {
            point = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject target = GameObject.Find("StarPointImage");
        transform.DOMove(target.transform.position, 1).OnComplete(() => {
            Player.instance.StarPoint += point;
            Destroy(this.gameObject);
        }); //�̵� �Ŀ� �÷��̾� ��Ÿ����Ʈ ������ �ı�
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
