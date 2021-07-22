using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientExecutionerWeapon : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); //라인렌더러 설정
        StartCoroutine(nameof(DestroyCoroutine));
    }

    public void SetDir(Vector3 start, Vector3 end)
    {
        //라인렌더러 위치 표시
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    //생성 0.2초뒤 파괴
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
