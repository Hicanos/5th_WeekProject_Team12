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
            Debug.Log($"����ȹ��: {DataManager.LegacyList[LegacyID].ToString()}");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹ü�� ���� ��������� Ȯ��(���� �÷��̾�)
        if (collision.CompareTag("Player"))
        {
            GetGift();
            Destroy(gameObject); //������ ����ϰ� ������Ʈ�� ����
        }
    }
}
