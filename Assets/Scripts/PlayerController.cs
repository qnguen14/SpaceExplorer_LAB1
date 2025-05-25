using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Camera mainCam;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        mainCam = Camera.main;
        screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            objectWidth = sr.bounds.extents.x;
            objectHeight = sr.bounds.extents.y;
        }
    }

    void Update()
    {
        Vector3 move = Vector3.zero;

        // Sử dụng phím mũi tên thay vì WASD
        if (Keyboard.current.upArrowKey.isPressed)
            move += Vector3.up;
        if (Keyboard.current.downArrowKey.isPressed)
            move += Vector3.down;
        if (Keyboard.current.leftArrowKey.isPressed)
            move += Vector3.left;
        if (Keyboard.current.rightArrowKey.isPressed)
            move += Vector3.right;

        Vector3 newPos = transform.position + move.normalized * moveSpeed * Time.deltaTime;

        newPos.x = Mathf.Clamp(newPos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        newPos.y = Mathf.Clamp(newPos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        transform.position = newPos;
    }
}
