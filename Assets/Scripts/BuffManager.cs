using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
	public class BuffManager : MonoBehaviour
	{
        public static BuffManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public Transform buffSlot;
		public GameObject buffPrefab;
		private struct MyBuff
		{
			public MyBuff(Buff b, int t, GameObject mybuff)
			{
				buff = b;
				timeLeft = t;
				buffInGame = mybuff;
			}
			public Buff buff;
			public int timeLeft;
			public GameObject buffInGame;
		};
		List<MyBuff> states = new List<MyBuff>();
		public KeyValuePair<int, int> BuffCard(Card card)
		{
			int t = card.Time;
			int p = card.Points;
			foreach (MyBuff myBuff in states)
			{
				Buff buff = myBuff.buff;
				int type = (int)card.Type;
				if (type < 4)
				{
					int timeBuff = buff.Effects[type].time;
					int pointBuff = buff.Effects[type].point;
					t = Mathf.Max(60, t + timeBuff * 60);
					p = Mathf.Max(1, p + pointBuff);
				}
			}
			return new KeyValuePair<int, int>(t, p);
		}
		public bool ChangingBuff(Card card)
		{
            bool changed = false;
			foreach (AddingBuffs addBuff in card.AddBuffs)
			{
				if (addBuff.come)
				{
					Buff newState = addBuff.buff;
					
					if (states.Exists(x => x.buff == newState))
					{
						Debug.Log("Same buff" + newState.Title);
					} else
					{
						if (Random.Range(0f, 1f) <= addBuff.probability)
						{
                            Debug.Log("Adding buff " + newState.Title);
                            int t = addBuff.overrideLength > 0 ? addBuff.overrideLength : newState.length;
							GameObject mybuff = Instantiate(buffPrefab, buffSlot);
							mybuff.GetComponent<BuffViz>().Buff = newState;
							states.Add(new MyBuff(newState, t, mybuff));
                            changed = true;
						}
					}
				} else
				{
					Buff newState = addBuff.buff;
					
					MyBuff myBuff = states.Find(x => x.buff == newState);
					if (myBuff.buff != null)
					{
						if (Random.Range(0f, 1f) <= addBuff.probability)
						{
                            Debug.Log("Removing buff " + newState.Title);
                            if (myBuff.buffInGame != null)
								Destroy(myBuff.buffInGame);
							states.Remove(myBuff);
                            changed = true;
                        }
					}
				}
			}
            return changed;
		}
	}
}