using UnityEngine;


public abstract class Car
{
    public Vector3 position;
    public abstract float carSpeed { get; }

    public abstract string name { get; }

    public Road road { get; set; }

    public float goingPercent { get; protected set; }

    public static int itemCapacity { get; set; }
    public abstract Resource.Resources resources { get; set; }
}

public class SupplyCar : Car
{
    public override string name { get; } = "SupplyCar";

    public override float carSpeed { get; } = 0.5f;

    public static int itemCapacity { get; set; } = 100;
    public override Resource.Resources resources { get; set; } = new Resource.Resources(0);
}

public class BridgeSupplyCar : Car
{
    public override string name { get; } = "BridgeSupplyCar";

    public override float carSpeed { get; } = 0.4f;

    public static int itemCapacity { get; set; } = 200;
    public override Resource.Resources resources { get; set; } = new Resource.Resources(1);
}

public class CivilianCar : Car
{
    public override string name { get; } = "CivilianCar";

    public override float carSpeed { get; } = 0.6f;

    public static int itemCapacity { get; set; } = 6;
    public override Resource.Resources resources { get; set; } = new Resource.Resources(2);
}

public class ArmedCar : Car
{
    public override string name { get; } = "ArmedCar";

    public override float carSpeed { get; } = 0.3f;

    public static int itemCapacity { get; set; } = 30;
    public override Resource.Resources resources { get; set; } = new Resource.Resources(3);
}
