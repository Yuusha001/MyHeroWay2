using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyHeroWay
{
    public class SkillsControllerUI : MonoBehaviour
    {
        private UIManager uiManager;
        [ReadOnly]
        public Button[] skillsBtn;
        public void Initialize(UIManager _uiManager)
        {
            this.uiManager = _uiManager;
            foreach (var item in skillsBtn)
            {
                item.onClick.AddListener(() => SkillsHandler(item));
            }
        }

        private void SkillsHandler(Button owner)
        {
            foreach (var item in skillsBtn)
            {
                item.transform.GetChild(2).gameObject.SetActive((item == owner));
            }
            
        }

        [Button("GetReference")]
        private void GetReference()
        {
            skillsBtn = this.transform.GetComponentsInChildren<Button>();
        }
        
    }
}
