using UnityEngine;
using TMPro;

public class GearDeposit : MonoBehaviour
{
    public int gearsNeeded = 5;

    public string promptText = "Press 'E' to deposit gears";
    public string animationTrigger = "PickFruit";

    public TMP_Text winText;

    private int gearsDeposited = 0;
    private ResourceCounter resourceCounter;

    void Start()
    {
        resourceCounter = FindObjectOfType<ResourceCounter>();

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if (resourceCounter == null)
            return;

        if (resourceCounter.RemoveResource("Gears", 1))
        {
            gearsDeposited++;

            Debug.Log("Deposited " + gearsDeposited + "/" + gearsNeeded);

            // Win after 5 gears have been deposited
            if (gearsDeposited >= 5)
            {
                WinGame();
            }
        }
        else
        {
            Debug.Log("You don't have any gears.");
        }
    }

    void WinGame()
    {
        Debug.Log("You Win!");

        if (winText != null)
        {
            winText.text = "You Win!";
            winText.gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }
}