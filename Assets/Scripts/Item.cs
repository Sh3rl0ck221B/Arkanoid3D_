using System;
using DefaultNamespace;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Vector3 _velocity;
    private ItemType _itemType;

    private void Start()
    {
        int number = UnityEngine.Random.Range(0, 2);
        _itemType = number == 1 ? ItemType.Waverider : ItemType.ElongatedMan;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // change Color
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = DetermineColour();
    }

    void Update()
    {
        float newPositionZ = transform.position.z - 0.05f;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPositionZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            determineImpact();
            Destroy(gameObject);
        }
    }

    private void determineImpact()
    {
        switch (_itemType)
        {
            case ItemType.Waverider:
                slowDownBall();
                break;

            case ItemType.ElongatedMan:
                elongatePaddle();
                break;
        }
    }

    private void slowDownBall()
    {
        Ball ball = GameObject.Find("Ball").GetComponent<Ball>();
        if (!ball.isSlowedDown)
        {
            ball.slowDown();
        }
    }

    private void elongatePaddle()
    {
        Paddle paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
        if (!paddle.isElongated)
        {
            paddle.elongate();
        }
    }

    private Color DetermineColour()
    {
        switch (_itemType)
        {
            case ItemType.Waverider: return Color.red;
            case ItemType.ElongatedMan: return Color.yellow;
        }

        return Color.black;
    }
}