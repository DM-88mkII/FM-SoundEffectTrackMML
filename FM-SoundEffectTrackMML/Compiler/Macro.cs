


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	class Macro
	{
		int mNumber;
		MML mMML;
		
		
		
		~Macro()
		{
		}
		
		
		
		public Macro()
		{
		}
		
		
		
		public int Number
		{
			get { return mNumber; }
		}
		
		
		
		public MML Mml
		{
			get { return mMML; }
		}
		
		
		
		public bool Import(string Text, int oLine)
		{
			var c = new Cursor(Text);
			int Number = 0;
			int oBase = 0;
			
			if (c.IsMark('#') >= 2){
				c.Commit();
				if (c.IsMark('*') > 0){
					c.Commit();
					if (c.GetDecimal(ref Number, true)){
						if (Number >= 0){
							c.Commit();
							if (c.IsMark('{') > 0){
								c.Commit();
								
								oBase = c.Current;
								var nMML = c.IsMML();
								if (nMML >= 0){
									c.Commit();
									if (c.IsMark('}') > 0){
										c.Commit();
										if (c.IsTerm){
											mNumber = Number;
											mMML = new MML(oLine, oBase, c.Substring(oBase, nMML));
											return true;
										}
									}
								}
							}
						} else {
							Console.WriteLine($"!! Error !! : Illegal parameter");
							Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
							return false;
						}
					}
				}
			}
			Console.WriteLine($"!! Error !! : Syntax error");
			Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
			return false;
		}
		
		
		
		public static bool IsMacro(string Text)
		{
			var c = new Cursor(Text);
			
			if (c.IsMark('#') >= 2){
				if (c.IsMark('*') > 0){
					return true;
				}
			}
			return false;
		}
	}
}
