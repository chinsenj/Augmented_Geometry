using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class minimize : MonoBehaviour {

	private bool minimized; 
	public GameObject targetPanel;
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
		} 
		else {
			minimized = true;
			buttonText.text = "+";
			targetPanel.SetActive(false);
		}
			
	}
}
