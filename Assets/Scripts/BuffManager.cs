using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
	public class BuffManager : MonoBehaviour
	{
		public Transform buffSlot;
		public GameObject buffPrefab;
		private struct MyBuff
		{
			public MyBuff(Buff b, int t)
			{
				buff = b;
				timeLeft = t;
			}
			public Buff buff;
			public int timeLeft;
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
		public void ChangingBuff(Card card)
		{
			foreach (AddingBuffs addBuff in card.AddBuffs)
			{
				if (addBuff.come)
				{
					Buff newState = addBuff.buff;
					Debug.Log("buff " + newState.Title);
					if (states.Exists(x => x.buff == newState))
					{
						Debug.Log("Same buff");
					} else
					{
						if (Random.Range(0, 1) <= addBuff.probability)
						{
							int t = addBuff.overrideLength > 0 ? addBuff.overrideLength : newState.length;
							states.Add(new MyBuff(newState, t));
							GameObject mybuff = Instantiate(buffPrefab, buffSlot);
							mybuff.GetComponent<BuffViz>().buff = newState;
						}
					}
				}
			}
		}
	}
}