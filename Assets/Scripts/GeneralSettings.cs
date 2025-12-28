using UnityEngine;

public class GeneralSettings : MonoBehaviour
{
    private float gravityModifier = 1.7f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
