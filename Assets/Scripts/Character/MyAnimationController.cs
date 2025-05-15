using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MyAnimationController : MonoBehaviour
{
    /// <summary>
    /// 애니메이션 컨트롤을 위한 Animator 컴포넌트
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// 방향 전환을 위한 SpriteRenderer (필요할 경우)
    /// </summary>
    private SpriteRenderer spriteRenderer;
   
    private void Awake()
    {
        
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    
    /// <summary>
    /// 캐릭터 좌우 방향 반전을 처리함
    /// </summary>
    /// <param name="flip">왼쪽이면 true, 오른쪽이면 false</param>
    public void SetFlip(bool flip)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = flip;
    }
    /// <summary>
    /// 이동 중 여부에 따라 'IsMove' 파라미터 설정
    /// </summary>
    /// <param name="isMoving">움직이고 있는지 여부</param>
    public void SetMove(bool isMoving)
    {
        _animator.SetBool("IsMove", isMoving);
    }
    public void SetJump(bool isJump)
    {
        _animator.SetBool("IsJump", isJump);
    }

    public void SetSkill(bool isSkill)
    {
        _animator.SetBool("IsSkill",isSkill);
    }

    public void SetCrash()
    {
        _animator.SetTrigger("IsCrash");
    }

    public void SetSwitch(bool isSwitch)
    {
        _animator.SetBool("IsSwitch", isSwitch);
    }
    
}
