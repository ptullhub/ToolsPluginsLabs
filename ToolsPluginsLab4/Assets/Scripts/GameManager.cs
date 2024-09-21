using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioManager audioManager;

    public GameObject playerPrefab;
    public GameObject meteorPrefab;
    public GameObject bigMeteorPrefab;
    
    public bool gameOver = false;

    public int meteorCount = 0;

    public CinemachineVirtualCamera vCam;
    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera activeCam;

    private GameObject player;
    private Player playerScript;

    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        playerScript = player.GetComponent<Player>();

        vCam.Follow = player.transform;
        activeCam = vCam;

        InvokeRepeating("SpawnMeteor", 1f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && player != null)
        {
            playerScript.Movement();
        }
    }

    public void PlayerShoot(InputAction.CallbackContext context)
    {
        if (player != null && context.started && playerScript.canShoot)
        {
            audioManager.audioSource.PlayOneShot(audioManager.laserSound);
            playerScript.Shooting();
        }
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            isMoving = true;
        }
        else if (context.canceled)
        {
            isMoving = false;
        }
    }
    public void Restart(InputAction.CallbackContext context)
    {
        if (gameOver)
        {
            SceneManager.LoadScene("Week5Lab");
        }
    }
    void SpawnMeteor()
    {
        Instantiate(meteorPrefab, new Vector3(UnityEngine.Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
    }

    public void BigMeteor()
    {
        if (meteorCount == 5)
        {
            meteorCount = 0;
            Instantiate(bigMeteorPrefab, new Vector3(UnityEngine.Random.Range(-8, 8), 7.5f, 0), Quaternion.identity);
            ZoomOut();
        }
    }

    private void ZoomOut()
    {
        vCam.Priority = 0;
        vCam2.Follow = player.transform;
        activeCam = vCam2;
    }

    public void ZoomIn()
    {
        vCam.Priority = 20;
        activeCam = vCam;
    }
}
