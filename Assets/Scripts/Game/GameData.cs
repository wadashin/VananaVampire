using System;
using System.Collections.Generic;

public enum SelectType
{
    Skill = 1,
    Passive = 2,
    Execute = 3,
}

[Serializable]
public class SkillSelectTable
{
    public SelectType Type;
    public int TargetId;
    public string Name;
    public int Level;
    public int Probability;
}

public class GameData
{
    static public List<SkillSelectTable> SkillSelectTable = new List<SkillSelectTable>()
    {
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 1, Name = "遠距離弾", Level = 0, Probability = 80 },
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 2, Name = "近距離範囲", Level = 0, Probability = 80 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 1, Name = "攻撃UP", Level = 0, Probability = 40 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 2, Name = "速度UP", Level = 0, Probability = 20 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 3, Name = "攻撃速度UP", Level = 5, Probability = 10 },
        new SkillSelectTable(){ Type = SelectType.Execute, TargetId = 1, Name = "回復", Level = 0, Probability = 90 },
        new SkillSelectTable(){ Type = SelectType.Execute, TargetId = 2, Name = "ゴールド", Level = 0, Probability = 40 }
    };

    static public List<int> LevelTable = new List<int>()
    {
        0, //level 1
        5, //level 1→2
        15,
        25,//...
        40,
        55,
        70,
        90,
        110,
        130,
        150,
        190,
        230,
        270,
        310,
        350,
        400,
        450,
        500,
        600,
        700,
        800,
    };
}
