using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Entities
{
    public enum BallType
    {
        BasicBall = 0,
        BigBall = 1,
        ExplosiveBall = 2
    }
    [System.Serializable]
    public struct BallSelectButton
    {
        public Button button;
        public Image backgroundImage;
        public TMP_Text text;
    }
}
