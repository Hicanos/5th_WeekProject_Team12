using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //������: ��/���� : ���� Trigger�� ������ ��������� Ȯ�� = Layer�� ���� 
    //�˸��� �浹 ü���� ������ �ö�. �ƴ϶�� �ı����� ����
    //Gift: ����. ȹ���ϸ� true. �� �������� ���� 3���� ������ �������� �޼�
    
    //���⼭ �� ������ ������ 0 = �� 1��
    public GameObject FishObj;
    public GameObject BoneObj;
    public static int FishCount;
    public static int BoneCount;


    //�̺�Ʈ �ý������� Fish�� Bone�� ī��Ʈ ��ȭ�� ���� ������ ȣ��
    public static event Action OnItemCountChanged;

    private void Awake()
    {
        //Fish, Bone ������Ʈ �Ʒ��� �ִ� �ڽĵ�(Fish Ŭ�е�) ī��Ʈ)
        BoneCount = BoneObj.transform.childCount;
        FishCount = FishObj.transform.childCount;
        OnItemCountChanged?.Invoke();
    }

    public static void ChangeFishCount(int delta)
    {
        FishCount += delta;
        OnItemCountChanged?.Invoke();
    }

    public static void ChangeBoneCount(int delta)
    {
        BoneCount += delta;
        OnItemCountChanged?.Invoke();
    }

    // ������ ȹ�� �� = �ش� �����۰� �����ϴ� ���� ���� �� �ı�
    //    FishCount--;
    //    BoneCount--;
    //    Destroy(this);
}
