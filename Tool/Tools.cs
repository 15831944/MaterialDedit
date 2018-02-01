using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Tool
{
    public class Tools
    {
        public T ObjClone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }

        /// <summary>
        /// Json字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T JsonStingToObj<T>(string strJson)
        {
            if (strJson != "" || strJson != null)
            {
                return JsonConvert.DeserializeObject<T>(strJson);
            }
            else
            {
                return default(T);
            }
        }


        /// <summary>
        /// 对象转换成Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjToJsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
