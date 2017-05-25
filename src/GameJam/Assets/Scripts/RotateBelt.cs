using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBelt : MonoBehaviour {

    public float RotateSpeed;

	// Use this for initialization
	void Start () {
        RotateSpeed = 2;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(0, RotateSpeed, 0);
	}
}
