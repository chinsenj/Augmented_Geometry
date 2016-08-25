using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResolutionSlider : MonoBehaviour {

    public GameObject slideer;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = slideer.GetComponent<Slider>().value.ToString();
	}
}
