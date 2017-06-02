using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public int zoom = 1;
    // Screen height divded by pixels per unit sprites for consistent camera size
    private float worldUnitHeight = Screen.height / 16.0f;
	private Transform target;

    public Transform Target
    {
        get; set;
    }

    // Use this for initialization
    void Start () {
        // Find camera, player, and set default zoom
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	public float ChangeZoom(int zoom)
    {
        switch (zoom)
        {
            case 0:
                return worldUnitHeight / 8;
            case 1:
                return worldUnitHeight / 4;
            default:
                return worldUnitHeight / 2;
        }
    }

    // Update is called once per frame
    void Update () {

        if (target) {
            transform.position = target.transform.position + new Vector3(0.0f, 0.0f, -10.0f);
            Camera.main.orthographicSize = ChangeZoom(zoom);
        }
    }
}
