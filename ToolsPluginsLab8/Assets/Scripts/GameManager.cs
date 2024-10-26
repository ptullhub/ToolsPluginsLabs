using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int totalScore;

    Target smallTargetBuild;
    Target mediumTargetBuild;
    Target largeTargetBuild;

    private ISaveable scoreSaveSystem;
    private JsonSaveSystem positionSaveSystem;

    private void Awake()
    {
        LoadGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        totalScore = scoreSaveSystem.Load();
        scoreText.text = $"Score: {totalScore}";

        MovingTarget.OnTargetDestroyed += IncrementScore;

        ITargetBuilder builder = new ConcreteTargetBuilder();
        TargetRegistry registry = new TargetRegistry(builder);

        smallTargetBuild = registry.BuildSmallTarget();
        mediumTargetBuild = registry.BuildMediumTarget();
        largeTargetBuild = registry.BuildLargeTarget();

        InvokeRepeating("SpawnSmallTarget", 1, 3);
        InvokeRepeating("SpawnMediumTarget", 3, 6);
        InvokeRepeating("SpawnLargeTarget", 5, 9);
    }

    void SpawnSmallTarget()
    {
        GameObject smallTarget = Instantiate(targetPrefab, new Vector3(UnityEngine.Random.Range(-8, 8), 2, 0), Quaternion.identity);
        MovingTarget movingTarget = smallTarget.GetComponent<MovingTarget>();
        movingTarget.InitializeValues(smallTargetBuild.Size, smallTargetBuild.Speed, smallTargetBuild.PointValue);
    }

    void SpawnMediumTarget() 
    {
        GameObject mediumTarget = Instantiate(targetPrefab, new Vector3(UnityEngine.Random.Range(-8, 8), 3, 0), Quaternion.identity);
        MovingTarget movingTarget = mediumTarget.GetComponent<MovingTarget>();
        movingTarget.InitializeValues(mediumTargetBuild.Size, mediumTargetBuild.Speed, mediumTargetBuild.PointValue);
    }

    void SpawnLargeTarget()
    {
        GameObject largeTarget = Instantiate(targetPrefab, new Vector3(UnityEngine.Random.Range(-8, 8), 5, 0), Quaternion.identity);
        MovingTarget movingTarget = largeTarget.GetComponent<MovingTarget>();
        movingTarget.InitializeValues(largeTargetBuild.Size, largeTargetBuild.Speed, largeTargetBuild.PointValue);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when GameManager is destroyed
        MovingTarget.OnTargetDestroyed -= IncrementScore;
    }

    void IncrementScore(int score)
    {
        totalScore += score;
        scoreText.text = $"Score: {totalScore}";
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void SaveGame()
    {
        Vector3 playerPosition = player.transform.position;

        List<Vector3> enemyPositions = new List<Vector3>();
        foreach (var enemy in FindObjectsOfType<MovingTarget>())
        {
            enemyPositions.Add(enemy.transform.position);
        }

        positionSaveSystem.Save(playerPosition, enemyPositions);
        scoreSaveSystem.Save(totalScore);
    }

    private void LoadGame()
    {
        scoreSaveSystem = new ScoreSaveSystem();
        positionSaveSystem = new JsonSaveSystem();

        JsonSaveData data = positionSaveSystem.Load();
        if (data != null)
        {
            player.transform.position = data.playerPosition.position;

            foreach (var enemyPos in data.enemyPositions)
            {
                GameObject enemy = Instantiate(targetPrefab, enemyPos.position, Quaternion.identity);
            }
        }
    }
}
