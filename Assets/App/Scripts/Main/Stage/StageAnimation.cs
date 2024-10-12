using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Main.Player;
using App.Static;
using App.Main.Item;
using App.ScriptableObjects;

namespace App.Main.Stage
{
    public class StageAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject _back_Wall;
        [SerializeField] private GameObject _back_Game;
        [SerializeField] private List<RuntimeAnimatorController> _back_Game_AnimatorControllerList;
        [SerializeField] private Text stageText;
        [SerializeField] private Text catText;
        private string _catDefaultText;
        [SerializeField] private Text timeText;
        private string _timeDefaultText;
        [SerializeField] private Text comboText;
        [SerializeField] private Text comboGaugeText;
        private string _comboDefaultText;
        [SerializeField] private RectMask2D comboTextMask;

        [SerializeField] private PlayerDatastore _playerDatastore;
        private int latestHP;
        private List<GameObject> heartPrefabList = new List<GameObject>();
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private GameObject _heartPos;
        [SerializeField] private GameObject _heartBreakEffectPrefab;
        [SerializeField] private Canvas _canvas_Main;
        [SerializeField] private ProcessSystem _processSystem;
        [SerializeField] private ItemTable _itemTable;
        [SerializeField] private List<int> _UseItemIdList = new List<int>();
        [SerializeField] private GameObject _itemPanelPrefab;
        [SerializeField] private GameObject _itemGaugePrefab;
        [SerializeField] private GameObject _itemPanelPos;
        private List<(int id, GameObject panelPrefab, GameObject gaugePrefab)> _itemList = new List<(int id, GameObject panelPrefab, GameObject gaugePrefab)>();
        private List<(int id, GameObject panelPrefab, GameObject gaugePrefab)> itemShowList = new List<(int id, GameObject panelPrefab, GameObject gaugePrefab)>();
        private List<Vector3> _itemPanelPosList = new List<Vector3>();
        [SerializeField] private SpriteData _spriteData;
        [SerializeField] private StageSystem _stageSystem;

        private void Start()
        {
            int stageId = GetComponent<StageSystem>().CurrentStageNumberID;
            _back_Wall.GetComponent<SpriteRenderer>().sprite = _spriteData.GetBackWallSprite(stageId);
            _back_Game.GetComponent<Animator>().runtimeAnimatorController = _back_Game_AnimatorControllerList[stageId - 1];
            stageText.text = string.Format(stageText.text, stageId);
            _catDefaultText = catText.text;
            _timeDefaultText = timeText.text;
            _comboDefaultText = comboText.text;
            CreatePanels();
        }

        private void CreateHearts()
        {
            if(_playerDatastore.Parameter == null)
            {
                return;
            }

            latestHP = StatisticsDatastore._remainingLive;
            for(int i = 0; i < _playerDatastore.Parameter.Live.MaxValue; i++)
            {
                var newHeartPrefab = Instantiate(_heartPrefab, _heartPos.transform.position, Quaternion.identity);
                newHeartPrefab.transform.SetParent(_canvas_Main.transform);
                newHeartPrefab.transform.localScale = _heartPos.transform.localScale;
                newHeartPrefab.transform.localPosition += new Vector3(_heartPos.transform.localScale.x * _heartPos.GetComponent<RectTransform>().sizeDelta.x * i, 0.0f, 0.0f);
                heartPrefabList.Add(newHeartPrefab);
                if(i >= latestHP)
                {
                    newHeartPrefab.SetActive(false);
                }
            }
        }

        private void CreatePanels()
        {
            for(int i = 0; i < _UseItemIdList.Count; i++)
            {
                _itemPanelPosList.Add(_itemPanelPos.transform.localPosition + new Vector3(_itemPanelPos.transform.localScale.x * _itemPanelPos.GetComponent<RectTransform>().sizeDelta.x * i, 0.0f, 0.0f));
                var newItemPanel = Instantiate(_itemPanelPrefab, _itemPanelPos.transform.position, Quaternion.identity);
                newItemPanel.transform.SetParent(_canvas_Main.transform);
                newItemPanel.transform.localScale = _itemPanelPos.transform.localScale;
                newItemPanel.GetComponent<Image>().sprite = _spriteData.GetUseItemSprite(i);
                newItemPanel.SetActive(false);
                var newGaugePrefab = Instantiate(_itemGaugePrefab, _itemPanelPos.transform.position, Quaternion.identity);
                newGaugePrefab.transform.SetParent(_canvas_Main.transform);
                newGaugePrefab.transform.localScale = _itemPanelPos.transform.localScale;
                newGaugePrefab.GetComponent<Image>().sprite = _spriteData.GetUseItemSprite(i);
                newGaugePrefab.SetActive(false);
                _itemList.Add((_UseItemIdList[i], newItemPanel, newGaugePrefab));
            }
        }

        private void Update()
        {
            if(heartPrefabList.Count == 0)
            {
                CreateHearts();
                return;
            }

            UpdateHearts();
            UpdateCat();
            UpdateTime();
            UpdateItems();
            UpdateCombo();
        }

        private void UpdateHearts()
        {
            int currentHP = _playerDatastore.Parameter.Live.CurrentValue;
            if(latestHP > currentHP)
            {
                for(int i = currentHP; i < latestHP; i++)
                {
                    heartPrefabList[i].SetActive(false);
                    var newHeartBreakEffectPrefab = Instantiate(_heartBreakEffectPrefab, heartPrefabList[i].transform.position, Quaternion.identity);
                    newHeartBreakEffectPrefab.transform.SetParent(_canvas_Main.transform);
                    newHeartBreakEffectPrefab.transform.localScale = _heartPos.transform.localScale;
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

        private void UpdateCat()
        {
            catText.text = string.Format(_catDefaultText, _stageSystem.GetTotalCat());
        }
        private void UpdateTime()
        {
            timeText.text = string.Format(_timeDefaultText, (int)_processSystem.GetRemainingTimerLimit());
        }

        private void UpdateItems()
        {
            var itemTimeTable = _itemTable.GetTimeList();

            for(int i = 0; i < itemTimeTable.Count; i++)
            {
                for(int j = 0; j < _itemList.Count; j++)
                {
                    if(itemTimeTable[i].id == _itemList[j].id)
                    {
                        if(itemTimeTable[i].time > 0f)
                        {
                            _itemList[j].panelPrefab.SetActive(true);
                            _itemList[j].gaugePrefab.SetActive(true);
                            _itemList[j].gaugePrefab.GetComponent<Image>().fillAmount = itemTimeTable[i].time / 20f;
                            if(!itemShowList.Contains(_itemList[j]))
                            {
                                itemShowList.Add(_itemList[j]);
                            }
                        }
                        else
                        {
                            _itemList[j].panelPrefab.SetActive(false);
                            _itemList[j].gaugePrefab.SetActive(false);
                            for(int k = 0; k < itemShowList.Count; k++)
                            {
                                if(itemShowList[k].id == _itemList[j].id)
                                {
                                    itemShowList.RemoveAt(k);
                                }
                            }
                        }
                    }
                }
            }

            for(int i = 0; i < itemShowList.Count; i++)
            {
                itemShowList[i].panelPrefab.transform.localPosition = _itemPanelPosList[i];
                itemShowList[i].gaugePrefab.transform.localPosition = _itemPanelPosList[i];
            }
        }

        private void UpdateCombo()
        {
            comboText.text = string.Format(_comboDefaultText, _playerDatastore.GetComboCount());
            comboGaugeText.text = comboText.text;
            if(_playerDatastore.GetComboCount() == 0)
            {
                comboTextMask.padding = new Vector4(0.0f, 0.0f, 0.0f, comboTextMask.transform.localScale.y * comboTextMask.GetComponent<RectTransform>().sizeDelta.y);
            }
            else
            {
                comboTextMask.padding = new Vector4(0.0f, 0.0f, 0.0f, comboTextMask.transform.localScale.y * comboTextMask.GetComponent<RectTransform>().sizeDelta.y
                * ((float)_playerDatastore.GetComboResetCount() / _playerDatastore.GetMaxComboResetCount()));
            }
        }
    }
}