using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] LayerMask blockRaycast;

    float lookRotationSpeed = 8f;
    float timer = 0f;
    [SerializeField] float mouseHoldTime;

    public delegate void PlayerEvent(bool state);
    public static PlayerEvent DisablePlayerController;
    public static PlayerEvent EnablePlayerController;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        Debug.Log("Is on Navmesh: " + agent.isOnNavMesh);
        if (agent.isOnNavMesh)
        {
            agent.isStopped = false;
            agent.Warp(transform.position);
        }
    }


    void Update()
    {

        ClickToMove();
        FaceTarget();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseApp();
        }

    }

    void ClickToMove()
    {
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && timer <= mouseHoldTime)
        {
            //Debug.Log("Click!");
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, blockRaycast))
            {
                Debug.Log("Block Raycast");
            }
            else if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, clickableLayers))
            {
                agent.destination = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            timer = 0f;

        }
    }

    void FaceTarget()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = (agent.steeringTarget - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }
    }

    public void CloseApp()
    {
        Debug.Log("Quit The Game");
        Application.Quit();
    }

    private void DisableSelf(bool state)
    {
        gameObject.SetActive(state);
    }

    private void OnEnable()
    {
        PlayerController.DisablePlayerController += DisableSelf;
        PlayerController.EnablePlayerController -= DisableSelf;
        if (agent.isOnNavMesh)
        {
            //agent.destination = transform.position;
            agent.isStopped = false;
        }
    }
    private void OnDisable()
    {
        PlayerController.DisablePlayerController -= DisableSelf;
        PlayerController.EnablePlayerController += DisableSelf;
        if (agent.isOnNavMesh)
        {
            agent.isStopped = true;
        }
    }

    private void OnDestroy()
    {
        PlayerController.DisablePlayerController -= DisableSelf;
        PlayerController.EnablePlayerController -= DisableSelf;
    }
}