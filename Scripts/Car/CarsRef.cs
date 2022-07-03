using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cars", fileName = "Cars Reference", order = 0)]
public class CarsRef : ScriptableObject
{
    public List<GameObject> cars = new List<GameObject>();
}
