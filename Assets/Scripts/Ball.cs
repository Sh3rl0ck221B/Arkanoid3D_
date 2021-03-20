using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 velocity;
    public float maxX;
    public float maxZ;
    private AudioSource audioSource;
    public AudioClip gameOver;
    public bool isSlowedDown;

    void Start()
    {
        velocity = new Vector3(0, 0, maxZ);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Paddle"))
        {
            audioSource.Play();
            var maxDist = other.transform.localScale.x * 1f * 0.5f + transform.localScale.x * 1f * 0.5f;
            var dist = transform.position.x - other.transform.position.x;
            var nDist = dist / maxDist;
            
            velocity = new Vector3(nDist * maxX, 0, -velocity.z);
        }
        else if (other.CompareTag("Wall"))
        {
            audioSource.Play();
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        }
        else if (other.CompareTag("TopWall"))
        {
            audioSource.Play();
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        else if (other.CompareTag("BottomWall"))
        {
            transform.position = new Vector3(0, 0, -16);
            velocity = new Vector3(0, 0, maxZ);
            
            Paddle paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
            
            paddle.ResetPlayer();
            paddle.AdaptLive();
            
            if (paddle.player.lives == 0)
            {
                GameOver(paddle);
            }
        }
        else if (other.CompareTag("Brick"))
        {
            audioSource.Play();
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
    }
    
    private void GameOver(Paddle paddle)
    {
        GameObject backgroundmusic = GameObject.Find("AudioPlayer");
        Destroy(backgroundmusic);
                
        GameObject pointsObject = GameObject.Find("Points");
        TextMesh mesh = pointsObject.GetComponent<TextMesh>();
        mesh.text = Convert.ToString("Game Over | Punktzahl:" + paddle.player.points);
        velocity = new Vector3(0, 0, 0);
        audioSource.PlayOneShot(gameOver);
    }
    
    private void WonGame(Paddle paddle)
    {
        GameObject backgroundmusic = GameObject.Find("AudioPlayer");
        Destroy(backgroundmusic);
                
        GameObject pointsObject = GameObject.Find("Points");
        TextMesh mesh = pointsObject.GetComponent<TextMesh>();
        mesh.text = Convert.ToString("Game Over | Punktzahl:" + paddle.player.points);
        velocity = new Vector3(0, 0, 0);
        audioSource.PlayOneShot(gameOver);
    }

    public void slowDown()
    {
        velocity *= 0.5f;
        isSlowedDown = true;
        Invoke(nameof(speedUp), 10);
    }

    private void speedUp()
    {
        velocity *= 2f;
        isSlowedDown = false;        
    }
}