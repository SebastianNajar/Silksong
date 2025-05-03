using System.Collections;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float timer = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            timer = Random.Range(0f, 15f);
        }

        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.06f);

        spriteRenderer.enabled = true;
    }
}
