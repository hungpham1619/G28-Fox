using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
namespace VanHuy
{
    public class PanelLose : MonoBehaviour
    {
        public Button bttReplay;
        public Button bttHome;
        public TextMeshProUGUI txtScoreResult;
        // Start is called before the first frame update
        void Start()
        {
            txtScoreResult.text = LogicGame.ins.gameController.score.ToString();
            bttReplay.onClick.AddListener(OnClickReplay);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OnClickReplay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
