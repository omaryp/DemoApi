using DemoApi.Models;
using DemoApi.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Translators
{
    public static class UserTranslator
    {
        public static Usuario TranslateAsUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new Usuario();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("Name"))
                item.Name = SqlHelper.GetNullableString(reader, "Name");

            if (reader.IsColumnExists("Email"))
                item.Email = SqlHelper.GetNullableString(reader, "Email");

            if (reader.IsColumnExists("Address"))
                item.Address = SqlHelper.GetNullableString(reader, "Address");

            if (reader.IsColumnExists("Mobile"))
                item.Mobile = SqlHelper.GetNullableString(reader, "Mobile");

            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetBoolean(reader, "IsActive");

            return item;
        }

        public static List<Usuario> TranslateAsUsersList(this SqlDataReader reader)
        {
            var list = new List<Usuario>();
            while (reader.Read())
            {
                list.Add(TranslateAsUser(reader, true));
            }
            return list;
        }
    }
}

