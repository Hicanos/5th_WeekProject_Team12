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
        // 시작 시 자신의 Rigidbody 불러옴
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            // Rigidbody가 없다면 경고 메시지
            Debug.LogError("Box 스크립트는 Rigidbody 컴포넌트가 필요합니다. GameObject 이름: " + gameObject.name);
        }
        

        //boxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();
        if(boxCollider == null)
        {
            Debug.LogError("Box 스크립트는 BoxCollider2D 컴포넌트가 필요합니다. GameObject 이름: " + gameObject.name);
        }

    }

    // 등록된 오브젝트와 충돌이 지속되는 동안 계속 호출-밀기

    private void OnCollisionStay2D(Collision2D collision)
    {
        //해당 오브젝트의 layer = Dog (7번)라면 실행되면 됨
        int charLayer = collision.gameObject.layer;
        string layerName = LayerMask.LayerToName(charLayer);
     
        Debug.Log("접촉한 오브젝트의 Layer:" + layerName);

        // allowedPusher 목록에서 충돌한 오브젝트의 레이어의 포함여부 확인
        if (charLayer == LayerMask.NameToLayer("Dog"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY; //회전금지. Y금지(위로 올라가는 것 방지)
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX; //회전 금지, x금지
        }

    }


}

