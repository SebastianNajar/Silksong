using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerController controller;

    //Slash
    public Animator slashAnimator;
    public float cooldown;
    private float slashTimer;
    public AudioClip slashClip;

    //Dash
    private bool hasDash = false;
    public float dashTimer = 0.5f;
    public float dashSpeed;
    public int presses = 0;

    private void Start()
    {
        slashTimer = 0;
    }

    void Update()
    {
        slashTimer -= Time.deltaTime;
        dashTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && slashTimer < 0)
        {
            slashAnimator.SetTrigger("attack");
            SoundManager.instance.PlaySoundClip(slashClip, transform, 0.5f);
            slashTimer = cooldown;
        }

        if (hasDash)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
                dashTimer = 0;
                presses++;
            }

            if (dashTimer > 0.5)
            {
                presses = 0;
            }

            if (Input.GetKeyDown(KeyCode.D) && dashTimer < 0.3f && presses == 2 && controller.facingRight ||
                Input.GetKeyDown(KeyCode.A) && dashTimer < 0.3f && presses == 2 && !controller.facingRight)
            {
                StartCoroutine(Dash());
            }
        }
    }

    public void ObtainDash()
    {
        hasDash = true;
    }

    IEnumerator Dash()
    {
        controller.animator.speed = 1;
        controller.animator.SetTrigger("dash");

        int dir = controller.GetDirection();
        controller.canMove = false;
        controller.RB.gravityScale = 0;
        controller.hangCounter = 0;
        controller.RB.linearVelocity = new(dir * dashSpeed, 0);

        yield return new WaitForSeconds(0.1f);

        controller.canMove = true;
        controller.RB.gravityScale = 1;
    }
}
