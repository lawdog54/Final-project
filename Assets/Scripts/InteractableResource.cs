using UnityEngine;

public class InteractableResource : MonoBehaviour
{

    public string resourceName = "Apple";

    public int amountPerCollect = 1;
    public int usesRemaining = 1;

    public string promptText = "Press 'E' to collect";
    public string animationTrigger = "PickFruit";

    public bool destroyWhenEmpty = true;
    public float batteryTimeBonus = 15f;
    private GameTimer gameTimer;

    private ResourceCounter resourceCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourceCounter = FindObjectOfType<ResourceCounter>();
          gameTimer = FindObjectOfType<GameTimer>();
    }

    public void Interact()
    {
        if (usesRemaining <= 0)
        {
            return;
        }
        if (resourceCounter != null)
        {
            resourceCounter.AddResource(resourceName, amountPerCollect);
        }

           if (resourceName == "Batteries" && gameTimer != null)
    {
        gameTimer.AddTime(batteryTimeBonus);
    }
        usesRemaining--;
        if (usesRemaining <= 0 && destroyWhenEmpty)
        {
            gameObject.SetActive(false);
        }

        

    }



}
