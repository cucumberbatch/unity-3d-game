using System.Collections.Generic;
using UnityEngine;

namespace BulletsPoolNamespace
{
	public class BulletsPool
	{
		
		public List<Transform> pool;
		public int capacity = 30;
		
		public BulletsPool()
		{
			pool = new List<Transform>(capacity);
		}

		public BulletsPool(int capacity)
		{
			this.capacity = capacity;
			pool = new List<Transform>(capacity);
		}


		public void putBullet(Transform bullet)
		{
			if (pool.Count == capacity)
			{
				return;
			}
			
			//bullet.gameObject.SetActive(false);
			pool.Add(bullet);
		}
		
		public Transform getBullet()
		{
			if (pool[0] == null)
			{
				return null;
			}
			
			Transform removed = pool[0];
			//removed.gameObject.SetActive(true);
			pool.RemoveAt(0);
			return removed;
		}
		

		public void clearPool()
		{
			pool = new List<Transform>(capacity);
		}
		
	}
}