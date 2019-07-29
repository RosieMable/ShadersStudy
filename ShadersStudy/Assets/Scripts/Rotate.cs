using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = 2;
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, speed, 0);
	}
}
