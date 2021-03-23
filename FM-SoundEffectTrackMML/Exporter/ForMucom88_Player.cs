


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FM_SoundEffectTrackMML.OPNA;
using static FM_SoundEffectTrackMML.Util;



namespace FM_SoundEffectTrackMML
{
	namespace ForMucom88
	{
		public class Player
		{
			class Track {
				public Dictionary<int, Timbre> maTimbre;
				Sequence mSequence;
				
				public bool mb1st = true;
				public bool mbSlur = false;
				public int mClock = 1;
				public int mScale = -1;
				public int mRelativeVolume = 0;
				public Volume mAbsoluteVolume = new Volume();
				public int mDetune = 0;
				public int mStaccato = 0;
				public Timbre mTimbre = new Timbre();
				
				public int mReverv = 0;				// todo
				public bool mbReverv = false;		// todo
				public bool mbRevervMode = false;	// todo
				
				public bool mbLoop = false;
				public bool mbLoopMark = false;
				
				
				
				public Track(Dictionary<int, Timbre> aTimbre, List<Delivery.Part> aPart)
				{
					maTimbre = aTimbre;
					mSequence = new Sequence(aPart);
				}
				
				
				
				public bool IsTerm
				{
					get { return mSequence.IsTerm; }
				}
				
				
				
				public bool Peek(out Packet @Packet)
				{
					return mSequence.Peek(out Packet);
				}
				
				
				
				public CallstackLogger.Logger Logger(int oLogger)
				{
					return mSequence.Part.CallstackLogger[oLogger];
				}
				
				
				
				public bool Tick(ref StringBuilder Queue, ref int Clock, int oTrack, bool[] abKeyOn, bool[] abKeyOff, out bool bScale, ref Register TRegister)
				{
					abKeyOff[oTrack] = false;
					bScale = false;
					
					bool Result = true;
					if (!IsTerm){
						--mClock;
						if ((abKeyOn[oTrack] || mb1st) && mClock <= mStaccato && !mbSlur){
							Duration(ref Queue, ref Clock, false);
							
							abKeyOn[oTrack] = false;
							abKeyOff[oTrack] = true;
							
							mb1st = false;
						}
						if (mClock == 0){
							bool bBreak = false;
							while (!IsTerm && !bBreak){
								Packet Packet;
								if (mSequence.Pop(out Packet)){
									if (Packet.IsScale){
										Duration(ref Queue, ref Clock, false);
										#if false
										if (!mbLoopMark && mbLoop){
											mbLoopMark = true;
											Queue.Append($"L ");
										}
										#endif
										
										mScale = Packet.Scale;
										mbSlur = Packet.IsSlur;
										mClock = (Packet.Value > 0)? Packet.Value: 0x100;
										if (!mbSlur){
											abKeyOn[oTrack] = true;
										}
										bScale = true;
										bBreak = true;
									} else {
										switch ((int)Packet.CommandEnum){
											case (int)Packet.eCommand.r:{
												mbSlur = false;
												mClock = (Packet.Value > 0)? Packet.Value: 0x100;
												
												abKeyOn[oTrack] = false;
												abKeyOff[oTrack] = true;
												bBreak = true;
												break;
											}
											case (int)Packet.eCommand.q:{
												mStaccato = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.t:{
												Queue.Append($"t{Packet.Value} ");
												break;
											}
											case (int)Packet.eCommand.T:{
												Queue.Append($"T{Packet.Value} ");
												break;
											}
											case (int)Packet.eCommand.DL:{
												mDetune &= ~0xff;
												mDetune |= Packet.Value;
												break;
											}
											case (int)Packet.eCommand.DH:{
												mDetune &= 0xff;
												mDetune |= Packet.ValueInt<<8;
												break;
											}
											case (int)Packet.eCommand.Timbre:{
												var oTimbre = Packet.Value;
												var Params = maTimbre[oTimbre].Params;
												TRegister.FB.Value = Params.FB;
												TRegister.AL.Value = Params.AL;
												
												TRegister.aOP[0].AR.Value = Params.OP1.AR;
												TRegister.aOP[0].DR.Value = Params.OP1.DR;
												TRegister.aOP[0].SR.Value = Params.OP1.SR;
												TRegister.aOP[0].RR.Value = Params.OP1.RR;
												TRegister.aOP[0].SL.Value = Params.OP1.SL;
												TRegister.aOP[0].TL.Value = Params.OP1.TL;
												TRegister.aOP[0].KS.Value = Params.OP1.KS;
												TRegister.aOP[0].MT.Value = Params.OP1.MT;
												TRegister.aOP[0].DT.Value = Params.OP1.DT;
												
												TRegister.aOP[1].AR.Value = Params.OP2.AR;
												TRegister.aOP[1].DR.Value = Params.OP2.DR;
												TRegister.aOP[1].SR.Value = Params.OP2.SR;
												TRegister.aOP[1].RR.Value = Params.OP2.RR;
												TRegister.aOP[1].SL.Value = Params.OP2.SL;
												TRegister.aOP[1].TL.Value = Params.OP2.TL;
												TRegister.aOP[1].KS.Value = Params.OP2.KS;
												TRegister.aOP[1].MT.Value = Params.OP2.MT;
												TRegister.aOP[1].DT.Value = Params.OP2.DT;
												
												TRegister.aOP[2].AR.Value = Params.OP3.AR;
												TRegister.aOP[2].DR.Value = Params.OP3.DR;
												TRegister.aOP[2].SR.Value = Params.OP3.SR;
												TRegister.aOP[2].RR.Value = Params.OP3.RR;
												TRegister.aOP[2].SL.Value = Params.OP3.SL;
												TRegister.aOP[2].TL.Value = Params.OP3.TL;
												TRegister.aOP[2].KS.Value = Params.OP3.KS;
												TRegister.aOP[2].MT.Value = Params.OP3.MT;
												TRegister.aOP[2].DT.Value = Params.OP3.DT;
												
												TRegister.aOP[3].AR.Value = Params.OP4.AR;
												TRegister.aOP[3].DR.Value = Params.OP4.DR;
												TRegister.aOP[3].SR.Value = Params.OP4.SR;
												TRegister.aOP[3].RR.Value = Params.OP4.RR;
												TRegister.aOP[3].SL.Value = Params.OP4.SL;
												TRegister.aOP[3].TL.Value = Params.OP4.TL;
												TRegister.aOP[3].KS.Value = Params.OP4.KS;
												TRegister.aOP[3].MT.Value = Params.OP4.MT;
												TRegister.aOP[3].DT.Value = Params.OP4.DT;
												break;
											}
											case (int)Packet.eCommand.R:{
												mReverv = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.RF:{
												mbReverv = (Packet.Value > 0);
												break;
											}
											case (int)Packet.eCommand.Rm:{
												mbRevervMode = (Packet.Value > 0);
												break;
											}
											case (int)Packet.eCommand.pM:{
												TRegister.LR.Value = 0;
												Queue.Append($"p0 ");
												break;
											}
											case (int)Packet.eCommand.pR:{
												TRegister.LR.Value = 1;
												Queue.Append($"p1 ");
												break;
											}
											case (int)Packet.eCommand.pL:{
												TRegister.LR.Value = 2;
												Queue.Append($"p2 ");
												break;
											}
											case (int)Packet.eCommand.pC:{
												TRegister.LR.Value = 3;
												Queue.Append($"p3 ");
												break;
											}
											case (int)Packet.eCommand.J:{
												Queue.Append($"J ");
												break;
											}
											case (int)Packet.eCommand.L:{
												if (oTrack == 0){
													Queue.Append($"L ");
													mbLoop = true;
												}
												break;
											}
											case (int)Packet.eCommand.yFB:{
												TRegister.FB.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yAL:{
												TRegister.AL.Value = Packet.Value;
												break;
											}
											
											case (int)Packet.eCommand.yAR:{
												TRegister.aOP[oTrack].AR.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yDR:{
												TRegister.aOP[oTrack].DR.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.ySR:{
												TRegister.aOP[oTrack].SR.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yRR:{
												TRegister.aOP[oTrack].RR.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.ySL:{
												TRegister.aOP[oTrack].SL.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yTL:{
												TRegister.aOP[oTrack].TL.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yKS:{
												TRegister.aOP[oTrack].KS.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yMT:{
												TRegister.aOP[oTrack].DT.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.yDT:{
												TRegister.aOP[oTrack].DT.Value = Packet.Value;
												break;
											}
											case (int)Packet.eCommand.ySE:{
												TRegister.aOP[oTrack].SE.Value = Packet.Value;
												break;
											}
											
											case (int)Packet.eCommand.v:{
												mAbsoluteVolume.Value = Packet.Value + mRelativeVolume;
												TRegister.aOP[oTrack].TL.Value = s_aValue_TL[mAbsoluteVolume.Clamp];
												break;
											}
											case (int)Packet.eCommand.V:{
												mRelativeVolume = Packet.ValueInt;
												break;
											}
											case (int)Packet.eCommand.vUp:{
												mAbsoluteVolume.Value += Packet.Value;
												TRegister.aOP[oTrack].TL.Value = s_aValue_TL[mAbsoluteVolume.Clamp];
												break;
											}
											case (int)Packet.eCommand.vDown:{
												mAbsoluteVolume.Value -= Packet.Value;
												TRegister.aOP[oTrack].TL.Value = s_aValue_TL[mAbsoluteVolume.Clamp];
												break;
											}
										}
									}
								}
							}
							
						}
					}
					return Result;
				}
			}
			
			
			
			static readonly byte[] s_aValue_TL = new byte[]{
				42, 40, 37, 34, 32, 29, 26, 24, 21, 18, 16, 13, 10, 8, 5, 2,
			};
			
			Track[] maTrack = new Track[4];
			string mAlias = "C";
			
			
			
			~Player()
			{
			}
			
			
			
			public Player(Delivery @Delivery)
			{
				maTrack[0] = new Track(Delivery.TimbreLibrary, Delivery.PartLibrary.Bake('1'));
				maTrack[1] = new Track(Delivery.TimbreLibrary, Delivery.PartLibrary.Bake('2'));
				maTrack[2] = new Track(Delivery.TimbreLibrary, Delivery.PartLibrary.Bake('3'));
				maTrack[3] = new Track(Delivery.TimbreLibrary, Delivery.PartLibrary.Bake('4'));
			}
			
			
			
			public bool Convert(out string Muc)
			{
				bool Result = true;
				
				var Queue = new StringBuilder(0x4000);
				int nOutput = 0;
				{	// 
					
					{	// 
						var Register = OPNA.s_Reg_KeyOn;
						int Value = OPNA.s_aValue_Ch;
						Queue.Append($"{mAlias} o1q0S0,0,0,0 ");
						Queue.Append($"yTL,1,$7f yTL,2,$7f yTL,3,$7f yTL,4,$7f ");
						Queue.Append($"yKA,1,$00 yKA,2,$00 yKA,3,$00 yKA,4,$00 ");
						Queue.Append($"ySL,1,$ff ySL,2,$ff ySL,3,$ff ySL,4,$ff ");
						Queue.Append($"c%1& ");
						Append(ref Queue, ref nOutput, Register, Value);
						Queue.Append($"c%1& \r\n");
						Queue.Append($"{mAlias} ");
					}
					
					bool[] abTerm = new bool[4]{false, false, false, false};
					bool[] abKeyOn = new bool[4]{false, false, false, false};
					bool[] abKeyOff = new bool[4]{false, false, false, false};
					bool[] abScale = new bool[4]{false, false, false, false};
					int[] aScale = new int[4]{-1, -1, -1, -1};
					int[] aDetune = new int[4]{0, 0, 0, 0};
					int Clock = -1;
					
					var TRegister = new Register();
					var KeyOn = new Register.Param();
					var aBlock_FNumber = new Register.Param[]{
						new Register.Param(),
						new Register.Param(),
						new Register.Param(),
						new Register.Param(),
					};
					KeyOn.Value = s_aValue_Ch;
					
					while (Result && !(abTerm[0] && abTerm[1] && abTerm[2] && abTerm[3])){
						++Clock;
						
						{	// 
							int oTrack = 0;
							foreach (var t in maTrack){
								Result = t.Tick(ref Queue, ref Clock, oTrack, abKeyOn, abKeyOff, out abScale[oTrack], ref TRegister);
								if (Result){
									abTerm[oTrack] = t.IsTerm;
								} else {
									break;
								}
								++oTrack;
							}
						}
						
						if (Result){
							bool bKeyOff = false;
							bKeyOff = (abKeyOff[0])? abKeyOff[0]: bKeyOff;
							bKeyOff = (abKeyOff[1])? abKeyOff[1]: bKeyOff;
							bKeyOff = (abKeyOff[2])? abKeyOff[2]: bKeyOff;
							bKeyOff = (abKeyOff[3])? abKeyOff[3]: bKeyOff;
							
							bool bScale = false;
							bScale = (abScale[0])? abScale[0]: bScale;
							bScale = (abScale[1])? abScale[1]: bScale;
							bScale = (abScale[2])? abScale[2]: bScale;
							bScale = (abScale[3])? abScale[3]: bScale;
							
							if (bKeyOff || bScale){
								if (bKeyOff){
									// KeyOff
									var Register = OPNA.s_Reg_KeyOn;
									for (int oTrack = 0; oTrack < maTrack.Length; ++oTrack){
										if (abKeyOff[oTrack]) KeyOn.Value &= ~s_aValue_KeyOn[oTrack];
									}
									if (KeyOn.IsModified) Append(ref Queue, ref nOutput, Register, KeyOn.Value);
								}
								
								if (bScale){
									// KeyOn
									var Register = OPNA.s_Reg_KeyOn;
									for (int oTrack = 0; oTrack < maTrack.Length; ++oTrack){
										if (abScale[oTrack]) KeyOn.Value |= s_aValue_KeyOn[oTrack];
									}
									if (KeyOn.IsModified) Append(ref Queue, ref nOutput, Register, KeyOn.Value);
								}
							}
							
							{	// 
								if (TRegister.FB.IsModified || TRegister.AL.IsModified){
									var Register = OPNA.s_Reg_FB_AL;
									int Value = TRegister.AL.Value | (TRegister.FB.Value << 3);
									Append(ref Queue, ref nOutput, Register, Value);
								}
								
								for (int oTrack = 0; oTrack < maTrack.Length; ++oTrack){
									if (TRegister.aOP[oTrack].MT.IsModified || TRegister.aOP[oTrack].DT.IsModified){
										var Register = OPNA.s_aReg_DT_MT[oTrack];
										int Value = TRegister.aOP[oTrack].MT.Value | (TRegister.aOP[oTrack].DT.Value << 4);
										Append(ref Queue, ref nOutput, Register, Value);
									}
									if (TRegister.aOP[oTrack].TL.IsModified){
										var Register = OPNA.s_aReg_TL[oTrack];
										int Value = TRegister.aOP[oTrack].TL.Value;
										Append(ref Queue, ref nOutput, Register, Value);
									}
									if (TRegister.aOP[oTrack].AR.IsModified || TRegister.aOP[oTrack].KS.IsModified){
										var Register = OPNA.s_aReg_KS_AR[oTrack];
										int Value = TRegister.aOP[oTrack].AR.Value | (TRegister.aOP[oTrack].KS.Value << 6);
										Append(ref Queue, ref nOutput, Register, Value);
									}
									if (TRegister.aOP[oTrack].DR.IsModified || TRegister.aOP[oTrack].AM.IsModified){
										var Register = OPNA.s_aReg_AM_DR[oTrack];
										int Value = TRegister.aOP[oTrack].DR.Value | (TRegister.aOP[oTrack].AM.Value << 7);
										Append(ref Queue, ref nOutput, Register, Value);
									}
									if (TRegister.aOP[oTrack].SR.IsModified){
										var Register = OPNA.s_aReg_SR[oTrack];
										int Value = TRegister.aOP[oTrack].SR.Value;
										Append(ref Queue, ref nOutput, Register, Value);
									}
									if (TRegister.aOP[oTrack].SL.IsModified || TRegister.aOP[oTrack].RR.IsModified){
										var Register = OPNA.s_aReg_SL_RR[oTrack];
										int Value = TRegister.aOP[oTrack].RR.Value | (TRegister.aOP[oTrack].SL.Value << 4);
										Append(ref Queue, ref nOutput, Register, Value);
									}
									if (TRegister.aOP[oTrack].SE.IsModified){
										var Register = OPNA.s_aReg_SSGEG[oTrack];
										int Value = TRegister.aOP[oTrack].SE.Value;
										Append(ref Queue, ref nOutput, Register, Value);
									}
								}
							}
							
							{	// 
								int oTrack = 0;
								foreach (var t in maTrack){
									if (aScale[oTrack] != t.mScale
									||	aDetune[oTrack] != t.mDetune
										){
										aScale[oTrack] = t.mScale;
										aDetune[oTrack] = t.mDetune;
										
										var Detune = aDetune[oTrack];
										var oScale = aScale[oTrack];
										if (oScale >= 0 && oScale < OPNA.s_aValue_Block_FNumber.Length){
											var Register0 = OPNA.s_aaReg_Block_FNumber[oTrack][0];
											var Register1 = OPNA.s_aaReg_Block_FNumber[oTrack][1];
											var Value = OPNA.s_aValue_Block_FNumber[oScale] + Detune;
											aBlock_FNumber[oTrack].Value = Value;
											if (aBlock_FNumber[oTrack].IsModified){
												// The order cannot be changed
												Append(ref Queue, ref nOutput, Register1, aBlock_FNumber[oTrack].Value>>8);
												Append(ref Queue, ref nOutput, Register0, aBlock_FNumber[oTrack].Value&0xff);
											}
										} else {
											Console.WriteLine("!! Error !! : Out of scale");
											Result = false;
											break;
										}
									}
									++oTrack;
								}
								if (!Result){
									foreach (var t in maTrack){
										Packet Packet;
										if (t.Peek(out Packet)){
											if (Packet.Logger >= 0){
												var Logger = t.Logger(Packet.Logger);
												Logger.Callstack.Log(Logger.In_oLine, Packet.Column, Logger.In_nMacro);
											}
										} else {
											Console.WriteLine("!! Error !! : Fatal error");
										}
									}
								}
							}
						}
						
						if (maTrack[0].mbLoop && abTerm[0]){
							break;
						}
					}
					
					if (Result){
						Duration(ref Queue, ref Clock, !maTrack[0].mbLoop);
					}
				}
				Muc = Queue.ToString();
				return Result;
			}
			
			
			
			void Append(ref StringBuilder Queue, ref int nOutput, int Register, int Value)
			{
				if (nOutput++ == 0){
					Queue.Append($"\r\n{mAlias} ");
				} else {
					nOutput = (nOutput == 10)? 0: nOutput;
				}
				
				Queue.Append($"y${Register:x02},${Value:x02} ");
			}
			
			
			
			static void Duration(ref StringBuilder Queue, ref int Clock, bool bTerm)
			{
				if (Clock > 0){
					var Surplus = Clock & 0x7f;
					if (Surplus > 0){
						Clock -= Surplus;
						Queue.Append($"c%{Surplus}");
						if (!(Clock == 0 && bTerm)) Queue.Append("&");
					}
					while (Clock > 0){
						Clock -= 0x80;
						Queue.Append($"c%128");
						if (!(Clock == 0 && bTerm)) Queue.Append("&");
					}
					Queue.Append($" ");
				}
			}
		}
	}
}
