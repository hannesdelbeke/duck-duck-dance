using UnityEngine;

public class InputArea : MonoBehaviour
{
    public float inputThreshold = 0.5f;

    void OnDrawGizmosSelected()
    {
        // Draw a wireframe box around the input area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(inputThreshold * 2, inputThreshold * 2, 0));
    }
}
