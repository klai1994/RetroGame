using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDirectable {

    [SerializeField] float speed = 3f;
    [SerializeField] Projectile projectilePrefab;

    private Rigidbody2D rbody;
    private float threshold = 0.2f;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        directionMagnitudes = new Dictionary<Directions, Vector2>();
        directionMagnitudes.Add(Directions.Left, Vector2.left);
        directionMagnitudes.Add(Directions.Right, Vector2.right);
        directionMagnitudes.Add(Directions.Down, Vector2.down);
        directionMagnitudes.Add(Directions.Up, Vector2.up);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerShoot();
        }

        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rbody.MovePosition(rbody.position + moveDirection * speed * Time.deltaTime);
        GetDirection();
    }

    public Directions GetDirection()
    {
        if (Input.GetAxisRaw("Horizontal") < -threshold)
        {
            return direction = Directions.Left;
        }
        else if (Input.GetAxisRaw("Horizontal") > threshold)
        {
            return direction = Directions.Right;
        }
        else if (Input.GetAxisRaw("Vertical") < -threshold)
        {
            return direction = Directions.Down;
        }
        else if (Input.GetAxisRaw("Vertical") > threshold)
        {
            return direction = Directions.Up;
        }
        else
        {
            return direction;
        }
    }

    void PlayerShoot()
    {
        Projectile projectile;
        projectile = Instantiate(projectilePrefab, (transform.position + 
            (Vector3)directionMagnitudes[direction]), Quaternion.identity);

        projectile.damage = damage;
        projectile.GetComponent<Rigidbody2D>().velocity += 
            directionMagnitudes[direction] * projectile.projectileSpeed;
    }
}
