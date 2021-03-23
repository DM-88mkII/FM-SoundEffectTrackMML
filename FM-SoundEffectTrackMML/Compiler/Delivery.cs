


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;



namespace FM_SoundEffectTrackMML
{
	[DataContract]
	public class Delivery
	{
		[DataContract]
		public class Part
		{
			[DataMember]
			List<Packet> maPacket;
			
			[DataMember]
			CallstackLogger mCallstackLogger;
			
			
			
			public Part(List<Packet> aPacket, CallstackLogger @CallstackLogger)
			{
				maPacket = aPacket;
				mCallstackLogger = CallstackLogger;
			}
			
			
			
			public List<Packet> Packet
			{
				get { return maPacket; }
			}
			
			
			
			public CallstackLogger CallstackLogger
			{
				get { return mCallstackLogger; }
			}
		}
		
		[DataMember]
		Dictionary<int, Timbre> maTimbre = new Dictionary<int, Timbre>();
		
		[DataMember]
		MultiMap<char, Part> maPart = new MultiMap<char, Part>();
		
		
		
		~Delivery()
		{
		}
		
		
		
		public Delivery()
		{
		}
		
		
		
		public ref Dictionary<int, Timbre> TimbreLibrary
		{
			get { return ref maTimbre; }
		}
		
		
		
		public ref MultiMap<char, Part> PartLibrary
		{
			get { return ref maPart; }
		}
		
		
		
		public bool ExistChannel(char Channel)
		{
			return maPart.ContainsKey(Char.ToUpper(Channel)) || maPart.ContainsKey(Char.ToLower(Channel));
		}
		
		
		
		public bool Export(string FilePathVmt)
		{
			using (var ms = new MemoryStream())
			using (var fs = new FileStream(FilePathVmt, FileMode.Create))
			using (var sw = new StreamWriter(fs))
			{
				var Serializer = new DataContractJsonSerializer(typeof(Delivery));
				Serializer.WriteObject(ms, this);
				var Encode = Encoding.UTF8.GetString(ms.ToArray());
				sw.Write(Encode);
			}
			return true;
		}
		
		
		
		public static bool Import(out Delivery @Delivery, string FilePathVmt)
		{
			Delivery = new Delivery();
			if (File.Exists(FilePathVmt)){
				using (var fs = new FileStream(FilePathVmt, FileMode.Open))
				{
					var Serializer = new DataContractJsonSerializer(typeof(Delivery));
					Delivery = (Delivery)Serializer.ReadObject(fs);
				}
				return true;
			}
			return false;
		}
	}
}
