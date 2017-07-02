using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IsometricSpriteRenderer : MonoBehaviour {

    const int ORDER_SCALE = -5;

    void Update ()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sortingOrder = (int)(transform.position.y * ORDER_SCALE);
	}
}
