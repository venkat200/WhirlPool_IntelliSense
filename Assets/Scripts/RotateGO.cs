
using UnityEngine;

public class RotateGO : MonoBehaviour
{ 
    private Touch touch;
    private Vector2 touchPos;

    private Quaternion rotation;
    public float rotateSpeedModifier = 0.5f;

   
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
               // float rotationX = Mathf.Clamp(touch.deltaPosition.y * rotateSpeedModifier, -25, 25);
               // rotationY = Quaternion.Euler(0f, touch.deltaPosition.x * rotateSpeedModifier, 0f);

                rotation = Quaternion.Euler(0f, touch.deltaPosition.x * rotateSpeedModifier, 0f);

                transform.rotation = rotation * transform.rotation;
            }
        }

    }
}
