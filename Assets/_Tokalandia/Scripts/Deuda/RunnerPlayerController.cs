using UnityEngine;

public class RunnerPlayerController : MonoBehaviour
{
    [Header("Lane Settings")]
    public float[] laneXPositions = { -1.8f, 0f, 1.8f };
    public int currentLane = 1;
    public float laneSwitchSpeed = 12f;

    [Header("Forward Movement")]
    public float forwardSpeed = 6f;

    [Header("Swipe Settings")]
    public float minSwipeDistance = 50f;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isSwiping = false;

    private void Update()
    {
        HandleKeyboardInput();
        HandleTouchInput();
        MovePlayer();
    }

    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                isSwiping = true;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (!isSwiping) return;

                touchEndPos = touch.position;
                Vector2 swipeDelta = touchEndPos - touchStartPos;

                if (Mathf.Abs(swipeDelta.x) > minSwipeDistance &&
                    Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        MoveRight();
                    }
                    else
                    {
                        MoveLeft();
                    }
                }

                isSwiping = false;
                break;
        }
    }

    void MoveLeft()
    {
        currentLane = Mathf.Max(0, currentLane - 1);
    }

    void MoveRight()
    {
        currentLane = Mathf.Min(laneXPositions.Length - 1, currentLane + 1);
    }

    void MovePlayer()
    {
        Vector3 targetPosition = new Vector3(
            laneXPositions[currentLane],
            transform.position.y,
            transform.position.z - forwardSpeed * Time.deltaTime
        );

        Vector3 newPosition = transform.position;

        newPosition.x = Mathf.Lerp(
            transform.position.x,
            targetPosition.x,
            laneSwitchSpeed * Time.deltaTime
        );

        newPosition.z = targetPosition.z;

        transform.position = newPosition;
    }
}