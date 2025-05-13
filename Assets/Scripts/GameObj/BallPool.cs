using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPool : Trap
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���̾� �� Tag Ȯ�� - �ߺ� ī��Ʈ ����
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dog")
            && collision.CompareTag("Player"))
        {
            collision.transform.position = initialPosition; //�÷��̾��� position�� Respawner�� ���������� ����
        }
        //���� �ƴϸ� �ƹ��� ȿ�� ����
    }
}
