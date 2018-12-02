using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class GameStatusViz : MonoBehaviour
    {

        public Text time;
        public Text art,feel,tech,gameplay;

        public void DisplayStatus()
        {
            GameManager manager = GameManager.Instance;
            time.text = manager.Time.ToString();
            art.text = manager.Points[CardType.Art].ToString();
            feel.text = manager.Points[CardType.Feel].ToString();
            gameplay.text = manager.Points[CardType.Gameplay].ToString();
            tech.text = manager.Points[CardType.Tech].ToString();

        }

    }
}

