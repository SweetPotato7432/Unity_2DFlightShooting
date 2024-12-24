using UnityEngine;

public class JoystickTest : MonoBehaviour
{
    [SerializeField]
    VariableJoystick variableJoystick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(variableJoystick.Direction);
    }
}
