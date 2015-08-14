using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace KFramework.DebuggerVisualizers.ListVisualizers
{
    public class ListVisualizerObjectSource : VisualizerObjectSource
    {
        public Type TargetType;

        public override void GetData(object target, Stream outgoingData)
        {
            KeyValuePair<Type, string> pair = new KeyValuePair<Type, string>(target.GetType(),ObjectToString(target));
            base.GetData(pair, outgoingData);
        }

        /// <summary>
        /// 序列化物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ObjectToString<T>(T value)
        {
            XmlSerializer ser = new XmlSerializer(value.GetType());
            MemoryStream stream = new MemoryStream();
            ser.Serialize(stream, value);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string result = reader.ReadToEnd();
            stream.Dispose();
            reader.Dispose();
            return result;

        }
    }
}
