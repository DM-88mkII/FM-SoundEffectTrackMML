


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;



namespace FM_SoundEffectTrackMML
{
	class Parser
	{
		enum eMode {
			None,
			Error,
			Setting,
			Timbre,
			Macro,
			Track,
		};
		
		Setting mSetting = new Setting();
		
		Timbre.ePhase TimbrePhase = Timbre.ePhase.None;
		Timbre mTimbre = new Timbre();
		
		MacroLibrary maMacro = new MacroLibrary();
		
		MultiMap<char, Track> maTrack = new MultiMap<char, Track>();
		
		Dictionary<char, PacketStream> maPacketStream = new Dictionary<char, PacketStream>();
		
		Delivery mDelivery;
		
		
		
		~Parser()
		{
		}
		
		
		
		public Parser()
		{
		}
		
		
		
		public bool Parse(out Delivery @Delivery, string[] aLine)
		{
			Delivery = new Delivery();
			mDelivery = Delivery;
			
			bool Result = Import(aLine);
			return (Result)? Convert(): Result;
			
			#if TEST//[
			if (Result) Dump(mDelivery);
			return Result;
			#endif//]
		}
		
		
		
		eMode GetMode(string Text)
		{
			if (Setting.IsSetting(Text)) return eMode.Setting;
			if (Timbre.IsTimbre(Text)) return eMode.Timbre;
			if (Macro.IsMacro(Text)) return eMode.Macro;
			if (Track.IsTrack(Text)) return eMode.Track;
			
			var c = new Cursor(Text);
			c.SkipSpace();
			return (c.IsTerm)? eMode.None: eMode.Error;
		}
		
		
		
		bool Import(string[] aLine)
		{
			Console.WriteLine("Importing...");
			
			bool Result = true;
			for (int oLine = 0; oLine < aLine.Length && Result; ++oLine){
				switch ((int)GetMode(aLine[oLine])){
					case (int)eMode.None:{
						break;
					}
					case (int)eMode.Error:{
						Console.WriteLine($"!! Error !! : Syntax error");
						Console.WriteLine($"Line {oLine+1}");
						Result = false;
						break;
					}
					case (int)eMode.Setting:{
						if (TimbrePhase == Timbre.ePhase.None){
							var Kind = Setting.eKind.Error;
							if (mSetting.Import(out Kind, aLine[oLine], oLine)){
								switch ((int)Kind){
									case (int)Setting.eKind.Error:{
										Result = false;
										break;
									}
									case (int)Setting.eKind.OCTAVE_UPDOWN:
									case (int)Setting.eKind.VOLUME_UPDOWN:{
										break;
									}
								}
							} else {
								Result = false;
							}
						} else {
							Console.WriteLine($"!! Error !! : Syntax error");
							Console.WriteLine($"Line {oLine+1}");
							Result = false;
						}
						break;
					}
					case (int)eMode.Timbre:{
						if (mTimbre.Import(out TimbrePhase, aLine[oLine], oLine)){
							switch ((int)TimbrePhase){
								case (int)Timbre.ePhase.None:{
									Result = false;
									break;
								}
								case (int)Timbre.ePhase.Number:
								case (int)Timbre.ePhase.FB_AL:
								case (int)Timbre.ePhase.OP1:
								case (int)Timbre.ePhase.OP2:
								case (int)Timbre.ePhase.OP3:{
									break;
								}
								case (int)Timbre.ePhase.OP4:{
									mDelivery.TimbreLibrary.Add(mTimbre.Params.Number, mTimbre);
									
									TimbrePhase = Timbre.ePhase.None;
									mTimbre = new Timbre();
									break;
								}
							}
						} else {
							Result = false;
						}
						break;
					}
					case (int)eMode.Macro:{
						if (TimbrePhase == Timbre.ePhase.None){
							var v = new Macro();
							if (v.Import(aLine[oLine], oLine)){
								maMacro.Add(v);
							} else {
								Result = false;
							}
						} else {
							Console.WriteLine($"!! Error !! : Syntax error");
							Console.WriteLine($"Line {oLine+1}");
							Result = false;
						}
						break;
					}
					case (int)eMode.Track:{
						if (TimbrePhase == Timbre.ePhase.None){
							var v = new Track();
							if (v.Import(aLine[oLine], oLine)){
								Console.Write($"{v.Channel}");
								maTrack.Add(v.Channel, v);
							} else {
								Result = false;
							}
						} else {
							Console.WriteLine($"!! Error !! : Syntax error");
							Console.WriteLine($"Line {oLine+1}");
							Result = false;
						}
						break;
					}
				}
			}
			Console.WriteLine("");
			return Result;
		}
		
		
		
		bool Convert()
		{
			Console.WriteLine("Converting...");
			
			bool Result = true;
			foreach (var i in maTrack){
				var Channel = i.Value.Key;
				var Track = i.Value.Value;
				Spread Spread;
				
				if (!maPacketStream.ContainsKey(Channel)){
					maPacketStream.Add(Channel, new PacketStream(mSetting));
				}
				
				Console.Write($"{Channel}");
				Result = maMacro.TrackToSpread(out Spread, Track);
				if (Result){
					List<Packet> aPacket;
					CallstackLogger CallstackLogger;
					
					Result = maPacketStream[Channel].SpreadToPacket(out aPacket, out CallstackLogger, Spread);
					if (Result){
						mDelivery.PartLibrary.Add(Channel, new Delivery.Part(aPacket, CallstackLogger));
					}
				} else {
					break;
				}
			}
			Console.WriteLine("");
			return Result;
		}
		
		
		
		void Dump(Delivery @Delivery)
		{
			foreach (var p in Delivery.PartLibrary){
				var Channel = p.Value.Key;
				var Part = p.Value.Value;
				Console.WriteLine($"{Channel}");
				
				foreach (var v in Part.Packet){
					Console.WriteLine($"{v.CommandEnum} {v.Value} {v.Column} {v.Logger}");
					
					if (v.Logger >= 0){
						var Logger = Part.CallstackLogger[v.Logger];
						Logger.Callstack.Log(Logger.In_oLine, v.Column, Logger.In_nMacro);
					}
				}
			}
		}
	}
}
