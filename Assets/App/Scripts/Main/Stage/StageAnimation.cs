using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Main.Player;
using App.Static;
using App.Main.Item;
using App.ScriptableObjects;
using App.Main.Player.Perk;
using App.Common;

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
        [SerializeField] private RectMask2D comboGaugeMask;

        [SerializeField] private PlayerDatastore _playerDatastore;
        private int latestHP;
        private List<GameObject> heartPrefabList = new List<GameObject>();
        [SerializeField] private GameObject _heartPrefab;
        [SerializeField] private GameObject _heartPos;
        [SerializeField] private GameObject _heartBreakEffectPrefab;
        [SerializeField] private Canvas _canvas_Main;
        [SerializeField] private Canvas _canvas_Perk;
        [SerializeField] private ProcessSystem _processSystem;
        [SerializeField] private ItemTable _itemTable;
        [SerializeField] private List<int> _UseItemIdList = new List<int>();
        [SerializeField] private GameObject _itemPanelPrefab;
        [SerializeField] private GameObject _itemGaugePrefab;
        [SerializeField] private GameObject _itemPanelPos;
        [SerializeField] private RectMask2D perkGaugeMask;
        private List<(int id, GameObject panelPrefab, GameObject gaugePrefab)> _itemList = new List<(int id, GameObject panelPrefab, GameObject gaugePrefab)>();
        private List<(int id, GameObject panelPrefab, GameObject gaugePrefab)> itemShowList = new List<(int id, GameObject panelPrefab, GameObject gaugePrefab)>();
        private List<Vector3> _itemPanelPosList = new List<Vector3>();
        [SerializeField] private GameObject _ownedPerkPanelPrefab;
        [SerializeField] private GameObject _ownedPerkPanelPos;
        [SerializeField] private GameObject _ownedPerkPanel_ShowPos;
        [SerializeField] private GameObject _ownedPerkPanelTextPrefab;
        [SerializeField] private GameObject _ownedPerkPanelTextPos;
        [SerializeField] private GameObject _ownedPerkPanelText_ShowPos;
        [SerializeField] private GameObject perkListPanel;
        private List<(int id, GameObject panelPrefab, GameObject textPrefab, GameObject Panel_ShowPrefab, GameObject text_ShowPrefab)> ownedPerkPanelList = new List<(int id, GameObject panelPrefab, GameObject textPrefab, GameObject Panel_ShowPrefab, GameObject text_ShowPrefab)>();
        private string _ownedPerkPanelDefaultText;
        private List<(int id, int stackCount)> ownedPerkIntList = new List<(int id, int stackCount)>();
        [SerializeField] private SpriteData _spriteData;
        [SerializeField] private StageSystem _stageSystem;

        private void Start()
        {
            int worldId = GetComponent<StageSystem>().CurrentWorldNumberID;
            int stageId = GetComponent<StageSystem>().CurrentStageNumberID;
            _back_Wall.GetComponent<SpriteRenderer>().sprite = _spriteData.GetBackWallSprite(worldId);
            _back_Game.GetComponent<Animator>().runtimeAnimatorController = _back_Game_AnimatorControllerList[worldId - 1];
            stageText.text = string.Format(stageText.text, stageId);
            _catDefaultText = catText.text;
            _timeDefaultText = timeText.text;
            _comboDefaultText = comboText.text;
            _ownedPerkPanelDefaultText = _ownedPerkPanelTextPrefab.GetComponent<Text>().text;
            CreateItemPanels();
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

        private void CreateItemPanels()
        {
            for(int i = 0; i < _UseItemIdList.Count; i++)
            {
                _itemPanelPosList.Add(_itemPanelPos.transform.localPosition + new Vector3(_itemPanelPos.transform.localScale.x * _itemPanelPos.GetComponent<RectTransform>().sizeDelta.x * i, 0.0f, 0.0f));
                var newItemPanelPrefab = Instantiate(_itemPanelPrefab, _itemPanelPos.transform.position, Quaternion.identity);
                newItemPanelPrefab.transform.SetParent(_canvas_Main.transform);
                newItemPanelPrefab.transform.localScale = _itemPanelPos.transform.localScale;
                newItemPanelPrefab.GetComponent<Image>().sprite = _spriteData.GetUseItemSprite(i);
                newItemPanelPrefab.SetActive(false);
                var newGaugePrefab = Instantiate(_itemGaugePrefab, _itemPanelPos.transform.position, Quaternion.identity);
                newGaugePrefab.transform.SetParent(_canvas_Main.transform);
                newGaugePrefab.transform.localScale = _itemPanelPos.transform.localScale;
                newGaugePrefab.GetComponent<Image>().sprite = _spriteData.GetUseItemSprite(i);
                newGaugePrefab.SetActive(false);
                _itemList.Add((_UseItemIdList[i], newItemPanelPrefab, newGaugePrefab));
            }
        }

        private void CreateOwnedPerkPanels()
        {
            if(_playerDatastore.Parameter == null)
            {
                return;
            }

            for(int i = 0; i < _playerDatastore.PerkSystem.PerkList.AllPerkList.Length - 1; i++)
            {
                var newOwnedPerkPanelPrefab = Instantiate(_ownedPerkPanelPrefab, _ownedPerkPanelPos.transform.position, Quaternion.identity);
                newOwnedPerkPanelPrefab.transform.SetParent(_canvas_Main.transform);
                newOwnedPerkPanelPrefab.transform.localScale = _ownedPerkPanelPos.transform.localScale;
                newOwnedPerkPanelPrefab.transform.localPosition += 
                new Vector3(_ownedPerkPanelPos.transform.localScale.x * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.x * (i % 7),
                -_ownedPerkPanelPos.transform.localScale.y * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.y * (i / 7), 0.0f);
                newOwnedPerkPanelPrefab.SetActive(false);
                var newOwnedPerkPanelTextPrefab = Instantiate(_ownedPerkPanelTextPrefab, _ownedPerkPanelTextPos.transform.position, Quaternion.identity);
                newOwnedPerkPanelTextPrefab.transform.SetParent(_canvas_Main.transform);
                newOwnedPerkPanelTextPrefab.transform.localScale = _ownedPerkPanelTextPos.transform.localScale;
                newOwnedPerkPanelTextPrefab.transform.localPosition += 
                new Vector3(_ownedPerkPanelPos.transform.localScale.x * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.x * (i % 7),
                -_ownedPerkPanelPos.transform.localScale.y * _ownedPerkPanelPos.GetComponent<RectTransform>().sizeDelta.y * (i / 7), 0.0f);
                newOwnedPerkPanelTextPrefab.SetActive(false);  

                var newOwnedPerkPanel_ShowPrefab = Instantiate(_ownedPerkPanelPrefab, _ownedPerkPanel_ShowPos.transform.position, Quaternion.identity);
                newOwnedPerkPanel_ShowPrefab.transform.SetParent(_canvas_Perk.transform);
                newOwnedPerkPanel_ShowPrefab.transform.localScale = _ownedPerkPanel_ShowPos.transform.localScale;
                newOwnedPerkPanel_ShowPrefab.transform.localPosition +=
                new Vector3(_ownedPerkPanel_ShowPos.transform.localScale.x * _ownedPerkPanel_ShowPos.GetComponent<RectTransform>().sizeDelta.x * (i % 7),
                -_ownedPerkPanel_ShowPos.transform.localScale.y * _ownedPerkPanel_ShowPos.GetComponent<RectTransform>().sizeDelta.y * (i / 7), 0.0f);
                newOwnedPerkPanel_ShowPrefab.transform.SetParent(perkListPanel.transform);
                newOwnedPerkPanel_ShowPrefab.SetActive(false);
                var newOwnedPerkPanelText_ShowPrefab = Instantiate(_ownedPerkPanelTextPrefab, _ownedPerkPanelText_ShowPos.transform.position, Quaternion.identity);
                newOwnedPerkPanelText_ShowPrefab.transform.SetParent(_canvas_Perk.transform);
                newOwnedPerkPanelText_ShowPrefab.transform.localScale = _ownedPerkPanelText_ShowPos.transform.localScale;
                newOwnedPerkPanelText_ShowPrefab.transform.localPosition +=
                new Vector3(_ownedPerkPanel_ShowPos.transform.localScale.x * _ownedPerkPanel_ShowPos.GetComponent<RectTransform>().sizeDelta.x * (i % 7),
                -_ownedPerkPanel_ShowPos.transform.localScale.y * _ownedPerkPanel_ShowPos.GetComponent<RectTransform>().sizeDelta.y * (i / 7), 0.0f);
                newOwnedPerkPanelText_ShowPrefab.transform.SetParent(perkListPanel.transform);
                newOwnedPerkPanelText_ShowPrefab.SetActive(false);

                ownedPerkPanelList.Add((-1, newOwnedPerkPanelPrefab, newOwnedPerkPanelTextPrefab, newOwnedPerkPanel_ShowPrefab, newOwnedPerkPanelText_ShowPrefab));
            }

            for(int i = 0; i < StatisticsDatastore._totalAquiredPerkList.Count; i++)
            {
                ownedPerkIntList.Add((StatisticsDatastore._totalAquiredPerkList[i].id, StatisticsDatastore._totalAquiredPerkList[i].stackCount));
                SetNewPerkPanel(StatisticsDatastore._totalAquiredPerkList[i].id);
                SetPerkCountText(StatisticsDatastore._totalAquiredPerkList[i].id, StatisticsDatastore._totalAquiredPerkList[i].stackCount);
            }
        }

        private void Update()
        {
            if(heartPrefabList.Count == 0)
            {
                CreateHearts();
                return;
            }

            if(ownedPerkPanelList.Count == 0)
            {
                CreateOwnedPerkPanels();
                return;
            }

            UpdateHearts();
            UpdateCat();
            UpdateTime();
            UpdateItems();
            UpdateCombo();
            UpdateEXP();
            UpdatePerks();
        }

        private void UpdateHearts()
        {
            int newHP = _playerDatastore.Parameter.Live.CurrentValue;
            if(latestHP > newHP)
            {
                for(int i = newHP; i < latestHP; i++)
                {
                    heartPrefabList[i].SetActive(false);
                    var newHeartBreakEffectPrefab = Instantiate(_heartBreakEffectPrefab, heartPrefabList[i].transform.position, Quaternion.identity);
                    newHeartBreakEffectPrefab.transform.SetParent(_canvas_Main.transform);
                    newHeartBreakEffectPrefab.transform.localScale = _heartPos.transform.localScale;
                }
            }
            else if(latestHP < newHP)
            {
                for(int i = latestHP; i < newHP; i++)
                {
                    heartPrefabList[i].SetActive(true);
                }
            }
            latestHP = newHP;
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
                comboGaugeMask.padding = new Vector4(0.0f, 0.0f, 0.0f, comboGaugeMask.transform.localScale.y * comboGaugeMask.GetComponent<RectTransform>().sizeDelta.y);
            }
            else
            {
                comboGaugeMask.padding = new Vector4(0.0f, 0.0f, 0.0f, comboGaugeMask.transform.localScale.y * comboGaugeMask.GetComponent<RectTransform>().sizeDelta.y
                * ((float)_playerDatastore.GetComboResetCount() / _playerDatastore.GetMaxComboResetCount()));
            }
        }

        private void UpdateEXP()
        {
            float currentExp = _playerDatastore.GetCurrentExperiencePoint(_playerDatastore.GetExperiencePointValue());
            float maxExp = _playerDatastore.GetNeedExp(_playerDatastore.GetLevelValue());
            perkGaugeMask.padding = new Vector4(0.0f, 0.0f, 0.0f, perkGaugeMask.transform.localScale.y * perkGaugeMask.GetComponent<RectTransform>().sizeDelta.y
            * (1.0f-currentExp / maxExp));
        }

        private void UpdatePerks()
        {
            var newOwnedPerkList = _playerDatastore.PerkSystem.PerkList.OwnedPerkList;
            if(newOwnedPerkList.Count == 0) return;
            if(newOwnedPerkList.Count > ownedPerkIntList.Count) 
            {
                int id = newOwnedPerkList[newOwnedPerkList.Count - 1].GetId();
                SetNewPerkPanel(id);
                ownedPerkIntList.Add((id, 1));
            }
            for(int i = 0; i < newOwnedPerkList.Count; i++)
                if(newOwnedPerkList[i].GetStackCount() != ownedPerkIntList[i].stackCount)
                    SetPerkCountText(ownedPerkIntList[i].id, ownedPerkIntList[i].stackCount + 1);
        }

        private void SetNewPerkPanel(int id)
        {
            for(int i = 0; i < ownedPerkPanelList.Count; i++)
            {
                var selectedPanelPrefab = ownedPerkPanelList[i].panelPrefab;
                var selectedPanel_ShowPrefab = ownedPerkPanelList[i].Panel_ShowPrefab;
                
                if(selectedPanelPrefab.activeSelf == true) continue;
                selectedPanelPrefab.SetActive(true);
                selectedPanelPrefab.GetComponent<Image>().sprite = _spriteData.GetPerkSprite(id);
                selectedPanelPrefab.GetComponent<PerkIcon>().Initialize(id);
                selectedPanel_ShowPrefab.SetActive(true);
                selectedPanel_ShowPrefab.GetComponent<Image>().sprite = _spriteData.GetPerkSprite(id);
                selectedPanel_ShowPrefab.GetComponent<PerkIcon>().Initialize(id);

                ownedPerkPanelList[i] = (id, selectedPanelPrefab, ownedPerkPanelList[i].textPrefab, selectedPanel_ShowPrefab, ownedPerkPanelList[i].text_ShowPrefab);
                return;
            }
        }

        private void SetPerkCountText(int id, int stackCount)
        {
            for(int i = 0; i < ownedPerkPanelList.Count; i++)
            {
                var selectedTextPrefab = ownedPerkPanelList[i].textPrefab;
                var selectedText_ShowPrefab = ownedPerkPanelList[i].text_ShowPrefab;

                if(ownedPerkPanelList[i].id != id) continue;
                selectedTextPrefab.GetComponent<Text>().text = string.Format(_ownedPerkPanelDefaultText, stackCount);
                if(stackCount > 1) selectedTextPrefab.SetActive(true);
                selectedText_ShowPrefab.GetComponent<Text>().text = string.Format(_ownedPerkPanelDefaultText, stackCount);
                if(stackCount > 1) selectedText_ShowPrefab.SetActive(true);
                ownedPerkIntList[i] = (id, stackCount);
                return;
            }
        }
    }
}