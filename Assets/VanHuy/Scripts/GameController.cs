using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
namespace VanHuy
{
    public class GameController : MonoBehaviour
    {
        public LayerMask layerMask;
        public GameObject vfxBoom;
        public GameObject vfxCoin;
        public GameObject vfxBox;
        public GameObject vfxSlow;
        public GameObject vfxPoison;
        public GameObject vfxFreeze;
        public Camera cam;
        public CinemachineImpulseSource impulseSource;
        public GameObject hammerPref;
        public LogicGame logicGame;
        public bool isEndTimeGame;
        public bool isDie;
        public bool isFreezeTime;
        public bool isX2Score;
        public bool isThor;
        public bool isPoison;
        public int indexLv;
        public int coin;
        public int score;
        public float timeGame;
        public List<LevelGame> listLevelGame;
        public List<FoxController> listFoxSpawn;
        public List<int> listTargetScores;
        public LevelGame levelGameSelect;
        RaycastHit2D hit;
        Vector2 posHit;
        float startTime;
        // Start is called before the first frame update
        private void Awake()
        {
            Time.timeScale = 1;
            LoadCoin();
            SpawnLevel();
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (isDie || isEndTimeGame)
            {
                return;
            }

            ClickHitScene();
        }
        public void LoadCoin()
        {
            coin = PlayerPrefs.GetInt(logicGame.gameSave.coinSave);
            logicGame.uiTop.SetCoin(0);
        }
        public void SpawnLevel()
        {
            LevelGame levelGameClone = Instantiate(listLevelGame[indexLv], new Vector3(0, 0, 0), Quaternion.identity);
            levelGameSelect = levelGameClone;
            timeGame = levelGameSelect.timeGame;
        }
        public void ClickHitScene()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time > startTime)
                {
                    startTime = 0.2f + Time.time; // 0.2s la thoi gian delay sau moi lan bam chuot
                    CreateRaycast();
                }
            }
        }
        private void CreateRaycast() // kiem tra xem co dap trung quai hay k 
        {
            posHit = cam.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.CircleCast(posHit, 0.2f, Vector2.zero, 0, layerMask);
            if (hit.collider != null) //Dap trung
            {
                CheckCollisonFox(); //Dap trung cao
                CheckCollisonHole(); // Dap trung lo cua cao nhung k co cao
            }
            else //Dap truot
            {
                SpawnHammer(0);
            }
        }
        void CheckCollisonFox()
        {
            FoxController foxController = hit.collider.GetComponent<FoxController>();
            if (hit.collider.CompareTag("Fox"))
            {
                SpawnHammer(1);
                if (isThor == false)
                {
                    foxController.ActionHitFox();
                }
                else
                {
                    for (int i = 0; i < listFoxSpawn.Count; i++)
                    {
                        listFoxSpawn[i].ActionHitFox();
                    }
                }
            }
        }
        void CheckCollisonHole()
        {
            if(hit.collider.CompareTag("hole"))
            {
                SpawnHammer(0);
            }    
        }
        void SpawnHammer(int id)   //id kiem tra xem dap trung doi tuong nao (dap cao hay dap ra ngoai )
        {
            //id = 1 la dap trung cao
            if(id == 1)
            {
                Instantiate(hammerPref, new Vector2(hit.transform.position.x,hit.transform.position.y+1), Quaternion.identity); //tao ra cay bua 
            }
            //id = 0 la dap ra ngoai
            else
            {
                Instantiate(hammerPref, new Vector2(posHit.x, posHit.y), Quaternion.identity); //tao ra cay bua 
            }
        }
        public void SpawnVfx(GameObject obj_,Transform point_)
        {
            StartCoroutine(DelaySpawnVFX(obj_, point_));
        }
        IEnumerator DelaySpawnVFX(GameObject obj_, Transform point_)
        {
            yield return new WaitForSeconds(0.025f);
            GameObject vfxClone = Instantiate(obj_, point_);
            Destroy(vfxClone, 1.5f);
        }
        public void ShakeCam(float force_)
        {
            impulseSource.GenerateImpulse(force_);
        }
        public void SetEndTime()
        {
            if (score >= listTargetScores[indexLv])
            {
                logicGame.uiController.ShowPanelWin();
            }
            else
            {
                logicGame.uiController.ShowPanelLose();
            }
        }
    }
}
