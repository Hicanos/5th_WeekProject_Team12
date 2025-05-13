using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹ü�� ���̾� Ȯ��
        int charLayer = collision.gameObject.layer;

        //���̾� �� Tag Ȯ�� - �ߺ� ī��Ʈ ����
        if(charLayer == LayerMask.NameToLayer("Cat")&& collision.CompareTag("Player"))
        {
            GetItem();
        }
        //����̰� �ƴϸ� �ƹ��� ȿ�� ����
    }

    public void GetItem()
    {
        Item.FishCount--;
        Destroy(gameObject);
    }
}
