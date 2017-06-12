using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    public enum Orientation
    {
        Right, Left
    }

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody rigidbody;
    private float distanceToGround;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    void Update()
    {
        // ジャンプ状態の時に地面の上にいるようになったらジャンプ状態ではなくなる。
        if (animator.GetBool("Jumping"))
        {
            if (IsGrounded())
            {
                animator.SetBool("Jumping", false);
            }
        }

        // 走っているかどうかを設定
        var speed = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z).sqrMagnitude;
        animator.SetBool("Running", speed > 0.1);
    }
    public void SetOrientation(Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.Left:
                spriteRenderer.flipX = true;
                break;
            case Orientation.Right:
                spriteRenderer.flipX = false;
                break;
        }
    }
    // 移動する。Updateから毎フレーム呼び出す。
    public void Move(Vector2 vec)
    {
        rigidbody.velocity = new Vector3(vec.x, rigidbody.velocity.y, vec.y);
    }

    // ジャンプする
    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.AddForce(new Vector3(0, rigidbody.mass * 3, 0), ForceMode.Impulse);
            SendMessage("OnJump");
            animator.SetBool("Jumping", true);
        }
    }

    // 地面の上にいるかどうか
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
    }
}
