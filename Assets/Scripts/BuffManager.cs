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