using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class LowerLeftArmModelChanger : MonoBehaviour
    {
        public List<GameObject> handModels;

        private void Awake()
        {
            GetAllHandModels();
        }

        private void GetAllHandModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                handModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllHandModels()
        {
            foreach (GameObject handModel in handModels)
            {
                handModel.SetActive(false);
            }
        }

        public void EquipHandModelByName(string handName)
        {
            for (int i = 0; i < handModels.Count; i++)
            {
                if (handModels[i].name == handName)
                {
                    handModels[i].SetActive(true);
                }
            }
        }

    }

}

