using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��� ĳ����(�÷��̾�/���� ��)�� ���� ����� ����ϴ� �߻� ��Ʈ�ѷ�
/// �̵�, ü�� ó��, ��������Ʈ ���� �� �⺻ �ൿ ����
/// </summary>


public abstract class Characterbase: MonoBehaviour
{
    // �Է°� ����
    protected float moveX;
    protected float moveY;
    protected Vector2 moveInput;

    protected Rigidbody2D rb;                    // �̵��� ���� ������ٵ�
    protected SpriteRenderer spriteRenderer;     // �¿� ������ ���� ��������Ʈ ������
    protected MyAnimationController Anim;
   protected enum CHAR
    {
        DOG,
        CAT
    }
    [Header("���� ��Ÿ��")]
    [SerializeField]protected  CHAR enumChar;
    protected void ControlKey()
    {
        switch (enumChar)
        {
            case CHAR.DOG:
                leftKey = KeyCode.LeftArrow;
                rightKey = KeyCode.RightArrow;
                jumpKey = KeyCode.UpArrow;
                SkillKey = KeyCode.DownArrow;
                break;

            case CHAR.CAT:
                leftKey = KeyCode.A;
                rightKey = KeyCode.D;
                jumpKey = KeyCode.W;
                SkillKey = KeyCode.S;
                break;
        }

    }

    [Header("����Ű ����")]
    [SerializeField]protected KeyCode leftKey ;
    [SerializeField]protected KeyCode rightKey ;
    [SerializeField]protected KeyCode jumpKey ;
    [SerializeField]protected KeyCode SkillKey ;
    [SerializeField]protected int moveSpeed=5;
       
    [Header("Ⱦ��ũ�� ���� ��� ���� �ý���")]
    [SerializeField]protected Transform groundCheck;      // �ٴ� ���� ��ġ
    [SerializeField]protected float rayRange = 0.1f; // �ٴ� üũ ����(raycast����)
    [SerializeField]protected LayerMask groundLayer;      // �ٴ� ���̾�
    [SerializeField]protected float jumpPower = 5f;       // ���� ��
    [SerializeField]protected int maxJumpCount = 2;
    protected int currentJumpCount = 0;
   
    [Header("��� �ǹ�")]
    [SerializeField] protected Transform headPivot;
     protected GameObject helmet;

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




    //���� ���� ������ �Ұ� �ʿ� �� ���
    protected bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayRange, groundLayer);
        return hit.collider != null;
    }
    protected void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * rayRange);
    }
    //���� �ٱ����� �ݶ��̴� ����
    /*  private void OnTriggerEnter2D(Collider2D other)
      {
          if (other.CompareTag("wall"))
          {

          }
      }*/
    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    protected void HandleInput()
    {
        /*moveX = Input.GetAxisRaw("Horizontal");*/
        moveX = 0;

        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        moveInput = new Vector2(moveX, moveY).normalized;
    }
    /// <summary>
    /// �̵� �� ���ο� ���� �ִϸ��̼� �Ķ���� ����
    /// </summary>
    protected void HandleAnimation()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }




    /// <summary>
    /// ĳ���� �¿� ���� (flipX) ó��
    /// ���⿡�� �������� ����
    /// </summary>
    protected void HeadSpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            if (headPivot != null)
            {
                headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
            }
    }


    /// <summary>
    /// ���� Ű �Է� ���� �� ��Ÿ�� Ȯ�� �� ���� ����
    /// </summary>
    protected void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey))
        {

            if (currentJumpCount < maxJumpCount)
            {
                //Vector3 v = rb.velocity;
                //v.y +=  jumpPower;
                //rb.velocity = v;
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Y �ӵ� �ʱ�ȭ
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                currentJumpCount++;
                Anim.SetJump(true);
                Debug.Log($"���� {currentJumpCount}/{maxJumpCount}");
            }


        }
    }


}

