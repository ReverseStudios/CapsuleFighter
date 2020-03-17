using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PhotonView PV;
 
    [Header("Player Movement")]
    [SerializeField] float playerSpeed;
    [SerializeField] float groundDistnace = 0.6f;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;

    [Header("Player Physics")]
    [SerializeField] Vector3 velocity;
    [SerializeField] float gravity;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            TakeInput();
            LookAtMouse();
        }
    }

    void TakeInput()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistnace, ground);

        if(isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float X = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float Z = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;

        Vector3 move = transform.right * X + transform.forward * Z;
        controller.Move(move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void LookAtMouse()
    {
        /*Vector3 RayCaster;
        RayCaster.x = 0;
        RayCaster.y = 30;
        RayCaster.z = 0;
        Debug.Log(RayCaster);
*/

        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;

        if(groundPlane.Raycast(cameraRay,out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
