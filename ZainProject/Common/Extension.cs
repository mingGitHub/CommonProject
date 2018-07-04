using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZainProject.Common
{
    /// <summary>
    /// DataTable与泛型集合的互相转换
    /// </summary>
   public static  class Extension
    {
        /// <summary>
        /// 将datatable转换为泛型集合,利用反射来的
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="inputDataTable"></param>
        /// <returns></returns>
        public static List<TEntity> ToList<TEntity>(this DataTable inputDataTable) where TEntity : class, new()
        {
            if (inputDataTable == null)
            {
                throw new ArgumentNullException("input datatable is null");
            }
            Type type = typeof(TEntity);
            PropertyInfo[] propertyInfos = type.GetProperties();
            List<TEntity> lstEntitys = new List<TEntity>();
            foreach (DataRow row in inputDataTable.Rows)
            {
                object obj = Activator.CreateInstance(type);
                foreach (PropertyInfo pro in propertyInfos)
                {
                    foreach (DataColumn col in inputDataTable.Columns)
                    {
                        //如果直接查询的数据库，数据库是不区别大小写的，所以转换为小写忽略大小写的问题
                        if (col.ColumnName.ToLower().Equals(pro.Name.ToLower()))
                        {
                            //属性是否是可写的，如果是只读的属性跳过。
                            if (pro.CanWrite)
                            {
                                //判断类型，基本类型，如果是其他的类属性
                                if (pro.PropertyType == typeof(System.Int32))
                                {
                                    if (row[pro.Name.ToLower()] == DBNull.Value || string.IsNullOrEmpty(row[pro.Name.ToLower()].ToString()))
                                    {
                                        pro.SetValue(obj, 0, null);
                                    }
                                    else
                                    {
                                        pro.SetValue(obj, Convert.ToInt32(row[pro.Name.ToLower()]), null);
                                    }

                                }
                                else if (pro.PropertyType == typeof(System.String))
                                {
                                    pro.SetValue(obj, row[pro.Name.ToLower()].ToString().Trim(), null);
                                }
                                else if (pro.PropertyType == typeof(System.Boolean))
                                {
                                    pro.SetValue(obj, Convert.ToBoolean(row[pro.Name.ToLower()]), null);
                                }
                                else if (pro.PropertyType == typeof(System.DateTime))
                                {
                                    if (row[pro.Name.ToLower()] == DBNull.Value || string.IsNullOrEmpty(row[pro.Name.ToLower()].ToString()))
                                    {
                                        pro.SetValue(obj, Convert.ToDateTime("1900-01-01 00:00:00"), null);
                                    }
                                    else
                                    {
                                        pro.SetValue(obj, Convert.ToDateTime(row[pro.Name.ToLower()]), null);
                                    }

                                }
                                else if (pro.PropertyType == typeof(System.Int64))
                                {
                                    if (row[pro.Name.ToLower()] == DBNull.Value || string.IsNullOrEmpty(row[pro.Name.ToLower()].ToString()))
                                    {
                                        pro.SetValue(obj, 0, null);
                                    }
                                    else
                                    {
                                        pro.SetValue(obj, Convert.ToInt64(row[pro.Name.ToLower()]), null);
                                    }

                                }
                                else if (pro.PropertyType == typeof(System.Int16))
                                {
                                    if (row[pro.Name.ToLower()] == DBNull.Value || string.IsNullOrEmpty(row[pro.Name.ToLower()].ToString()))
                                    {
                                        pro.SetValue(obj, 0, null);
                                    }
                                    else
                                    {
                                        pro.SetValue(obj, Convert.ToInt16(row[pro.Name.ToLower()]), null);
                                    }

                                }
                                else if (pro.PropertyType == typeof(System.TimeSpan))
                                {
                                    if (row[pro.Name.ToLower()] == DBNull.Value || string.IsNullOrEmpty(row[pro.Name.ToLower()].ToString()))
                                    {
                                        pro.SetValue(obj, 0, null);
                                    }
                                    else
                                    {
                                        pro.SetValue(obj, TimeSpan.Parse(Convert.ToInt16(row[pro.Name.ToLower()]).ToString()), null);
                                    }

                                }
                                else if (pro.PropertyType == typeof(System.Decimal))
                                {
                                    if (row[pro.Name.ToLower()] == DBNull.Value || string.IsNullOrEmpty(row[pro.Name.ToLower()].ToString()))
                                    {
                                        pro.SetValue(obj, 0, null);
                                    }
                                    else
                                    {
                                        pro.SetValue(obj, Convert.ToDecimal(row[pro.Name.ToLower()]), null);
                                    }

                                }
                                else
                                {
                                    pro.SetValue(obj, row[pro.Name.ToLower()], null);
                                }

                            }
                        }
                    }
                }
                TEntity tEntity = obj as TEntity;
                lstEntitys.Add(tEntity);
            }
            return lstEntitys;
        }


        /// <summary>
        /// 将list转换为datatable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="inputList"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<TEntity>(this List<TEntity> inputList) where TEntity : class, new()
        {
            if (inputList == null)
            {
                throw new ArgumentNullException("参数inputList不可以为空");
            }
            DataTable dt = null;
            Type type = typeof(TEntity);
            if (inputList.Count == 0)
            {
                dt = new DataTable(type.Name);
                return dt;
            }
            else { dt = new DataTable(); }
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (var item in propertyInfos)
            {
                dt.Columns.Add(new DataColumn() { ColumnName = item.Name, DataType = item.PropertyType });
            }
            foreach (var item in inputList)
            {
                DataRow row = dt.NewRow();
                foreach (var pro in propertyInfos)
                {
                    row[pro.Name] = pro.GetValue(item, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }





    }
}
