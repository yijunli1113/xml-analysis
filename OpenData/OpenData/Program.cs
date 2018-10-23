﻿using OpenData.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
//分支測試_1012

namespace OpenData
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = findOpenData();
            Repositorys aa = new Repositorys();
            var makeconn = aa.Connection();
            nodes.ForEach(nodes_data =>
                {
                    aa.Insert_Data_SQL(makeconn , nodes_data);
                });


            Console.ReadKey();

        }


        static List<OpenData> findOpenData()
        {
            List<OpenData> result = new List<OpenData>();
            var xml = XElement.Load(@"D:\軟體工程\OpenData\xml-analysis\datagovtw_dataset_20181009.xml");

            var nodes = xml.Descendants("node").ToList();

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                OpenData aa = new OpenData();

                aa.資料集名稱 = getValue(node, "資料集名稱");
                aa.資料集提供機關聯絡人 = getValue(node, "資料集提供機關聯絡人");
                aa.資料集描述 = getValue(node, "資料集描述");
                aa.授權方式 = getValue(node, "授權方式");


                
                result.Add(aa);


            };
            // 1015------------------------------
            Func<XElement, string, string> getValueFunc = (node, properuName) =>
            {
                return node.Element(properuName)?.Value?.Trim();
            };
            Action<List<OpenData>> showOpenDataAction = (x) =>
            {
                //Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
                x.GroupBy(node => node.資料集提供機關聯絡人).ToList()
                    .ForEach(group =>
                    {
                        var key = group.Key;
                        var groupDatas = group.ToList();
                        var message = $"資料集提供機關聯絡人:{key},共有{groupDatas.Count()}筆資料";
                        Console.WriteLine(message);
                    });
            };
            nodes.ToList()
                .ForEach(node =>
                {
                    OpenData aa= new OpenData();
                    aa.資料集名稱 = getValue(node, "資料集名稱");
                    aa.資料集提供機關聯絡人 = getValue(node, "資料集提供機關聯絡人");
                    aa.資料集描述 = getValue(node, "資料集描述");
                    aa.授權方式 = getValue(node, "授權方式");
                    result.Add(aa);
                });

            //--------------------------------
            result = nodes.ToList()
                    .Select(node =>
                    {
                        OpenData aa = new OpenData();
                        aa.資料集名稱 = getValue(node, "資料集名稱");
                        aa.資料集提供機關聯絡人 = getValue(node, "資料集提供機關聯絡人");
                        aa.資料集描述 = getValue(node, "資料集描述");
                        aa.授權方式 = getValue(node, "授權方式");
                        return aa;
                    }).ToList();

            //.Where(x => x.資料集提供機關聯絡人 != null)
            //.Where(x = x.資料集名稱 >10)
            //.Tolist();

            result = result.Where(x => x.資料集提供機關聯絡人 != null).ToList();
   
            return result;
        }

        private static string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();
        }

        public static void ShowOpenData(List<OpenData> nodes)
        {
            Console.WriteLine(string.Format("共收到{0}筆的資料 ", nodes.Count));
            nodes.GroupBy(node => node.資料集提供機關聯絡人).ToList()
                .ForEach(group =>
                {
                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"資料集提供機關聯絡人:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                }
                );
        }
    }
}
