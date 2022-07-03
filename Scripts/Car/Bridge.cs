using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int materials = 0;
    [SerializeField] private int required = 1000;
    [SerializeField] private GameObject broken;
    [SerializeField] private GameObject fixedO;
    private bool eventTrigger1 = false;
    private bool eventTrigger2 = true;
    void Update()
    {
        if(materials == required && !eventTrigger1)
        {
            broken.SetActive(false);
            fixedO.SetActive(true);
            eventTrigger1 = true;
            eventTrigger1 = false;
        }
        else if(materials != required && !eventTrigger2)
        {
            broken.SetActive(true);
            fixedO.SetActive(false);
            eventTrigger1 = false;
            eventTrigger1 = true;
        }
    }
}