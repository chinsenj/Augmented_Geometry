using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class minimize : MonoBehaviour {

	public bool minimized; 
	public GameObject targetPanel;
    public GameObject ParametricEquations;
    public GameObject genButton;
	public Button minButton;
	public Text buttonText;

	// Use this for initialization
	void Start () {
		minimized = false;
		minButton.onClick.AddListener(()=>minimizePanel());
	}

	void minimizePanel(){
		if (minimized == true) {
			minimized = false;
			buttonText.text = "-";
			targetPanel.SetActive (true);
            ParametricEquations.SetActive(true);
            genButton.SetActive(true);
		} 
		else {
			minimized = true;
			buttonText.text = "+";
			targetPanel.SetActive(false);
            ParametricEquations.SetActive(false);
            genButton.SetActive(false);
        }
			
	}
}
