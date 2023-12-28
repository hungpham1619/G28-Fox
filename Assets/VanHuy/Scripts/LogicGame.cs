using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VanHuy
{
    public class LogicGame : MonoBehaviour
    {
        public static LogicGame ins;
        public GameController gameController;
        public GameSave gameSave;
        public UITop uiTop;
        public UIController uiController;
        private void Awake()
        {
            ins = this;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
