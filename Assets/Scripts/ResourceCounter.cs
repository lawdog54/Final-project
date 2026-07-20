using UnityEngine;
using TMPro;
public class ResourceCounter : MonoBehaviour
{

    public TextMeshProUGUI DisksText;
    public TextMeshProUGUI oreText;
    public TextMeshProUGUI GearsText;
    private int Disks;
    private int ores;
    private int Gears;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        DisksText.text = "Disks: " + Disks;
        GearsText.text = "Gears: " + Gears;
        oreText.text = "Ores: " + ores;
    }

    public void AddResource(string resourceName, int amount)
    {
        if (resourceName == "Disks")
        {
            Disks += amount;
        }
        else if (resourceName == "Gears")
        {
           Gears += amount;

        if (Gears >= 5)
        {
            WinGame();
        }
        }
        else if (resourceName == "Ores")
        {
            ores += amount;
        }

        UpdateUI();
    }

    private void WinGame()
{
    Debug.Log("You Win!");

    // Stop the game
    Time.timeScale = 0f;

    // Later you can show a Win UI panel here
}


}
