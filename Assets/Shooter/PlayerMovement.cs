using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePosition;

    public Camera cam;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        mousePosition = Input.mousePosition - transform.position - new Vector3(320f, 180f, 0f);
    }

    void FixedUpdate()
    {
        // TODO - cross sqr(2) -> 1
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        RotateToMouse();
        Debug.DrawLine(transform.position, Camera.main.ScreenToViewportPoint(Input.mousePosition));
    }

    void RotateToMouse()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = cam.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)cam.ScreenToViewportPoint(Input.mousePosition);

        //Debug.Log(positionOnScreen.ToString("F5") + " + " + mouseOnScreen.ToString("F5"));
        //Get the angle between the points
        positionOnScreen = new Vector2(positionOnScreen.x * 16, positionOnScreen.y * 9);
        mouseOnScreen = new Vector2(mouseOnScreen.x * 16, mouseOnScreen.y * 9);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        //Debug.Log(angle);

        //Ta Daaa
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle-90f));
    }

    void RotateToMouse2()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1000f*Time.deltaTime);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}