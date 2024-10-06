using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Main.Player;
using App.Static;

namespace App.Main.Stage
{
    public class StageAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject back_Wall;
        [SerializeField] private GameObject back_Game;
        [SerializeField] private List<Sprite> back_Wall_SpriteList;
        [SerializeField] private List<RuntimeAnimatorController> back_Game_AnimatorControllerList;
        [SerializeField] private Text stageText;
        [SerializeField] private PlayerDatastore playerDatastore = default;
        private int latestHP;
        private List<GameObject> heartPrefabList = new List<GameObject>();
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private GameObject heartPos;
        [SerializeField] private GameObject heartBreakEffectPrefab;
        [SerializeField] private Canvas canvas_Main;

        private void Start()
        {
            int stageId = GetComponent<StageSystem>().CurrentStageNumberID;
            back_Wall.GetComponent<SpriteRenderer>().sprite = back_Wall_SpriteList[stageId - 1];
            back_Game.GetComponent<Animator>().runtimeAnimatorController = back_Game_AnimatorControllerList[stageId - 1];
            stageText.text = "このステージ  " + stageId;
        }

        private void CreateHearts()
        {
            if(playerDatastore.Parameter == null)
            {
                return;
            }

            latestHP = StatisticsDatastore._remainingLive;
            for(int i = 0; i < playerDatastore.Parameter.Live.MaxValue; i++)
            {
                var newHeartPrefab = Instantiate(heartPrefab, heartPos.transform.position, Quaternion.identity);
                newHeartPrefab.transform.SetParent(canvas_Main.transform);
                newHeartPrefab.transform.localScale = heartPos.transform.localScale;
                newHeartPrefab.transform.localPosition += new Vector3(heartPos.transform.localScale.x * heartPos.GetComponent<RectTransform>().sizeDelta.x * i, 0.0f, 0.0f);
                heartPrefabList.Add(newHeartPrefab);
                if(i >= latestHP)
                {
                    newHeartPrefab.SetActive(false);
                }
            }
        }

        private void Update()
        {
            if(heartPrefabList.Count == 0)
            {
                CreateHearts();
                return;
            }

            int currentHP = playerDatastore.Parameter.Live.CurrentValue;
            if(latestHP > currentHP)
            {
                for(int i = currentHP; i < latestHP; i++)
                {
                    heartPrefabList[i].SetActive(false);
                    var newHeartBreakEffectPrefab = Instantiate(heartBreakEffectPrefab, heartPrefabList[i].transform.position, Quaternion.identity);
                    newHeartBreakEffectPrefab.transform.SetParent(canvas_Main.transform);
                    newHeartBreakEffectPrefab.transform.localScale = heartPos.transform.localScale;
                }
            }
            else if(latestHP < currentHP)
            {
                for(int i = latestHP; i < currentHP; i++)
                {
                    heartPrefabList[i].SetActive(true);
                }
            }
            latestHP = currentHP;
        }
    }
}