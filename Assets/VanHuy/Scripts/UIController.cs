using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace VanHuy
{
    public class UIController : MonoBehaviour
    {
        public Button bttPause;
        public GameObject panelPause;
        public GameObject panelWin;
        public GameObject panelLose;
        // Start is called before the first frame update
        void Start()
        {
            bttPause.onClick.AddListener(OnClickPause);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OnClickPause()
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }
        public void ShowPanelLose()
        {
            StartCoroutine(Delay_ShowPanelLose());
        }
        IEnumerator Delay_ShowPanelLose()
        {
            yield return new WaitForSeconds(1.5f);
            panelLose.SetActive(true);

        }
        public void ShowPanelWin()
        {
            StartCoroutine(DelayShowPanelWin());
        }
        IEnumerator DelayShowPanelWin()
        {
            yield return new WaitForSeconds(1f);
            panelWin.SetActive(true);
        }
    }
}
