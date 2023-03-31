using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class LeftElbowModelChanger : MonoBehaviour
    {
        public List<GameObject> elbowModels;

        private void Awake()
        {
            GetAllElbowModels();
        }

        private void GetAllElbowModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                elbowModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllElbowModels()
        {
            foreach (GameObject elbowModel in elbowModels)
            {
                elbowModel.SetActive(false);
            }
        }

        public void EquipElbowModelByName(string elbowName)
        {
            for (int i = 0; i < elbowModels.Count; i++)
            {
                if (elbowModels[i].name == elbowName)
                {
                    elbowModels[i].SetActive(true);
                }
            }
        }
    }
}

