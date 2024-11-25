using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDisplay : MonoBehaviour
{
    private BaseCharacter baseCharacter;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        baseCharacter = GetComponentInParent<BaseCharacter>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.isVisible && !baseCharacter.isGrounded){
            spriteRenderer.enabled = false;
        }
        else if(!spriteRenderer.isVisible && baseCharacter.isGrounded){
            spriteRenderer.enabled = true;
        }
    }
}
