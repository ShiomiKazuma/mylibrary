using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack,
    Power,
    Speed,
    Defend,
    Null
}

public class SkillManager : SingletonBase<SkillManager>
{
    private int _skillPoint;
    List<SkillType> _skillList = new List<SkillType>();
    
    protected override void DoAwake()
    {
        
    }

    /// <summary>
    /// スキルが習得済みかを返す
    /// </summary>
    /// <param name="skillType">スキル名</param>
    /// <returns>習得済みか</returns>
    public bool HasSkill(SkillType skillType)
    {
        return _skillList.Contains(skillType);
    }

    /// <summary>
    /// スキルが習得できるかを返すメソッド
    /// </summary>
    /// <param name="cost">必要なスキルポイント</param>
    /// <param name="skillType">習得していなければならないスキル</param>
    /// <returns>スキル習得できたか</returns>
    public bool CanLearnSkill(int cost, SkillType skillType)
    {
        if (cost > _skillPoint)
            return false;

        //習得すべきスキルがない場合
        if (skillType == SkillType.Null)
            return true;

        return HasSkill(skillType);
    }

    /// <summary>
    /// スキルを習得するメソッド
    /// </summary>
    /// <param name="skillType">習得したスキル</param>
    public void LearnSkill(SkillType skillType)
    {
        _skillList.Add(skillType);
    }
}
