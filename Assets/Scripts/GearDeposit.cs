using UnityEngine;

public class GearDeposit : MonoBehaviour
{
    public int gearsNeeded = 5;

    public string promptText = "Press 'E' to deposit gears";
    public string animationTrigger = "PickFruit";

    private int gearsDeposited = 0;
    private ResourceCounter resourceCounter;

    void Start()
    {
        resourceCounter = FindObjectOfType<ResourceCounter>();
    }

    public void Interact()
    {
        if (resourceCounter == null)
            return;

        if (resourceCounter.RemoveResource("Gears", 1))
        {
            gearsDeposited++;

            Debug.Log("Deposited " + gearsDeposited + "/" + gearsNeeded);

            if (gearsDeposited >= gearsNeeded)
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
        Time.timeScale = 0f;
    }
}