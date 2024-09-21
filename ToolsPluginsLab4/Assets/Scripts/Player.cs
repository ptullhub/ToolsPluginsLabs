using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject laserPrefab;

    private float speed = 6f;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;
    public bool canShoot = true;

    private GameManager manager;
    private PlayerInput input;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance.GetComponent<GameManager>();
        input = manager.GetComponent<PlayerInput>();
    }

    void Update()
    {
        
    }
    public void Movement()
    {
        var move = input.actions["Move"].ReadValue<Vector2>();
        transform.Translate(new Vector3(move.x, move.y, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    public void Shooting()
    {
        if (canShoot)
        {
            Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            canShoot = false;
            StartCoroutine("Cooldown");
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.75f);
        canShoot = true;
    }
}
