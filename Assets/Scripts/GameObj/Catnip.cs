using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : Trap
{    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���̾� �� Tag Ȯ�� - �ߺ� ī��Ʈ ����
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cat")
            && collision.CompareTag("Player"))
        {
            collision.transform.position = initialPosition; //�÷��̾��� position�� Respawner�� ���������� ����
        }
        //����̰� �ƴϸ� �ƹ��� ȿ�� ����
    }
}
