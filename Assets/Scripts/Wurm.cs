using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Wurm : MonoBehaviour
{
    [HideInInspector] [SerializeField] private InputActionAsset controls;
    [HideInInspector] [SerializeField] private Spline spline;
    [HideInInspector] [SerializeField] private SplineContainer splineContainer;
    [HideInInspector] [SerializeField] private SplineExtrude splineExtrude;

    [HideInInspector] [SerializeField] private MeshRenderer meshRenderer;
    [HideInInspector] [SerializeField] private bool selected;

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
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
        }

        spline = GetComponent<SplineContainer>().Spline;
        splineExtrude.RebuildOnSplineChange = true;
        Generate(default);
        var debugActionMap = controls.FindActionMap("Debug");
        var generateInputAction = debugActionMap.FindAction("Generate");
        generateInputAction.performed += Generate;

        var playerActionMap = controls.FindActionMap("Player");
        var moveObjectInputAction = playerActionMap.FindAction("MoveObject");
        moveObjectInputAction.performed += MoveObject;

        var selectObjectInputAction = playerActionMap.FindAction("SelectObject");
        selectObjectInputAction.performed += SelectObject;
    }

    private void Generate(InputAction.CallbackContext context)
    {
        spline.Clear();
        SetRandomSplineNodes();
        SetRandomRadius();
        //SetRandomPosition();
        SetRandomColor();
    }
    
    private void MoveObject(InputAction.CallbackContext context)
    {
        if (selected)
        {
            var moveObjectInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
            transform.position += moveObjectInput * 0.01f;
        }
    }

    private void SelectObject(InputAction.CallbackContext context)
    {
        selected = !selected;
    }

    private void SetMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    private void SetRandomColor()
    {
        SetMaterial(new Material(Shader.Find("Universal Render Pipeline/Lit"))
        {
            color = Random.ColorHSV()
        });
    }

    private void SetRandomPosition()
    {
        transform.position = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f));
    }

    private void SetRandomSplineNodes()
    {
        for (int i = 0; i < Random.Range(3, 10); i++)
            spline.Add(new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 2f), Random.Range(-1f, 1f)));
    }

    private void SetRandomRadius()
    {
        splineExtrude.Radius = Random.Range(0.01f, 0.1f);
        splineExtrude.Rebuild();
    }


    // Update is called once per frame
    void Update()
    {
    }
}