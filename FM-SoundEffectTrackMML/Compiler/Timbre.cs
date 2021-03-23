


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;



namespace FM_SoundEffectTrackMML
{
	[DataContract]
	public class Timbre
	{
		[DataContract]
		public class SParams
		{
			[DataContract]
			public class SOperator
			{
				[DataMember]
				public byte AR, DR, SR, RR, SL, TL, KS, MT, DT;
			}
			
			[DataMember]
			public int Number;
			
			[DataMember]
			public byte FB;
			public byte AL;
			
			[DataMember]
			public SOperator OP1 = new SOperator();
			
			[DataMember]
			public SOperator OP2 = new SOperator();
			
			[DataMember]
			public SOperator OP3 = new SOperator();
			
			[DataMember]
			public SOperator OP4 = new SOperator();
			
			[DataMember]
			public string Name;
		}
		[DataMember]
		SParams mTimbre = new SParams();
		
		public enum ePhase {
			None,
			Number,
			FB_AL,
			OP1,
			OP2,
			OP3,
			OP4,
		}
		[DataMember]
		ePhase mPhase = ePhase.Number;
		
		
		
		~Timbre()
		{
		}
		
		
		
		public Timbre()
		{
		}
		
		
		
		public SParams Params
		{
			get { return mTimbre; }
		}
		
		
		
		public bool Import(out ePhase Phase, string Text, int oLine)
		{
			var c = new Cursor(Text);
			
			Phase = ePhase.None;
			
			if (c.SkipSpace() >= 2){
				c.Commit();
				
				switch ((int)mPhase){
					case (int)ePhase.Number:{
						int Number = 0;
						
						if (c.IsMark('@') > 0){
							c.Commit();
							if (c.GetDecimal(ref Number, true) && Number >= 1 && Number <= 255){
								c.Commit();
								if (c.IsMark(':') > 0){
									c.Commit();
									if (c.IsMark('{') > 0){
										c.Commit();
										if (c.IsTerm){
											mTimbre.Number = Number;
											
											Phase = mPhase;
											mPhase = ePhase.FB_AL;
											return true;
										}
									}
								}
							} else {
								Console.WriteLine($"!! Error !! : Illegal parameter");
								Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
								return false;
							}
						}
						break;
					}
					case (int)ePhase.FB_AL:{
						int FB = 0;
						int AL = 0;
						
						if (c.GetValue(ref FB, false) && FB >= 0 && FB <= 7){
							c.Commit();
							if (c.GetValue(ref AL, true) && AL >= 0 && AL <= 7){
								c.Commit();
								if (c.IsTerm){
									mTimbre.FB = (byte)FB;
									mTimbre.AL = (byte)AL;
									
									Phase = mPhase;
									mPhase = ePhase.OP1;
									return true;
								}
							}
						} else {
							Console.WriteLine($"!! Error !! : Illegal parameter");
							Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
							return false;
						}
						break;
					}
					case (int)ePhase.OP1:{
						int AR = 0;
						int DR = 0;
						int SR = 0;
						int RR = 0;
						int SL = 0;
						int TL = 0;
						int KS = 0;
						int MT = 0;
						int DT = 0;
						
						if (c.GetValue(ref AR, false) && AR >= 0 && AR <= 31){
							c.Commit();
							if (c.GetValue(ref DR, false) && DR >= 0 && DR <= 31){
								c.Commit();
								if (c.GetValue(ref SR, false) && SR >= 0 && SR <= 31){
									c.Commit();
									if (c.GetValue(ref RR, false) && RR >= 0 && RR <= 15){
										c.Commit();
										if (c.GetValue(ref SL, false) && SL >= 0 && SL <= 15){
											c.Commit();
											if (c.GetValue(ref TL, false) && TL >= 0 && TL <= 127){
												c.Commit();
												if (c.GetValue(ref KS, false) && KS >= 0 && KS <= 3){
													c.Commit();
													if (c.GetValue(ref MT, false) && MT >= 0 && MT <= 15){
														c.Commit();
														if (c.GetValue(ref DT, true) && DT >= 0 && DT <= 7){
															c.Commit();
															if (c.IsTerm){
																mTimbre.OP1.AR = (byte)AR;
																mTimbre.OP1.DR = (byte)DR;
																mTimbre.OP1.SR = (byte)SR;
																mTimbre.OP1.RR = (byte)RR;
																mTimbre.OP1.SL = (byte)SL;
																mTimbre.OP1.TL = (byte)TL;
																mTimbre.OP1.KS = (byte)KS;
																mTimbre.OP1.MT = (byte)MT;
																mTimbre.OP1.DT = (byte)DT;
																
																Phase = mPhase;
																mPhase = ePhase.OP2;
																return true;
															}
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						Console.WriteLine($"!! Error !! : Illegal parameter");
						Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
						return false;
					}
					case (int)ePhase.OP2:{
						int AR = 0;
						int DR = 0;
						int SR = 0;
						int RR = 0;
						int SL = 0;
						int TL = 0;
						int KS = 0;
						int MT = 0;
						int DT = 0;
						
						if (c.GetValue(ref AR, false) && AR >= 0 && AR <= 31){
							c.Commit();
							if (c.GetValue(ref DR, false) && DR >= 0 && DR <= 31){
								c.Commit();
								if (c.GetValue(ref SR, false) && SR >= 0 && SR <= 31){
									c.Commit();
									if (c.GetValue(ref RR, false) && RR >= 0 && RR <= 15){
										c.Commit();
										if (c.GetValue(ref SL, false) && SL >= 0 && SL <= 15){
											c.Commit();
											if (c.GetValue(ref TL, false) && TL >= 0 && TL <= 127){
												c.Commit();
												if (c.GetValue(ref KS, false) && KS >= 0 && KS <= 3){
													c.Commit();
													if (c.GetValue(ref MT, false) && MT >= 0 && MT <= 15){
														c.Commit();
														if (c.GetValue(ref DT, true) && DT >= 0 && DT <= 7){
															c.Commit();
															if (c.IsTerm){
																mTimbre.OP2.AR = (byte)AR;
																mTimbre.OP2.DR = (byte)DR;
																mTimbre.OP2.SR = (byte)SR;
																mTimbre.OP2.RR = (byte)RR;
																mTimbre.OP2.SL = (byte)SL;
																mTimbre.OP2.TL = (byte)TL;
																mTimbre.OP2.KS = (byte)KS;
																mTimbre.OP2.MT = (byte)MT;
																mTimbre.OP2.DT = (byte)DT;
																
																Phase = mPhase;
																mPhase = ePhase.OP3;
																return true;
															}
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						Console.WriteLine($"!! Error !! : Illegal parameter");
						Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
						return false;
					}
					case (int)ePhase.OP3:{
						int AR = 0;
						int DR = 0;
						int SR = 0;
						int RR = 0;
						int SL = 0;
						int TL = 0;
						int KS = 0;
						int MT = 0;
						int DT = 0;
						
						if (c.GetValue(ref AR, false) && AR >= 0 && AR <= 31){
							c.Commit();
							if (c.GetValue(ref DR, false) && DR >= 0 && DR <= 31){
								c.Commit();
								if (c.GetValue(ref SR, false) && SR >= 0 && SR <= 31){
									c.Commit();
									if (c.GetValue(ref RR, false) && RR >= 0 && RR <= 15){
										c.Commit();
										if (c.GetValue(ref SL, false) && SL >= 0 && SL <= 15){
											c.Commit();
											if (c.GetValue(ref TL, false) && TL >= 0 && TL <= 127){
												c.Commit();
												if (c.GetValue(ref KS, false) && KS >= 0 && KS <= 3){
													c.Commit();
													if (c.GetValue(ref MT, false) && MT >= 0 && MT <= 15){
														c.Commit();
														if (c.GetValue(ref DT, true) && DT >= 0 && DT <= 7){
															c.Commit();
															if (c.IsTerm){
																mTimbre.OP3.AR = (byte)AR;
																mTimbre.OP3.DR = (byte)DR;
																mTimbre.OP3.SR = (byte)SR;
																mTimbre.OP3.RR = (byte)RR;
																mTimbre.OP3.SL = (byte)SL;
																mTimbre.OP3.TL = (byte)TL;
																mTimbre.OP3.KS = (byte)KS;
																mTimbre.OP3.MT = (byte)MT;
																mTimbre.OP3.DT = (byte)DT;
																
																Phase = mPhase;
																mPhase = ePhase.OP4;
																return true;
															}
															break;
														}
													}
												}
											}
										}
									}
								}
							}
						}
						Console.WriteLine($"!! Error !! : Illegal parameter");
						Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
						return false;
					}
					case (int)ePhase.OP4:{
						int AR = 0;
						int DR = 0;
						int SR = 0;
						int RR = 0;
						int SL = 0;
						int TL = 0;
						int KS = 0;
						int MT = 0;
						int DT = 0;
						string Name = "";
						
						if (c.GetValue(ref AR, false) && AR >= 0 && AR <= 31){
							c.Commit();
							if (c.GetValue(ref DR, false) && DR >= 0 && DR <= 31){
								c.Commit();
								if (c.GetValue(ref SR, false) && SR >= 0 && SR <= 31){
									c.Commit();
									if (c.GetValue(ref RR, false) && RR >= 0 && RR <= 15){
										c.Commit();
										if (c.GetValue(ref SL, false) && SL >= 0 && SL <= 15){
											c.Commit();
											if (c.GetValue(ref TL, false) && TL >= 0 && TL <= 127){
												c.Commit();
												if (c.GetValue(ref KS, false) && KS >= 0 && KS <= 3){
													c.Commit();
													if (c.GetValue(ref MT, false) && MT >= 0 && MT <= 15){
														c.Commit();
														if (c.GetValue(ref DT, false) && DT >= 0 && DT <= 7){
															c.Commit();
															if (c.GetString(ref Name)){
																c.Commit();
																if (c.IsMark('}') > 0){
																	c.Commit();
																	if (c.IsTerm){
																		mTimbre.OP4.AR = (byte)AR;
																		mTimbre.OP4.DR = (byte)DR;
																		mTimbre.OP4.SR = (byte)SR;
																		mTimbre.OP4.RR = (byte)RR;
																		mTimbre.OP4.SL = (byte)SL;
																		mTimbre.OP4.TL = (byte)TL;
																		mTimbre.OP4.KS = (byte)KS;
																		mTimbre.OP4.MT = (byte)MT;
																		mTimbre.OP4.DT = (byte)DT;
																		mTimbre.Name = Name;
																		
																		Phase = mPhase;
																		mPhase = ePhase.Number;
																		return true;
																	}
																}
																break;
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						Console.WriteLine($"!! Error !! : Illegal parameter");
						Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
						return false;
					}
				}
			}
			Console.WriteLine($"!! Error !! : Syntax error");
			Console.WriteLine($"Line {oLine+1} : Column {c.Preview+1}");
			return false;
		}
		
		
		
		public static bool IsTimbre(string Text)
		{
			var c = new Cursor(Text);
			
			if (c.SkipSpace() >= 2){
				c.Commit();
				if (c.IsMark('@') > 0){
					return true;
				}
				if (c.IsDecimal() > 0){
					return true;
				}
			}
			return false;
		}
	}
}
