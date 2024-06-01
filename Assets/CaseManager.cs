using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public GetMotherboard getMotherboard;
    public MotherboardManager mbManager;
    List<string> list = new List<string>();
    public List<string> getMissingComponents()
    {
        list = new List<string>();
        if(getMotherboard.getMotherboard() == null)
        {
            list.Add("No se ha localizado el componente: Tarjeta madre");
        }
        else
        {
            List<string> listComponents = mbManager.getComponents();
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
