using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera exterior, interior;
    public OrbitCamera orbit;
    public NarrativeManager narrative;

    void Start()
    {
        exterior.gameObject.SetActive(true);
        interior.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SwitchToInterior();
        }
    }

    public void SwitchToInterior()
    {
        exterior.gameObject.SetActive(false);
        interior.gameObject.SetActive(true);
        orbit.orbiting = false;
        narrative.StartNarrative();
    }
}
