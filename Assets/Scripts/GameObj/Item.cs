using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    //������/Trap: ��/���� : ���� Trigger�� ������ ��������� Ȯ�� = �±� ����
    //�˸��� �浹 ü���� ������ �ö�. �ƴ϶�� �ı����� ����
    //Gift: ����. ȹ���ϸ� true. �� �������� ���� 3���� ������ �������� �޼�
    // : ������ �Ŵ���, ������Ʈ �Ŵ����� ����
    // Gift 0���� Ʃ�丮�� ������
    [Header("���� �±�")] public string compareTag;
    public GameObject Player;
    int BonusCount = 0;

    public string GetTargetTagName()
    {
        return compareTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetTargetTagName();
        if (collision.CompareTag(compareTag))
        {
            GetItem();

        }
    }
    void GetItem()
    {   // ������ ȹ�� �� = BonusCount(����)���� �� �ı�
        BonusCount++;
        Destroy(this);
    }
}
