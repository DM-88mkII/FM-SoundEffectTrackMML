


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	public class Setting
	{
		public enum eKind {
			Error,
			OCTAVE_UPDOWN,
			VOLUME_UPDOWN,
		}
		
		const string cWord = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_";
		const string cWordDecimal = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_0123456789";
		
		const string cOCTAVE_UPDOWN = "OCTAVE_UPDOWN";
		const string cVOLUME_UPDOWN = "VOLUME_UPDOWN";
		
		const string cOCTAVE_UPDOWN_False = "><";
		const string cOCTAVE_UPDOWN_True = "<>";
		
		const string cVOLUME_UPDOWN_False = ")(";
		const string cVOLUME_UPDOWN_True = "()";
		
		bool mbOctaveSwap = false;
		bool mbVolumeSwap = false;
		
		
		
		public Setting()
		{
		}
		
		
		
		public bool OctaveSwap
		{
			get { return mbOctaveSwap; }
		}
		
		
		
		public bool VolumeSwap
		{
			get { return mbVolumeSwap; }
		}
		
		
		
		public bool Import(out eKind Setting, string Text, int oLine)
		{
			var c = new Cursor(Text);
			Setting = eKind.Error;
			
			if (c.IsMark('#') >= 1){
				c.Commit();
				
				var nWord = c.IsChars(cWordDecimal, false);
				if (nWord > 0){
					var Word = c.Substring(c.Preview, nWord).ToUpper();
					if (Word == cOCTAVE_UPDOWN){
						if (c.SkipSpace() > 0){
							c.Commit();
							
							var oChar = c.Current;
							var nChar = c.IsChars(cOCTAVE_UPDOWN_False, false);
							if (nChar == cOCTAVE_UPDOWN_False.Length){
								c.SkipSpace();
								c.Commit();
								if (c.IsTerm){
									var UpDown = c.Substring(oChar, nChar);
									if (UpDown == cOCTAVE_UPDOWN_False){
										Setting = eKind.OCTAVE_UPDOWN;
										mbOctaveSwap = false;
										return true;
									}
									if (UpDown == cOCTAVE_UPDOWN_True){
										Setting = eKind.OCTAVE_UPDOWN;
										mbOctaveSwap = true;
										return true;
									}
									
									Console.WriteLine($"!! Error !! : Illegal parameter");
									Console.WriteLine($"Line {oLine+1} : Column {oChar+1}");
									return false;
								}
							}
						}
					}
					if (Word == cVOLUME_UPDOWN){
						if (c.SkipSpace() > 0){
							c.Commit();
							
							var oChar = c.Current;
							var nChar = c.IsChars(cVOLUME_UPDOWN_False, false);
							if (nChar == cVOLUME_UPDOWN_False.Length){
								c.SkipSpace();
								c.Commit();
								if (c.IsTerm){
									var UpDown = c.Substring(oChar, nChar);
									if (UpDown == cVOLUME_UPDOWN_False){
										Setting = eKind.VOLUME_UPDOWN;
										mbVolumeSwap = false;
										return true;
									}
									if (UpDown == cVOLUME_UPDOWN_True){
										Setting = eKind.VOLUME_UPDOWN;
										mbVolumeSwap = true;
										return true;
									}
									
									Console.WriteLine($"!! Error !! : Illegal parameter");
									Console.WriteLine($"Line {oLine+1} : Column {oChar+1}");
									return false;
								}
							}
						}
					}
				}
			}
			Console.WriteLine($"!! Error !! : Syntax error");
			Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
			return false;
		}
		
		
		
		public static bool IsSetting(string Text)
		{
			var c = new Cursor(Text);
			
			if (c.IsMark('#') >= 1){
				if (c.IsChars(cWord) > 0){
					return true;
				}
			}
			return false;
		}
	}
}
