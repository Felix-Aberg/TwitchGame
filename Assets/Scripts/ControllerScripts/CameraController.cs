using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float boostSpeedModifier;
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    public float mouseClampY;

    Vector3 velocity; //This is local to the players rotation
    Vector3 eulerRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        //Basic input camera controls
        //Input button names are in all caps, see "VirtualKeys" enum for button names

        velocity.x = Input.GetAxis("HORIZONTAL");
        velocity.y = Input.GetAxis("STACKED");
        velocity.z = Input.GetAxis("VERTICAL");

        if (Input.GetButton("BOOST"))
        {
            velocity *= boostSpeedModifier;
        }

        velocity *= moveSpeed * Time.unscaledDeltaTime;
        transform.Translate(velocity, Space.Self);
    }

    void Rotate()
    {
        if (Input.GetButton("FIRE2"))
        {
            eulerRotation.y += Input.GetAxis("MOUSEX") * mouseSensitivityX;
            eulerRotation.x += -Input.GetAxis("MOUSEY") * mouseSensitivityY;

            if (eulerRotation.x > mouseClampY)
            {
                eulerRotation.x = mouseClampY;
            }
            else if (eulerRotation.x < -mouseClampY)
            {
                eulerRotation.x = -mouseClampY;
            }
            //*/
            //deltaRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation);
        }
    }
}
