using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    // This script is solely made for the Demo Scene where you can rotate the table around using the Keys D and A

    public float speed = 10f;
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * speed);

        } else if (Input.GetKey(KeyCode.A))
        {

            this.transform.Rotate(Vector3.down * Time.deltaTime * speed);
        }
	}
}
