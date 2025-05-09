using System.Collections;
using System.Collections.Generic;

    using UnityEngine;

/// <summary>
/// 모든 캐릭터(플레이어/몬스터 등)의 공통 기능을 담당하는 추상 컨트롤러
/// 이동, 체력 처리, 스프라이트 반전 등 기본 행동 제공
/// </summary>
public abstract class Characterbase: MonoBehaviour
{
    public GameObject helmet;
    public int moveSpeed=5;
    protected Rigidbody2D rb;                    // 이동을 위한 리지드바디
    protected SpriteRenderer spriteRenderer;     // 좌우 반전을 위한 스프라이트 렌더러
    protected MyAnimationController Anim;
    /// <summary> 초기화: 리지드바디, 스프라이트 찾고 스탯 초기화 </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }

    /// <summary> 이동 처리: 속도 적용 및 방향 반전 </summary>
    protected virtual void Move(Vector2 input)
    {

        Vector2 velocity = rb.velocity;
        velocity.x = input.x * moveSpeed;
        rb.velocity = velocity;

        
            // 좌우 반전 처리
       /*     if (input.x != 0)
                spriteRenderer.flipX = input.x < 0; */

     }
   
   protected virtual void Skill()
    { 
        
    
    }
    
}
  
