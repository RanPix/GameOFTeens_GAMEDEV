using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, Range(0, 3), Tooltip("0 - Suplly, 1 - BridgeBuilder, 2 - Civilian, 3 - Armed")]
    private int carType;

    [HideInInspector] public Car car;
    [HideInInspector] public Road curRoad;

    [HideInInspector] public int destination;
    [HideInInspector] public bool moveDir;

    private void Awake()
    {
        switch (carType)
        {
            case 0:
                car = new SupplyCar();
                break;
            case 1:
                car = new BridgeSupplyCar();
                break;
            case 2:
                car = new CivilianCar();
                break;
            case 3:
                car = new ArmedCar();
                break;
            default:
                throw new System.NullReferenceException("Which car?");
        }
    }

    void Start()
    {
        destination = curRoad.getDestination(moveDir, destination); 
    }

    void FixedUpdate()
    {
        if (curRoad == null)
            return;

        if (curRoad.nextIsBroken(moveDir, destination) == false)
            transform.position = Vector3.MoveTowards(gameObject.transform.position, curRoad.waypoints[destination].pos, car.carSpeed);
        transform.LookAt(curRoad.waypoints[destination].pos);

        if (car.name == "BridgeSupplyCar" && curRoad.nextIsBroken(moveDir, destination) == true)
            FixRoad();

        if (curRoad.onDestination(gameObject.transform.position, destination))
            destination = curRoad.getDestination(moveDir, destination);

        if(curRoad.onDestination(gameObject.transform.position, destination) == true)
        {
            OnPoint();
        }
    }

    private void OnPoint()
    {
        Points end = curRoad.getEndPoint(moveDir);

        switch (car.name)
        {
            case "SupplyCar": end.resources.Supply.amount += car.resources.Supply.amount;
                break;
            case "BridgeSupplyCar": end.resources.BridgeSupply.amount += car.resources.BridgeSupply.amount;
                break;
            case "CivilianCar": end.resources.Civilians.amount += car.resources.Civilians.amount;
                break;
            case "ArmedCar": end.resources.Weapons.amount += car.resources.Weapons.amount;
                break;
        }

        Destroy(gameObject);
    }

    private void FixRoad()
    {
        if (car.resources.BridgeSupply.amount >= 200 == false)
        {
            return;
        }

        curRoad.fixRoad(moveDir, destination);
        car.resources.BridgeSupply.amount = 0;

        moveDir = !moveDir;
        curRoad.getDestination(moveDir, destination);
    }
}
