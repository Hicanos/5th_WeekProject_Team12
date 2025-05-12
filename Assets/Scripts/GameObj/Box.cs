using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
 
    [Tooltip("밀리는 속도")] // 무거운 오브젝트는 숫자를 더 줄여서 쓸 것
    public float pushSpeed = 1.0f;

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
            // 박스 위치 - 플레이어 위치 = 밀리는 방향 계산
            Vector3 pushDirection = CalculatePushDirection(collision.transform.position, collision.relativeVelocity);
            PushBox(pushDirection);           

        }
        // 만약 충돌한 오브젝트의 태그가 allowedPusherLayers 목록에 없다면, 아무것도 하지 않음

    }


    private void PushBox(Vector2 pushDirection)
    {
        // Rigidbody에 계산된 방향으로 오브젝트 직접이동
        // 이동 중 Raycast에 다른 충돌체가 발생하고, 가까워지면 정지해야함. 

        int layerMask = ~((1 << LayerMask.NameToLayer("Dog")) | (1 << LayerMask.NameToLayer("Ground"))); //개를 제외한 모든 Layer의 충돌판정 (개는 밀어야함, 땅은 무시해야함(사유: 중력없음))

        Vector2 boxSize = boxCollider.size; // BoxColider2D의 크기 
        Vector2 adjustedOrigin = (Vector2)transform.position + (boxSize.x / 2) * pushDirection;

        Debug.DrawRay(adjustedOrigin, pushDirection * pushSpeed, Color.red, 0.1f);



        //Raycast 사용 - 강아지랑 닿으면 미는 방향으로 이동해야함. 다른 오브젝트와 만나면 정지함.

        RaycastHit2D hitInfo = Physics2D.BoxCast
            (origin: adjustedOrigin, // + 박스의 너비/2 (*박스 콜라이더의 너비를 가져옴) * pushDirection 
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

        //Dog의 이동방향 - 밀기방향 조정
        if (relativeVelocity.magnitude > 0.1f)
        {
            direction = new Vector3(relativeVelocity.x, 0, 0).normalized;
        }

        direction.y = 0; // Y축 이동 제한
        return direction.normalized; // Y축을 0으로 만들었으니 방향 벡터를 재정규화
    }


}

