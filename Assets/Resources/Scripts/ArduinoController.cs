using UnityEngine;
using System.Collections;
using Uniduino;

public class ArduinoController : MonoBehaviour {

	public Arduino arduino;

	// Use this for initialization
	void Start () {
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
	}

	void ConfigurePins () {
		arduino.pinMode (13, PinMode.OUTPUT);
		StartCoroutine (BlinkLoop ());
	}

	IEnumerator BlinkLoop() {
		while (true) {
			arduino.digitalWrite (13, Arduino.HIGH);
			yield return new WaitForSeconds (1);
			arduino.digitalWrite (13, Arduino.LOW);
			yield return new WaitForSeconds (1);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
