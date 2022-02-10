using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public GameObject gameOverScreen;
    public bool isDead;
    public int Score;
    public Text text;
    private  Vector2 _direction = Vector2.right;
    
    private List<Transform> _segments;

    public Transform segmentPrefabs;

    private void Start()
    {
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }
    private void Update()
    {
        if(!isDead)
        {
            text.text = Score.ToString();

            if (Input.GetKeyDown(KeyCode.W))
            {
                _direction = Vector2.up;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _direction = Vector2.down;
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                _direction = Vector2.left;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _direction = Vector2.right;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void FixedUpdate()
    {

        for(int i= _segments.Count -1; i>0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        
        this.transform.position = new Vector3(
        
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f

        );
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            Score++;
        }

        if (other.tag == "Wall")
        {
            Die();
        }
        if (other.tag == "Segment")
        {
            Die();
        }
    }


    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefabs);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    void Die()
    {
        isDead = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

}
