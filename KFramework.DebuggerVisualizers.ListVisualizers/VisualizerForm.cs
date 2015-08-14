using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace KFramework.DebuggerVisualizers.ListVisualizers
{
    public partial class VisualizerForm : Form
    {
        private object _target;
        public VisualizerForm(object target)
        {
            InitializeComponent();
            _target = target;
        }
        DataTable dt;
        private void VisualizerForm_Load(object sender, EventArgs e)
        {
            KeyValuePair<Type, string> pair = (KeyValuePair<Type, string>)_target;

            var list = StringToObject(pair.Value, pair.Key);
            gvProperties.AutoGenerateColumns = true;
            dt = ListToDataTable((IList)list);
            gvProperties.DataSource = dt;

        }

        /// <summary>
        /// xml字串反序列化物件
        /// </summary>
        /// <param name="xmlStr">xml字串</param>
        /// <returns></returns>
        private object StringToObject(string xmlStr, Type type)
        {
            XmlSerializer ser = new XmlSerializer(type);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(xmlStr);
            MemoryStream stream = new MemoryStream(buffer);
            object result = ser.Deserialize(stream);
            stream.Dispose();
            return result;
        }

        private DataTable ListToDataTable(IList users)
        {
            //取得 UsersAttribute類別中的成員名稱和屬性型別
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(users[0].GetType());

            //新增DataTable欄位名稱和型別
            DataTable dt = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (prop.PropertyType.IsGenericType && (prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    dt.Columns.Add(prop.Name, prop.PropertyType.GetGenericArguments()[0]);
                else
                    dt.Columns.Add(prop.Name, prop.PropertyType);
            }
            //新增資料
            object[] values = new object[props.Count];
            foreach (var item in users)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var dv = dt.DefaultView;
                dv.RowFilter = txtFilter.Text.Trim();
                gvProperties.DataSource = dv;
            }
            catch
            {


            }
        }
    }
}
