using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class UnitCard : MonoBehaviour
{
    private Tilemap UnitBoard; //��ȯ�� Ÿ�ϸ�
    private Player player; //�÷��̾�

    public GameObject UnitPrefab; //��ȯ�� ����������
    private GameObject unit;
    public int crystal = 10; //���� �� �Ҹ��� ����
    public string unitName; //��ȯ�� �����̸�


    private void Awake()
    {
        UnitBoard = GameObject.Find("UnitBoard").GetComponent<Tilemap>();
    }
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CallUnit()
    {
        //���� ���� ������ ��ȯ
        if (GameManager.instance.IsStart == false)
        {
            int currentCount = GameObject.FindGameObjectsWithTag("Unit").Length; //��ȯ�� ���� ��

            //��ȯ ������ �ִ� ���ּ� ���� �۾ƾ� ��ȯ ����
            if (currentCount < player.CallUnitCountMax)
            {
                Vector3Int v3Int;
                do
                {
                    int randX = Random.Range(0, 4);
                    int randY = Random.Range(0, 4);
                    v3Int = new Vector3Int(randX, randY, 0);
                } while (UnitBoard.GetColor(v3Int) == Color.green);

                //Instantiate(unit, board.CellToWorld(v3Int), Quaternion.identity);
                unit = Instantiate(UnitPrefab, UnitBoard.CellToWorld(v3Int) + new Vector3(0.9f, 1.4f, 0), Quaternion.identity);
                UnitBoard.SetTileFlags(v3Int, TileFlags.None);
                UnitBoard.SetColor(v3Int, Color.green);
                Debug.Log("��ȯ" + v3Int);
                GetComponent<Button>().interactable = false;

                player.Crystal -= crystal; //��ȯ ��븸ŭ ���� ����

                //���� �ǸŽÿ� �ǵ����ޱ����� ��� ����
                unit.GetComponent<Unit>().UnitPrice = crystal;

                //���� ���� ����
                if (unitName == "Baroque" || unitName == "Fenny" || unitName == "Jenis" || unitName == "Nano" || unitName == "Orihiru" || unitName == "Squil")
                {
                    unit.GetComponent<Unit>().Level = 1;
                }
                else if (unitName == "Anima" || unitName == "Destiny" || unitName == "Dicafrio" || unitName == "Hades" || unitName == "Rang" || unitName == "Wright")
                {
                    unit.GetComponent<Unit>().Level = 2;
                }
                else if (unitName == "Batti" || unitName == "Beomho" || unitName == "Kelsy" || unitName == "Rifi" || unitName == "Spinps")
                {
                    unit.GetComponent<Unit>().Level = 3;
                }
                else if (unitName == "Crusher" || unitName == "Kirabee" || unitName == "Tomb")
                {
                    unit.GetComponent<Unit>().Level = 4;
                }

                //���� �������� ����ī�忡 �ش��ϴ� �̸��� ������ �����ؼ� ���� ��� ����
                for (int i = 0; i < GameObject.Find("CardSelects").transform.childCount; i++)
                {
                    if (GameObject.Find("CardSelects").transform.GetChild(i).name == unitName + "Card(Clone)")
                    {
                        GameObject.Find("CardSelects").transform.GetChild(i).GetComponent<UnitCard>().crystal += 10; //��ȯ ��� ����
                    }

                }
                //ī�忡 �ش��ϴ� �÷��̾��� ����ī�� ���� �Ҹ� ��� ����
                GameObject card = player.UnitCards.Find(x => x.name == unitName + "Card");
                card.GetComponent<UnitCard>().crystal += 10; //��ȯ ��� ����
                player.CallUnitCount++; //���� ��ȯ �� ����
                Debug.Log(player.CallUnitCount);
            }
        }
        //���� �����ϸ� ��ȯ ����
        else
        {
            Debug.Log("��ȯ �Ұ�");
        }

        //GameObject.Find("CallUnitCountText").GetComponent<CallUnitCountText>().RenewText(); //���� ��ȯ �ؽ�Ʈ ����
    }
}
