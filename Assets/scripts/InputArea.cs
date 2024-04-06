using UnityEngine;

public class InputArea : MonoBehaviour
{
    
    public float inputThreshold = 0.5f;
    private static InputArea instance;
    public static InputArea Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    void OnDrawGizmosSelected()
    {
        // Draw a wireframe box around the input area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(inputThreshold * 2, inputThreshold * 2, 0));
    }
}
