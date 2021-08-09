using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StarPoint : MonoBehaviour
{
    private int point=0; //유닛의 레벨 == 스타포인트
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
        }); //이동 후에 플레이어 스타포인트 증가후 파괴
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
