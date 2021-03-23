


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;



namespace FM_SoundEffectTrackMML
{
	[Serializable]
	[DataContract]
	public class Callstack
	{
		[Serializable]
		[DataContract]
		public class Record {
			[DataMember]
			int mLine;
			
			[DataMember]
			int moBase;
			
			[DataMember]
			int moText;
			
			
			
			public Record(int Line, int oBase, int oText)
			{
				mLine = Line;
				moBase = oBase;
				moText = oText;
			}
			
			
			
			public bool IsRoot
			{
				get { return (moText < 0); }
			}
			
			
			
			public int Line
			{
				get { return mLine; }
			}
			
			
			
			public int Base
			{
				get { return moBase; }
			}
			
			
			
			public int Column
			{
				get { return moBase + ((IsRoot)? 0: moText); }
			}
		}
		
		[DataMember]
		Stack<Record> mCallstack = new Stack<Record>();
		
		[DataMember]
		char mChannel;
		
		
		
		~Callstack()
		{
		}
		
		
		
		public Callstack(char Channel)
		{
			mChannel = Channel;
		}
		
		
		
		public char Channel
		{
			get { return mChannel; }
		}
		
		
		
		public int Count
		{
			get { return mCallstack.Count; }
		}
		
		
		
		public void Push(Record @Record)
		{
			mCallstack.Push(Record);
		}
		
		
		
		public Record Pop()
		{
			return mCallstack.Pop();
		}
		
		
		
		public void Log(int Column)
		{
			foreach (var v in mCallstack){
				if (v.IsRoot){
					if (Column >= 0){
						Console.WriteLine($"Channel {mChannel} : Line {v.Line+1} : Column {Column+1}");
					} else {
						Console.WriteLine($"Channel {mChannel} : Line {v.Line+1}");
					}
				} else {
					Console.WriteLine($"From : Line {v.Line+1} : Column {v.Column+1}");
				}
			}
		}
		
		
		
		public void Log(int Line, int Column, int Macro)
		{
			if (Macro >= 0){
				Console.WriteLine($"Macro {Macro} : Line {Line+1} : Column {Column+1}");
				Log(-1);
			} else {
				Log(Column);
			}
		}
	}
}
