


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;



namespace FM_SoundEffectTrackMML
{
	class Program
	{
		[STAThread]
		static void Main(string[] aArg)
		{
			if (aArg.Length >= 1){
				var FileMml = aArg[0];
				
				if (File.Exists(FileMml)){
					Console.WriteLine($"Reading...");
					Console.WriteLine($"\"{FileMml}\"");
					var aText = File.ReadAllLines(FileMml);
					Delivery Delivery;
					
					var p = new Parser();
					if (p.Parse(out Delivery, aText)){
						#if TEST//[
						if (Delivery.Export(FileVmt)){
							Console.WriteLine("Completed");
						}
						#endif//]
						
						{	// 
							List<string> aMuc;
							var Exporter = new ForMucom88.Exporter();
							Exporter.Convert(out aMuc, Delivery);
						}
					}
				} else {
					Console.WriteLine($"!! Error !! : File not found : \"{FileMml}\"");
				}
			} else {
				Console.WriteLine("input_vmt.mml");
			}
//			Console.ReadKey();
		}
	}
}
