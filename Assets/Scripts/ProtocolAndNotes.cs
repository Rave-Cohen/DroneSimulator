using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtocolAndNotes : MonoBehaviour
{
    // this script is protocol for progress and to note important information
    /*
     * *LEGEND*
     * changes i made by myself on given scripts of drone etc... appear with a key word --> *change*
     * i changed some of the moving vectors in control script which freeze strafing - try to solve
     * 
     * 
     * 
     * NOW:
     * 
     * As of now - need to build main page to start the experiment with
     * Can also design it as simulator with exe as desktop game for windows
     * 
     * build more enviornments
     * 
     * IN GENERAL:
     * Work on respwan the drone - theres some given feature in the scripts - check it and how can it be used
     * Need to split rock collissions and time stamp collisions - no need now
     * MAYBE - try to fix better strafe graphics
     * 
     * LATER:
     * randomness - we can implement the control needed to pass boulders with stack contains right \ left command
     * we can later use this stack to find the ErrP
     * 
     * 
     * 
     * 
     * DONE:
     * check that triggers are trully sent - need new computer porbably -  implemented socket and triggers are sent to unicorn data
     * 
     * Set the experiment enviornment 20 turns - make an array of -1 for left and 1 for right match the turn needed and with every trigger
     * and a counter variable which grows with each turn taken
     * 
     * organize triggers before every boulder - 5 sec before the need to turn
     * 
     * made flickering lights - red: strafe left - imagine moving ur left arm, right: strafe right - imagine moving ur right arm
     * 
     * 
     *
     * 
     * How to run LSL data stream:
     * Connect and activate the unicorn kit and BT
     * Choose DevTools and unicorn LSL from the unicorn main menu
     * Start the stream - we need to see constant light from the unicorn headset
     * 
     *Check activation:
     * run the device with given UI - (start from formal unicorn Recorder) - only to check we get good data
     * Close recorder
     * Run readLslStream from Mafat python directory
     * Now u should get live LSL data stream on python
     * 
     * While its running on python - we run the unity simulator
     * 
     * 
     * 
     * 
     * 
     * MainScene - List of movement: 
     * private float[] strafeArr = new float[25] {-1, 1, -1, -1, 1, 1, -1, 1, 1, 1,-1, -1, -1, 1, -1, 1, -1, 1 , -1, 1, -1 , 1, -1, 1, -1};
     * total 13 left and 12 right
     */


}
