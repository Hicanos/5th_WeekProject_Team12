using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
     private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        // ���� �� �ڽ��� Rigidbody �ҷ���
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            // Rigidbody�� ���ٸ� ��� �޽���
            Debug.LogError("Box ��ũ��Ʈ�� Rigidbody ������Ʈ�� �ʿ��մϴ�. GameObject �̸�: " + gameObject.name);
        }
        

        //boxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
        if(boxCollider == null)
        {
            Debug.LogError("Box ��ũ��Ʈ�� BoxCollider2D ������Ʈ�� �ʿ��մϴ�. GameObject �̸�: " + gameObject.name);
        }

    }

    // ��ϵ� ������Ʈ�� �浹�� ���ӵǴ� ���� ��� ȣ��-�б�

    private void OnCollisionStay2D(Collision2D collision)
    {
        //�ش� ������Ʈ�� layer = Dog (7��)��� ����Ǹ� ��
        int charLayer = collision.gameObject.layer;
        string layerName = LayerMask.LayerToName(charLayer);
     
        Debug.Log("������ ������Ʈ�� Layer:" + layerName);

        // allowedPusher ��Ͽ��� �浹�� ������Ʈ�� ���̾��� ���Կ��� Ȯ��
        if (charLayer == LayerMask.NameToLayer("Dog"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY; //ȸ������. Y����(���� �ö󰡴� �� ����)
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX; //ȸ�� ����, x����
        }

    }


}

