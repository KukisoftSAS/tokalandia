using UnityEngine;

public class RunnerPlayerController : MonoBehaviour
{
    public float[] laneXPositions = { -1.8f, 0f, 1.8f };
    public int currentLane = 1;
    public float laneSwitchSpeed = 12f;
    public float fixedZ = 0f;

    [Header("Swipe Settings")]
    public float minSwipeDistance = 60f;

    private Vector2 touchStartPos;
    private bool isSwiping = false;

    void Update()
    {
        HandleKeyboardInput();
        HandleTouchInput();
        MovePlayer();
    }

    void HandleKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            MoveRight();
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

                Vector2 swipeDelta = touch.position - touchStartPos;

                if (Mathf.Abs(swipeDelta.x) > minSwipeDistance &&
                    Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                        MoveRight();
                    else
                        MoveLeft();
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
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            laneSwitchSpeed * Time.deltaTime
        );
    }
}