using System.Collections;
using UnityEngine;
using UnityEngine.AI;
    public class Avoider : MonoBehaviour
    {
        [Range(0, 360)]
        public float angle = 80f;
        public float radius = 6f;
        public float moveSpeed = 3.5f;
        public bool toggleGizmos;

        public GameObject player;

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask blockRaycast;

        public bool canSeePlayer;



        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.speed = moveSpeed;
            StartCoroutine(FOVRoutine());
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(player.transform.position);
        }

        private IEnumerator FOVRoutine()
        {
            float delay = 0.2f;
            WaitForSeconds wait = new WaitForSeconds(delay);

            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, blockRaycast))
                    {
                        canSeePlayer = true;
                        RunAway();
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
        }

        private void RunAway()
        {
            float radiusAroundEnemy = 10f;
            float minDistanceBetweenPoints = 2f;


            var sampler = new PoissonDiscSampler(radiusAroundEnemy * 2, radiusAroundEnemy * 2, minDistanceBetweenPoints);

            Vector3 enemyPosition = transform.position;
            Vector3 bestEscapePoint = enemyPosition;
            float shortestDistance = Mathf.Infinity;

            foreach (var sample in sampler.Samples())
            {
                Vector3 candidatePoint = new Vector3(
                    enemyPosition.x + sample.x - radiusAroundEnemy,
                    enemyPosition.y,
                    enemyPosition.z + sample.y - radiusAroundEnemy
                );

                NavMeshHit hit;
                if (NavMesh.SamplePosition(candidatePoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    Vector3 directionToPoint = (hit.position - player.transform.position).normalized;
                    if (Physics.Raycast(player.transform.position, directionToPoint, Vector3.Distance(player.transform.position, hit.position), blockRaycast))
                    {
                        float distanceToEnemy = Vector3.Distance(hit.position, enemyPosition);

                        if (distanceToEnemy < shortestDistance)
                        {
                            bestEscapePoint = hit.position;
                            shortestDistance = distanceToEnemy;
                        }
                    }
                }

                Debug.DrawRay(enemyPosition, candidatePoint - enemyPosition, Color.green, 2f);
            }

            if (bestEscapePoint != enemyPosition)
            {
                agent.SetDestination(bestEscapePoint);
            }
        }

    }

