using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //���̾� �� Tag Ȯ�� - �ߺ� ī��Ʈ ����
        if (collision.gameObject.layer == LayerMask.NameToLayer("Dog")
            && collision.CompareTag("Player"))
        {
            GetItem();
        }
        //���� �ƴϸ� �ƹ��� ȿ�� ����
    }

    public void GetItem()
    {
        Item.ChangeBoneCount(-1);
        Destroy(gameObject);
    }
}
