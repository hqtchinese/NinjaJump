using System;
using System.Collections.Generic;
using UnityEngine;
using GameBase.Editor;

namespace GameBase.Tool.Tween
{

    [Serializable]
    public class TweenCurveInfo
    {
        [RenameField("曲线类型")]
        public CurveType type;
        [RenameField("动作曲线")]
        public AnimationCurve curve;
    }

    [Resource("Asset/Tween/TweenCurve")]
    [CreateAssetMenu(menuName = "GameAssets/Tween/Curve",fileName = "TweenCurve")]
    public class CurveAsset : LocalAsset<CurveAsset>
    {
        [SerializeField]
        public List<TweenCurveInfo> curveList;
        public Dictionary<CurveType,TweenCurveInfo> curveDict;

        public override void Init()
        {
            curveDict = curveList.ToDict<CurveType,TweenCurveInfo>(item => item.type);
        }
        
    }
    
}