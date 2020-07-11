using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

public static class Extensions
{
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const string pwdchars = "ABCDEFGVWXYZ0123456789@@@!%~@_*%$@abcdefghijklmnop";

    private const string nums = "0123456789";
    private static Random random = new Random();


    public static DataTable ToDataTable<T>(this List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table 
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name, type);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

    public static string GetJSONFromXML(this XmlNodeList xmlNodeList) 
    {
        StringBuilder str = new StringBuilder();
        str.Append("[");
        if (xmlNodeList.Count > 0)
        {
            int xmlCount = xmlNodeList.Count, i = 1;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                str.Append("{");
                int childCount = xmlNode.ChildNodes.Count, j = 1;
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    string Name = childNode.Name;
                    string InnerText = childNode.InnerText;

                    str.Append(string.Format("\"{0}\" : \"{1}\"" + (childCount > j ? "," : ""), Name, InnerText));
                    j++;
                }
                str.Append("}" + (xmlCount > i ? "," : ""));
                i++;
            } 
        }
        str.Append("]");
        return str.ToString();
    }

    public static string GenerateRandomString()
    {
        return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GenerateRandomPwdString()
    {
        return new string(Enumerable.Repeat(pwdchars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GenerateRandomDigitString(int length)
    {
        var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
        return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
    }

    public static string GenerateRandomDateTime(int count)
    {
        //var dateNumber =string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Year , DateTime.Now.Month , DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Millisecond, DateTime.Now.ToOADate());
        var dateNumber = string.Format("{0}{1}{2}{3}{4}", DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond, count);
        return dateNumber;
    }

    public static int ActivationCodeGenerator(int ActivationCodeLast, int count)
    {
        var strVal = Convert.ToString(ActivationCodeLast + count);
        var intVal = Convert.ToInt32(strVal.PadRight(9, '0'));
        //int store =0;
        //string add = "000";
        //store = int.Parse((ActivationCodeLast) + add) + count;
        return intVal;
    }

    public static string GenerateRandomNumberString()
    {
        return new string(Enumerable.Repeat(nums, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string SplitCamelCase(this string str)
    {
        if (str != null)
            return Regex.Replace(
                          Regex.Replace(
                              str,
                              @"(\P{Ll})(\P{Ll}\p{Ll})",
                              "$1 $2"
                          ),
                          @"(\p{Ll})(\P{Ll})",
                          "$1 $2"
                      );
        else
            return string.Empty;
    }

    public static string Encrypt(this string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public static string Decrypt(this string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string GetFormattedValue(this int Value)
    {
        return Value.ToString("n0");
    }

    public static string GetFormattedValue(this decimal Value)
    {
        try
        {
            return Value.ToString("n0");
        }
        catch (Exception)
        {
            return "0";
        }
    }

    public static string GetFormattedDate(this DateTime Value)
    {
        if (Value == null)
        {
            return "";
        }
        return Value.ToString("dd/MM/yyyy");
    }

}

public static class CollectionExtentions
{
    public static void RemoveAll<T>(this IList<T> iList, IEnumerable<T> itemsToRemove)
    {
        var set = new HashSet<T>(itemsToRemove);

        var list = iList as List<T>;
        if (list == null)
        {
            int i = 0;
            while (i < iList.Count)
            {
                if (set.Contains(iList[i])) iList.RemoveAt(i);
                else i++;
            }
        }
        else
        {
            list.RemoveAll(set.Contains);
        }
    }
}

public static class EnumExt
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static int ToRawIntValue(this Enum value)
    {
        return Convert.ToInt32((Enum.Parse(value.GetType(), value.ToString())));
    }

    public static T GetEnumByStringValue<T>(this string Value) where T : struct, IConvertible
    {
        try
        {
            return (T)System.Enum.Parse(typeof(T), Value);
        }
        catch (Exception)
        {
            return (T)System.Enum.Parse(typeof(T), "None");
        }
    }

    public static string GetEnumDescriptionByStringValue<T>(this string Value)
    {
        try
        {
            if (!string.IsNullOrEmpty(Value))
            {
                int EnumVal = Convert.ToInt32(Value);
                Type type = typeof(T);
                var field = type.GetFields().AsEnumerable<FieldInfo>().Skip(1).Where(x => Convert.ToInt32(x.GetValue(x.Name)) == EnumVal).FirstOrDefault();
                if (field != null)
                {
                    DescriptionAttribute attrDesc = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute), true);
                    return attrDesc.Description;
                }
                return "None";
            }
            else
            {
                return "None";
            }
        }
        catch (Exception)
        {

            return "None";
        }
    }

    public static string ToDescription(this Enum value)
    {
        DescriptionAttribute attribute = value.GetType()
            .GetField(value.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute;
        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static string GetDescription(this Enum value)
    {
        try
        {

            if (value == null)
                return "-";

            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;

        }
        catch (Exception)
        {
            return null;
        }
    }

    public static string GetCookieKey(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;
        //if (Extensions.IsProduction())
        //{
        //    var CookieKey = string.Format("p{0}", attribute == null ? value.ToString() : attribute.Description);
        //    return CookieKey;
        //}

        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static string GetLabelClass(this Enum value)
    {
        if (value == null)
        {
            return "";
        }

        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                    as DescriptionAttribute;

        var Result = attribute == null ? value.ToString() : attribute.Description;
        if (Result == "Pending")
        {
            return "label-warning";
        }
        else if (Result == "Created")
        {
            return "label-success";
        }
        else if (Result == "Error")
        {
            return "label-danger";
        }
        else
        {
            return "";
        }
    }

    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static string RemoveSpecialCharactersFromContact(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if (c >= '0' && c <= '9')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static IEnumerable<string> GetEnumDescriptionsList(Type type)
    {
        var descs = new List<string>();
        var names = Enum.GetNames(type);
        foreach (var name in names)
        {
            var field = type.GetField(name);
            var fds = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
            foreach (DescriptionAttribute fd in fds)
            {
                descs.Add(fd.Description);
            }
        }
        return descs;
    }

}
