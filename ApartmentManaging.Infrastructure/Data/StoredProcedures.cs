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
            public const string Get_Paged_ApartmentTypes = "sp_GetPagedApartmentTypes";
            public const string Get_ApartmentType_By_Id = "sp_GetApartmentTypeById";
            public const string Add_Apartment_Type = "sp_AddApartmentType";
            public const string Update_Apartment_Type = "sp_UpdateApartmentType";
            public const string Delete_Apartment_Type = "sp_DeleteApartmentType";
        }

        public static class Apartment
        {
            public const string Get_Paged_Apartments = "sp_GetPagedApartments";
            public const string Get_Apartment_By_Id = "sp_GetApartmentById";
            public const string Add_Apartment = "sp_AddApartment";
            public const string Update_Apartment = "sp_UpdateApartment";
            public const string Delete_Apartment = "sp_DeleteApartment";
        }

        public static class User
        {
            public const string Insert_User = "sp_InsertUser";
            public const string Update_User = "sp_UpdateUser";
            public const string Delete_User = "sp_DeleteUser";
            public const string Get_User_By_Id = "sp_GetUserById";
            public const string Search_Users = "sp_SearchUsers";
            public const string Login_User = "sp_LoginUser";
        }

        public static class Permission
        {
            public const string Get_Permissions_By_RoleId = "sp_GetPermissionsByRoleId";
            public const string Add_Permission = "sp_AddPermission";
            public const string Update_Permission = "sp_UpdatePermission";
            public const string Delete_Permission = "sp_DeletePermission";
        }
    }
}
