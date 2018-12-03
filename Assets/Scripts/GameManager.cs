using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        GameStatusViz viz;
		CardSpawner spawner;
        public List<Card> initialHand = new List<Card>();
        public Card EatCard, SleepCard;
        public Buff Hungry, Tired;
        public Card PublishCard;

        List<CardManager> currentHand = new List<CardManager>();
        public int Time 
        { 
            get { return _time; } 
            private set
            {
                _time = value;
                if(_time <= 0)
                {
                    GameOver();
                }
                
            }
        }
        private int _time = 48 * 60;
        private int lastSleepTime = 48*60, lastEatTime = 48*60;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;

            } else if(Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public Dictionary<CardType, int> Points { get; private set; } 

        private void Start()
        {
            Points = new Dictionary<CardType, int>(); 
            Points.Add(CardType.Art, 0);
            Points.Add(CardType.Feel, 0);
            Points.Add(CardType.Gameplay, 0);
            Points.Add(CardType.Tech, 0);
            viz = GetComponent<GameStatusViz>();
			spawner = GetComponent<CardSpawner>();
            if(viz == null)
            {
                Debug.LogWarning("Game manager: missing visualizer");
            }
            viz.DisplayStatus();

            foreach(Card card in initialHand)
            {
                Spawn(card);
            }

        }

        public bool CanPlayCard(CardManager cardManager)
        {
            Card card = cardManager.Card;

            //deal with buff
            KeyValuePair<int, int> newTimePoint = BuffManager.Instance.BuffCard(card);
            int cardTime = newTimePoint.Key;
            int cardPoints = newTimePoint.Value;
            if (cardTime > Time)
            {
                Debug.Log("Game manager: Trying to do " + card + " but don't have enough time");
                return false;
            }
            return true;
        }

        public void PlayCard(CardManager cardManager)
        {
            Card card = cardManager.Card;
            bool spawnedNewCard = false;
            Debug.Log("Game manager: Played card: " + card);

            //deal with buff
            KeyValuePair<int, int> newTimePoint = BuffManager.Instance.BuffCard(card);
			int cardTime = newTimePoint.Key;
			int cardPoints = newTimePoint.Value;

            currentHand.Remove(cardManager);


            //update values
            Time -= cardTime;
			if (Points.ContainsKey(card.Type))
            {
                Points[card.Type] += cardPoints;
            }
            viz.DisplayStatus();

            //deal with eat and sleep
            if (card == EatCard)
            {
                lastEatTime = Time;
            }
            if (card == SleepCard)
            {
                lastSleepTime = Time;
            }
            TestEatAndSleep();

            //publish card;
            if(Time <= 8*10)
            {
                Spawn(PublishCard);
                spawnedNewCard = true;
            }
            if(card == PublishCard)
            {
                PublishGame();
            }

            //unlock cards
            foreach (Card newCard in card.UnlockCards)
            {
                if (currentHand.Count <= 7 && !spawnedNewCard)
                {
                    Spawn(newCard);
                    spawnedNewCard = true;
                } else
                {
                    spawner.cardPool.Add(newCard);
                }
            }


            //change buff
            bool changed = BuffManager.Instance.ChangingBuff(card);
            if(changed)
            {
                foreach(CardManager manager in currentHand)
                {
                    manager.UpdateValues();
                }
            }

            if (currentHand.Count <= 7)
            {
                SpawnRandomly();
            }


            DetectGameOver();

        }

        public void Discard(Card card)
        {

        }

        public void GameOver()
        {
            string remark = GenerateRemark();


            viz.DisplayGameOver(remark);
        }
        

        private string GenerateRemark()
        {
            int great = 18, good = 10;
            if (!gamePublished)
                return "Oh no no no no. \nDid you forgot to publish your game?\n\nYou gotta publish the game, \nor nobody will be able to see it!";
            var gameplay = Points[CardType.Gameplay];
            var art = Points[CardType.Art];
            var feel = Points[CardType.Feel];
            var tech = Points[CardType.Tech];
            var overall = gameplay + art + feel + tech;
            string ret;
            if (gameplay >= great && art >= great && feel >= 18 && tech >= 18)
                ret = "Wow! You made the game ever!\nEverthing about this game is perfect!\n\n";
            else if (overall >= 4 * great)
                ret = "You made an awesome game,\nalthough some aspect of the game still need a little polish.\n\n";
            else if (overall >= 4 * good)
                ret = "Your game is pretty good.\nIt's not the game of the year\nbut your efforts certainly shines through.\n\n";
            else
                ret = "Your game is...well...a game.\nDon't give up! You'll get better at this!\n\n";

            if (gameplay >= great)
                ret += "The gameplay is super on point!\n";
            else if(gameplay >= good)
                ret += "The game is fun to play.\n";
            else
                ret += "The gameplay needs some more work.\n";

            if (art >= great)
                ret += "The aesthetic is exquisite!\n";
            else if (art >= good)
                ret += "And good job with the graphics and the audio.\n";
            else
                ret += "Maybe you should spend more time on the art and music.\n";

            if (feel >= great)
                ret += "The story is gripping, and the characters are so memerable!\n";
            else if (feel >= good)
                ret += "The game tells a good story.\n";
            else
                ret += "The storyline feels weak.\n";

            if (tech >= great)
                ret += "And finally, the game runs smoothly without a bug!\n";
            else if (tech >= good)
                ret += "The game runs fine, but it could benefit from more time spend on coding.\n";
            else
                ret += "The game has come technical difficulties, making it hard to play.\n";
            return ret;

        }

        public void Restart()
        {
            SceneManager.LoadScene("GameScene");
        }

        public bool InHand(Card card)
        {
            foreach(CardManager manager in currentHand)
            {
                if (card == manager.Card)
                    return true;
            }
            return false;
        }

        public void DetectGameOver()
        {
            bool gameOver = true;
            foreach(CardManager manager in currentHand)
            {
                if (manager.Card.Time <= Time)
                {
                    gameOver = false;
                }
            }
            if (gameOver)
                GameOver();
        }

        void Spawn(Card card)
        {
            var manager = spawner.Spawn(card);
            if(manager != null)
                currentHand.Add(manager);
        }

        void SpawnRandomly()
        {
            var manager = spawner.RandomlyGetOne();
            if (manager != null)
                currentHand.Add(manager);
        }


        bool TestEatAndSleep()
        {
            bool spawned = false;
            var timeSinceLastMeal = lastEatTime - Time;
            if(timeSinceLastMeal >= 13*60)
            {
                Debug.Log("Time since last meal: " + timeSinceLastMeal + ", hungry buff");
                BuffManager.Instance.AddBuff(Hungry);
            }
            if (timeSinceLastMeal >= 6*60)
            {
                Debug.Log("Time since last meal: " + timeSinceLastMeal + ", spawning eat");
                Spawn(EatCard);
                spawned = true;
            }
            var timeSinceLastSleep = lastSleepTime - Time;
            if(timeSinceLastSleep >= 28 * 60)
            {
                Debug.Log("Time since last sleep: " + timeSinceLastSleep + ", tired buff");
                BuffManager.Instance.AddBuff(Tired);
            }
            if (timeSinceLastSleep >= 20 * 60)
            {
                Debug.Log("Time since last sleep: " + timeSinceLastSleep + ", spawning sleep");
                Spawn(SleepCard);
                spawned = true;
            }

            return spawned;
        }

        bool gamePublished = false;
        public void PublishGame()
        {
            gamePublished = true;
            GameOver();
        }

    }
}

