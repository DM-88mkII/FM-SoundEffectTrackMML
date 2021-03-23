


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;



namespace FM_SoundEffectTrackMML
{
	[DataContract]
	public class CallstackLogger
	{
		[DataContract]
		public class Logger
		{
			[DataMember]
			Callstack mCallstack;
			
			[DataMember]
			int mIn_oLine;
			
			[DataMember]
			int mIn_nMacro;
			
			
			
			public Logger(Callstack @Callstack, int In_oLine, int In_nMacro)
			{
				mCallstack = Callstack;
				mIn_oLine = In_oLine;
				mIn_nMacro = In_nMacro;
			}
			
			
			
			public Callstack Callstack
			{
				get { return mCallstack; }
			}
			
			
			
			public int In_oLine
			{
				get { return mIn_oLine; }
			}
			
			
			
			public int In_nMacro
			{
				get { return mIn_nMacro; }
			}
		}
		
		[DataMember]
		List<Logger> maLogger = new List<Logger>();
		
		
		
		~CallstackLogger()
		{
		}
		
		
		
		public CallstackLogger()
		{
		}
		
		
		
		public int Count
		{
			get { return maLogger.Count; }
		}
		
		
		
		public Logger this[int oLogger]
		{
			get { return maLogger[oLogger]; }
		}
		
		
		
		public int Add(Callstack @Callstack, int In_oLine, int In_nMacro)
		{
			maLogger.Add(new Logger(Callstack.DeepClone(), In_oLine, In_nMacro));
			return maLogger.Count - 1;
		}
	}
}
