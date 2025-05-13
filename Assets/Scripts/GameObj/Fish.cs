using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        //���̾� �� Tag Ȯ�� - �ߺ� ī��Ʈ ����
        if(collision.gameObject.layer == LayerMask.NameToLayer("Cat")
            && collision.CompareTag("Player"))
        {
            GetItem();
        }
        //����̰� �ƴϸ� �ƹ��� ȿ�� ����
    }

    public void GetItem()
    {
        Item.ChangeFishCount(-1);
        Destroy(gameObject);
    }
}
