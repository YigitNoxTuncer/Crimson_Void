using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NOX
{
    public class BackModelChanger : MonoBehaviour
    {
        public List<GameObject> backModels;

        private void Awake()
        {
            GetAllBackModels();
        }

        private void GetAllBackModels()
        {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++)
            {
                backModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllBackModels()
        {
            foreach (GameObject backModel in backModels)
            {
                backModel.SetActive(false);
            }
        }

        public void EquipBackModelByName(string backName)
        {
            for (int i = 0; i < backModels.Count; i++)
            {
                if (backModels[i].name == backName)
                {
                    backModels[i].SetActive(true);
                }
            }
        }
    }

}
