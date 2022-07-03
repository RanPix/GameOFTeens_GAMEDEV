using UnityEngine.UI;
using UnityEngine;

public class CanvasInGame : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    [SerializeField] private Button OpenCloseButton;
    [SerializeField] private Text OpenCloseText;

    [SerializeField] private Text civiliansAmount_text;
    [SerializeField] private Text buildingResourcesAmount_text;
    [SerializeField] private Text supplyAmount_text;
    [SerializeField] private Text weaponsAmount_text;

    [SerializeField] private Slider resAmountSlider;
    [SerializeField] private Text sliderInfo_text;

    private int civiliansAmount_int;
    private int buildingResourcesAmount_int;
    private int supplyAmount_int;
    private int weaponsAmount_int;

    private int resourceType;
    private int resourceAmount;

    [HideInInspector] public Points from;
    [HideInInspector] public Points to;

    bool Opened;

    public void OpenClosePanel()
    {
        if (Opened == true) Opened = false;
        else Opened = true;
    }

    public void SendCars()
    {
        print("yes");

        print(from == null);
        print(to == null);

        if (from != null && to != null)
        {
            print("yes yes");
            from.SetPointDestination(to, resourceAmount, resourceType);
        }
    }

    public void SetAmount(float value)
    {
        switch (resourceType)
        {
            case 0: resAmountSlider.maxValue = civiliansAmount_int; break;
            case 1: resAmountSlider.maxValue = buildingResourcesAmount_int; break;
            case 2: resAmountSlider.maxValue = supplyAmount_int; break;
            case 3: resAmountSlider.maxValue = weaponsAmount_int; break;
        }

        int valueI = Mathf.FloorToInt(value);

        print(valueI);

        sliderInfo_text.text = $"{valueI}";
        resourceAmount = Mathf.FloorToInt(valueI);
    }

    private void Update()
    {
        if(Opened == true)
        {
            OpenCloseText.text = "Закрити";
        }
        else
        {
            OpenCloseText.text = "Відкрити";
        }

        if (from != null)
        {
            supplyAmount_int = from.resources.Supply.amount;
            buildingResourcesAmount_int = from.resources.BridgeSupply.amount;
            civiliansAmount_int = from.resources.Civilians.amount;
            weaponsAmount_int = from.resources.Weapons.amount;

            supplyAmount_text.text = $"Гуманітарка: {supplyAmount_int}";
            buildingResourcesAmount_text.text = $"Буд. Мат.: {buildingResourcesAmount_int}";
            civiliansAmount_text.text = $"Цивільні: {civiliansAmount_int}";
            weaponsAmount_text.text = $"Зброя: {weaponsAmount_int}";
        }

        Panel.SetActive(Opened);
    }

    public void DropdownItemSelected(int index)
    {
        print(index);
        resourceType = index;
    }
}
