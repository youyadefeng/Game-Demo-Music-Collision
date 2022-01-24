using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    public float rollSpeed = 2f;
    Vector3 startPos;
    BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * rollSpeed);
        if(transform.position.z < startPos.z - box.size.z/2)
            transform.position = startPos;
    }
}
