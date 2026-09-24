using UnityEngine;

public class InputManager : MonoBehaviour
{
    public System.Action SwipeUp;

    [SerializeField] private float minimumSwipeDistance = 100f;

    private Vector2 startPosition;
    private bool isSwiping;

    private FeedManager feedManager;

    private void Awake()
    {
        feedManager = FindFirstObjectByType<FeedManager>();

        SwipeUp += OnSwipeUp;
    }

    private void Update()
    {
        DetectMouseInput();
        DetectTouchInput();
    }

    private void DetectMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            Vector2 endPosition = Input.mousePosition;

            CheckSwipe(endPosition - startPosition);

            isSwiping = false;
        }
    }

    private void DetectTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            startPosition = touch.position;
            isSwiping = true;
        }

        if (touch.phase == TouchPhase.Ended && isSwiping)
        {
            Vector2 endPosition = touch.position;

            CheckSwipe(endPosition - startPosition);

            isSwiping = false;
        }
    }

    private void CheckSwipe(Vector2 delta)
    {
        if (delta.y < minimumSwipeDistance)
            return;

        if (Mathf.Abs(delta.y) <= Mathf.Abs(delta.x))
            return;

        SwipeUp?.Invoke();
    }

    private void OnSwipeUp()
    {
        feedManager.Next();
    }
}