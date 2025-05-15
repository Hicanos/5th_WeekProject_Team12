using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD

using Unity.Mathematics;
=======
>>>>>>> trymerge
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// ��� ĳ����(�÷��̾�/���� ��)�� ���� ����� ����ϴ� �߻� ��Ʈ�ѷ�
/// �̵�, ü�� ó��, ��������Ʈ ���� �� �⺻ �ൿ ����
/// </summary>
public enum PLAYERSTATE //���¸ӽ� enum (���� �̱���)
{
    IDLE,
    MOVE,
    JUMP,
    DASH,
    WALL
}
//TilemapCollider�� CompositeCollider �Բ� ���� �߻��ϴ� ���׶����� �÷��װ� �����ϰ� ���͹��Ƚ��ϴ�. (velocity.x �� �����ص� y�� ���� ���� �ٲ�ϴ�. )
//���� �ӽ� �����ؾ߰ڽ��ϴ�. 

public abstract class Characterbase : MonoBehaviour
{
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
   
    
    protected PLAYERSTATE currentState;
    //���º��� �Լ� �̱���
    protected void ChangeState(PLAYERSTATE newState)
    {
        if (currentState == newState) return;

        Debug.Log($"���� ����: {currentState} -> {newState}");
        currentState = newState;
    }

    //���� ��Ÿ�� enum
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
                skillKey = KeyCode.DownArrow;
                break;

            case CHAR.CAT:
                leftKey = KeyCode.A;
                rightKey = KeyCode.D;
                jumpKey = KeyCode.W;
                skillKey = KeyCode.S;
                break;
        }

    }

    /*---------------------------------------ĳ���� ���� ����---------------------------------------*/
    
    // �Է°� ����
    protected float moveX;
    protected float moveY;
    protected Vector2 moveInput;
  
    [Header("�̸�")]
    [SerializeField] protected string Name = string.Empty;

    [Header("����Ű ����")]
    [SerializeField] protected KeyCode leftKey;
    [SerializeField] protected KeyCode rightKey;
    [SerializeField] protected KeyCode jumpKey;
    [SerializeField] protected KeyCode skillKey;
    [SerializeField] protected int moveSpeed = 5;

    [Header("Ⱦ��ũ�� ���� ��� ���� �ý���")]
    [SerializeField] protected Transform groundCheck;      // �ٴ� ���� ��ġ
    [SerializeField] protected float groundRayRange = 0.1f; // �ٴ� üũ ����(raycast����)
    [SerializeField] protected LayerMask groundLayer;      // �ٴ� ���̾�
    [SerializeField] protected float jumpPower = 5f;       // ���� ��
    [SerializeField] protected int maxJumpCount = 2;//�������� �ִ� Ƚ��
    protected int currentJumpCount = 0;//���� ������ ���� �� ����

    [Header("��� �ǹ�")]
    [SerializeField] protected Transform headPivot;
    protected GameObject helmet;

    [Header("��ų ��Ÿ��")]
    [SerializeField] protected float skillCoolTime = 1f;
    
    /*---------------------------------------�ܹ߼� ��ų ����---------------------------------------*/
    //��ų ��û�� ��
    protected bool skillReq = false;

    // ��ų �Է� ���� �ż���
    protected virtual void InstantSkillCall()
    {
        if (Input.GetKeyDown(skillKey) && skillReady)
        {
            //��Ÿ�� ���� 
            StartCoroutine(SkillCoolDown(skillCoolTime));
            skillReq = true;
            Debug.Log($"{Name}��ų Ű �Է�");
        }
    }

    //�ܹ߼� ��ųȣ��
    protected virtual void InstantSkillActivate()
    {
        if (skillReq)
        {
            skillReq = false;
            InstantSkill();

            Debug.Log($"{Name}��ų�ߵ�");
        }
    }
    // �����ǿ� �ܹ߼� ��ų �Լ�
    protected virtual void InstantSkill() { }

    /*---------------------------------------����� ��ų ����---------------------------------------*/
    //��۽�ų�� �Ұ�
    protected bool isToggled = false;

    //����� ��ų �Է� ����
    protected virtual void ToggleSkillCall()
    {
        if (Input.GetKeyDown(skillKey) && !isToggled)
        {
            //��Ÿ�� ���� 
            StartCoroutine(SkillCoolDown(skillCoolTime));
            isToggled = true;
            ToggleSkillOn();
            Debug.Log($"{Name}��ų Ű �Է�");
            Debug.Log($"{Name}��۽�ų�ߵ�");
        }
        else if (Input.GetKeyDown(skillKey) && isToggled)
        {
            isToggled = false;
            ToggleSkillOff();
            Debug.Log($"{Name}��ų Ű �Է�");
            Debug.Log($"{Name}��۽�ų����");
        }
    }

    //�����ǿ� ��� ��ų On�Լ�
    protected virtual void ToggleSkillOn() { }

    //�����ǿ� ��� ��ų Off�Լ�
    protected virtual void ToggleSkillOff() { }

    /*---------------------------------------��Ÿ�� �ڷ�ƾ---------------------------------------*/
    //��ų ��Ÿ�ӿ� ��
    protected bool skillReady = true;

    //��ų ��Ÿ�ӿ� �ڷ�ƾ
    protected virtual IEnumerator SkillCoolDown(float cooldown)
    {
        Debug.Log("��Ÿ�� �ߵ�");
        cooldown = skillCoolTime;
        skillReady = false;
        yield return new WaitForSeconds(cooldown);
        skillReady = true;
        Debug.Log("��Ÿ�� ��");
    }
    /*---------------------------------------�ִϸ��̼ǿ� �Լ�---------------------------------------*/
    protected virtual void HandleJumpAnim()
    {
        bool isJump = currentJumpCount > 0 || (currentJumpCount == 0 && !IsGrounded());
        Anim.SetJump(isJump);
    }
    protected virtual void HandleSkillAnim()
    {
        bool isSkill = skillReq;
        Anim.SetSkill(isSkill);
    }

    protected virtual void HandleCrashAnim()
    {
        Anim.SetCrash();
    }
    protected virtual void HandleMoveAnim()
    {
        bool isMoving = moveX != 0;
        Anim.SetMove(isMoving);
    }
    /// <summary>
    /// ĳ���� �¿� ���� (flipX) ó��
    /// ���⿡�� �������� ����
    /// </summary>
    protected virtual void SpriteFlip()
    {
        if (moveX != 0)
            Anim.SetFlip(moveX < 0);
        if (moveX != 0)
            if (headPivot != null)
            {
                headPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f);
            }
    }
    /*---------------------------------------�����Ӱ��� �Լ�---------------------------------------*/
    //���� ���� ���� ��
    protected bool jumpReq = false;
    //���� �پ��ִ��� ������ �ҹ�ȯ �Լ�
    protected virtual bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundRayRange, groundLayer);
        return hit.collider != null;
    }
    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    protected virtual void MoveCall()
    {
        moveX = 0;
        if (Input.GetKey(leftKey)) moveX -= 1;
        if (Input.GetKey(rightKey)) moveX += 1;
        //�Է°��� ���� ���� ���ϱ�
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    /// <summary> �̵� ó��: �ӵ� ���� </summary>
    protected virtual void MoveActivate(Vector2 input)
    {
        Vector2 velocity = rb.velocity;
        // �̵��ӵ��� ���⿡ �����ֱ�
        velocity.x = input.x * moveSpeed;
        rb.velocity = velocity;
    }

    /// <summary>
    /// ���� Ű �Է� ���� �� ��Ÿ�� Ȯ�� �� ���� ����
    /// </summary>
    protected virtual void JumpCall()//����Ű �Է� ����
    {   //Ű �Է�, ���� Ƚ���� �ִ� �������� ���� ��,��Ÿ�� ���� �ƴ� �� 
        if ((Input.GetKeyDown(jumpKey) && currentJumpCount < maxJumpCount))
        {
            // �ܼ� �����߿� ���� ����
            if (currentJumpCount == 0 && !IsGrounded())
            { return; }

            jumpReq = true;
        }
    }

    //���� ȣ��
    protected virtual void JumpAtivate()
    {
        if (jumpReq)
        {   // Y �ӵ� �ʱ�ȭ
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            //up�������� �����Ŀ���ŭ ���޽� ��������
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            if (currentJumpCount < maxJumpCount)
            {
                currentJumpCount++;
                //���� Ƚ�� ���� �� ������ �α� 
                Debug.Log($"{Name}���� {currentJumpCount}/{maxJumpCount}");
            }
            jumpReq = false;
        }
    }

    //���� �����ߴ��� �����ϴ� �ż���
    protected void CheckLanding()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0 && IsGrounded())
        {
            currentJumpCount = 0;
            Anim.SetJump(false);

            Debug.Log($"{Name}����!");
        }
    }
    /*---------------------------------------ect.---------------------------------------*/

    //raycast ����� �ð�ȭ�ϱ� ���� �ż���
    protected virtual void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundRayRange);

    }
}

