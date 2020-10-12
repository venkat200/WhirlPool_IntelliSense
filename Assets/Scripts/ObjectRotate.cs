using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ObjectRotate : MonoBehaviour
{
    float rotationSpeed = 3;

    float targetAngle = 0.0f;
    public float resetRotationSpeed = 125.0f;
    public bool resetRotation = false;


    float rotationAngle = 310.0f;
    public bool dimensionClick = false, dimensionsRotate = false, intermediateRotate = false;

    [SerializeField]
    GameObject Arrow_L, Arrow_B, Arrow_H;

    public int AllowRotation = 1;

    [SerializeField]
    GameObject UIHandlerObject;
    UIHandler UIHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        UIHandlerScript = UIHandlerObject.GetComponent<UIHandler>();
    }

    // Update is called once per frame
    void Update()
    {

        if (resetRotation == true)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, resetRotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, angle, 0);

            // if(transform.eulerAngles.y == targetAngle)
            if (Mathf.Abs(transform.eulerAngles.y - targetAngle) < 1f)
            {
                transform.eulerAngles = new Vector3(0, targetAngle, 0);
                resetRotation = false;
            }
        }

        if (dimensionsRotate == true)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, rotationAngle, resetRotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, angle, 0);

            if ( Mathf.Abs(transform.eulerAngles.y - rotationAngle) < 1f )
            {
                transform.eulerAngles = new Vector3(0, rotationAngle, 0);

                dimensionsRotate = false;
            }
        }

        if (dimensionClick == true && !(transform.eulerAngles.y % 360 >= 0f && transform.eulerAngles.y % 360 < 270f) && !UIHandlerScript.delayStart_CMButton)
        {
            Arrow_B.SetActive(true);
            Arrow_H.SetActive(true);
            Arrow_L.SetActive(true);
        }
        else if (dimensionClick == true && (transform.eulerAngles.y % 360 == 0f) && !UIHandlerScript.delayStart_CMButton)
        {
            Arrow_B.SetActive(false);
            Arrow_H.SetActive(true);
            Arrow_L.SetActive(true);
        }
        else if(dimensionClick == true && !UIHandlerScript.delayStart_CMButton)
        {
            Arrow_B.SetActive(false);
            Arrow_H.SetActive(false);
            Arrow_L.SetActive(false);
        }
    }


    void OnMouseDrag()
    {
        float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad * AllowRotation;
        float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad * AllowRotation;

        if (rotationX > -0.3f && rotationX < 0.3f)
        {
            transform.RotateAround(Vector3.up, -rotationX);
            // transform.RotateAround(Vector3.right, rotationY);
        }
    }

    IEnumerator DimensionClickedTransition()
    {
        if (UIHandlerScript.delayStart_CMButton)
        {
            yield return new WaitForSeconds(1f);
            UIHandlerScript.delayStart_CMButton = false;
        }
        dimensionsRotate = true;
        Arrow_B.SetActive(true);
        Arrow_H.SetActive(true);
        Arrow_L.SetActive(true);
    }

    public void OnDimensionClicked()
    {
        dimensionClick = !dimensionClick;

        if (dimensionClick == true)
        {
            resetRotation = true;
            StartCoroutine(DimensionClickedTransition());
        }
        else
        {
            Arrow_B.SetActive(false);
            Arrow_H.SetActive(false);
            Arrow_L.SetActive(false);

            resetRotation = true;
        }
    }


}




/*
public class ObjectRotate : MonoBehaviour
{ 
    private Touch touch;
    private Vector2 touchPosition;
    private Quaternion rotationY;
    private float rotateSpeedModifier = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(0f, -touch.deltaPosition.x * rotateSpeedModifier, 0f);
                transform.rotation = rotationY * transform.rotation;
            }
        }
    }
}
*/





