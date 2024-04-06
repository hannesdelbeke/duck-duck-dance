using UnityEngine;

public class InputController : MonoBehaviour
{
    public DanceGameController danceGameController;

    void Update()
    {
        // Keyboard input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            danceGameController.ProcessInput(Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            danceGameController.ProcessInput(Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            danceGameController.ProcessInput(Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            danceGameController.ProcessInput(Direction.Down);
        }

        // Xbox controller input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Ignore diagonal input
        if (Mathf.Abs(xAxis) > Mathf.Abs(yAxis))
        {
            // Horizontal movement
            if (xAxis < -0.5f)
            {
                danceGameController.ProcessInput(Direction.Left);
            }
            else if (xAxis > 0.5f)
            {
                danceGameController.ProcessInput(Direction.Right);
            }
        }
        else
        {
            // Vertical movement
            if (yAxis > 0.5f)
            {
                danceGameController.ProcessInput(Direction.Up);
            }
            else if (yAxis < -0.5f)
            {
                danceGameController.ProcessInput(Direction.Down);
            }
        }
    }
}