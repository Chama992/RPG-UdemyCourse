using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material originalMat;
    [SerializeField]private Material hitFXMat;
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMat = spriteRenderer.material;
    }
    public IEnumerator FlashFX()
    {
        spriteRenderer.material = hitFXMat;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.material = originalMat;
    }
}
