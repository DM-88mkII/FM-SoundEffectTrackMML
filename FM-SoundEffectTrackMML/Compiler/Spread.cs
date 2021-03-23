


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	public class Spread
	{
		char mChannel;
		string mText;
		
		
		
		~Spread()
		{
		}
		
		
		
		public Spread(char Channel, string Text)
		{
			mChannel = Channel;
			mText = Text;
		}
		
		
		
		public char Channel
		{
			get { return mChannel; }
		}
		
		
		
		public string Text
		{
			get { return mText; }
		}
	}
}
