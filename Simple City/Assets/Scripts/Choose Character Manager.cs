using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// <summary>
// Choose character manager.cs
// Tapiwa Musinga
// 06/18/2024
// </summary>
public class ChooseCharacterManager : MonoBehaviour
{
    public static bool _femaleDummy;                 //Defines if robot black is selected
    public static bool _maleDummy;                 //Defines if robot white is selected
    
    void Awake() {
        _femaleDummy = false;        //Robot black is false on start up
        _maleDummy = false;        //Robot white is false on start up
        
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);            //Don't destroy object when loading the scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
