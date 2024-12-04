using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.InputSystem;

public class Wurm : MonoBehaviour
{
    //[SerializeField] private Transform wurmTransform;
    [HideInInspector] [SerializeField] private InputActionAsset controls;
    [HideInInspector] [SerializeField] private Spline spline;
    [HideInInspector] [SerializeField] private SplineContainer splineContainer;
    [HideInInspector] [SerializeField] private SplineExtrude splineExtrude;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spline ??= new Spline();
        if (splineContainer == null)
            splineContainer = gameObject.AddComponent<SplineContainer>();
        if (splineExtrude == null)
        {
            splineExtrude = gameObject.AddComponent<SplineExtrude>();
            splineExtrude.Container = splineContainer;
            splineExtrude.RebuildOnSplineChange = true;
            gameObject.GetComponent<MeshFilter>().mesh = new Mesh();
            gameObject.GetComponent<MeshRenderer>().SetMaterials(new List<Material>
            {
                new (Shader.Find("Universal Render Pipeline/Lit"))
            });
        }

        spline = GetComponent<SplineContainer>().Spline;
        splineExtrude.RebuildOnSplineChange = true;
        
        Generate(default);
        var debugActionMap = controls.FindActionMap("Debug");
        var generateInputAction = debugActionMap.FindAction("Generate");
        generateInputAction.performed += Generate;
        
        /*var playerActionMap = controls.FindActionMap("Player");
        var playerInputAction = playerActionMap.FindAction("MoveObject");
        playerInputAction.performed += MoveObject;*/
    }

    public void Generate(InputAction.CallbackContext context)
    {
        spline.Clear();
        SetSplineNodes();
        SetRadius();
        //SetPosition();
    }

    private void SetPosition()
    {
        //wurmTransform = transform.position(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }

    private void SetSplineNodes()
    {
        for (int i = 0; i < Random.Range(3, 10); i++)
            spline.Add(new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 2f), Random.Range(-1f, 1f)));
    }

    private void SetRadius()
    {
        splineExtrude.Radius = Random.Range(0.01f, 0.1f);
        splineExtrude.Rebuild();
    }

    private void MoveObject(InputAction.CallbackContext context)
    {
        transform.position = context.ReadValue<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnInteract()
    {
    }
}
