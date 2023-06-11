using System.Data;
using System.Reflection;

namespace ShopBridge
{
    public static class Generic
    {
        public static List<T> ConvertTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;

        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] != DBNull.Value)
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(pro.Name);
                            Type propertytype = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                            pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], propertytype), null);
                        }
                        else
                        {
                            pro.SetValue(obj, null, null);
                        }
                    }
                    else
                    {
                        continue;
                    }

                }
            }
            return obj;
        }
    }
}
