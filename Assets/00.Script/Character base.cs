using System.Collections;
using System.Collections.Generic;

    using UnityEngine;

/// <summary>
/// ��� ĳ����(�÷��̾�/���� ��)�� ���� ����� ����ϴ� �߻� ��Ʈ�ѷ�
/// �̵�, ü�� ó��, ��������Ʈ ���� �� �⺻ �ൿ ����
/// </summary>
public abstract class Characterbase: MonoBehaviour
{
    public GameObject helmet;
    public int moveSpeed=5;
    protected Rigidbody2D rb;                    // �̵��� ���� ������ٵ�
    protected SpriteRenderer spriteRenderer;     // �¿� ������ ���� ��������Ʈ ������
    protected MyAnimationController Anim;
    /// <summary> �ʱ�ȭ: ������ٵ�, ��������Ʈ ã�� ���� �ʱ�ȭ </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }

    /// <summary> �̵� ó��: �ӵ� ���� �� ���� ���� </summary>
    protected virtual void Move(Vector2 input)
    {

        Vector2 velocity = rb.velocity;
        velocity.x = input.x * moveSpeed;
        rb.velocity = velocity;

        
            // �¿� ���� ó��
       /*     if (input.x != 0)
                spriteRenderer.flipX = input.x < 0; */

     }
   
   protected virtual void Skill()
    { 
        
    
    }
    
}
  
