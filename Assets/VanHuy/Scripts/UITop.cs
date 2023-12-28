using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace VanHuy
{
    public class UITop : MonoBehaviour
    {
        public static UITop ins;
        public TextMeshProUGUI txtCoin;
        public Image imgCoinParent;
        public Image imgCoinPref;
        public Image imgTimeGameFill;
        public TextMeshProUGUI txtLevelScore;
        public TextMeshProUGUI txtCurrentScore;
        public TextMeshProUGUI txtX2Score;
        public TextMeshProUGUI txtThor;
        public TextMeshProUGUI txtCurrentScorePref;
        public TextMeshProUGUI txtScoreParent;
        public GameObject poisonScreen;
        public GameObject parentCoinSpawn;
        public LogicGame logicGame;
        float startTimeFreeze;
        float startTimeX2Score = 5;
        float startTimeThor = 5;
        float startTimePoisonScreen = 5;
        private void Awake()
        {
            ins = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            txtLevelScore.text ="Level Score: " + logicGame.gameController.listTargetScores[logicGame.gameController.indexLv].ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (logicGame.gameController.isDie)
            {
                return;
            }
            SetTimeX2Score();
            SetTimeThor();
            SetTimePoison();
            if (logicGame.gameController.isFreezeTime == false)
            {
                SetTimeGame();
            }
            else
            {
                SetFreezeTime();
            }
        }
        public void SetCoin(int coin_)
        {
            logicGame.gameController.coin += coin_;
            PlayerPrefs.SetInt(logicGame.gameSave.coinSave, logicGame.gameController.coin);
            txtCoin.text = logicGame.gameController.coin.ToString();
        }
        public void SpawnImgCoin(int n_,Transform pos_)
        {
            for (int i = 0; i < n_; i++)
            {
                Image imgCoinClone = Instantiate(imgCoinPref, parentCoinSpawn.transform);
                float a_ = i / 10f;
                imgCoinClone.transform.position = logicGame.gameController.cam.WorldToScreenPoint(pos_.position + new Vector3(Random.Range(-0.1f+a_, 0.1f + a_), Random.Range(-0.1f + a_, 0.1f + a_)));
                StartCoroutine(DelayMoveCoin(imgCoinClone));
            }
        }
        IEnumerator DelayMoveCoin(Image image_)
        {
            yield return new WaitForSeconds(0.4f);
            image_.transform.DOMove(imgCoinParent.transform.position, 0.85f).SetEase(Ease.InQuad).OnComplete(() => 
            {
                SetCoin(1);
                Destroy(image_.gameObject);
            });
        }

        public void SetTimeGame()
        {
            if (logicGame.gameController.isEndTimeGame == false)
            {
                logicGame.gameController.timeGame -= Time.deltaTime;
                if (logicGame.gameController.timeGame <= 0)
                {
                    logicGame.gameController.SetEndTime();
                    logicGame.gameController.isEndTimeGame = true;
                }
                imgTimeGameFill.fillAmount = logicGame.gameController.timeGame / logicGame.gameController.levelGameSelect.timeGame;
            }
        }
        public void SetFreezeTime()
        {
            startTimeFreeze += Time.deltaTime;
            if (startTimeFreeze >= 3)
            {
                startTimeFreeze = 0;
                logicGame.gameController.isFreezeTime = false;
            }
        }
        public void SetTimeX2Score()
        {
            if (logicGame.gameController.isX2Score)
            {
                startTimeX2Score -= Time.deltaTime;
                if (startTimeX2Score <= 0)
                {
                    startTimeX2Score = 5;
                    logicGame.gameController.isX2Score = false;
                    txtX2Score.text = "";
                }
                else
                {
                    txtX2Score.text = startTimeX2Score.ToString("F0");
                }
            }
        }
        public void SetTimeThor()
        {
            if (logicGame.gameController.isThor)
            {
                startTimeThor -= Time.deltaTime;
                if (startTimeThor <= 0)
                {
                    startTimeThor = 5;
                    logicGame.gameController.isThor = false;
                    txtThor.text = "";
                }
                else
                {
                    txtThor.text = startTimeThor.ToString("F0");
                }
            }
        }
        public void SetTimePoison()
        {
            if (logicGame.gameController.isPoison)
            {
                poisonScreen.SetActive(true);
                startTimePoisonScreen -= Time.deltaTime;
                if (startTimePoisonScreen <= 0)
                {
                    startTimePoisonScreen = 5;
                    poisonScreen.SetActive(false);
                    logicGame.gameController.isPoison = false;
                }
            }
        }
        public void SetTextScoreCurrent(int a_)
        {
            logicGame.gameController.score += a_;
            txtCurrentScore.text = logicGame.gameController.score.ToString();
        }
        public void SpawnTextScoreCurrent(int a_,Transform pos_)
        {
            TextMeshProUGUI txtScoreClone = Instantiate(txtCurrentScorePref, parentCoinSpawn.transform);
            txtScoreClone.text = "+"+a_;
            txtScoreClone.transform.position = logicGame.gameController.cam.WorldToScreenPoint(pos_.position + new Vector3(0.2f, 1.15f));
            StartCoroutine(DelayTextScoreCurrent(a_, txtScoreClone));
        }
        IEnumerator DelayTextScoreCurrent(int a_,TextMeshProUGUI txt_)
        {
            yield return new WaitForSeconds(0.4f);
            txt_.transform.DOMove(txtScoreParent.transform.position, 0.85f).SetEase(Ease.InQuad).OnComplete(() =>
            {
                SetTextScoreCurrent(a_);
                Destroy(txt_.gameObject);
            });
        }
    }
}
