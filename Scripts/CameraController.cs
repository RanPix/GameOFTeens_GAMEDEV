using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float camHeight;

    [SerializeField] private LayerMask pointsLayer; 

    private Camera mainCam;

    private int pointsDestinationStep;

    private Points fromPoint;
    private Points toPoint;

    [SerializeField] private CanvasInGame CanvasScr;

    void Start()
    {
        mainCam = GetComponent<Camera>();

        transform.position = new Vector3(transform.position.x, camHeight, transform.position.z);
    }

    void Update()
    {
        MoveCamera();

        GetDestinationCheck();
    }

    private void MoveCamera()
    {
        float dx = Input.GetAxis("Horizontal") * speed;
        float dz = Input.GetAxis("Vertical") * speed;

        transform.position += new Vector3(dx, 0, dz);
    }

    private void GetDestinationCheck()
    {
        if (!Input.GetMouseButtonDown(1))
            return;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, pointsLayer))
        {
            print("xd");

            CanvasScr.from = null;
            CanvasScr.to = null;

            pointsDestinationStep = 0;
            return;
        }

        switch (pointsDestinationStep)
        {
            case 0:
                pointsDestinationStep++;

                print(pointsDestinationStep);

                CanvasScr.from = hit.transform.GetComponent<Points>();
                break;
            case 1:
                print(pointsDestinationStep);
                pointsDestinationStep = 0;

                print(pointsDestinationStep);

                CanvasScr.to = hit.transform.GetComponent<Points>();
                break;
        }
    }
}
