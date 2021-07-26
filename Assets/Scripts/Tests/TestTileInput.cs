using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//타일맵 입력 테스트 스크립트
public class TestTileInput : MonoBehaviour
{
    public Tilemap tilemap;


    //마우스가 타일 위에 위치할 때만 작업할 것이기 때문에 onMouseOver를 사용했습니다.

    //가능하면 기즈모로 하는것도 좋을것 같네요.

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



            //타일 색 바꿀 때 이게 있어야 하더군요
            this.tilemap.SetTileFlags(v3Int, TileFlags.None);

            //타일 색 바꾸기
            this.tilemap.SetColor(v3Int, (Color.red));
            Debug.Log(x + ", " + y);
        }
    }
    private void OnMouseExit()
    {
        this.tilemap.RefreshAllTiles();

    }
}
