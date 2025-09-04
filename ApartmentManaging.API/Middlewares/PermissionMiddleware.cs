using ApartmentManaging.Domain.Interfaces;
using ApartmentManaging.Shared.DTOs.Response.API;
using System.Security.Claims;
using ApartmentManaging.Shared.Utils;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ApartmentManaging.API.Middlewares
{
    /// <summary>
    /// Middleware kiểm tra quyền truy cập (authorization) dựa trên Role và Permission.
    /// </summary>
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor khởi tạo middleware.
        /// </summary>
        /// <param name="next">Delegate đại diện cho middleware tiếp theo trong pipeline.</param>
        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        /// <summary>
        /// Hàm xử lý chính của middleware. Kiểm tra xem người dùng có quyền truy cập vào endpoint hiện tại không.
        /// </summary>
        public async Task InvokeAsync(HttpContext context, IPermissionRepository permissionRepo)
        {
            string path = context.Request.Path.Value ?? "";
            string method = context.Request.Method.ToUpper();

            // 1. Bỏ qua các endpoint public (không cần auth)
            var allowAnonymousEndpoints = new[]
            {
                "/api/auth/login",
                "/api/user/register"
            };

            if (allowAnonymousEndpoints.Any(e => path.StartsWith(e, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }

            // 2. Kiểm tra đã đăng nhập chưa
            if (context.User.Identity?.IsAuthenticated != true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(Messages.Unauthorized);
                return;
            }

            // 3. Lấy RoleId từ token
            var roleIdClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleIdClaim == null || !int.TryParse(roleIdClaim.Value, out int roleId))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync(Messages.Forbidden);
                return;
            }

            // 4. Truy vấn danh sách permission từ DB
            var permissions = await permissionRepo.GetPermissionsByRoleId(roleId);

            // 5. So sánh endpoint & method bằng Regex
            //    - Ví dụ: permission.ApiEndpoint = "/api/user/{id}"
            //    - Regex sẽ chuyển thành "/api/user/[^/]+" để khớp với thực tế như "/api/user/5"
            bool hasPermission = permissions.Any(p =>
            {
                // Chuyển endpoint dạng {id} thành regex để match được các giá trị động
                string pattern = "^" + Regex.Replace(p.ApiEndpoint, "\\{[^/]+\\}", "[^/]+") + "$";

                // So khớp path hiện tại với endpoint định nghĩa và kiểm tra HTTP method
                return Regex.IsMatch(path, pattern, RegexOptions.IgnoreCase)
                       && p.HttpMethod.Equals(method, StringComparison.OrdinalIgnoreCase);
            });

            if (!hasPermission)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new APIResponse<string>(
                    StatusCodes.Status403Forbidden,
                    Messages.Forbidden,
                    null
                );

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
                return;
            }


            await _next(context);
        }
    }
}
