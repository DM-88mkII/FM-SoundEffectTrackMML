


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;



namespace FM_SoundEffectTrackMML
{
	[DataContract]
	public class Packet {
		[DataContract]
		public enum eCommand : byte {
			o1c,	// 00
			o1ch,	// 01
			o1d,	// 02
			o1dh,	// 03
			o1e,	// 04
			o1f,	// 05
			o1fh,	// 06
			o1g,	// 07
			o1gh,	// 08
			o1a,	// 09
			o1ah,	// 0a
			o1b,	// 0b
			
			o2c,	// 0c
			o2ch,	// 0d
			o2d,	// 0e
			o2dh,	// 0f
			o2e,	// 10
			o2f,	// 11
			o2fh,	// 12
			o2g,	// 13
			o2gh,	// 14
			o2a,	// 15
			o2ah,	// 16
			o2b,	// 17
			
			o3c,	// 18
			o3ch,	// 19
			o3d,	// 1a
			o3dh,	// 1b
			o3e,	// 1c
			o3f,	// 1d
			o3fh,	// 1e
			o3g,	// 1f
			o3gh,	// 20
			o3a,	// 21
			o3ah,	// 22
			o3b,	// 23
			
			o4c,	// 24
			o4ch,	// 25
			o4d,	// 26
			o4dh,	// 27
			o4e,	// 28
			o4f,	// 29
			o4fh,	// 2a
			o4g,	// 2b
			o4gh,	// 2c
			o4a,	// 2d
			o4ah,	// 2e
			o4b,	// 2f
			
			o5c,	// 30
			o5ch,	// 31
			o5d,	// 32
			o5dh,	// 33
			o5e,	// 34
			o5f,	// 35
			o5fh,	// 36
			o5g,	// 37
			o5gh,	// 38
			o5a,	// 39
			o5ah,	// 3a
			o5b,	// 3b
			
			o6c,	// 3c
			o6ch,	// 3d
			o6d,	// 3e
			o6dh,	// 3f
			o6e,	// 40
			o6f,	// 41
			o6fh,	// 42
			o6g,	// 43
			o6gh,	// 44
			o6a,	// 45
			o6ah,	// 46
			o6b,	// 47
			
			o7c,	// 48
			o7ch,	// 49
			o7d,	// 4a
			o7dh,	// 4b
			o7e,	// 4c
			o7f,	// 4d
			o7fh,	// 4e
			o7g,	// 4f
			o7gh,	// 50
			o7a,	// 51
			o7ah,	// 52
			o7b,	// 53
			
			o8c,	// 54
			o8ch,	// 55
			o8d,	// 56
			o8dh,	// 57
			o8e,	// 58
			o8f,	// 59
			o8fh,	// 5a
			o8g,	// 5b
			o8gh,	// 5c
			o8a,	// 5d
			o8ah,	// 5e
			o8b,	// 5f
			
			r,		// 60
			q,		// 61
			t,		// 62
			T,		// 63
			DL,		// 64 DL and DH must always exist consecutively, and DH must come after DL
			DH,		// 65 DL and DH must always exist consecutively, and DH must come after DL
			R,		// 66
			RF,		// 67
			Rm,		// 68
			pM,		// 69
			pR,		// 6a
			pL,		// 6b
			pC,		// 6c
			J,		// 6d
			L,		// 6e
			Timbre,	// 6f
			
			yFB,	// 70
			yAL,	// 71
			yAR,	// 72
			yDR,	// 73
			ySR,	// 74
			yRR,	// 75
			ySL,	// 76
			yTL,	// 77
			yKS,	// 78
			yMT,	// 79
			yDT,	// 7a
			ySE,	// 7b
			
			// Slur
			o1c_ = 0x80,	// 80
			o1ch_,	// 81
			o1d_,	// 82
			o1dh_,	// 83
			o1e_,	// 84
			o1f_,	// 85
			o1fh_,	// 86
			o1g_,	// 87
			o1gh_,	// 88
			o1a_,	// 89
			o1ah_,	// 8a
			o1b_,	// 8b
			
			o2c_,	// 8c
			o2ch_,	// 8d
			o2d_,	// 8e
			o2dh_,	// 8f
			o2e_,	// 90
			o2f_,	// 91
			o2fh_,	// 92
			o2g_,	// 93
			o2gh_,	// 94
			o2a_,	// 95
			o2ah_,	// 96
			o2b_,	// 97
			
			o3c_,	// 98
			o3ch_,	// 99
			o3d_,	// 9a
			o3dh_,	// 9b
			o3e_,	// 9c
			o3f_,	// 9d
			o3fh_,	// 9e
			o3g_,	// 9f
			o3gh_,	// a0
			o3a_,	// a1
			o3ah_,	// a2
			o3b_,	// a3
			
			o4c_,	// a4
			o4ch_,	// a5
			o4d_,	// a6
			o4dh_,	// a7
			o4e_,	// a8
			o4f_,	// a9
			o4fh_,	// aa
			o4g_,	// ab
			o4gh_,	// ac
			o4a_,	// ad
			o4ah_,	// ae
			o4b_,	// af
			
			o5c_,	// b0
			o5ch_,	// b1
			o5d_,	// b2
			o5dh_,	// b3
			o5e_,	// b4
			o5f_,	// b5
			o5fh_,	// b6
			o5g_,	// b7
			o5gh_,	// b8
			o5a_,	// b9
			o5ah_,	// ba
			o5b_,	// bb
			
			o6c_,	// bc
			o6ch_,	// bd
			o6d_,	// be
			o6dh_,	// bf
			o6e_,	// c0
			o6f_,	// c1
			o6fh_,	// c2
			o6g_,	// c3
			o6gh_,	// c4
			o6a_,	// c5
			o6ah_,	// c6
			o6b_,	// c7
			
			o7c_,	// c8
			o7ch_,	// c9
			o7d_,	// ca
			o7dh_,	// cb
			o7e_,	// cc
			o7f_,	// cd
			o7fh_,	// ce
			o7g_,	// cf
			o7gh_,	// d0
			o7a_,	// d1
			o7ah_,	// d2
			o7b_,	// d3
			
			o8c_,	// d4
			o8ch_,	// d5
			o8d_,	// d6
			o8dh_,	// d7
			o8e_,	// d8
			o8f_,	// d9
			o8fh_,	// da
			o8g_,	// db
			o8gh_,	// dc
			o8a_,	// dd
			o8ah_,	// de
			o8b_,	// df
			
			// 
			v,		// e0
			V,		// e1
			vUp,	// e2
			vDown,	// e3
		}
		
		[DataMember]
		byte mCommand;
		
		[DataMember]
		byte mValue;
		
		[DataMember]
		int moText;
		
		[DataMember]
		int moLogger;
		
		
		
		public Packet(eCommand Command, int Value, int oText, int oLogger)
		{
			mCommand = (byte)Command;
			mValue = (byte)Value;
			moText = oText;
			moLogger = oLogger;
		}
		
		
		
		public byte Command
		{
			set { mCommand = value; }
			get { return mCommand; }
		}
		
		
		
		public byte Value
		{
			set { mValue = value; }
			get { return mValue; }
		}
		
		
		
		public int Column
		{
			set { moText = value; }
			get { return moText; }
		}
		
		
		
		public int Logger
		{
			set { moLogger = value; }
			get { return moLogger; }
		}
		
		
		
		public eCommand CommandEnum
		{
			set { mCommand = (byte)value; }
			get { return (eCommand)mCommand; }
		}
		
		
		
		public int ValueInt
		{
			set { mValue = unchecked((byte)value); }
			get { return (int)unchecked((sbyte)mValue); }
		}
		
		
		
		public bool IsScale
		{
			get { return ((mCommand >= (byte)eCommand.o1c && mCommand <= (byte)eCommand.o8b) || (mCommand >= (byte)eCommand.o1c_ && mCommand <= (byte)eCommand.o8b_)); }
		}
		
		
		
		public bool IsSlur
		{
			get { return (mCommand >= (byte)eCommand.o1c_ && mCommand <= (byte)eCommand.o8b_); }
		}
		
		
		
		public int Scale
		{
			get { return (IsScale)? (mCommand & 0x7f): -1; }
		}
		
		
		
		public bool AddSlur()
		{
			if (IsScale){
				mCommand |= 0x80;
				return true;
			}
			return false;
		}
		
		
		
		public static bool IsFullScale(int FullScale)
		{
			return (FullScale >= (int)eCommand.o1c && FullScale <= (int)eCommand.o8b);
		}
	}
}
