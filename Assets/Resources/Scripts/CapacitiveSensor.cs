using UnityEngine;
using System.Collections;
using Uniduino;

public class CapacitiveSensor : MonoBehaviour {

	public Arduino arduino;

	// Use this for initialization
	void Start () {
		arduino = Arduino.global;
	}

	// Update is called once per frame
	void Update () {

	}

	private int error;
	private ulong  leastTotal;
	private uint   loopTimingFactor;
	private ulong  CS_Timeout_Millis;
	private ulong  CS_AutocaL_Millis;
	private ulong  lastCal;
	private ulong  total;
	private byte sBit;
	private byte sReg;
	private byte rBit;
	private byte rReg;

	public CapacitiveSensor(byte sendPin, byte receivePin)
	{
		ulong F_CPU = 16000000;
		error = 1;
		loopTimingFactor = 310;		// determined empirically -  a hack
		CS_Timeout_Millis = (ulong)(2000 * (float)loopTimingFactor * (float)F_CPU) / 16000000;
		CS_AutocaL_Millis = 20000;

		arduino.pinMode(sendPin, PinMode.OUTPUT);						// sendpin to OUTPUT
		arduino.pinMode(receivePin, PinMode.INPUT);						// receivePin to INPUT
		arduino.digitalWrite(sendPin, Arduino.LOW);

//		sBit = PIN_TO_BITMASK(sendPin);					// get send pin's ports and bitmask
//		sReg = PIN_TO_BASEREG(sendPin);					// get pointer to output register
//
//		rBit = PIN_TO_BITMASK(receivePin);				// get receive pin's ports and bitmask
//		rReg = PIN_TO_BASEREG(receivePin);

		// get pin mapping and port for receive Pin - from digital pin functions in Wiring.c
		leastTotal = 0x0FFFFFFFL;   // input large value for autocalibrate begin
	//	lastCal = millis();         // set millis for start
	}

//// Public Methods //////////////////////////////////////////////////////////////
//// Functions available in Wiring sketches, this library, and other libraries
//
//	public long capacitiveSensor(byte samples)
//	{
//		total = 0;
//		if (samples == 0) return 0;
//		if (error < 0) return -1;            // bad pin
//
//
//		for (byte i = 0; i < samples; i++) {    // loop for samples parameter - simple lowpass filter
//			if (SenseOneCycle() < 0)  return -2;   // variable over timeout
//		}
//
//			// only calibrate if time is greater than CS_AutocaL_Millis and total is less than 10% of baseline
//			// this is an attempt to keep from calibrating when the sensor is seeing a "touched" signal
//
//			if ( (millis() - lastCal > CS_AutocaL_Millis) && abs(total  - leastTotal) < (int)(.10 * (float)leastTotal) ) {
//				leastTotal = 0x0FFFFFFFL;          // reset for "autocalibrate"
//				lastCal = millis();
//			}
//
//		// routine to subtract baseline (non-sensed capacitance) from sensor return
//		if (total < leastTotal) leastTotal = total;                 // set floor value to subtract from sensed value
//		return(total - leastTotal);
//
//	}
//
//	public long capacitiveSensorRaw(byte samples)
//	{
//		total = 0;
//		if (samples == 0) return 0;
//		if (error < 0) return -1;                  // bad pin - this appears not to work
//
//		for (butet i = 0; i < samples; i++) {    // loop for samples parameter - simple lowpass filter
//			if (SenseOneCycle() < 0)  return -2;   // variable over timeout
//		}
//
//		return total;
//	}
//
//
//	public void reset_CS_AutoCal(){
//		leastTotal = 0x0FFFFFFFL;
//	}
//
//	public void set_CS_AutocaL_Millis(ulong autoCal_millis){
//		CS_AutocaL_Millis = autoCal_millis;
//	}
//
//	public void set_CS_Timeout_Millis(ulong timeout_millis){
//		CS_Timeout_Millis = (timeout_millis * (float)loopTimingFactor * (float)F_CPU) / 16000000;  // floats to deal with large numbers
//	}
//
//// Private Methods /////////////////////////////////////////////////////////////
//// Functions only available to other functions in this library
//
//	private int SenseOneCycle()
//	{
//	    noInterrupts();
//		DIRECT_WRITE_LOW(sReg, sBit);	// sendPin Register low
//		DIRECT_MODE_INPUT(rReg, rBit);	// receivePin to input (pullups are off)
//		DIRECT_MODE_OUTPUT(rReg, rBit); // receivePin to OUTPUT
//		DIRECT_WRITE_LOW(rReg, rBit);	// pin is now LOW AND OUTPUT
//		delayMicroseconds(10);
//		DIRECT_MODE_INPUT(rReg, rBit);	// receivePin to input (pullups are off)
//		DIRECT_WRITE_HIGH(sReg, sBit);	// sendPin High
//	    interrupts();
//
//		while ( !DIRECT_READ(rReg, rBit) && (total < CS_Timeout_Millis) ) {  // while receive pin is LOW AND total is positive value
//			total++;
//		}
//		//Serial.print("SenseOneCycle(1): ");
//		//Serial.println(total);
//
//		if (total > CS_Timeout_Millis) {
//			return -2;         //  total variable over timeout
//		}
//
//		// set receive pin HIGH briefly to charge up fully - because the while loop above will exit when pin is ~ 2.5V
//	    noInterrupts();
//		DIRECT_WRITE_HIGH(rReg, rBit);
//		DIRECT_MODE_OUTPUT(rReg, rBit);  // receivePin to OUTPUT - pin is now HIGH AND OUTPUT
//		DIRECT_WRITE_HIGH(rReg, rBit);
//		DIRECT_MODE_INPUT(rReg, rBit);	// receivePin to INPUT (pullup is off)
//		DIRECT_WRITE_LOW(sReg, sBit);	// sendPin LOW
//	    interrupts();
//
//		while ( DIRECT_READ(rReg, rBit) && (total < CS_Timeout_Millis) ) {  // while receive pin is HIGH  AND total is less than timeout
//			total++;
//		}
//
//		if (total >= CS_Timeout_Millis) {
//			return -2;     // total variable over timeout
//		} else {
//			return 1;
//		}
//	}
//
}
