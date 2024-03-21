using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBlock : MonoBehaviour
{
    SkillType _skillType;
    [SerializeField, Tooltip("習得に必要なスキル")] SkillType _hasSkill;
    [SerializeField, Tooltip("コスト")] int _cost;
    [SerializeField, Tooltip("名前")] string _name;
    [SerializeField, Tooltip("スキル情報")] string _info;

    public void Onclick()
    {
        //　取得済みならなにもしない
        if (SkillManager.Instatnce.HasSkill(this._skillType))
        {
            Debug.Log("習得済み");
            return;
        }

        //習得可能かを判定する
        if (SkillManager.Instatnce.CanLearnSkill(_cost, _hasSkill))
        {
            SkillManager.Instatnce.LearnSkill(this._skillType);
            Debug.Log("習得");
        }           
        else
            Debug.Log("習得不可");
    }
}
