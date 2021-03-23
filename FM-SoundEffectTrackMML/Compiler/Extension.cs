


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class ObjectExtension
{
	public static T DeepClone<T>(this T v)
	{
		using (var Stream = new System.IO.MemoryStream()){
			var Formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			Formatter.Serialize(Stream, v);
			Stream.Seek(0, System.IO.SeekOrigin.Begin);
			return (T)Formatter.Deserialize(Stream);
		}
	}
}
