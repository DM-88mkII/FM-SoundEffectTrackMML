


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;



namespace FM_SoundEffectTrackMML
{
	namespace ForMucom88
	{
		public class Exporter
		{
			~Exporter()
			{
			}
			
			
			
			public Exporter()
			{
			}
			
			
			
			public bool Convert(out List<string> aMuc, Delivery @Delivery)
			{
				bool Result = true;
				
				aMuc = new List<string>();
				string Muc;
				
				Player Player = new Player(Delivery);
				Result = Player.Convert(out Muc);
				if (Result) aMuc.Add(Muc);
				
				if (Result){
					var Queue = new StringBuilder(0x4000);
					foreach (var v in aMuc) Queue.Append(v + "\r\n");
					
					{	// 
						var Text = Queue.ToString();
						Text = (String.IsNullOrEmpty(Text))? "\r\n": Text;
						Clipboard.SetText($"{Text}");
					}
				}
				return Result;
			}
		}
	}
}
