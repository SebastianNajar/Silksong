using System.Collections;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    private Color color;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
    }

    private void Update()
    {
        if (timer <= 0f)
        {
            timer = Random.Range(0f, 12f);
        }

        timer -= Time.deltaTime;

        if(timer < 0f)
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        color.a = 0.3f; 
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.06f);

        color.a = 1f;
        spriteRenderer.color = color;
    }
}
