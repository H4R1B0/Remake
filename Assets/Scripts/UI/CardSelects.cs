using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelects : MonoBehaviour
{
    private Player player; //�÷��̾�
    public List<GameObject> selects; //��ü�� 5�� ī���

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(this.transform.position);
        player = Player.instance; //Player �ν��Ͻ� �ҷ�����
        RefreshUnitCard();
        player.Crystal += 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RefreshUnitCard()
    {
        //�÷��̾��� ũ����Ż�� 10�� �̻������� ��� ����
        if (player.Crystal >= 10)
        {
            player.Crystal -= 10; //���������ϸ� ũ����Ż ����
                                  //Debug.Log(player.UnitCards.Count);
            Transform grid = this.transform; //CardSelects grid ��������
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
