namespace ApartmentManaging.Infrastructure.Data
{
    /// <summary>
    /// Lưu trữ tên các stored procedure dùng trong toàn bộ ứng dụng.
    /// Giúp tránh hard-code tên thủ tục SQL rải rác trong source code.
    /// </summary>
    public static class StoredProcedures
    {
        public static class ApartmentType
        {
            public const string Get_Paged_ApartmentTypes = "sp_Get_Paged_ApartmentTypes";
            public const string Get_ApartmentType_By_Id = "sp_Get_ApartmentType_By_Id";
            public const string Add_Apartment_Type = "sp_Add_ApartmentType";
            public const string Update_Apartment_Type = "sp_Update_ApartmentType";
            public const string Delete_Apartment_Type = "sp_Delete_ApartmentType";
        }

        public static class Apartment
        {
            public const string Get_Paged_Apartments = "sp_Get_Paged_Apartments";
            public const string Get_Apartment_By_Id = "sp_Get_Apartment_By_Id";
            public const string Add_Apartment = "sp_Add_Apartment";
            public const string Update_Apartment = "sp_Update_Apartment";
            public const string Delete_Apartment = "sp_Delete_Apartment";
        }

        public static class User
        {
            public const string Insert_User = "sp_Insert_User";
            public const string Update_User = "sp_Update_User";
            public const string Delete_User = "sp_Delete_User";
            public const string Get_User_By_Id = "sp_Get_User_By_Id";
            public const string Search_Users = "sp_Search_Users";
            public const string Login_User = "sp_Login_User";
        }

        public static class Permission
        {
            public const string Get_Permissions_By_RoleId = "sp_Get_Permissions_By_RoleId";
            public const string Add_Permission = "sp_Add_Permission";
            public const string Update_Permission = "sp_Update_Permission";
            public const string Delete_Permission = "sp_Delete_Permission";
        }
    }
}
