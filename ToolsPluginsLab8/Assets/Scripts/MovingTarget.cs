using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    public float size = 0;
    public float speed = 0;
    public int scoreValue = 0;

    public static event Action<int> OnTargetDestroyed;

    private bool movingRight;
    void Start()
    {
        int num = UnityEngine.Random.Range(0,2);
        if (num == 0 )
        {
            movingRight = true;
        }
        else if (num == 1)
        {
            movingRight = false;
        }
    }
    void Update()
    {
        Move();
    }

    public void InitializeValues(float _size, float _speed, int _scoreValue)
    {
        size = _size;
        speed = _speed;
        scoreValue = _scoreValue;

        gameObject.transform.localScale = new Vector3(size, size, 1);
    }

    private void Move()
    {
       if (movingRight)
       {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
       }
       else
       {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
       }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            OnTargetDestroyed?.Invoke(scoreValue);
            Destroy(this.gameObject);
        }
        Flip();
    }

    private void Flip()
    {
        if (movingRight)
        {
            movingRight = false;
        }
        else if (!movingRight)
        {
            movingRight = true;
        }
    }
}
