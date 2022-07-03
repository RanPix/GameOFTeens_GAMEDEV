using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private List<Road> roadsAvailable = new List<Road>();
    [HideInInspector] public Points pointDestination;

    [SerializeField] private CarsRef carsRef;
    private List<GameObject> carTypes = new List<GameObject>();

    [SerializeField] private string name;
    [SerializeField] private bool endangered;

    [SerializeField] private int carType;
    //private int roadSpawn;

    [SerializeField] private bool producesSupply;
    [SerializeField] private bool producesBridgeSupply;
    [SerializeField] private bool producesWeapons;

    [SerializeField] private int maxResources;
    [SerializeField] private int maxCivilians;
    [SerializeField] private int currentCivilians;
    public Resource.Resources resources = new Resource.Resources(); // у сіхітео
    [SerializeField] private int income;

    private void Awake()
    {
        carTypes = carsRef.cars;

        if (currentCivilians > maxCivilians) currentCivilians = maxCivilians;

        // for(int i = 1000; i > 0; i -= 7) Console.WriteLine($"{i+7} - 7 = {i}"); //Зібрались два дед інсайда і два не вдупляючих що вони творять програміста в барі

        resources.Supply       = new Resource("Supply");//Resource.ResourceType["Supply"]; // сіхітео
        resources.BridgeSupply = new Resource("BridgeSupply");//Resource.ResourceType["BridgeSupply"]; // ону ці ку ні во
        resources.Civilians    = new Resource("Civilians");//Resource.ResourceType["Civilians"]; // е но то кокуні
        resources.Weapons      = new Resource("Weapons");//Resource.ResourceType["Weapons"]; // о ну сіхеніта

        resources.Civilians.amount = currentCivilians;

        
    }
    
    public Resource.Resources CarInventory(int amount, int resourceType)
    {
        switch (resourceType)
        {
            case 0:
                if (resources.Supply.amount - amount < 0) return null;

                resources.Supply.amount -= amount;
                return new Resource.Resources(0, amount);
            case 1:
                if (resources.BridgeSupply.amount - amount < 0) return null;

                resources.BridgeSupply.amount -= amount;
                return new Resource.Resources(1, amount);
            case 2:
                if (resources.Civilians.amount - amount < 0) return null;

                resources.Civilians.amount -= amount;
                return new Resource.Resources(2, amount);
            case 3:
                if (resources.Weapons.amount - amount < 0) return null;

                resources.Weapons.amount -= amount;
                return new Resource.Resources(3, amount);

            default: 
                throw new System.NullReferenceException("Which truck?");
        }
    }

    public void SpawnCar(Road road, int amount, int resourceType)
    {
        Resource.Resources invetrory = new Resource.Resources();
        int itemCapacity = 0;

        switch (resourceType)
        {
            case 0: itemCapacity = SupplyCar.itemCapacity; break;
            case 1: itemCapacity = BridgeSupplyCar.itemCapacity; break;
            case 2: itemCapacity = CivilianCar.itemCapacity; break;
            case 3: itemCapacity = ArmedCar.itemCapacity; break;
        }

        for (int i = amount; i > 0; i -= itemCapacity)
        {
            if (i > itemCapacity) invetrory = CarInventory(itemCapacity, resourceType);
            else invetrory = CarInventory(i, resourceType);
            if (invetrory == null) return;

            CarController controller = new CarController();

            if (road.begining == GetComponent<Points>())
            {
                GameObject spawnedCar = Instantiate(carTypes[resourceType], road.waypoints[0].pos, Quaternion.identity);
                controller = spawnedCar.GetComponent<CarController>();


                controller.moveDir = true;
            }
            else if (road.ending == GetComponent<Points>())
            {
                GameObject spawnedCar = Instantiate(carTypes[resourceType], road.waypoints[road.waypoints.Count - 1].pos, Quaternion.identity);
                controller = spawnedCar.GetComponent<CarController>();

                controller.destination = road.waypoints.Count - 1;
                controller.moveDir = false;
            }

            controller.curRoad = road;
            controller.car.resources = invetrory;
        }
    }

    public void ChangeCarType(int change) { carType += change; Mathf.Clamp(carType, 0, carTypes.Count); }

    public void SetPointDestination(Points toPoint, int amount, int resourceType)
    {
        for (int i = 0; i < roadsAvailable.Count; i++)
        {
            if (roadsAvailable[i].begining == toPoint || roadsAvailable[i].ending == toPoint)
            {
                SpawnCar(roadsAvailable[i], amount, resourceType);
                break;
            }
        }
    }

    private float tick;

    void Update()
    {
        tick += Time.deltaTime;

        if(tick >= 1f)
        {
            tick = 0;

            if (producesSupply)
            {
                if (resources.Supply.amount + income < maxResources) resources.Supply.amount += income;
                else resources.Supply.amount = maxResources;
            }
            if (producesBridgeSupply)
            {
                if (resources.BridgeSupply.amount + income < maxResources) resources.BridgeSupply.amount += income;
                else resources.BridgeSupply.amount = maxResources;
            }
            if (producesWeapons)
            {
                if (resources.Weapons.amount + income < maxResources) resources.Weapons.amount += income;
                else resources.Weapons.amount = maxResources;
            }
        }
    }
}
