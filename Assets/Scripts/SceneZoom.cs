using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneZoom : MonoBehaviour
{
    public GameObject _SceneObject;

    // Zoom Functionality
    private float _diff_Start, _diff_move;
    // ScaleRange.x defines min scale and ScaleRange.y defines max scale
    public Vector2 ScaleRange;

    public float ScrollSensitvity;
    public float TouchScrollSensitivity;
    // public float ScrollSensitvity = 120f;
    // public float TouchScrollSensitivity = 3f;

    [SerializeField]
    GameObject ScaleRangeSelectionObject;
    UIHandler UIHandlerScript;

    // Start is called before the first frame update
    void Start()
    {
        UIHandlerScript = ScaleRangeSelectionObject.GetComponent<UIHandler>();
    }

    void OnEnable()
    {

        UIHandlerScript = ScaleRangeSelectionObject.GetComponent<UIHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIHandlerScript.ARView)
        {
            ScaleRange = new Vector2(2f, 6f);
            TouchScrollSensitivity = 6;
        }
        else
        {
            ScaleRange = new Vector2(0.75f, 3.5f);
            TouchScrollSensitivity = 3;
        }
        checkInput();
    }

    void checkInput()
    {
        
        if (Input.touchCount > 1)
        {
            if (Input.touches[1].phase == TouchPhase.Began)
            {
                _diff_Start = Vector3.Distance(Input.touches[1].position, Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
            {
                _diff_move = Vector3.Distance(Input.touches[1].position, Input.touches[0].position);

                if (_diff_move > _diff_Start)
                {
                    if (_SceneObject.transform.localScale.y < ScaleRange.y)
                        _SceneObject.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f * TouchScrollSensitivity;

                    _diff_Start = _diff_move;
                }
                else if (_diff_move < _diff_Start)
                {
                    if (_SceneObject.transform.localScale.y > ScaleRange.x)
                        _SceneObject.transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f * TouchScrollSensitivity;

                    _diff_Start = _diff_move;
                }
                else
                { }
            }
        }
       

        //Zooming Input from our Mouse Scroll Wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
            if (_SceneObject.transform.localScale.y <= ScaleRange.y && _SceneObject.transform.localScale.y >= ScaleRange.x)
            {
                if ((_SceneObject.transform.localScale + (new Vector3(1f, 1f, 1f) * Time.deltaTime * ScrollAmount * 0.5f)).y > ScaleRange.y)
                {
                    _SceneObject.transform.localScale = new Vector3(ScaleRange.y, ScaleRange.y, ScaleRange.y);
                }
                else if ((_SceneObject.transform.localScale + (new Vector3(1f, 1f, 1f) * Time.deltaTime * ScrollAmount * 0.5f)).y < ScaleRange.x)
                {
                    _SceneObject.transform.localScale = new Vector3(ScaleRange.x, ScaleRange.x, ScaleRange.x);
                }
                else
                {
                    _SceneObject.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * ScrollAmount * 0.5f;
                }
            }
        }

    }
}
