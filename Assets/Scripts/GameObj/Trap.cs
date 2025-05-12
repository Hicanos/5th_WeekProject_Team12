using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("���� �±�")] public string compareTag;
    public GameObject Player;
    private Vector3 initialPosition; //�ʱ� ���� ������Ʈ�� ��ġ (��/�����/���� ��)

    private void Start()
    {
        //�� �÷��̾��� ������ �ʱⰪ ����
        initialPosition = Player.transform.position;    
    }


    public string GetTargetTagName()
    {
        return compareTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetTargetTagName();
        if (collision.CompareTag(compareTag))
        {
            //������Ʈ�� TargetName�� �ε��� Player�� �±׺�
            //���� �ε����ٸ� ��, ����̰� �ε����ٸ� ����̰� �ʱ���ġ�� ��
            //�̸� Ȱ���ϸ�, ���� �߶��ؼ� ����������ų� ����ġ ��ų �� ���� ������Ʈ�� ���� �ڸ��� �ű� �� ����
            Player.transform.position = initialPosition;
           
        }
    }

}
