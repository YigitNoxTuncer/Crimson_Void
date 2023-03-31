using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class LeftShoulderModelChanger : MonoBehaviour
    {
        public List<GameObject> shoulderModels;

        private void Awake()
        {
            GetAllShoulderModels();
        }

        private void GetAllShoulderModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                shoulderModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllShoulderModels()
        {
            foreach (GameObject shoulderModel in shoulderModels)
            {
                shoulderModel.SetActive(false);
            }
        }

        public void EquipShoulderModelByName(string shoulderName)
        {
            for (int i = 0; i < shoulderModels.Count; i++)
            {
                if (shoulderModels[i].name == shoulderName)
                {
                    shoulderModels[i].SetActive(true);
                }
            }
        }
    }

}
