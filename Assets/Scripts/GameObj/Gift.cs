using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] public int LegacyID;

    void GetGift()
    {
        //ȹ���� ������ ID(��ȣ)�� DataManager�� AquiredLegacy�� ���� ���
        if (!DataManager.AquiredLegacy.Contains(LegacyID))
        {
            //LegacyID�� DataManager�� AquiredLegacy�� �߰�, ������ ����Ʈ�� �� ������ Ȯ��.
            DataManager.AquiredLegacy.Add(LegacyID);
            Debug.Log($"����ȹ��: {ObjManager.LegacyList[LegacyID].ToString()}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹ü�� ���� ��������� Ȯ��(���� �÷��̾�)
        if (collision.CompareTag("Dog") || collision.CompareTag("Cat"))
        {
            GetGift();
            Destroy(gameObject); //������ ����ϰ� ������Ʈ�� ����
        }
    }
}
