using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour {
    [SerializeField]
    private float blinkSpeed = 10f;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float MAX_OPACITY = 0.75f;
    [SerializeField]
    private float MIN_OPACITY = 0.1f;

    private float target;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = MAX_OPACITY;
    }

    private void Update() {
        Color color = spriteRenderer.color;
        color.a = Mathf.Lerp(color.a, target, blinkSpeed * Time.deltaTime);
        spriteRenderer.color = color;

        if (Mathf.Abs(color.a - target) <= 0.05f) target = target == MAX_OPACITY ? MIN_OPACITY : MAX_OPACITY;
    }


}
