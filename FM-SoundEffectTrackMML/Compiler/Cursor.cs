


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	class Cursor
	{
		const string cSpace = " \t";
		const string cDecimal = "0123456789";
		const string cHexadecimal = "0123456789ABCDEFabcdef";
		const string cMML = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+-@%$<>()&^[]/*:!|., \t";
		
		string mText;
		int moText = 0;
		int moPrev = 0;
		
		
		
		public Cursor(string Text)
		{
			mText = Text;
		}
		
		
		
		public int Current
		{
			get { return moText; }
		}
		
		
		
		public int Preview
		{
			get { return moPrev; }
		}
		
		
		
		public bool IsTerm
		{
			get { return (moText == mText.Length || (moText < mText.Length && mText[moText] == ';')); }
		}
		
		
		
		public char Char
		{
			get { return mText[moText]; }
		}
		
		
		
		public void Commit()
		{
			moPrev = moText;
		}
		
		
		
		public void Revert()
		{
			moText = moPrev;
		}
		
		
		
		public int IsChars(string Chars, bool bSkipSpace = true)
		{
			var oText = moText;
			for (; moText < mText.Length; ++moText){
				if (Chars.IndexOf(mText[moText]) < 0) break;
			}
			if (bSkipSpace) SkipSpace();
			return moText - oText;
		}
		
		
		
		public void Offset(int Offset)
		{
			moText += Offset;
			if (moText < 0) moText = 0;
			if (moText > mText.Length) moText = mText.Length;
		}
		
		
		
		public int SkipSpace()
		{
			return IsChars(cSpace, false);
		}
		
		
		
		public int IsComment()
		{
			var oText = moText;
			if (moText < mText.Length && mText[moText] == ';'){
				moText = mText.Length;
			}
			return moText - oText;
		}
		
		
		
		public int IsMark(char c, bool bSkipSpace = true)
		{
			var oText = moText;
			if (moText < mText.Length && mText[moText] == c){
				++moText;
				if (bSkipSpace) SkipSpace();
			}
			return moText - oText;
		}
		
		
		
		public int IsDecimal(bool bSkipSpace = true)
		{
			var oText = moText;
			if (moText < mText.Length && (mText[moText] == '+' || mText[moText] == '-')){
				++moText;
			}
			for (; moText < mText.Length; ++moText){
				if (cDecimal.IndexOf(mText[moText]) < 0) break;
			}
			if (bSkipSpace) SkipSpace();
			return moText - oText;
		}
		
		
		
		public int GetDecimal(bool bSkipSpace = true)
		{
			var oText = moText;
			
			int Result = 0;
			int.TryParse(mText.Substring(oText, IsDecimal(bSkipSpace)), out Result);
			return Result;
		}
		
		
		
		public bool GetDecimal(ref int Value, bool bTerm, bool bSkipSpace = true)
		{
			var oText = moText;
			if (IsDecimal(bSkipSpace) > 0){
				moText = oText;
				
				Value = GetDecimal(bSkipSpace);
				if (bTerm) return true;
				if (IsMark(',', bSkipSpace) > 0) return true;
				
				moText = oText;
			}
			return false;
		}
		
		
		
		public int IsValue(bool bSkipSpace = true)
		{
			var oText = moText;
			if (moText < mText.Length && mText[moText] == '$'){
				++moText;
				
				for (; moText < mText.Length; ++moText){
					if (cHexadecimal.IndexOf(mText[moText]) < 0) break;
				}
				if (bSkipSpace) SkipSpace();
				return moText - oText;
			} else {
				return IsDecimal(bSkipSpace);
			}
		}
		
		
		
		public int GetValue(bool bSkipSpace = true)
		{
			var oText = moText;
			if (moText < mText.Length && mText[moText] == '$'){
				var nValue = IsValue(bSkipSpace);
				if (nValue > 1){
					moText = oText;
					
					int Result = int.Parse(mText.Substring(moText+1, nValue-1), System.Globalization.NumberStyles.HexNumber);
					moText += nValue;
					return Result;
				}
				moText = oText;
				return 0;
			} else {
				return GetDecimal(bSkipSpace);
			}
		}
		
		
		
		public bool GetValue(ref int Value, bool bTerm, bool bSkipSpace = true)
		{
			var oText = moText;
			if (IsValue(bSkipSpace) > 0){
				moText = oText;
				
				Value = GetValue(bSkipSpace);
				if (bTerm) return true;
				if (IsMark(',', bSkipSpace) > 0) return true;
				
				moText = oText;
			}
			return false;
		}
		
		
		
		public int IsClock(bool bSkipSpace = true)
		{
			var oText = moText;
			if (moText < mText.Length && mText[moText] == '%'){
				++moText;
			}
			for (; moText < mText.Length; ++moText){
				if (cDecimal.IndexOf(mText[moText]) < 0) break;
			}
			if (bSkipSpace) SkipSpace();
			return moText - oText;
		}
		
		
		
		public bool GetClock(ref int Clock, int Semibreve, int DefaultDuration, bool bSkipSpace = true)
		{
			var oText = moText;
			if (IsClock(false) > 0){
				moText = oText;
				
				if (moText < mText.Length && mText[moText] == '%'){
					++moText;
					
					Clock = GetDecimal(bSkipSpace);
				} else {
					var Divide = GetDecimal(false);
					Clock = (Divide > 0)? Semibreve / Divide: DefaultDuration;
					GetDot(ref Clock, bSkipSpace);
				}
			} else {
				moText = oText;
				
				Clock = DefaultDuration;
				GetDot(ref Clock, bSkipSpace);
			}
			return (Clock > 0);
		}
		
		
		
		void GetDot(ref int Clock, bool bSkipSpace = true)
		{
			var Dot = Clock;
			for (; moText < mText.Length; ++moText){
				if (mText[moText] == '.' && Dot > 1){
					var Half = Dot >> 1;
					if ((Half << 1) == Dot){
						Dot = Half;
						Clock += Dot;
					} else {
						break;
					}
				} else {
					break;
				}
			}
			if (bSkipSpace) SkipSpace();
		}
		
		
		
		public string Substring(int oText, int nText)
		{
			return mText.Substring(oText, nText);
		}
		
		
		
		public bool GetString(ref string Word, bool bSkipSpace = true)
		{
			if ((moText+1) < mText.Length && mText[moText] == '\"'){
				var oHead = moText;
				var oTail = mText.IndexOf('\"', oHead+1);
				if (oHead >= 0 && oTail >= 0){
					moText += oTail-oHead+1;
					
					Word = mText.Substring(oHead+1, oTail-oHead-1);
					
					if (bSkipSpace) SkipSpace();
					return true;
				}
			}
			return false;
		}
		
		
		
		public bool Compare(string Word, bool bSkipSpace = true)
		{
			var oText = moText;
			
			int oWord = 0;
			for (; oWord < Word.Length; ++oWord){
				if (oText < mText.Length && mText[oText] == Word[oWord]){
					++oText;
					continue;
				} else {
					break;
				}
			}
			if (oWord == Word.Length){
				moText = oText;
				
				if (bSkipSpace) SkipSpace();
				return true;
			}
			return false;
		}
		
		
		
		public int IsMML(bool bSkipSpace = true)
		{
			return IsChars(cMML, bSkipSpace);
		}
	}
}
