using UnityEngine;

public class MouseLook : MonoBehaviour
{
    void Update()
    {
        LookAtMouse();
    }

    void LookAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 dir = new Vector3(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y,
            mousePos.z - transform.position.z
            );

        transform.up = dir;
    }
}
