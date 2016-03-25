using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Data;
using System.Collections;
using System.Reflection;

namespace KFramework.DebuggerVisualizers.ListVisualizers
{
    /// <summary>
    /// 20160325-因enum型別在xml會序列化失敗，所以改用DataTable的方式
    /// </summary>
    public class ListVisualizerObjectSource : VisualizerObjectSource
    {
        public Type TargetType;

        public override void GetData(object target, Stream outgoingData)
        {
            KeyValuePair<Type, string> pair = new KeyValuePair<Type, string>(target.GetType(), ToDataTableXmlString((IEnumerable)target));
            base.GetData(pair, outgoingData);
        }

        ///// <summary>
        ///// 序列化物件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public string ObjectToString<T>(T value)
        //{
        //    XmlSerializer ser = new XmlSerializer(value.GetType());
        //    MemoryStream stream = new MemoryStream();
        //    ser.Serialize(stream, value);
        //    stream.Position = 0;
        //    StreamReader reader = new StreamReader(stream);
        //    string result = reader.ReadToEnd();
        //    stream.Dispose();
        //    reader.Dispose();
        //    return result;

        //}

        public string ToDataTableXmlString(IEnumerable value)
        {
            var type = value.GetType().GetGenericArguments()[0];
            DataTable dataTable = new DataTable(type.Name);
            PropertyInfo[] Props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (object item in value)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            using (var memoryStream = new MemoryStream())
            {
                dataTable.WriteXml(memoryStream);
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }

        }
    }
}
