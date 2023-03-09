using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform healthImageParent;
    [SerializeField] private GameObject healthImagePrefab;

    private List<HealthImage> healthImages = new List<HealthImage>();
    public void AddMissingHealthImages(int currentHealth)
    {
        int healthToAddOrRemove = currentHealth - healthImages.Count;

        if (healthToAddOrRemove > 0) for (int i = 0; i < healthToAddOrRemove; i++) AddHealthImage();
        else if (healthToAddOrRemove < 0) for (int i = 0; i < Mathf.Abs(healthToAddOrRemove); i++) RemoveHealthImage();
    }
    private void AddHealthImage()
    {
        GameObject newHealthImage = Instantiate(healthImagePrefab, healthImageParent);
        HealthImage healthImage = newHealthImage.GetComponent<HealthImage>();
        healthImages.Add(healthImage);
    }
    private void RemoveHealthImage()
    {
        HealthImage healthImage = healthImages[healthImages.Count - 1];
        healthImages.Remove(healthImage);
        healthImage.StartDelete();
    }
}
