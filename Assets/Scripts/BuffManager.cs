using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
	public class BuffManager : MonoBehaviour
	{
		List<Buff> states;
		public void ChangingBuff(Card card)
		{
			foreach (AddingBuffs addBuff in card.AddBuffs)
			{
				if (addBuff.come)
				{
					Buff newState = addBuff.buff;
					states.Add(newState);
				}
			}
		}
	}
}