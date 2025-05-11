using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��� ĳ����(�÷��̾�/���� ��)�� ���� ����� ����ϴ� �߻� ��Ʈ�ѷ�
/// �̵�, ü�� ó��, ��������Ʈ ���� �� �⺻ �ൿ ����
/// </summary>


public abstract class Characterbase : MonoBehaviour
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
    [SerializeField] protected CHAR enumChar;
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


    [Header("�̸�")]
    [SerializeField] protected string Name = string.Empty;

    [Header("����Ű ����")]
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode jumpKey;
    [SerializeField] protected KeyCode SkillKey;
    [SerializeField] protected int moveSpeed = 5;

    [Header("Ⱦ��ũ�� ���� ��� ���� �ý���")]
    [SerializeField] protected Transform groundCheck;      // �ٴ� ���� ��ġ
    [SerializeField] protected float groundRayRange = 0.1f; // �ٴ� üũ ����(raycast����)
    [SerializeField] protected LayerMask groundLayer;      // �ٴ� ���̾�
    [SerializeField] protected float jumpPower = 5f;       // ���� ��
    [SerializeField] protected int maxJumpCount = 2;//�������� �ִ� Ƚ��
    protected int currentJumpCount = 0;//���� ������ ���� �� ����

    [Header("����� �� ��Ž��")]
    [SerializeField] protected Transform WallCheck;//�� ���� ��ġ
    [SerializeField] protected float wallRayRange = 0.1f; //��  üũ ����(raycast����)
    [SerializeField] protected LayerMask wallLayer;//�� ���̾�

    [Header("�ı����ɿ�����Ʈ")]
    [SerializeField] protected GameObject DestroyAbleObj;//������ �������� �ı��Ǵ� ������Ʈ 
    [SerializeField] protected Transform DestroyCheck;//�ı� ����
    [SerializeField] protected float DestroyRange = 0.1f; //�ı�  üũ ����(raycast����)
    [SerializeField] protected LayerMask DestroyLayer;//�ı��� ������Ʈ ���̾�
    [Header("��� �ǹ�")]
    [SerializeField] protected Transform headPivot;
    protected GameObject helmet;

    [Header("��ų ��Ÿ��")]
    [SerializeField] protected float SkillCoolTime = 1f;
    /// <summary> �ʱ�ȭ: ������ٵ�, ��������Ʈ ã�� ���� �ʱ�ȭ </summary>
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Anim = GetComponentInChildren<MyAnimationController>();
    }


    //��ų�� ����
    protected bool SkillReq = false; //��ų ��û�� ��
    protected bool skillReady = true;//��ų ��Ÿ�ӿ� ��
    protected virtual void InstantSkillCall() // ��ų �Է� ���� �ż���
    {
        if (Input.GetKeyDown(SkillKey) && skillReady)
        {

            StartCoroutine(SkillCoolDown(SkillCoolTime));//��Ÿ�� ���� 
            SkillReq = true;
            Debug.Log($"{Name}��ų Ű �Է�");
        }


    }


    protected virtual void InstantSkillActivate()      //�ܹ߼� ��ųȣ��
    {
        if (SkillReq)
        {
            SkillReq = false;
            InstantSkill();

            Debug.Log($"{Name}��ų�ߵ�");
        }

    }
    protected virtual void InstantSkill() // �ܹ߼� ��ų
    {


    }
    protected virtual void ToggleSkillCall()//����� ��ų �Է� ����
    {
        if (Input.GetKeyDown(SkillKey) && !isToggled && IsWallClimb())
        {
            StartCoroutine(SkillCoolDown(SkillCoolTime));//��Ÿ�� ���� 
            isToggled = true;
            ToggleSkillOn();
            Debug.Log($"{Name}��ų Ű �Է�");
            Debug.Log($"{Name}��۽�ų�ߵ�");
        }
        else if (Input.GetKeyDown(SkillKey) && isToggled)
        {

            isToggled = false;
            ToggleSkillOff();
            Debug.Log($"{Name}��ų Ű �Է�");
            Debug.Log($"{Name}��۽�ų����");
        }
    }

    protected bool isToggled = false;
   

    protected virtual void ToggleSkillOn()
    { }

    protected virtual void ToggleSkillOff()
    { }


    protected virtual IEnumerator SkillCoolDown(float cooldown)//��ų ��Ÿ�ӿ� �ڷ�ƾ
    {
        Debug.Log("��Ÿ�� �ߵ�");
        cooldown = SkillCoolTime;
        skillReady = false;
        yield return new WaitForSeconds(cooldown);
        skillReady = true;
        Debug.Log("��Ÿ�� ��");
    }
    protected virtual void HandleSkillAnim()
    {
        bool isSkill = SkillReq;
        Anim.SetSkill(isSkill);

    }




    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    protected virtual void MoveCall() //�̵�Ű �Է� ���� 
    {
        /*moveX = Input.GetAxisRaw("Horizontal");*///���� �ٸ� �� ĳ���͸� �����ؾ� �ؼ� �Է� ����� �ٲ� 
        moveX = 0;

        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        moveInput = new Vector2(moveX, moveY).normalized;//�Է°��� ���� ���� ���ϱ�
    }
    /// <summary> �̵� ó��: �ӵ� ���� </summary>
    protected virtual void MoveActivate(Vector2 input) //�̵� ȣ��� �ż��� 
    {

        Vector2 velocity = rb.velocity;
        velocity.x = input.x * moveSpeed;// �̵��ӵ��� ���⿡ �����ֱ�

        rb.velocity = velocity;


        // �¿� ���� ó��
        /*     if (input.x != 0)
                 spriteRenderer.flipX = input.x < 0; */

    }
    /// <summary>
    /// �̵� �� ���ο� ���� �ִϸ��̼� �Ķ���� ����
    /// </summary>
    protected virtual void HandleMoveAnim()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }
    protected virtual void SpriteFlip() // �¿���� ���� �ż���
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
    /// ĳ���� �¿� ���� (flipX) ó��
    /// ���⿡�� �������� ����
    /// </summary>


    protected bool jumpReq = false;//���� ���� ���� ��
    /// <summary>
    /// ���� Ű �Է� ���� �� ��Ÿ�� Ȯ�� �� ���� ����
    /// </summary>
    protected void JumpCall()//����Ű �Է� ����
    {
        if ((Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount) && !isClimb)//Ű �Է�, ���� Ƚ���� �ִ� �������� ���� ��,��Ÿ�� ���� �ƴ� �� 
        {
            if (currentJumpCount == 0 && !IsGrounded())// �ܼ� �����߿� ���� ����
            { return; }
            jumpReq = true;
        }
    }
    protected virtual void JumpAtivate()//���� ȣ��
    {
        if (jumpReq)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Y �ӵ� �ʱ�ȭ
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);//up�������� �����Ŀ���ŭ ���޽� ��������

            if (currentJumpCount < maxJumpCount)//���� Ƚ�� ���� �� ������ �α� 
            {
                currentJumpCount++;
                Debug.Log($"{Name}���� {currentJumpCount}/{maxJumpCount}");
            }
            jumpReq = false;
        }


    }

    protected virtual void HandleJumpAnim()
    {
      
            bool isJump = currentJumpCount > 0 || (currentJumpCount == 0 && !IsGrounded());
            Anim.SetJump(isJump);
        
    }

    protected void CheckLanding()//���� �����ߴ��� �����ϴ� �ż���
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 && IsGrounded())
        {
            currentJumpCount = 0;
            Anim.SetJump(false);

            Debug.Log($"{Name}����!");
        }
    }

    //���� ���� ������ �Ұ� �ʿ� �� ���
    protected bool IsGrounded()//���� �پ��ִ��� ������ �Ұ�
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRayRange, groundLayer);
        return hit.collider != null;
    }
    //�� ������ �Ұ�
    protected bool IsWallClimb()//��Ÿ�� ������ �������� ������ �� ��
    {
        if (!spriteRenderer.flipX)
        {
            RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.right, wallRayRange, wallLayer);
            return hit.collider != null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(WallCheck.position, Vector2.left, wallRayRange, wallLayer);
            return hit.collider != null;
        }


    }
    protected void IsNotClimb() //���� �ʰ��Ͽ� �����϶� ��Ŭ ��ų ����
    { 
        if(isToggled && !IsWallClimb())
        {
         
            ToggleSkillOff();
               
        }
    }
    protected bool isClimb = false; //��Ÿ�� ������ Ȯ���� ��
    protected void OnDrawGizmosSelected() //raycast ����� �ð�ȭ�ϱ� ���� �ż���
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundRayRange);
        if (WallCheck == null || spriteRenderer == null) return;

        if (!spriteRenderer.flipX)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(WallCheck.position, WallCheck.position + Vector3.right * wallRayRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(WallCheck.position, WallCheck.position + Vector3.left * wallRayRange);
        }


    }

    protected bool IsDestroy()//�ı����� ���� �� ��
    {
        if (DestroyCheck == null) return false;
        else
        {
            if (!spriteRenderer.flipX)
            {
                RaycastHit2D hit = Physics2D.Raycast(DestroyCheck.position, Vector2.right, DestroyRange, DestroyLayer);
                return hit.collider != null;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(DestroyCheck.position, Vector2.left, DestroyRange, DestroyLayer);
                return hit.collider != null;
            }
        }

    }

}

