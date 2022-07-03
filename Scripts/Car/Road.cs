using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint
{
    public Vector3 pos;
    public bool broken;
}

public class Road : MonoBehaviour
{
    public List<Waypoint> waypoints = new List<Waypoint>();

    public Points begining;
    public Points ending;

    public int getDestination(bool moveDir, int destination) => Mathf.Clamp(moveDir ? destination + 1 : destination - 1, 0, waypoints.Count - 1);
    public bool nextIsBroken(bool moveDir, int destination) => waypoints[destination].broken == true;
    public void fixRoad(bool moveDir, int destination) => waypoints[destination].broken = false; 


    public Points getEndPoint(bool moveDir)
    {
        if (moveDir) return ending;
        else return begining;
    }
    public bool onDestination(Vector3 position, int destination) => position == waypoints[destination].pos; 

}
