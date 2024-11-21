using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.InputSystem;

public class Wurm : MonoBehaviour
{
    [SerializeField] private InputActionAsset controls;
 //   PlayerInput action = new PlayerInput();
 //   private InputAction interact;
    public Spline spline;
    public SplineExtrude splineExtrude;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splineExtrude.RebuildOnSplineChange = true;
        
        spline = GetComponent<SplineContainer>().Spline;
        
        Generate(default);
        var actionMap = controls.FindActionMap("Debug");
        var inputAction = actionMap.FindAction("Generate");
        inputAction.performed += Generate;
        
 //       interact = InputSystem.actions.FindAction("Interact");
    }

    public void Generate(InputAction.CallbackContext context)
    {
        OnInteract();
        
    }

    // Update is called once per frame
    void Update()
    {
 //       if (interact.IsPressed())
 //           OnInteract();
    }

    void OnInteract()
    {
        spline.Clear();
        for (int i = 0; i < Random.Range(3, 10); i++)
            spline.Add(new Vector3(Random.Range(-5, 5), Random.Range(0, 5), Random.Range(-5, 5)));
    }
}
