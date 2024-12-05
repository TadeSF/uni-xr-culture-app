using UnityEngine;
using UnityEngine.InputSystem;

public class SkriptExecutor : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
    [HideInInspector] [SerializeField] private GameObject newWurmObject;
    [HideInInspector] [SerializeField] private Wurm wurm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var debugActionMap = controls.FindActionMap("Debug");
        var generateInputAction = debugActionMap.FindAction("Generate");
        generateInputAction.performed += GenerateSpline;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSpline(InputAction.CallbackContext context)
    {
        newWurmObject = new GameObject();
        wurm = newWurmObject.AddComponent<Wurm>();
        wurm.controls = controls;
    }
}
