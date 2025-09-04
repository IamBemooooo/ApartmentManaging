namespace ApartmentManaging.Shared.Utils
{
    /// <summary>
    /// Lớp chứa các hằng số thông điệp dùng chung trong ứng dụng.
    /// Giúp tránh việc lặp lại chuỗi thông điệp ở nhiều nơi.
    /// </summary>
    public static class Messages
    {
        public const string NotFound = "Resource not found.";
        public const string DataConstraintViolation = "Operation failed due to data constraint violation.";
        public const string ApartmentNotFound = "Apartment not found.";
        public const string ApartmentTypeNotFound = "Apartment type not found.";
        public const string InvalidInput = "Invalid input.";
        public const string Unauthorized = "Unauthorized access.";
        public const string Forbidden = "Forbidden access.";
        public const string InternalError = "An internal error occurred. Please try again later.";
        public const string CreatedSuccess = "Resource created successfully.";
        public const string CreatedFailed = "Resource creation failed.";
        public const string UpdatedSuccess = "Resource updated successfully.";
        public const string UpdatedFailed = "Resource update failed.";
        public const string DeletedSuccess = "Resource deleted successfully.";
        public const string DeletedFailed = "Resource deletion failed.";
        public const string Conflict = "Resource conflict occurred.";
        public const string BadRequest = "Bad request.";
        public const string ValidationFailed = "Validation failed.";
        public const string LoginSuccess = "Login successful.";
        public const string LoginFailed = "Login failed. Please check your credentials.";
        public const string RegistrationSuccess = "Registration successful.";
        public const string RegistrationFailed = "Registration failed. Please try again.";
        public const string GetDataSuccess = "Data retrieved successfully.";
        public const string UserNotFound = "User not found.";
        public const string GetUserSuccess = "User retrieved successfully.";
    }
}