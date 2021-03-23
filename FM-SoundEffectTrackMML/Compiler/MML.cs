


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	class MML
	{
		int mLine;
		int moBase;
		string mText;
		
		
		
		~MML()
		{
		}
		
		
		
		public MML(int Line, int oBase, string Text)
		{
			mLine = Line;
			moBase = oBase;
			mText = Text;
		}
		
		
		
		public int Line
		{
			get { return mLine; }
		}
		
		
		
		public int Base
		{
			get { return moBase; }
		}
		
		
		
		public string Text
		{
			get { return mText; }
		}
	}
}
