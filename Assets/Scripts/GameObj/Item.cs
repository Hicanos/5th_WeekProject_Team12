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
    public static int FishCount = 0;
    public static int BoneCount = 0;


    private void Start()
    {
        //Fish, Bone ������Ʈ �Ʒ��� �ִ� �ڽĵ�(Fish Ŭ�е�) ī��Ʈ)
        BoneCount = BoneObj.transform.childCount;
        FishCount = FishObj.transform.childCount;
    }

    // ������ ȹ�� �� = �ش� �����۰� �����ϴ� ���� ���� �� �ı�
    //    FishCount--;
    //    BoneCount--;
    //    Destroy(this);
}
