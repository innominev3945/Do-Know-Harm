using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gauze_Hit_Box_Collider_Script : MonoBehaviour
{
    bool collideWithHitBox;
    float collisionSourceX;
    float collisionSourceY;

    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        collideWithHitBox = false;
        collisionSourceX = 0;
        collisionSourceY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Collison");
        if (collision.gameObject.tag == "Hit Box (Gauze)")
        {
            Debug.Log("Collison with hit box (gauze)");

            collideWithHitBox = true;
            collisionSourceX = collision.gameObject.transform.position.x;
            collisionSourceY = collision.gameObject.transform.position.y;
        }
    }

    public bool insideHitBox()
    {
        return collideWithHitBox;
    }

    public float getCollisionX()
    {
        return collisionSourceX;
    }

    public float getCollisionY()
    {
        return collisionSourceY;
    }
}
