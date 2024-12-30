using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtocolAndNotes : MonoBehaviour
{
    // this script is protocol for progress and to note important information
    /*
     * NOW:
     * check that triggers are trully sent - need new computer porbably
     * organize triggers before every boulder - 5 sec before the need to turn
     * As of now - need to build main page to start the experiment with
     * 
     * IN GENERAL:
     * Work on respwan the drone - theres some given feature in the scripts - check it and how can it be used
     * Need to split rock collissions and time stamp collisions - no need now
     * 
     * LATER:
     * we can implement the control needed to pass boulders with stack contains right \ left command
     * we can later use this stack to find the ErrP
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
     * We need to implement socket(!) for the unicorn device in the unity - implemented check if it works
     * 
     */


}
