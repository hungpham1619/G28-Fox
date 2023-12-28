using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace VanHuy
{
    public class PanelPause : MonoBehaviour
    {
        public Button bttExitPause;
        public Button bttReplay;
        public Button bttHome;
        // Start is called before the first frame update
        void Start()
        {
            bttExitPause.onClick.AddListener(OnClickBttExit);
            bttReplay.onClick.AddListener(OnClickReplay);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OnClickBttExit()
        {
            Time.timeScale = 1;
            LogicGame.ins.uiController.panelPause.SetActive(false);
        }
        public void OnClickReplay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
