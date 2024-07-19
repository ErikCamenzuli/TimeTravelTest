using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeTravel : MonoBehaviour
{
    [SerializeField] GameObject past, present;
    [SerializeField] bool presentIsVisiable = true;

    float timeToEffect = 0f;
    [SerializeField] float effectRatePerSecond = 1f;
    
    [SerializeField] float transitionTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Set the active state of the 'present' GameObject based on the initial visibility flag
        present.SetActive(presentIsVisiable);
        //Set the active state of the 'present' GameObject based on the initial visibility flag
        past.SetActive(!presentIsVisiable);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the 'E' key is pressed and if the time has passed the time required for the effect
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= timeToEffect) 
        {
            //Calculate the next time the effect can be triggered
            timeToEffect = Time.time +1 / effectRatePerSecond;
            //Call the function to switch active layers
            SwitchActiveLayers();
        }
    }
    //Function to switch between active layers
    void SwitchActiveLayers()
    {
        //Toggle the boolean flag to indicate whether present is visible or not
        presentIsVisiable = !presentIsVisiable;
        //Set the active state of the 'present' GameObject to the opposite of its current state
        present.SetActive(!present.activeSelf);
        //Set the active state of the 'present' GameObject to the opposite of its current state
        past.SetActive(!past.activeSelf);
    }

}
