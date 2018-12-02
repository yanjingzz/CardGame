using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardGame
{
	[CreateAssetMenu]
	public class Buff : ScriptableObject
	{
		public string Title;
		public string Description;
        public Sprite Image;
		public BuffType[] Effects;
		public int length = 2880;
	}

	[System.Serializable]
	public struct BuffType
	{
		public int time;
		public int point;
	}
}