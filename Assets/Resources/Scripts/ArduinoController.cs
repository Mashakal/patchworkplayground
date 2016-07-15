using UnityEngine;
using System.Collections;
using Uniduino;

public class ArduinoController : MonoBehaviour {

	public Arduino arduino;
	public bool left = false;
	public bool right = false;
	public bool up = false;
	public bool down = false;
	public bool special = false;

	private PlayerController pController;
	private FillPatternController fillController;

	// Use this for initialization
	void Start () {
		arduino = Arduino.global;
		arduino.Setup (ConfigurePins);
	}

	void ConfigurePins () {
		arduino.pinMode(0, PinMode.ANALOG);
		arduino.reportAnalog(0, 1);
		arduino.pinMode(1, PinMode.ANALOG);
		arduino.reportAnalog(1, 1);
		arduino.pinMode(2, PinMode.ANALOG);
		arduino.reportAnalog(2, 1);
		arduino.pinMode(3, PinMode.ANALOG);
		arduino.reportAnalog(3, 1);
		arduino.pinMode(4, PinMode.ANALOG);
		arduino.reportAnalog(4, 1);
	}


	// Update is called once per frame
	void Update () {
		int threshold = 200;
		int upValue = arduino.analogRead(0);
		int rightValue = arduino.analogRead(1);
		int downValue = arduino.analogRead(2);
		int leftValue = arduino.analogRead(3);
		int specialValue = arduino.analogRead(4);

		Debug.Log ("Up: " + upValue + "   Right: " + rightValue + "   Down: " + downValue + "   Left: " + leftValue + "   Jump: " + specialValue);

		if (upValue > threshold)
			up = true;
		else
			up = false;
		
		if (rightValue > threshold)
			right = true;
		else
			right = false;
		
		if (leftValue > threshold)
			left = true;
		else
			left = false;
		
		if (downValue > threshold)
			down = true;
		else
			down = false;
		
		if (specialValue > threshold)
 			special = true;
		else
			special = false;

	}
}
