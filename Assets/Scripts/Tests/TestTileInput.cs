using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Ÿ�ϸ� �Է� �׽�Ʈ ��ũ��Ʈ
public class TestTileInput : MonoBehaviour
{
    public Tilemap tilemap;


    //���콺�� Ÿ�� ���� ��ġ�� ���� �۾��� ���̱� ������ onMouseOver�� ����߽��ϴ�.

    //�����ϸ� ������ �ϴ°͵� ������ ���׿�.

    private void OnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);



        RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

        if (this.tilemap = hit.transform.GetComponent<Tilemap>())
        {
            this.tilemap.RefreshAllTiles();

            int x, y;
            x = this.tilemap.WorldToCell(ray.origin).x;
            y = this.tilemap.WorldToCell(ray.origin).y;

            Vector3Int v3Int = new Vector3Int(x, y, 0);



            //Ÿ�� �� �ٲ� �� �̰� �־�� �ϴ�����
            this.tilemap.SetTileFlags(v3Int, TileFlags.None);

            //Ÿ�� �� �ٲٱ�
            this.tilemap.SetColor(v3Int, (Color.red));
            Debug.Log(x + ", " + y);
        }
    }
    private void OnMouseExit()
    {
        this.tilemap.RefreshAllTiles();

    }
}
