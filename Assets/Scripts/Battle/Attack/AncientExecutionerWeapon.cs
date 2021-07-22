using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientExecutionerWeapon : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); //���η����� ����
        StartCoroutine(nameof(DestroyCoroutine));
    }

    public void SetDir(Vector3 start, Vector3 end)
    {
        //���η����� ��ġ ǥ��
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    //���� 0.2�ʵ� �ı�
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
