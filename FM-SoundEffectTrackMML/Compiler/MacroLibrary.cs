


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	class MacroLibrary
	{
		Dictionary<int, Macro> mMacro = new Dictionary</*Number*/int, Macro>();
		const int NestDepth = 16;
		
		
		
		~MacroLibrary()
		{
		}
		
		
		
		public MacroLibrary()
		{
		}
		
		
		
		public bool Add(Macro @Macro)
		{
			if (!mMacro.ContainsKey(Macro.Number)){
				mMacro.Add(Macro.Number, Macro);
				return true;
			}
			return false;
		}
		
		
		
		public bool TrackToSpread(out Spread @Spread, Track @Track)
		{
			var Callstack = new Callstack(Track.Channel);
			Callstack.Push(new Callstack.Record(Track.Mml.Line, Track.Mml.Base, -1));//Root
			
			var Queue = new StringBuilder(0x1000);
			Queue.Append('{');
			Queue.Append(String.Format($"{Track.Line},{Track.Mml.Base},-1,{Track.Mml.Line},{Track.Mml.Base},-1,"));//Root
			var Result = TrackToSpread(ref Callstack, ref Queue, Track.Channel, Track.Mml, -1, 0);
			Queue.Append('}');
			
			Spread = new Spread(Track.Channel, Queue.ToString());
			return Result;
		}
		
		
		
		bool TrackToSpread(ref Callstack @Callstack, ref StringBuilder Queue, char Channel, MML Mml, int nMacro, int nNest)
		{
			var c = new Cursor(Mml.Text);
			
			bool Result = true;
			while (!c.IsTerm && Result){
				if (c.IsMark('*', false) > 0){
					c.Commit();
					Callstack.Push(new Callstack.Record(Mml.Line, Mml.Base, c.Preview));
					
					var nDecimal = c.IsDecimal();
					if (nDecimal > 0){
						c.Revert();
						
						var MacroNumber = c.GetDecimal();
						if (mMacro.ContainsKey(MacroNumber)){
							var Macro = mMacro[MacroNumber];
							if (nNest < NestDepth){
								Queue.Append('{');
								Queue.Append(String.Format($"{Mml.Line},{Mml.Base},{c.Preview},{Macro.Mml.Line},{Macro.Mml.Base},{MacroNumber},"));
								Result = TrackToSpread(ref Callstack, ref Queue, Channel, Macro.Mml, MacroNumber, nNest+1);
								if (Result){
									c.Commit();
									
									Queue.Append('}');
									Queue.Append(String.Format($"{c.Current},"));
									
									Callstack.Pop();
								}
							} else {
								Console.WriteLine($"!! Error !! : Nesting too deep");
								Callstack.Log(Mml.Line, Mml.Base + c.Preview, nMacro);
								Result = false;
							}
						} else {
							Console.WriteLine($"!! Error !! : Macro not defined");
							Callstack.Log(Mml.Line, Mml.Base + c.Preview, nMacro);
							Result = false;
						}
					} else {
						Console.WriteLine($"!! Error !! : Illegal parameter");
						Callstack.Log(Mml.Line, Mml.Base + c.Preview, nMacro);
						Result = false;
					}
				} else {
					Queue.Append(c.Char);
					c.Offset(1);
					c.Commit();
				}
			}
			return Result;
		}
	}
}
