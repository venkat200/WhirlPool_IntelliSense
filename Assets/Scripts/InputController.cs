using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public Text Message;
    public Vector2 ScaleRange;

    private Vector2 _startPos, _activePos;
    private float _diff_Start, _diff_move;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _startPos = Input.touches[0].position;
            }
        }

        if (Input.touchCount == 1)
        {
           /* RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag  == "sceneobject")
                {
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        _activePos = Input.touches[0].position;

                        if(_activePos.x > _startPos.x)
                        {
                            transform.position += transform.right * Time.deltaTime * 5f;
                        }
                        else if (_activePos.x < _startPos.x)
                        {
                            transform.position -= transform.right * Time.deltaTime * 5f;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        _activePos = Input.touches[0].position;

                        if (_activePos.x > _startPos.x)
                        {
                            transform.Rotate(transform.up, Time.deltaTime * 0.1f);
                        }
                        else if (_activePos.x < _startPos.x)
                        {
                            transform.Rotate(transform.up, -1f * Time.deltaTime * 0.1f);
                        }
                        else
                        {

                        }
                    }
                }
            }*/
        }
        else if (Input.touchCount == 2)
        {            
            if(Input.touches[1].phase == TouchPhase.Began)
            {
                _diff_Start = Vector3.Distance(Input.touches[1].position, _startPos);
            }                   
            else if (Input.touches[1].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Ended)
            {
                _activePos = Input.touches[1].position;
                _diff_move = Vector3.Distance(Input.touches[1].position, _startPos);

                if (_diff_move > _diff_Start)
                {
                    if (transform.localScale.x < ScaleRange.y)
                        transform.localScale += new Vector3(1f,1f,1f) * Time.deltaTime * 0.1f;

                    _diff_Start = _diff_move;
                }
                else if (_diff_move < _diff_Start)
                {
                    if (transform.localScale.x > ScaleRange.x) 
                        transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.1f;

                    _diff_Start = _diff_move;
                }
                else
                { }
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
