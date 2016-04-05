using UnityEngine;
using System.Collections;

public class axis : MonoBehaviour {

	void Start () {
		MeshGenerator parent = transform.parent.GetComponent<MeshGenerator>();
		float centre = parent.resolution/2;
		transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f); 
	}

	void Update () {
	
	}
}
