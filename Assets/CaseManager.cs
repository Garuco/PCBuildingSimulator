using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public GetMotherboard getMotherboard;
    private MotherboardManager mbManager;


    public List<string> getMissingComponents()
    {
        List<string> list = new List<string>();
        if (getMotherboard.getMotherboard() == null)
        {
            list.Add("No se ha localizado el componente: Tarjeta madre");
        }
        else
        {
            mbManager = getMotherboard.getMotherboard().GetComponent<MotherboardManager>();
            List<string> listComponents = mbManager.GetComponents();
            if (listComponents.Count > 0)
            {
                foreach (string missComp in listComponents)
                {
                    list.Add(missComp);
                }
            }
        }
        
        return list;
    }
}
