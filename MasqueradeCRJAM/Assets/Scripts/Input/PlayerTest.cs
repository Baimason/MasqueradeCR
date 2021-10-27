using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Color baseColor = Color.white;
    [SerializeField] private Color jumpColor;
    [SerializeField] private Color specialColor;

    private int hash_BaseColor;
    private MaterialPropertyBlock mpb;

    void Start()
    {
        mpb = new MaterialPropertyBlock();
        playerInput.jumpAction += OnJump;
        playerInput.specialAction += OnSpecial;
        hash_BaseColor = Shader.PropertyToID("_Color");
    }

    void OnJump()
    {
        StopAllCoroutines();
        StartCoroutine(FadeColor(jumpColor));
    }
    void OnSpecial()
    {
        StopAllCoroutines();
        StartCoroutine(FadeColor(specialColor));
    }

    IEnumerator FadeColor(Color c)
    {
        renderer.GetPropertyBlock(mpb);
        mpb.SetColor(hash_BaseColor, c);
        renderer.SetPropertyBlock(mpb);

        float f = 0;
        while (f < 1)
        {
            Color cc = Color.Lerp(c, baseColor, f);
            mpb.SetColor(hash_BaseColor, cc);
            renderer.SetPropertyBlock(mpb);

            f += Time.deltaTime;
            yield return null;
        }

        mpb.SetColor(hash_BaseColor, baseColor);
        renderer.SetPropertyBlock(mpb);
    }

    void FixedUpdate()
    {
        transform.position += (Vector3)playerInput.MovementVector * Time.fixedDeltaTime * moveSpeed;
    }
}
