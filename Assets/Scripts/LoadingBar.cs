using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i += 2)
            transform.GetChild(i).Rotate(Vector3.up * Speed);

        for (int i = 1; i < transform.childCount; i += 2)
            transform.GetChild(i).Rotate(Vector3.up * -1f * Speed);
    }
}
