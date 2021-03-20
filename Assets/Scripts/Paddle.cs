using System;
using DefaultNamespace;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    public Player player;
    public float speed;
    public Transform playArea;
    private float _maxX;
    public GameObject myPrefab;
    public bool isElongated;
    private float _paddleSize;
    private float _playAreaSize;

    // Start is called before the first frame update
    void Start()
    {
        player = new Player();
        
        _playAreaSize = playArea.localScale.x * 10;
        _paddleSize = transform.localScale.x * 1;
        _maxX = 0.5f * _playAreaSize - 0.5f * _paddleSize;
        
        showLife();
    }


    void Update()
    {
        float dir = Input.GetAxis("Horizontal");
        float newX = transform.position.x + Time.deltaTime * speed * dir;
        float clampedX = Mathf.Clamp(newX, -_maxX, _maxX);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
    
    private void showLife()
    {
        int posx = 4;
        for (int i = player.lives; i >= 1; i--)
        {
            GameObject liveObject = Instantiate(myPrefab, new Vector3(posx, 0, 14), Quaternion.identity);
            liveObject.name = "Leben " + i;
            posx += 3;
        }
    }

    public void elongate()
    {
        ScalePaddle(2f);
        isElongated = true;
        Invoke(nameof(Abbrivate), 10);
    }

    private void Abbrivate()
    {
        ScalePaddle(0.5f);
        isElongated = false;
    }

    private void ScalePaddle(float scale)
    {
        _paddleSize = transform.localScale.x * scale;
        _maxX = 0.5f * _playAreaSize - 0.5f * _paddleSize;
        
        transform.localScale = new Vector3(_paddleSize,
            transform.localScale.y, transform.localScale.z);
    }
    
    public void ResetPlayer()
    {
        transform.localPosition = new Vector3(0, 0, transform.position.z);
        
    }
    public void AdaptLive()
    {
        GameObject live = GameObject.Find("Leben " + player.lives);
        player.lives -= 1;
        Destroy(live);
    }

    public void AddPoints(int points)
    { 
        player.points += points;
        GameObject.Find("Points").GetComponent<TextMesh>().text = Convert.ToString(player.points);
    }
    
}