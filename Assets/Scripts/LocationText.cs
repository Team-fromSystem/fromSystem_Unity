using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationText : MonoBehaviour
{
    private TextMeshProUGUI debugText;
    [SerializeField] CheckLocation checkLocation;

    void Awake(){
        debugText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text=$"{checkLocation.LocationState}";
    }
}
