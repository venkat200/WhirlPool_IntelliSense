using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private GameObject _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 _currentPos = transform.position;
        // Vector3 _dir = _camera.transform.position - transform.position;
        // transform.forward = _dir;
        transform.LookAt(_camera.transform);
    }
}
