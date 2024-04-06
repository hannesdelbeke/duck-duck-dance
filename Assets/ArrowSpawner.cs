using UnityEngine;
using System.Collections;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform upSpawnPoint;
    public Transform downSpawnPoint;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float arrowSpeed = 2.0f;
    public float spawnInterval = 2.0f; // Interval between arrow spawns

    private DanceGameController danceController;

    void Start()
    {
        danceController = FindObjectOfType<DanceGameController>(); // Find the DanceGameController script in the scene
        StartCoroutine(SpawnArrows());
    }

    IEnumerator SpawnArrows()
    {
        while (true)
        {
            SpawnArrow();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnArrow()
    {
        // Choose the appropriate spawn point based on arrow direction
        Transform spawnPoint;
        Direction randomDirection = (Direction)Random.Range(0, 4);
        switch (randomDirection)
        {
            case Direction.Up:
                spawnPoint = upSpawnPoint;
                break;
            case Direction.Down:
                spawnPoint = downSpawnPoint;
                break;
            case Direction.Left:
                spawnPoint = leftSpawnPoint;
                break;
            case Direction.Right:
                spawnPoint = rightSpawnPoint;
                break;
            default:
                spawnPoint = upSpawnPoint; // Default to upSpawnPoint if direction is not recognized
                break;
        }

        GameObject arrowObject = Instantiate(arrowPrefab, spawnPoint.position, Quaternion.identity);
        Arrow arrow = arrowObject.GetComponent<Arrow>();

        arrow.direction = randomDirection;
        arrow.speed = arrowSpeed;
    }

}
