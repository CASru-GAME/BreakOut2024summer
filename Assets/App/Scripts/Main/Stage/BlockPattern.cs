using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using App.Main.Block;

namespace App.Main.Stage
{
    public class BlockPattern : MonoBehaviour
    {
        public int TargetBlockCount { get; private set; } = 0;
        public int NormalBlockCount { get; private set; } = 0;
        [SerializeField] private StageSystem _stageSystem;
        [SerializeField] private List<GameObject> _normalBlockPrefabList = default;
        [SerializeField] private List<GameObject> _targetBlockPrefabList = default;
        [SerializeField] private float _blockMaxX = 4f;
        [SerializeField] private float _blockMinX = -4f;
        [SerializeField] private float _blockMaxY = 3f;
        [SerializeField] private float _blockMinY = -2f;
        [SerializeField] private int _horizontalBlockCount = 6;
        [SerializeField] private int _verticalBlockCount = 5;



        ///<summary>
        ///ステージ、周回回数を引数にとり、それに対応したパターンでブロックを生成する。
        ///</summary>
        ///<exception cref="ArgumentException">ステージIDまたは周回回数が0未満になる場合に発生します。</exception>
        public void CreateBlocks(int stageId, int laps)
        {
            if (stageId <= 0)
            {
                throw new ArgumentException("stageID cannot be negative");
            }
            if (laps <= 0)
            {
                throw new ArgumentException("laps cannot be negative");
            }

            //stageIdから使用するパターンを、lapsから個数を決定
            StageBlockPatternData stageBlockPatternData = StageBlockPatternData.GetStageBlocks(stageId, laps);

            //各パターンを領域を基に整列、生成座標をブロック単位で登録
            stageBlockPatternData.DecideBlockPosition(_blockMaxX, _blockMinX, _blockMaxY, _blockMinY, _horizontalBlockCount, _verticalBlockCount);

            //ターゲットブロックの確定
            stageBlockPatternData.AssignTargetBlock();

            //生成
            InstantiateBlocks(stageBlockPatternData);
        }

        private void InstantiateBlocks(StageBlockPatternData stageBlockPatternData)
        {
            foreach (var blockPatternData in stageBlockPatternData.BlockPatternDataList)
            {
                foreach (var blockData in blockPatternData.BlockDataList)
                {
                    if (blockData.IsTarget)
                    {
                        TargetBlockCount++;
                        var blockInstance = Instantiate(_targetBlockPrefabList[blockData.Id], blockData.InstancePosition, Quaternion.identity);
                        blockInstance.GetComponent<Block.IBlock>().SetStage(_stageSystem);
                        blockInstance.transform.localScale = new Vector3((_blockMaxX - _blockMinX) / _horizontalBlockCount, (_blockMaxY - _blockMinY) / _verticalBlockCount, 1f);
                    }
                    else
                    {
                        NormalBlockCount++;
                        var blockInstance = Instantiate(_normalBlockPrefabList[blockData.Id], blockData.InstancePosition, Quaternion.identity);
                        blockInstance.GetComponent<Block.IBlock>().SetStage(_stageSystem);
                        blockInstance.transform.localScale = new Vector3((_blockMaxX - _blockMinX) / _horizontalBlockCount, (_blockMaxY - _blockMinY) / _verticalBlockCount, 1f);
                    }
                }
            }
        }

        class StageBlockPatternData
        {
            public List<BlockPatternData> BlockPatternDataList { get; private set; }

            public StageBlockPatternData(List<BlockPatternData> blockPatternDataList)
            {

                BlockPatternDataList = blockPatternDataList;
            }

            public void DecideBlockPosition(float blockMaxX, float blockMinX, float blockMaxY, float blockMinY, int horizontalBlockCount, int verticalBlockCount)
            {
                //各パターンの領域をそれぞれ取得
                var areaList = new List<List<(int x, int y)>>();
                foreach (var blockPatternData in BlockPatternDataList)
                {
                    areaList.Add(blockPatternData.GetAreaList());
                }

                //各パターンの全配置パターンをそれぞれ取得
                var AllPositionList = new List<List<List<(int x, int y)>>>();
                foreach (var patternArea in areaList)
                {
                    var PatternList = new List<List<(int x, int y)>>();
                    for (int i = 0; i < horizontalBlockCount; i++)
                    {
                        for (int j = 0; j < verticalBlockCount; j++)
                        {
                            bool isOutside = false;
                            foreach (var blockArea in patternArea)
                            {
                                if (blockArea.x + i < 0 || blockArea.x + i >= horizontalBlockCount || blockArea.y + j < 0 || blockArea.y + j >= verticalBlockCount)
                                {
                                    isOutside = true;
                                    break;
                                }
                            }
                            if (!isOutside)
                            {
                                var tmpPattern = new List<(int x, int y)>();
                                foreach (var blockArea in patternArea)
                                {
                                    tmpPattern.Add((blockArea.x + i, blockArea.y + j));
                                }
                                PatternList.Add(tmpPattern);
                            }
                        }
                    }

                    //パターンの順番をランダムにする
                    PatternList = PatternList.OrderBy(a => Guid.NewGuid()).ToList();
                    AllPositionList.Add(PatternList);
                }

                //バックトラックで座標候補を確定
                var selectedPositionList = new List<List<(int x, int y)>>();
                int attemptCount = 0, maxAttemptCount = 1000;
                FindPositionPattern(0, new List<List<(int x, int y)>>());
                void FindPositionPattern(int patternIndex, List<List<(int x, int y)>> decidedPositionList)
                {
                    //再帰回数が多すぎる場合の処理
                    if(attemptCount > maxAttemptCount)
                    {
                        Debug.Log("Too Many Attempts!!!!!!!!!!!!!!!!!");
                        return;
                    }

                    if (selectedPositionList.Count >= 1)
                    {
                        return;
                    }

                    if (patternIndex == BlockPatternDataList.Count)
                    {
                        selectedPositionList = new List<List<(int x, int y)>>(decidedPositionList);
                        return;
                    }

                    foreach (var positionList in AllPositionList[patternIndex])
                    {
                        bool isCovered = false;
                        foreach (var position in positionList)
                        {
                            foreach (var decidedPosition in decidedPositionList)
                            {
                                if (decidedPosition.Contains(position))
                                {
                                    isCovered = true;
                                }
                            }
                        }
                        if (isCovered)
                        {
                            continue;
                        }
                        decidedPositionList.Add(positionList);
                        FindPositionPattern(patternIndex + 1, decidedPositionList);
                        decidedPositionList.RemoveAt(decidedPositionList.Count - 1);
                    }
                }

                //各ブロックの生成座標を設定
                for (int i = 0; i < BlockPatternDataList.Count; i++)
                {   
                    int patternAreaCount = 0;
                    for (int j = 0; j < BlockPatternDataList[i].BlockDataList.Count; j++)
                    {
                        BlockPatternDataList[i].BlockDataList[j].InstancePosition = new Vector2
                     (CoordinatePosition(selectedPositionList[i][j + patternAreaCount].x, blockMaxX, blockMinX, horizontalBlockCount),
                     CoordinatePosition(selectedPositionList[i][j + patternAreaCount].y, blockMaxY, blockMinY, verticalBlockCount));
                     patternAreaCount += BlockPatternDataList[i].BlockDataList[j].IntAreaList.Count - 1;
                    }
                }

                float CoordinatePosition(int x, float maxX, float minX, int blockCount)
                {
                    return (maxX - minX) / blockCount * x + minX;
                }
            }

            public void AssignTargetBlock()
            {
                foreach (var blockPatternData in BlockPatternDataList)
                {
                    int changeableBlockCount = 0;
                    foreach (var blockData in blockPatternData.BlockDataList)
                    {
                        if (blockData.IsChangeable)
                        {
                            changeableBlockCount++;
                        }
                    }

                    var tmpPatternFlagList = new List<bool>();
                    for (int i = 0; i < changeableBlockCount; i++)
                    {
                        tmpPatternFlagList.Add(i < blockPatternData.TargetBlockCount);
                    }

                    tmpPatternFlagList = tmpPatternFlagList.OrderBy(a => Guid.NewGuid()).ToList();

                    foreach (var blockData in blockPatternData.BlockDataList)
                    {
                        if (blockData.IsChangeable)
                        {
                            blockData.IsTarget = tmpPatternFlagList[0];
                            tmpPatternFlagList.RemoveAt(0);
                        }
                        else
                        {
                            blockData.IsTarget = false;
                        }
                    }
                }
            }

            public int GetTargetBlockCount()
            {
                int targetBlockCount = 0;
                foreach (var blockPatternData in BlockPatternDataList)
                {
                    targetBlockCount += blockPatternData.TargetBlockCount;
                }
                return targetBlockCount;
            }

            //ステージの分岐の登録
            public static StageBlockPatternData GetStageBlocks(int stageId, int laps)
            {
                switch (stageId)
                {
                    case 1:
                        switch (laps)
                        {
                            case 1:
                                return Stage1_1stLap();
                            case 2:
                                return Stage1_2ndLap();
                            default:
                                return Stage1_MoreThan3rdLap();
                        }
                    case 2:
                        switch (laps)
                        {
                            case 1:
                                return Stage2_1stLap();
                            case 2:
                                return Stage2_2ndLap();
                            default:
                                return Stage2_MoreThan3rdLap();
                        }
                    case 3:
                        switch (laps)
                        {
                            case 1:
                                return Stage3_1stLap();
                            case 2:
                                return Stage3_2ndLap();
                            default:
                                return Stage3_MoreThan3rdLap();
                        }
                    case 4:
                        switch (laps)
                        {
                            case 1:
                                return Stage4_1stLap();
                            case 2:
                                return Stage4_2ndLap();
                            default:
                                return Stage4_MoreThan3rdLap();
                        }
                    case 5:
                        switch (laps)
                        {
                            case 1:
                                return Stage5_1stLap();
                            case 2:
                                return Stage5_2ndLap();
                            default:
                                return Stage5_MoreThan3rdLap();
                        }
                    case 6:
                        switch (laps)
                        {
                            case 1:
                                return Stage6_1stLap();
                            case 2:
                                return Stage6_2ndLap();
                            default:
                                return Stage6_MoreThan3rdLap();
                        }
                    case 7:
                        switch (laps)
                        {
                            case 1:
                                return Stage7_1stLap();
                            case 2:
                                return Stage7_2ndLap();
                            default:
                                return Stage7_MoreThan3rdLap();
                        }
                    case 8:
                        switch (laps)
                        {
                            case 1:
                                return Stage8_1stLap();
                            case 2:
                                return Stage8_2ndLap();
                            default:
                                return Stage8_MoreThan3rdLap();
                        }
                    case 9:
                        switch (laps)
                        {
                            case 1:
                                return Stage9_1stLap();
                            case 2:
                                return Stage9_2ndLap();
                            default:
                                return Stage9_MoreThan3rdLap();
                        }
                    case 10:
                        switch (laps)
                        {
                            case 1:
                                return Stage10_1stLap();
                            case 2:
                                return Stage10_2ndLap();
                            default:
                                return Stage10_MoreThan3rdLap();
                        }
                    case 11:
                        switch (laps)
                        {
                            case 1:
                                return Stage11_1stLap();
                            case 2:
                                return Stage11_2ndLap();
                            default:
                                return Stage11_MoreThan3rdLap();
                        }
                    case 12:
                        switch (laps)
                        {
                            case 1:
                                return Stage12_1stLap();
                            case 2:
                                return Stage12_2ndLap();
                            default:
                                return Stage12_MoreThan3rdLap();
                        }
                    case 13:
                        switch (laps)
                        {
                            case 1:
                                return Stage13_1stLap();
                            case 2:
                                return Stage13_2ndLap();
                            default:
                                return Stage13_MoreThan3rdLap();
                        }
                    case 14:
                        switch (laps)
                        {
                            case 1:
                                return Stage14_1stLap();
                            case 2:
                                return Stage14_2ndLap();
                            default:
                                return Stage14_MoreThan3rdLap();
                        }

                    case 15:
                        switch (laps)
                        {
                            case 1:
                                return Stage15_1stLap();
                            case 2:
                                return Stage15_2ndLap();
                            default:
                                return Stage15_MoreThan3rdLap();
                        }
                }
                return null;
            }

            //各ステージの周回ごとのブロックパターン: 使用するブロックパターン
            public static StageBlockPatternData Stage1_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,5),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,5)});
            }

            public static StageBlockPatternData Stage1_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage1_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage2_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                        BlockPatternData.Horizontal_10_Normal_Changeable(1,7),
                        BlockPatternData.Horizontal_10_Normal_Changeable(1,7),
                        BlockPatternData.Horizontal_10_Normal_Changeable(1,7),
                        BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,1),
                        BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(5,1)});
            }

            public static StageBlockPatternData Stage2_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage2_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage3_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                        BlockPatternData.Surroundings_3_3_Normal_Changeable(0,8),
                        BlockPatternData.Horizontal_10_Hard(3),
                        BlockPatternData.Horizontal_10_Hard(3),
                        BlockPatternData.Horizontal_10_Hard(3)});
            }

            public static StageBlockPatternData Stage3_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage3_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage4_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._1_HorizontalMove_Horizontal_3_Vertical_2_Normal_Changeable(0,4),
                    BlockPatternData._1_HorizontalMove_Horizontal_3_Vertical_2_Normal_Changeable(0,0),
                    BlockPatternData._1_VerticalMove_Vertical_3_Normal_Changeable(5,5),
                    BlockPatternData.Horizontal_10_Normal(2),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,2),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,0)});
            }

            public static StageBlockPatternData Stage4_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage4_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage5_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._1_DiagonalMove_Changeable(0,5),
                    BlockPatternData.Horizontal_2_Vertical_2_Normal_1_SquareMove_Changeable(6,2),
                    BlockPatternData.Horizontal_4_Hard_Chageable(0,1),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,1),
                    BlockPatternData.Horizontal_10_Normal(6),
                    BlockPatternData._1_HorizontalMove_Changeable(0,1),
                    BlockPatternData.Surroundings_3_3_Normal_Changeable(0,0),
                    BlockPatternData._1_VerticalMove_Changeable(0,2)});
            }

            public static StageBlockPatternData Stage5_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage5_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage6_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._1_ScreenOffLeftHorizontalMove_Changeable(1,1),
                    BlockPatternData._1_ScreenOffRightHorizontalMove_Changeable(0,1),
                    BlockPatternData.Horizontal_10_Normal(2),
                    BlockPatternData.Horizontal_10_Normal(2),
                    BlockPatternData.Horizontal_10_Normal(2),
                    BlockPatternData.Vertical_10_Hard(3),
                    BlockPatternData.Vertical_3_Hard_Changeable(0,0),
                    BlockPatternData.Vertical_3_Hard_Changeable(0,0)});
            }

            public static StageBlockPatternData Stage6_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage6_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }
            public static StageBlockPatternData Stage7_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._1_Heal_Surroundings_3_3_Normal_Changeable(0,2),
                    BlockPatternData._1_Heal_Changeable_Surroundings_3_3_Normal(0,2),
                    BlockPatternData._1_VerticalMove_Vertical_3_Normal_Changeable(0,0),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,1),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,1),
                    BlockPatternData._1_DiagonalMove_Changeable(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,1),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,1),
                    BlockPatternData.Horizontal_4_Normal(0,1),
                    BlockPatternData.Horizontal_4_Normal(0,1)});
            }
            public static StageBlockPatternData Stage7_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage7_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage8_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_3_Normal_1_HorizontalMoveHeal_Changeable(0,1),
                    BlockPatternData._1_VerticalMoveHeal_Changeable_2_3_Normal(0,0),
                    BlockPatternData._1_Heal_Surroundings_3_3_Normal_Changeable(0,0),
                    BlockPatternData.Horizontal_2_Vertical_2_Normal_1_SquareMove_Changeable(0,0),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,2),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,2),
                    BlockPatternData.Horizontal_4_Normal_Changeable(0,2),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,1),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,1)});
            }
            public static StageBlockPatternData Stage8_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage8_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage9_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Heal_Surroundings_5_5_Hard_Changeable(0,5),
                    BlockPatternData.Horizontal_3_Normal_1_HorizontalMoveHeal_Changeable(0,2),
                    BlockPatternData._1_DiagonalMove_Changeable(0,0),
                    BlockPatternData.Horizontal_4_Normal_Changeable(0,1),
                    BlockPatternData.Horizontal_4_Normal(0,1),
                    BlockPatternData.Horizontal_4_Normal(0,1),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,0)});
            }
            public static StageBlockPatternData Stage9_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage9_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage10_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,6),
                    BlockPatternData.Horizontal_10_Normal_Changeable(2,6),
                    BlockPatternData.Horizontal_10_Normal_Changeable(1,6),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,3),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,3),
                    });
            }
            public static StageBlockPatternData Stage10_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage10_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage11_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._1_HorizontalMove_Horizontal_3_Vertical_2_Normal_Changeable(0,2),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,1),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,1),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,1),
                    BlockPatternData._1_HorizontalMove_Changeable(0,0),
                    BlockPatternData.Horizontal_4_Normal(0,0),
                    BlockPatternData.Horizontal_4_Normal(0,0),
                    BlockPatternData.Vertical_9_Normal_Changeable(0),
                    BlockPatternData.Vertical_9_Normal_Changeable(0)});
            }
            public static StageBlockPatternData Stage11_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage11_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage12_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._1_Heal_Changeable_Surroundings_3_3_Normal(0,0),
                    BlockPatternData._1_HorizontalMove_Horizontal_3_Vertical_2_Normal_Changeable(0,0),
                    BlockPatternData.Vertical_3_Hard_Changeable(0,0),
                    BlockPatternData.Horizontal_10_Normal_Changeable(2,0),
                    BlockPatternData.Horizontal_10_Normal_Changeable(2,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,0),
                    BlockPatternData._1_HorizontalMove_Changeable(0,0)});
            }
            public static StageBlockPatternData Stage12_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage12_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage13_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData._2_VerticalMoveHard(4,7),
                    BlockPatternData.Horizontal_2_Vertical_2_Normal_1_SquareMove_Changeable(0,0),
                    BlockPatternData.Horizontal_10_Normal_Changeable(2,0),
                    BlockPatternData.Horizontal_4_Hard_Chageable(0,0),
                    BlockPatternData.Horizontal_4_Hard_Chageable(0,0),
                    BlockPatternData.Horizontal_4_Normal_Changeable(0,0),
                    BlockPatternData._1_Heal_Surroundings_3_3_Normal_Changeable(0,0),
                    BlockPatternData.Vertical_4_Normal_Changeable(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,0)});
            }
            public static StageBlockPatternData Stage13_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage13_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage14_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Vertical_10_Normal_Changeable(3,0),
                    BlockPatternData._1_VerticalMoveHeal_Vertical_10_Hard_Changeable(0),
                    BlockPatternData._1_VerticalMoveHeal_Vertical_10_Hard_Changeable(0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal(0,0),
                    BlockPatternData._1_DiagonalMove(0,0),
                    BlockPatternData._1_HorizontalMove_Changeable(0,0),
                    BlockPatternData._1_HorizontalMove_Changeable(0,0),
                    });
            }
            public static StageBlockPatternData Stage14_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage14_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }

            public static StageBlockPatternData Stage15_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_3_Normal_1_HorizontalMoveHeal_Changeable(0,0),
                    BlockPatternData._1_HorizontalMove_Horizontal_3_Vertical_2_Normal_Changeable(0,0),
                    BlockPatternData.Horizontal_10_Normal_Changeable(2,0),
                    BlockPatternData.Horizontal_4_Hard_Chageable(0,0),
                    BlockPatternData.Horizontal_4_Hard_Chageable(0,0),
                    BlockPatternData._1_VerticalMoveHeal_Changeable_2_3_Normal(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,0),
                    BlockPatternData.Horizontal_2_Vertical_3_Normal_Changeable(0,0)});
            }
            public static StageBlockPatternData Stage15_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    });
            }

            public static StageBlockPatternData Stage15_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{

                });
            }
        }

        class BlockPatternData
        {
            public int TargetBlockCount { get; private set; }
            public List<BlockData> BlockDataList { get; private set; }

            public BlockPatternData(int targetBlockCount, List<BlockData> blockDataList)
            {
                TargetBlockCount = targetBlockCount;
                BlockDataList = blockDataList;
            }

            public List<(int x, int y)> GetAreaList()
            {
                var areaList = new List<(int x, int y)>();
                foreach (var blockData in BlockDataList)
                {
                    foreach (var area in blockData.IntAreaList)
                    {
                        areaList.Add(area);
                    }
                }
                return areaList;
            }

            //ブロックパターンの登録: 使用するブロック、グリッド上の座標
            //左下から右上に向かってxyが増える
            //座標は左下詰めで書く

            public static BlockPatternData Horizontal_4_Normal(int x,int y)
            {
                return new BlockPatternData(0, new List<BlockData>{
                    BlockData.Normal(x, y),
                    BlockData.Normal(x + 1, y),
                    BlockData.Normal(x + 2, y),
                    BlockData.Normal(x + 3, y)});
            }

            public static BlockPatternData Horizontal_4_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(0, new List<BlockData>{
                    BlockData.Normal_Changeable(x, y),
                    BlockData.Normal_Changeable(x + 1, y),
                    BlockData.Normal_Changeable(x + 2, y),
                    BlockData.Normal_Changeable(x + 3, y)});
            }

            public static BlockPatternData Horizontal_10_Normal(int y)
            {
                return new BlockPatternData(0, new List<BlockData>{
                    BlockData.Normal(0, y),
                    BlockData.Normal(1, y),
                    BlockData.Normal(2, y),
                    BlockData.Normal(3, y),
                    BlockData.Normal(4, y),
                    BlockData.Normal(5, y),
                    BlockData.Normal(6, y),
                    BlockData.Normal(7, y),
                    BlockData.Normal(8, y),
                    BlockData.Normal(9, y),});
            }

            public static BlockPatternData Horizontal_10_Normal_Changeable(int targetBlockCount,int y)
            {
                return new BlockPatternData(targetBlockCount, new List<BlockData>{
                    BlockData.Normal_Changeable(0, y),
                    BlockData.Normal_Changeable(1, y),
                    BlockData.Normal_Changeable(2, y),
                    BlockData.Normal_Changeable(3, y),
                    BlockData.Normal_Changeable(4, y),
                    BlockData.Normal_Changeable(5, y),
                    BlockData.Normal_Changeable(6, y),
                    BlockData.Normal_Changeable(7, y),
                    BlockData.Normal_Changeable(8, y),
                    BlockData.Normal_Changeable(9, y),});
            }

            public static BlockPatternData Horizontal_10_Hard(int y)
            {
                return new BlockPatternData(0, new List<BlockData>{
                    BlockData.Hard(0, y),
                    BlockData.Hard(1, y),
                    BlockData.Hard(2, y),
                    BlockData.Hard(3, y),
                    BlockData.Hard(4, y),
                    BlockData.Hard(5, y),
                    BlockData.Hard(6, y),
                    BlockData.Hard(7, y),
                    BlockData.Hard(8, y),
                    BlockData.Hard(9, y),});
            }

            public static BlockPatternData Vertical_3_Hard_Changeable(int x,int y)
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Hard_Changeable(x, y),
                    BlockData.Hard_Changeable(x, y + 1),
                    BlockData.Hard_Changeable(x, y + 2)});
            }

            public static BlockPatternData Vertical_4_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Normal_Changeable(x, y),
                    BlockData.Normal_Changeable(x, y + 1),
                    BlockData.Normal_Changeable(x, y + 2),
                    BlockData.Normal_Changeable(x, y + 3)});
            }
            public static BlockPatternData Vertical_9_Normal_Changeable(int x)
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Normal_Changeable(x, 0),
                    BlockData.Normal_Changeable(x, 1),
                    BlockData.Normal_Changeable(x, 2),
                    BlockData.Normal_Changeable(x, 3),
                    BlockData.Normal_Changeable(x, 4),
                    BlockData.Normal_Changeable(x, 5),
                    BlockData.Normal_Changeable(x, 6),
                    BlockData.Normal_Changeable(x, 7),
                    BlockData.Normal_Changeable(x, 8)});
            }

            public static BlockPatternData Vertical_10_Normal_Changeable(int targetBlockCount,int x)
            {
                return new BlockPatternData(targetBlockCount, new List<BlockData>{
                    BlockData.Normal_Changeable(x, 0),
                    BlockData.Normal_Changeable(x, 1),
                    BlockData.Normal_Changeable(x, 2),
                    BlockData.Normal_Changeable(x, 3),
                    BlockData.Normal_Changeable(x, 4),
                    BlockData.Normal_Changeable(x, 5),
                    BlockData.Normal_Changeable(x, 6),
                    BlockData.Normal_Changeable(x, 7),
                    BlockData.Normal_Changeable(x, 8),
                    BlockData.Normal_Changeable(x, 9)});
            }

            public static BlockPatternData Vertical_10_Hard(int x)
            {
                return new BlockPatternData(0, new List<BlockData>{
                    BlockData.Hard(x, 1),
                    BlockData.Hard(x, 2),
                    BlockData.Hard(x, 4),
                    BlockData.Hard(x, 5),
                    BlockData.Hard(x, 6),
                    BlockData.Hard(x, 8),
                    BlockData.Hard(x, 9),
                    BlockData.Hard(x, 10)});
            }

            public static BlockPatternData _1_VerticalMoveHeal_Vertical_10_Hard_Changeable(int x)
            {
                return new BlockPatternData(3, new List<BlockData>{
                    BlockData.Hard_Changeable(x, 0),
                    BlockData.Hard_Changeable(x, 1),
                    BlockData.Hard_Changeable(x, 2),
                    BlockData.Hard_Changeable(x, 3),
                    BlockData.Hard_Changeable(x, 4),
                    BlockData.Hard_Changeable(x, 5),
                    BlockData.Hard_Changeable(x, 6),
                    BlockData.Hard_Changeable(x, 7),
                    BlockData.Hard_Changeable(x, 8),
                    BlockData.Hard_Changeable(x, 9),
                    BlockData.Vertical_Move_Heal(x + 1,4)
                    });
            }
            public static BlockPatternData Horizontal_2_Vertical_3_Normal(int x,int y)
            {
                return new BlockPatternData(0, new List<BlockData>{
                    BlockData.Normal_Changeable(x, y),
                    BlockData.Normal_Changeable(x, y + 1),
                    BlockData.Normal_Changeable(x, y + 2),
                    BlockData.Normal_Changeable(x + 1, y),
                    BlockData.Normal_Changeable(x + 1, y + 1),
                    BlockData.Normal_Changeable(x + 1, y + 2),});
            }

            public static BlockPatternData Horizontal_2_Vertical_3_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Normal(x, y),
                    BlockData.Normal(x, y + 1),
                    BlockData.Normal(x, y + 2),
                    BlockData.Normal(x + 1, y),
                    BlockData.Normal(x + 1, y + 1),
                    BlockData.Normal(x + 1, y + 2),});
            }

            public static BlockPatternData Horizontal_4_Hard_Chageable(int x,int y)
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Hard_Changeable(x, y),
                    BlockData.Hard_Changeable(x + 1, y),
                    BlockData.Hard_Changeable(x + 2, y),
                    BlockData.Hard_Changeable(x + 3, y)});
            }

            public static BlockPatternData Surroundings_3_3_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Normal(x, y),
                    BlockData.Normal(x + 1, y),
                    BlockData.Normal(x + 2, y),
                    BlockData.Normal(x, y + 1) ,
                    BlockData.Normal_Changeable(x + 1, y + 1),
                    BlockData.Normal(x + 2, y + 1),
                    BlockData.Normal(x, y + 2),
                    BlockData.Normal(x + 1, y + 2),
                    BlockData.Normal(x +2, y + 2)});
            }

            public static BlockPatternData _1_HorizontalMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Horizontal_Move_Changeable(x,y)});
            }

            public static BlockPatternData _1_HorizontalMove_Horizontal_3_Vertical_2_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Horizontal_Move_Changeable(x,y + 3),
                    BlockData.Normal(x - 1,y + 1),
                    BlockData.Normal(x,y+ 1),
                    BlockData.Normal(x + 1,y + 1),
                    BlockData.Normal(x - 1,y),
                    BlockData.Normal(x,y),
                    BlockData.Normal(x + 1,y)});
            }

            public static BlockPatternData _1_VerticalMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Vertical_Move_Changeable(x,y)});
            }

            public static BlockPatternData _1_VerticalMove_Vertical_3_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Vertical_Move_Changeable(x,y),
                    BlockData.Normal(x - 2,y + 1),
                    BlockData.Normal(x - 2,y),
                    BlockData.Normal(x - 2,y - 1)});
            }

            
            public static BlockPatternData _1_DiagonalMove(int x,int y)
            {
                return new BlockPatternData(0,new List<BlockData>{
                    BlockData.Diagonal_Move(x,y)});
            }

            public static BlockPatternData _1_DiagonalMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Diagonal_Move_Changeable(x,y)});
            }

            public static BlockPatternData Horizontal_2_Vertical_2_Normal_1_SquareMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Square_Move(x,y),
                    BlockData.Normal_Changeable(x + 1,y + 1),
                    BlockData.Normal_Changeable(x + 1,y + 2),
                    BlockData.Normal_Changeable(x + 2,y + 1),
                    BlockData.Normal_Changeable(x + 2,y + 2),});
            }

            public static BlockPatternData _1_ScreenOffLeftHorizontalMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Left_Screen_Off_Horizontal_Move_Changeable(x,y)});
            }
            public static BlockPatternData _1_ScreenOffRightHorizontalMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Right_Screen_Off_Horizontal_Move_Changeable(x,y)});
            }
            public static BlockPatternData _1_ScreenOffVerticalMove_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Screen_Off_Vertical_Move_Changeable(x,y)});
            }
            public static BlockPatternData _1_Heal_Surroundings_3_3_Normal_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Heal_Changeable(x,y),
                    BlockData.Normal_Changeable(x + 1,y - 1),
                    BlockData.Normal_Changeable(x + 1,y),
                    BlockData.Normal_Changeable(x + 1,y + 1),
                    BlockData.Normal_Changeable(x,y - 1),
                    BlockData.Normal_Changeable(x,y + 1),
                    BlockData.Normal_Changeable(x - 1,y - 1),
                    BlockData.Normal_Changeable(x - 1,y),
                    BlockData.Normal_Changeable(x - 1,y + 1)});
            }
            public static BlockPatternData _1_Heal_Changeable_Surroundings_3_3_Normal(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Heal_Changeable(x,y),
                    BlockData.Normal(x + 1,y - 1),
                    BlockData.Normal(x + 1,y),
                    BlockData.Normal(x + 1,y + 1),
                    BlockData.Normal(x,y - 1),
                    BlockData.Normal(x,y + 1),
                    BlockData.Normal(x - 1,y - 1),
                    BlockData.Normal(x - 1,y),
                    BlockData.Normal(x - 1,y + 1)});
            }
            public static BlockPatternData Horizontal_3_Normal_1_HorizontalMoveHeal_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Horizontal_Move_Heal_Changeable(x,y),
                    BlockData.Normal(x - 1,y - 1),
                    BlockData.Normal(x,y - 1),
                    BlockData.Normal(x + 1,y - 1),
                    BlockData.Normal(x - 1,y + 1),
                    BlockData.Normal(x,y + 1),
                    BlockData.Normal(x + 1,y + 1)});
            }

            public static BlockPatternData _1_VerticalMoveHeal_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Vertical_Move_Heal_Changeable(x,y)});
            }
            public static BlockPatternData _1_VerticalMoveHeal_Changeable_2_3_Normal(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Vertical_Move_Heal_Changeable(x,y),
                    BlockData.Normal(x - 1,y + 1),
                    BlockData.Normal(x - 1,y ),
                    BlockData.Normal(x - 1,y - 1),
                    BlockData.Normal(x + 1,y + 1),
                    BlockData.Normal(x + 1,y ),
                    BlockData.Normal(x + 1,y - 1)});
            }

            public static BlockPatternData Heal_Surroundings_5_5_Hard_Changeable(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.UpLeft_Heal(x - 2,y + 2),
                    BlockData.Normal(x - 2,y + 1),
                    BlockData.Normal(x - 2,y),
                    BlockData.Normal(x - 2,y - 1),
                    BlockData.DownLeft_Heal(x - 2,y - 2),
                    BlockData.Normal(x - 1,y + 2),
                    BlockData.Normal(x - 1,y + 1),
                    BlockData.Normal(x - 1,y),
                    BlockData.Normal(x - 1,y - 1),
                    BlockData.Normal(x - 1,y - 2),
                    BlockData.Normal(x,y + 2),
                    BlockData.Normal(x,y + 1),
                    BlockData.Hard_Changeable(x,y),
                    BlockData.Normal(x,y - 1),
                    BlockData.Normal(x,y - 2),
                    BlockData.Normal(x + 1,y + 2),
                    BlockData.Normal(x + 1,y + 1),
                    BlockData.Normal(x + 1,y),
                    BlockData.Normal(x + 1,y - 1),
                    BlockData.Normal(x + 1,y - 2),
                    BlockData.UpRight_Heal(x + 2,y + 2),
                    BlockData.Normal(x + 2,y + 1),
                    BlockData.Normal(x + 2,y),
                    BlockData.Normal(x + 2,y - 1),
                    BlockData.DownRight_Heal(x + 2,y - 2),});
            }

            public static BlockPatternData _2_VerticalMoveHard(int x,int y)
            {
                return new BlockPatternData(1,new List<BlockData>{
                    BlockData.Vertical_Move_Hard(x,y),
                    BlockData.Vertical_Move_Hard(x + 1,y)});
            }
        }

        class BlockData
        {
            public readonly int Id;
            public readonly (int x, int y) IntPosition;
            public IReadOnlyList<(int x, int y)> IntAreaList;
            public readonly bool IsChangeable;
            public Vector2 InstancePosition;
            public bool IsTarget;

            public BlockData(int id, int x, int y, List<(int x, int y)> intAreaList, bool isChangeable)
            {
                Id = id;
                IntPosition = (x, y);
                IntAreaList = intAreaList.AsReadOnly();
                IsChangeable = isChangeable;
            }

            //ブロックの登録: ブロックID、(ｘ、ｙ)(書き替えない)、ブロックの領域(被らないようにステージが組まれる)、ターゲットになり得るか
            public static BlockData Normal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(0, x, y, intAreaList, false);
            }

            public static BlockData Normal_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(0, x, y, intAreaList, true);
            }   

            public static BlockData Hard(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(1, x, y, intAreaList, false);
            }

            public static BlockData Hard_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(1, x, y, intAreaList, true);
            }

            public static BlockData Horizontal_Move(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x - 1,y),(x + 1,y),(x - 2,y),(x + 2,y) };
                return new BlockData(2, x, y, intAreaList, false);
            }

            public static BlockData Horizontal_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x - 1,y),(x + 1,y),(x - 2,y),(x + 2,y), };
                return new BlockData(2, x, y, intAreaList, true);
            }

            public static BlockData Vertical_Move(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x, y - 1),(x, y + 1),(x,y - 2),(x,y + 2) };
                return new BlockData(3, x, y, intAreaList, false);
            }

            public static BlockData Vertical_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x, y - 1),(x, y + 1),(x,y - 2),(x,y + 2) };
                return new BlockData(3, x, y, intAreaList, true);
            }

            public static BlockData Diagonal_Move(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x + 1, y),(x, y + 1),(x + 1,y + 1),(x - 1,y),(x,y - 1),(x - 1,y - 1) };
                return new BlockData(4, x, y, intAreaList, false);
            }

            public static BlockData Diagonal_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x + 1, y),(x, y + 1),(x + 1,y + 1),(x - 1,y),(x,y - 1),(x - 1,y - 1) };
                return new BlockData(4, x, y, intAreaList, true);
            }

            public static BlockData Square_Move(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x + 1, y),(x + 2, y),(x + 3,y),(x + 3,y + 1),(x + 3,y + 2),(x + 3,y + 3),(x + 2,y + 3),(x + 1,y + 3),(x,y + 3),(x,y + 2),(x,y + 1) };
                return new BlockData(5, x, y, intAreaList, false);
            }

            public static BlockData Square_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x + 1, y),(x, y + 1),(x + 1,y + 1),(x - 1,y),(x,y - 1),(x - 1,y - 1) };
                return new BlockData(5, x, y, intAreaList, true);
            }

            public static BlockData Left_Screen_Off_Horizontal_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x - 1,y),(x + 1,y),(x + 2,y),(9,y) };
                return new BlockData(6, x, y, intAreaList, true);
            }

            public static BlockData Right_Screen_Off_Horizontal_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x + 1,y),(x - 1,y),(x - 2,y),(-8,y) };
                return new BlockData(7, x, y, intAreaList, true);
            }

            public static BlockData Screen_Off_Vertical_Move_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x,y - 1),(x,y - 11) };
                return new BlockData(8, x, y, intAreaList, true);
            }

            public static BlockData Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(9, x, y, intAreaList, false);
            }

            public static BlockData Heal_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(9, x, y, intAreaList, true);
            }

            public static BlockData Horizontal_Move_Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x - 1,y),(x + 1,y),(x - 2,y),(x + 2,y) };
                return new BlockData(10, x, y, intAreaList, false);
            }

            public static BlockData Horizontal_Move_Heal_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x - 1,y),(x + 1,y),(x - 2,y),(x + 2,y), };
                return new BlockData(10, x, y, intAreaList, true);
            }

            public static BlockData Vertical_Move_Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x, y - 1),(x, y + 1),(x,y - 2),(x,y + 2) };
                return new BlockData(11, x, y, intAreaList, false);
            }

            public static BlockData Vertical_Move_Heal_Changeable(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x, y - 1),(x, y + 1),(x,y - 2),(x,y + 2) };
                return new BlockData(11, x, y, intAreaList, true);
            }

            public static BlockData UpLeft_Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x - 2,y) };
                return new BlockData(12, x, y, intAreaList, false);
            }

            public static BlockData DownLeft_Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(13, x, y, intAreaList, false);
            }

            public static BlockData DownRight_Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y) };
                return new BlockData(14, x, y, intAreaList, false);
            }

            public static BlockData UpRight_Heal(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x + 2,y),(x - 2,y + 2) };
                return new BlockData(15, x, y, intAreaList, false);
            }

            public static BlockData Vertical_Move_Hard(int x, int y)
            {
                var intAreaList = new List<(int x, int y)> { (x, y),(x,y - 1),(x,y - 2),(x,y + 1),(x,y + 2),(x - 4,y),(x + 4,y) };
                return new BlockData(16, x, y, intAreaList, false);
            }
        }
    }
}