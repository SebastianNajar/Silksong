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
            SoundManager.instance.PlaySoundClip(slashClip, transform, 1);
            slashTimer = cooldown;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dashTimer = 0;
            presses++;
        }

        if(dashTimer > 0.5)
        {
            presses = 0;
        }

        if(Input.GetKeyDown(KeyCode.D) && dashTimer < 0.5f && presses == 2)
        {
            Debug.Log("Dash");
            int dir = controller.GetDirection();
            controller.RB.linearVelocity = new(dir * dashSpeed, controller.RB.linearVelocityY);
        }
    }
}
