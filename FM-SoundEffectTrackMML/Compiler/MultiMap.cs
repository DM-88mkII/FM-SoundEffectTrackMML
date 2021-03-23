


using System;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.Serialization;



namespace FM_SoundEffectTrackMML
{
	[DataContract]
	public class MultiMap<KeyT, ValueT>
	{
		[DataMember]
		private Dictionary<KeyT, List<KeyValuePair<KeyT, ValueT>>> mMap;
		
		[DataMember]
		private int mSize;
		
		
		
		public MultiMap()
		{
			mMap = new Dictionary<KeyT, List<KeyValuePair<KeyT, ValueT>>>();
			mSize = 0;
		}
		
		
		
		public int Size()
		{
			return mSize;
		}
		
		
		
		public void Clear()
		{
			mMap.Clear();
			mSize = 0;
		}
		
		
		
		public bool ContainsKey(KeyT Key)
		{
			return mMap.ContainsKey(Key);
		}
		
		
		
		public IEnumerator<KeyValuePair<int, KeyValuePair<KeyT, ValueT>>> GetEnumerator()
		{
			foreach (var Key in mMap){
				int i = 0;
				foreach (var Item in Key.Value){
					yield return new KeyValuePair<int, KeyValuePair<KeyT, ValueT>>(i++, Item);
				}
			}
		}
		
		
		
		public void Add(KeyT Key, ValueT Value)
		{
			var Item = new KeyValuePair<KeyT, ValueT>(Key, Value);
			if (mMap.ContainsKey(Key)){
				mMap[Key].Add(Item);
			} else {
				var List = new List<KeyValuePair<KeyT, ValueT>>();
				List.Add(Item);
				mMap.Add(Key, List);
			}
			++mSize;
		}
		
		
		
		public bool Find(out KeyValuePair<int, KeyValuePair<KeyT, ValueT>> rItr, KeyT Key, Predicate<KeyValuePair<KeyT, ValueT>> f)
		{
			if (mMap.ContainsKey(Key)){
				var i = mMap[Key].FindIndex(f);
				if (i >= 0){
					rItr = new KeyValuePair<int, KeyValuePair<KeyT, ValueT>>(i, new KeyValuePair<KeyT, ValueT>(Key, mMap[Key][i].Value));
					return true;
				}
			}
			rItr = new KeyValuePair<int, KeyValuePair<KeyT, ValueT>>(-1, new KeyValuePair<KeyT, ValueT>(default(KeyT), default(ValueT)));
			return false;
		}
		
		
		
		public void Remove(KeyValuePair<int, KeyValuePair<KeyT, ValueT>> Itr)
		{
			if (mMap.ContainsKey(Itr.Value.Key) && Itr.Key >= 0 && Itr.Key < mMap[Itr.Value.Key].Count){
				mMap[Itr.Value.Key].RemoveAt(Itr.Key);
				if (mMap[Itr.Value.Key].Count == 0) mMap.Remove(Itr.Value.Key);
				--mSize;
			}
		}
		
		
		
		public List<ValueT> Bake(KeyT Key)
		{
			if (mMap.ContainsKey(Key)){
				var Result = new List<ValueT>();
				foreach (var v in mMap[Key]){
					Result.Add(v.Value);
				}
				return Result;
			}
			return new List<ValueT>();
		}
	}
}
