using UnityEngine;

public class PlayerSwipeController : MonoBehaviour
{
    public float swipeSensitivity = 0.01f;
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                currentTouchPosition = touch.position;
                Vector2 touchDelta = currentTouchPosition - startTouchPosition;
                float moveX = touchDelta.x * swipeSensitivity;
                transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
                startTouchPosition = currentTouchPosition;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isSwiping = false;
            }
        }
    }
}
