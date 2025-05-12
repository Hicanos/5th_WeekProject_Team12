using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MyAnimationController : MonoBehaviour
{
    /// <summary>
    /// �ִϸ��̼� ��Ʈ���� ���� Animator ������Ʈ
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// ���� ��ȯ�� ���� SpriteRenderer (�ʿ��� ���)
    /// </summary>
    private SpriteRenderer spriteRenderer;
   
    private void Awake()
    {
        
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    
    /// <summary>
    /// ĳ���� �¿� ���� ������ ó����
    /// </summary>
    /// <param name="flip">�����̸� true, �������̸� false</param>
    public void SetFlip(bool flip)
    {
        if (spriteRenderer != null)
            spriteRenderer.flipX = flip;
    }
    /// <summary>
    /// �̵� �� ���ο� ���� 'IsMove' �Ķ���� ����
    /// </summary>
    /// <param name="isMoving">�����̰� �ִ��� ����</param>
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
    
}
