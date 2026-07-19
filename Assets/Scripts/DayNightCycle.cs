using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public Light sun;

    public float fullDayLengthInSeconds = 120f;

    private float dayIntensity = 0.2f;

    private float nightIntensity = 0.05f;

    private Color dayAmbientLight = new Color(0.55f, 0.58f, 0.62f);

    private Color nightAmbientLight = new Color(0.1f, 0.12f, 0.018f);

    private Color daySkyTint = new Color(0.35f, 0.45f, 0.6f);

    private Color nightSkyTint = new Color(0.005f, 0.0065f, 0.01f);

    private float timeOfDay = 0.25f;

    public bool IsNight {get; private set;}




    // Update is called once per frame
    void Update()
    {
        if (sun == null)
        {
            return;
        }

        timeOfDay += Time.deltaTime / fullDayLengthInSeconds;
        if (timeOfDay >= 1f)
        {
            timeOfDay = 0f;
        }
        UpdateLighting();
        
    }

    private void UpdateLighting()
    {
        float sunAngle = timeOfDay * 360f - 90f;
        sun.transform.localRotation = Quaternion.Euler(sunAngle, 170f, 0f);

        float lightAmount = Mathf.Clamp01(Mathf.Sin(timeOfDay * Mathf.PI));
        sun.intensity = Mathf.Lerp(nightIntensity, dayIntensity, lightAmount);

        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightAmount);

        RenderSettings.skybox.SetColor("_SkyTint", Color.Lerp(nightSkyTint, daySkyTint, lightAmount));

        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetColor("_Tint", Color.Lerp(nightSkyTint, daySkyTint, lightAmount));
           RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(0.8f, 0.65f, lightAmount));
        }
        IsNight = lightAmount < 0.25f;
    }




}
