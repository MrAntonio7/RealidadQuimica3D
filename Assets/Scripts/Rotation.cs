using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    public int spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(0, 90, 0);
        spinSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * spinSpeed * Time.deltaTime);
    }
}
