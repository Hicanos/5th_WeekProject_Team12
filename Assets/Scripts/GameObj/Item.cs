using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ItemType
{
    Item = 1, //������
    Trap, //����
    Gift //����Ʈ
}

public class Item : MonoBehaviour
{
    public int ItemType { get; private set; }
    public bool IsDog; 


    //������/Trap: ��/���� : ���� Trigger�� ������ ��������� Ȯ�� = �±� ����
    //�˸��� �浹 ü���� ������ �ö�. �ƴ϶�� �ı����� ����
    //Gift: ����. ȹ���ϸ� true. �� �������� ���� 3���� ������ �������� �޼�
    // : ������ �Ŵ���, ������Ʈ �Ŵ����� ����
    // Gift 0���� Ʃ�丮�� ������

    void GetItem()
    {

    }

    void Trapped()
    {

    }
    void GetGift()
    {

    }
}
