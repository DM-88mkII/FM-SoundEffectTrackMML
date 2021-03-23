


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	class Track
	{
		const string cChannel = "1234";
		
		char mChannel;
		int moLine;
		MML mMML;
		
		
		
		~Track()
		{
		}
		
		
		
		public Track()
		{
		}
		
		
		
		public char Channel
		{
			get { return mChannel; }
		}
		
		
		
		public int Line
		{
			get { return moLine; }
		}
		
		
		
		public MML Mml
		{
			get { return mMML; }
		}
		
		
		
		public bool Import(string Text, int oLine)
		{
			var c = new Cursor(Text);
			char Channel = '?';
			int oBase = 0;
			
			Channel = c.Char;
			if (c.IsChars(cChannel, false) == 1){
				c.Commit();
				if (c.SkipSpace() > 0){
					c.Commit();
					
					oBase = c.Current;
					var nMML = c.IsMML();
					if (nMML >= 0){
						c.Commit();
						if (c.IsTerm){
							mChannel = Channel;
							moLine = oLine;
							mMML = new MML(oLine, oBase, Text.Substring(oBase, nMML));
							return true;
						}
					}
				}
			}
			Console.WriteLine($"!! Error !! : Syntax error");
			Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
			return false;
		}
		
		
		
		public static bool IsTrack(string Text)
		{
			var c = new Cursor(Text);
			
			if (c.IsChars(cChannel, false) == 1){
				if (c.SkipSpace() > 0){
					return true;
				}
			}
			return false;
		}
	}
}
