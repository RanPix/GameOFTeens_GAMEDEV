using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string name { get; }
    public int amount { get; set; } = 0;

    public Resource(string name)
    {
        this.name = name;
    }

    public static Dictionary<string, Resource> ResourceType = new Dictionary<string, Resource>()
    {
        ["Supply"]       = new Resource("Supply"),
        ["BridgeSupply"] = new Resource("BridgeSupply"),
        ["Civilians"]    = new Resource("Civilians"),
        ["Weapons"]      = new Resource("Weapons")
    };

    public class Resources
    {
        public Resource? Supply = new Resource("Supply");
        public Resource? BridgeSupply = new Resource("BridgeSupply");
        public Resource? Civilians = new Resource("Civilians");
        public Resource? Weapons = new Resource("Weapons");

        public Resources() { }

        public Resources(int carType)
        {
            switch (carType)
            {
                case 0:
                    Supply = new Resource("Supply");// ResourceType["Supply"];
                    break;
                case 1:
                    BridgeSupply = new Resource("BridgeSupply");//ResourceType["BridgeSupply"];
                    break;
                case 2:
                    Civilians = new Resource("Civilians");//ResourceType["Civilians"];
                    break;
                case 3:
                    Weapons = new Resource("Weapons");//ResourceType["Weapons"];
                    break;
                default:
                    throw new System.NullReferenceException("Which resource?");
            }
        }

        public Resources(int carType, int amount)
        {
            switch (carType)
            {
                case 0:
                    Supply = new Resource("Supply"); //ResourceType["Supply"];
                    Supply.amount = amount;
                    break;
                case 1:
                    BridgeSupply = new Resource("BridgeSupply"); //ResourceType["BridgeSupply"];
                    BridgeSupply.amount = amount;
                    break;
                case 2:
                    Civilians = new Resource("Civilians"); //ResourceType["Civilians"];
                    Civilians.amount = amount;
                    break;
                case 3:
                    Weapons = new Resource("Weapons"); //ResourceType["Weapons"];
                    Weapons.amount = amount;
                    break;
                default:
                    throw new System.NullReferenceException("Which resource?");
            }
        }
    }
}