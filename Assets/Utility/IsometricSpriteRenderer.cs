using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IsometricSpriteRenderer : MonoBehaviour {

    void Update ()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sortingOrder = (int)(transform.position.y * -10);
	}
}
