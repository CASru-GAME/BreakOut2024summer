using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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

            //ターゲット数を更新
            //TargetBlockCount = stageBlockPatternData.GetTargetBlockCount();
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
                    AllPositionList.Add(PatternList);
                }

                //バックトラックで座標候補を確定
                var candidatePositionList = new List<List<List<(int x, int y)>>>();
                FindPositionPattern(0, new List<List<(int x, int y)>>());
                void FindPositionPattern(int patternIndex, List<List<(int x, int y)>> decidedPositionList)
                {
                    if (patternIndex == BlockPatternDataList.Count)
                    {
                        candidatePositionList.Add(new List<List<(int x, int y)>>(decidedPositionList));
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

                //座標候補から1種類抽出
                UnityEngine.Random.InitState(DateTime.Now.Millisecond);
                List<List<(int x, int y)>> selectedPositionList = candidatePositionList[UnityEngine.Random.Range(0, candidatePositionList.Count)];

                //各ブロックの生成座標を設定
                for (int i = 0; i < BlockPatternDataList.Count; i++)
                {
                    for (int j = 0; j < BlockPatternDataList[i].BlockDataList.Count; j++)
                    {
                        BlockPatternDataList[i].BlockDataList[j].InstancePosition = new Vector2
                     (CoordinatePosition(selectedPositionList[i][j].x, blockMaxX, blockMinX, horizontalBlockCount),
                     CoordinatePosition(selectedPositionList[i][j].y, blockMaxY, blockMinY, verticalBlockCount));
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
                }
                return null;
            }

            //各ステージの周回ごとのブロックパターン: 使用するブロックパターン
            public static StageBlockPatternData Stage1_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Vertical_3_Changeable(),
                    BlockPatternData.Vertical_3_Changeable()});
            }

            public static StageBlockPatternData Stage1_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Vertical_3_Changeable(),
                    BlockPatternData.Vertical_3_Changeable(),
                    BlockPatternData.Vertical_3_Changeable()});
            }

            public static StageBlockPatternData Stage1_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Vertical_3_Changeable(),
                    BlockPatternData.Vertical_3_Changeable(),
                    BlockPatternData.Vertical_3_Changeable(),
                    BlockPatternData.Vertical_3_Changeable()});
            }

            public static StageBlockPatternData Stage2_1stLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_3_Changeable(),
                    BlockPatternData.Horizontal_3_Changeable()});
            }

            public static StageBlockPatternData Stage2_2ndLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_3_Changeable(),
                    BlockPatternData.Horizontal_3_Changeable(),
                    BlockPatternData.Horizontal_3_Changeable()});
            }

            public static StageBlockPatternData Stage2_MoreThan3rdLap()
            {
                return new StageBlockPatternData(new List<BlockPatternData>{
                    BlockPatternData.Horizontal_3_Changeable(),
                    BlockPatternData.Horizontal_3_Changeable(),
                    BlockPatternData.Horizontal_3_Changeable(),
                    BlockPatternData.Horizontal_3_Changeable()});
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

            public static BlockPatternData Vertical_3_Changeable()
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Normal_Changeable(0, 0),
                    BlockData.Normal_Changeable(0, 1),
                    BlockData.Normal_Changeable(0, 2)});
            }

            public static BlockPatternData Horizontal_3_Changeable()
            {
                return new BlockPatternData(1, new List<BlockData>{
                    BlockData.Normal_Changeable(0, 0),
                    BlockData.Normal_Changeable(1, 0),
                    BlockData.Normal_Changeable(2, 0)});
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
        }
    }
}
