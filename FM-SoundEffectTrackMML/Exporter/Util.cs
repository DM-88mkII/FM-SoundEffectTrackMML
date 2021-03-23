


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FM_SoundEffectTrackMML
{
	public class Util {
		public class Sequence {
			List<Delivery.Part> maPart;
			int moPart = 0;
			int moPacket = -1;
			
			
			
			public Sequence(List<Delivery.Part> aPart)
			{
				maPart = aPart;
			}
			
			
			
			public bool IsTerm
			{
				get { return (moPart >= maPart.Count); }
			}
			
			
			
			public Delivery.Part Part
			{
				get { return (moPart < maPart.Count)? maPart[moPart]: null; }
			}
			
			
			
			public bool Pop(out Packet @Packet)
			{
				Packet = null;
				if (moPart < maPart.Count){
					if ((moPacket+1) < maPart[moPart].Packet.Count){
						Packet = maPart[moPart].Packet[++moPacket];
						return true;
					} else {
						++moPart;
						moPacket = -1;
						return Pop(out Packet);
					}
				}
				return false;
			}
			
			
			
			public bool Peek(out Packet @Packet)
			{
				Packet = null;
				if (!IsTerm){
					Packet = maPart[moPart].Packet[moPacket];
					return true;
				}
				return false;
			}
		}
		
		
		
		public class Volume {
			int mv = 0;
			
			
			
			public int Value
			{
				set { mv = value; }
				get { return mv; }
			}
			
			
			
			public int Clamp
			{
				get {
					var v = mv;
					v = (v >=  0)? v:  0;
					v = (v <= 15)? v: 15;
					return v;
				}
			}
		}
		
		
		
		public class Register {
			public class Param {
				int mValue = 0;
				bool mbModified = true;
				
				
				
				public int Value
				{
					set { mbModified = (mbModified || mValue != value); mValue = value; }
					get { mbModified = false; return mValue; }
				}
				
				
				
				public bool IsModified
				{
					get { return mbModified; }
				}
			}
			
			public Param FB = new Param();
			public Param AL = new Param();
			public Param LR = new Param();
			public Param AMS = new Param();
			public Param PMS = new Param();
			
			
			
			public class Operator {
				public Param AR = new Param();
				public Param DR = new Param();
				public Param SR = new Param();
				public Param RR = new Param();
				public Param SL = new Param();
				public Param TL = new Param();
				public Param KS = new Param();
				public Param MT = new Param();
				public Param DT = new Param();
				public Param SE = new Param();
				public Param AM = new Param();
			}
			
			public Operator[] aOP = new Operator[4]{
				new Operator(),
				new Operator(),
				new Operator(),
				new Operator(),
			};
		}
	}
}
