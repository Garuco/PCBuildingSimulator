using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public Material deskMaterial;
    public Material blueScreenMaterial;
    public Material blackScreenMaterial;

    public TextMeshProUGUI monitorText;
    public TextMeshProUGUI CaraTriste;

    private MeshRenderer monitorRenderer;

    void Start()
    {
        // Encuentra el MeshRenderer en el objeto Screen
        monitorRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public void SetDeskMaterial()
    {
        monitorRenderer.material = deskMaterial;
        Debug.Log("Desk material set.");
        UpdateMonitorText("");
        CleanSadFace();
    }

    public void SetBlueScreenMaterial(List<string> messageList)
    {
        monitorRenderer.material = blueScreenMaterial;
        PrintSadFace();
        UpdateMonitorText("Se ha producido un problema con su PC, por favor revise que todos los componentes estén conectados e intente de nuevo. Información sobre el error:\n\n\t" + string.Join("\n\n\t", messageList));
    }

    public void SetBlackMaterial()
    {
        monitorRenderer.material = blackScreenMaterial;
        UpdateMonitorText("Por favor, coloque el case sobre la mesa");
        CleanSadFace();
    }

    public void UpdateMonitorText(string newText)
    {
        monitorText.text = newText;
    }

    public void PrintSadFace()
    {
        CaraTriste.text = ":(";
    }

    public void CleanSadFace()
    {
        CaraTriste.text = "";
    }
}
