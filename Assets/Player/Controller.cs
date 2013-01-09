using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
    Transform relitiveTo;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float x;
        float y;
        float z;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        z = Input.GetAxis("Zoom");

        
        Vector3 translateBy;
        translateBy.y = z;
        translateBy.x = x;
        translateBy.z = y;
        Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        transform.Translate(rotation * translateBy, Space.World);

        
        if (Input.GetKey(KeyCode.Space))
        {
            float rx;
            float ry;
            rx = Input.GetAxis("Mouse X");
            ry = Input.GetAxis("Mouse Y");
            transform.RotateAround(new Vector3(0, 1, 0), rx);
            //relitiveTo.RotateAround(new Vector3(0, 1, 0), rx);
            transform.RotateAround(transform.right, ry);
            
        }
	}
}