using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public TextMeshProUGUI promptText;

    private InteractableResource currentResource;
    private GearDeposit currentDeposit;

    private Animator animator;
    private bool isInteracting;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        FindNearbyResource();
    }

    private void FindNearbyResource()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange);

        currentResource = null;
        currentDeposit = null;

        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            InteractableResource resource = hit.GetComponent<InteractableResource>();

            if (resource != null)
            {
                float distance = Vector3.Distance(transform.position, resource.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentResource = resource;
                    currentDeposit = null;
                }

                continue;
            }

            GearDeposit deposit = hit.GetComponent<GearDeposit>();

            if (deposit != null)
            {
                float distance = Vector3.Distance(transform.position, deposit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    currentDeposit = deposit;
                    currentResource = null;
                }
            }
        }

        if (promptText == null)
            return;

        if (!isInteracting)
        {
            if (currentResource != null)
            {
                promptText.text = currentResource.promptText;
                promptText.gameObject.SetActive(true);
            }
            else if (currentDeposit != null)
            {
                promptText.text = currentDeposit.promptText;
                promptText.gameObject.SetActive(true);
            }
            else
            {
                promptText.gameObject.SetActive(false);
            }
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed)
            return;

        if ((currentResource == null && currentDeposit == null) || isInteracting)
            return;

        StartCoroutine(InteractRoutine());
    }

    private IEnumerator InteractRoutine()
    {
        isInteracting = true;

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }

        if (animator != null)
        {
            if (currentResource != null && !string.IsNullOrEmpty(currentResource.animationTrigger))
            {
                animator.SetTrigger(currentResource.animationTrigger);
            }
            else if (currentDeposit != null && !string.IsNullOrEmpty(currentDeposit.animationTrigger))
            {
                animator.SetTrigger(currentDeposit.animationTrigger);
            }
        }

        yield return new WaitForSeconds(.8f);

        if (currentResource != null)
        {
            currentResource.Interact();
        }
        else if (currentDeposit != null)
        {
            currentDeposit.Interact();
        }

        yield return new WaitForSeconds(.3f);

        isInteracting = false;
    }
}