using UnityEngine;
using System.Collections;

public class RhythmGameController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform upSpawnPoint;
    public Transform downSpawnPoint;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public float arrowSpeed = 2.0f;
    public float spawnInterval = 2.0f; // Interval between arrow spawns

    private InputArea inputArea;

    void Start()
    {
        inputArea = FindObjectOfType<InputArea>(); // Find the InputArea script in the scene
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

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckInput(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            CheckInput(Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckInput(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckInput(Direction.Right);
        }
    }

    void CheckInput(Direction expectedDirection)
    {
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");
        foreach (GameObject arrow in arrows)
        {
            Arrow arrowComponent = arrow.GetComponent<Arrow>();
            if (arrowComponent.IsWithinInputArea(inputArea.gameObject.transform.position, inputArea.inputThreshold) && arrowComponent.direction == expectedDirection)
            {
                Destroy(arrow);
                Debug.Log("Success - Player pressed the correct input for the arrow.");
                // Player pressed the correct input for the arrow
                // Add scoring or other gameplay logic here
                return;
            }
        }
        Debug.Log("Fail - Player either missed the arrow or pressed the wrong input.");
        // Player either missed the arrow or pressed the wrong input
        // Add logic for missed arrow or wrong input here
    }
}
