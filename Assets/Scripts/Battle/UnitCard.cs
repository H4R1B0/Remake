using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class UnitCard : MonoBehaviour
{
    private Tilemap UnitBoard; //소환될 타일맵
    private Player player; //플레이어

    public GameObject UnitPrefab; //소환할 유닛프리팹
    private GameObject unit;
    public int crystal = 10; //선택 시 소모할 수정
    public string unitName; //소환할 유닛이름


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
        //게임 시작 전에만 소환
        if (GameManager.instance.IsStart == false)
        {
            int currentCount = GameObject.FindGameObjectsWithTag("Unit").Length; //소환된 유닛 수

            //소환 가능한 최대 유닛수 보다 작아야 소환 가능
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
                Debug.Log("소환" + v3Int);
                GetComponent<Button>().interactable = false;

                player.Crystal -= crystal; //소환 비용만큼 수정 감소

                //유닛 판매시에 되돌려받기위해 비용 저장
                unit.GetComponent<Unit>().UnitPrice = crystal;

                //유닛 레벨 지정
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

                //현재 보여지는 유닛카드에 해당하는 이름이 있으면 포함해서 수정 비용 증가
                for (int i = 0; i < GameObject.Find("CardSelects").transform.childCount; i++)
                {
                    if (GameObject.Find("CardSelects").transform.GetChild(i).name == unitName + "Card(Clone)")
                    {
                        GameObject.Find("CardSelects").transform.GetChild(i).GetComponent<UnitCard>().crystal += 10; //소환 비용 증가
                    }

                }
                //카드에 해당하는 플레이어의 유닛카드 수정 소모 비용 증가
                GameObject card = player.UnitCards.Find(x => x.name == unitName + "Card");
                card.GetComponent<UnitCard>().crystal += 10; //소환 비용 증가
                player.CallUnitCount++; //유닛 소환 수 증가
                Debug.Log(player.CallUnitCount);
            }
        }
        //게임 시작하면 소환 못함
        else
        {
            Debug.Log("소환 불가");
        }

        //GameObject.Find("CallUnitCountText").GetComponent<CallUnitCountText>().RenewText(); //유닛 소환 텍스트 갱신
    }
}
