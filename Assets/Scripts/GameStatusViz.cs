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
        public Text gameOverRemark;
        public GameObject GameOverPanel;

        public void DisplayStatus()
        {
            GameManager manager = GameManager.Instance;
            int hours = manager.Time / 60;
            int minutes = manager.Time % 60;
            time.text = hours.ToString() + "h " + minutes + "m left";
            art.text = "Art: " + manager.Points[CardType.Art].ToString();
            feel.text = "Feel: " + manager.Points[CardType.Feel].ToString();
            gameplay.text = "Gameplay: " + manager.Points[CardType.Gameplay].ToString();
            tech.text = "Tech: " +manager.Points[CardType.Tech].ToString();

        }

        public void DisplayGameOver(string remark)
        {
            gameOverRemark.text = remark;
            GameOverPanel.SetActive(true);
        }


    }
}

