using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class ChestModelChanger : MonoBehaviour
    {
        public List<GameObject> chestModels;

        private void Awake()
        {
            GetAllChestModels();
        }

        private void GetAllChestModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                chestModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllChestModels()
        {
            foreach (GameObject chestModel in chestModels)
            {
                chestModel.SetActive(false);
            }
        }

        public void EquipChestModelByName(string chestName)
        {
            for (int i = 0; i < chestModels.Count; i++)
            {
                if (chestModels[i].name == chestName)
                {
                    chestModels[i].SetActive(true);
                }
            }
        }

    }

}

