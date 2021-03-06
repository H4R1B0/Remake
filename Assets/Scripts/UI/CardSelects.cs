using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelects : MonoBehaviour
{
    private Player player; //플레이어
    public List<GameObject> selects; //교체할 5개 카드들

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.transform.position);
        player = Player.instance; //Player 인스턴스 불러오기
        RefreshUnitCard();
        player.Crystal += 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RefreshUnitCard()
    {
        //게임 시작 전이어야 리프레시 가능
        //플레이어의 크리스탈이 10개 이상있을때 사용 가능
        if (GameManager.instance.IsStart == false && player.Crystal >= 10)
        {
            player.Crystal -= 10; //리프레시하면 크리스탈 감소
                                  //Debug.Log(player.UnitCards.Count);
            Transform grid = this.transform; //CardSelects grid 가져오기
            for (int i = 0; i < selects.Count; i++)
            {
                //int rand = Random.Range(0, unitCards.transform.childCount);
                //Debug.Log(rand);
                int idx = Random.Range(0, player.UnitCards.Count);
                GameObject unitcard = Instantiate(player.UnitCards[idx].gameObject, selects[i].transform.position, Quaternion.identity);
                unitcard.transform.SetParent(grid);
                unitcard.transform.localScale = new Vector3(1, 1, 1);
                //unit.transform.localScale = new Vector3(1, 1, 1);
                Destroy(selects[i].gameObject);
                selects.RemoveAt(i);
                selects.Insert(i, unitcard);
            }
        }
    }
}
