using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerController controller;
    public Animator slashAnimator;
    public float cooldown;
    private float timer;
    public AudioClip slashClip;

    private void Start()
    {
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer < 0)
        {
            slashAnimator.SetTrigger("attack");
            SoundManager.instance.PlaySoundClip(slashClip, transform, 1);
            timer = cooldown;
        }
    }
}
