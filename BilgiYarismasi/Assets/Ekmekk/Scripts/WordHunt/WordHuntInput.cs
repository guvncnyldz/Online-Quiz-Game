using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHuntInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                hit.transform.GetComponent<LetterButton>().BeginOnClick();
            }
        }
    }
}