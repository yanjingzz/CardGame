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
            time.text = "Time: " + manager.Time.ToString();
            art.text = "Art: " + manager.Points[CardType.Art].ToString();
            feel.text = "Feel: " + manager.Points[CardType.Feel].ToString();
            gameplay.text = "Gameplay: " + manager.Points[CardType.Gameplay].ToString();
            tech.text = "Tech: " +manager.Points[CardType.Tech].ToString();

        }

    }
}

