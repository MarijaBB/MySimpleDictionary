using System.Collections;

	public class MySimpleDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
	{
		//fields
		private List<KeyValuePair<TKey, TValue>>[] buckets;
		private int numberOfElements;

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (var bucket in buckets)
			{
				if (bucket != null)
				{
					foreach (var pair in bucket)
					{
						yield return pair;
					}
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		//constructor
		public MySimpleDictionary(int size = 32)
		{
			buckets = new List<KeyValuePair<TKey, TValue>>[size];
			numberOfElements = 0;
		}
		private void Resize()
		{
			List<KeyValuePair<TKey, TValue>>[] oldBuckets = buckets;
			buckets = new List<KeyValuePair<TKey, TValue>>[numberOfElements*2];
			numberOfElements = 0;
			foreach (var bucket in oldBuckets)
			{
				if (bucket != null)
				{
					foreach (var pair in bucket)
					{
						var newPosition = HashFunction(pair.Key);
						if (buckets[newPosition] == null)
							buckets[newPosition] = new List<KeyValuePair<TKey, TValue>>();

						buckets[newPosition].Add(pair);
						numberOfElements++;
					}			
				}
			}
		}
		private void shouldResize()
		{
			if (numberOfElements >= buckets.Length * 0.75)
			{
				Resize();
			}
		}
		// [.]
		public TValue this[TKey key]
		{
			get
			{
				int position = HashFunction(key);
				var bucket = buckets[position];
				if (bucket != null)
				{
					foreach (var pair in bucket)
					{
						if (pair.Key.Equals(key))
							return pair.Value;
					}
				}
				throw new KeyNotFoundException($"Key '{key}' not found.");
			}

			set
			{
				if (value == null) 
					throw new ArgumentNullException(nameof(value));
				shouldResize();
				int position = HashFunction(key);
				if (buckets[position] == null)
					buckets[position] = new List<KeyValuePair<TKey, TValue>>();

				var bucket = buckets[position];
				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
					{
						bucket[i] = new KeyValuePair<TKey, TValue>(key, value);
						return;
					}
				}
				bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
				numberOfElements++;
			}
		}

		//get number of elements in dictionary
		public int Count()
		{
			return numberOfElements;
		}

		//add element to a dictionary
		public void Add(TKey key, TValue value)
		{
			if (value == null)
				throw new ArgumentNullException();
			shouldResize();
			int position = HashFunction(key);

			if (buckets[position] == null)
				buckets[position] = new List<KeyValuePair<TKey, TValue>>();

			foreach (var pair in buckets[position])
			{
				if (pair.Key.Equals(key))
					throw new ArgumentException("Key already exists");
			}

			buckets[position].Add(new KeyValuePair<TKey, TValue>(key, value));
			numberOfElements++;
		}

		//hash function
		private int HashFunction(TKey key)
		{
			if (key == null)
				throw new ArgumentNullException();
			return Math.Abs(key.GetHashCode()) % buckets.Length;
		}

		//does dictionary contain key 
		public bool ContainsKey(TKey key)
		{
			var position = HashFunction(key);

			if (this.buckets[position] == null)
				return false;

			foreach (var pair in this.buckets[position])
			{
				if (pair.Key.Equals(key))
					return true;
			}
			return false;
		}

		// does dicitonary contain value
		public bool ContainsValue(TValue value)
		{
			foreach (var bucket in this.buckets)
			{
				if (bucket!=null && bucket.Count > 0)
				{
					foreach (var pair in bucket)
					{
						if (pair.Value != null && pair.Value.Equals(value))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// remove pair that has provided key
		public bool Remove(TKey key)
		{
			var position = HashFunction(key);
			var bucket = buckets[position];

			if (bucket != null)
			{
				for (int i = 0; i < bucket.Count; i++)
				{
					if (bucket[i].Key.Equals(key))
					{
						bucket.RemoveAt(i);
						numberOfElements--;
						return true;
					}
				}
			}
			return false;
		}

		// clear everything from dictionary
		public void Clear()
		{
			foreach (var bucket in this.buckets)
			{
				if (bucket != null)
					bucket.Clear();
			}
			numberOfElements = 0;
		}

		// get all keys
		public IEnumerable<TKey> Keys
		{
			get
			{
				foreach (var bucket in buckets)
				{
					if (bucket != null)
					{
						foreach (var pair in bucket)
						{
							yield return pair.Key;
						}
					}
				}
			}
		}

		//get all values
		public IEnumerable<TValue> Values
		{
			get
			{
				foreach (var bucket in buckets)
				{
					if (bucket != null)
					{
						foreach (var pair in bucket)
						{
							yield return pair.Value;
						}
					}
				}
			}
		}

}   


