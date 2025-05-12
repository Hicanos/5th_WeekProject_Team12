using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
 
    [Tooltip("�и��� �ӵ�")] // ���ſ� ������Ʈ�� ���ڸ� �� �ٿ��� �� ��
    public float pushSpeed = 1.0f;

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
            // �ڽ� ��ġ - �÷��̾� ��ġ = �и��� ���� ���
            Vector3 pushDirection = CalculatePushDirection(collision.transform.position, collision.relativeVelocity);
            PushBox(pushDirection);           

        }
        // ���� �浹�� ������Ʈ�� �±װ� allowedPusherLayers ��Ͽ� ���ٸ�, �ƹ��͵� ���� ����

    }


    private void PushBox(Vector2 pushDirection)
    {
        // Rigidbody�� ���� �������� ������Ʈ �����̵�
        // �̵� �� Raycast�� �ٸ� �浹ü�� �߻��ϰ�, ��������� �����ؾ���. 

        int layerMask = ~((1 << LayerMask.NameToLayer("Dog")) | (1 << LayerMask.NameToLayer("Ground"))); //���� ������ ��� Layer�� �浹���� (���� �о����, ���� �����ؾ���(����: �߷¾���))

        Vector2 boxSize = boxCollider.size; // BoxColider2D�� ũ�� 
        Vector2 adjustedOrigin = (Vector2)transform.position + (boxSize.x / 2) * pushDirection;

        Debug.DrawRay(adjustedOrigin, pushDirection * pushSpeed, Color.red, 0.1f);



        //Raycast ��� - �������� ������ �̴� �������� �̵��ؾ���. �ٸ� ������Ʈ�� ������ ������.

        RaycastHit2D hitInfo = Physics2D.BoxCast
            (origin: adjustedOrigin, // + �ڽ��� �ʺ�/2 (*�ڽ� �ݶ��̴��� �ʺ� ������) * pushDirection 
            size: boxSize,
            angle: 0,
            direction: pushDirection,
            distance: pushSpeed * Time.deltaTime,
            layerMask: layerMask);

        if ( hitInfo.collider != null )
        {
            return;
        }
        float adjustedSpeed = pushSpeed * Mathf.Clamp(pushDirection.magnitude, 0.5f, 2.0f);
        transform.Translate(pushDirection * adjustedSpeed * Time.deltaTime, Space.World);
        //RaycastHit2D hitInfo = Physics2D.Raycast(origin: transform.position, direction: pushDirection);

    }

    private Vector3 CalculatePushDirection(Vector3 otherPosition, Vector2 relativeVelocity)
    {
        Vector3 direction = (transform.position - otherPosition).normalized;

        //Dog�� �̵����� - �б���� ����
        if (relativeVelocity.magnitude > 0.1f)
        {
            direction = new Vector3(relativeVelocity.x, 0, 0).normalized;
        }

        direction.y = 0; // Y�� �̵� ����
        return direction.normalized; // Y���� 0���� ��������� ���� ���͸� ������ȭ
    }


}

