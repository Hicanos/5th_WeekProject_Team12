using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] public int LegacyID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹ü�� ���� ���������� Ȯ��(���� �÷��̾�)
        if (collision.CompareTag("Player"))
        {
            ObjManager.Instance.CollectLegacy(LegacyID);
            Destroy(gameObject); //������ ����ϰ� ������Ʈ�� ����
        }
    }
}
