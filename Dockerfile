# Dockerfile cho ASP.NET Core API (build từ thư mục gốc)
# 1. Sử dụng image .NET SDK để build project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copy toàn bộ source code vào image (bao gồm các project phụ thuộc)
COPY . .

# 3. Restore các package nuget cho project API
RUN dotnet restore "ApartmentManaging.API/ApartmentManaging.API.csproj"

# 4. Build project API ở chế độ Release
RUN dotnet build "ApartmentManaging.API/ApartmentManaging.API.csproj" -c Release -o /app/build

# 5. Publish project API ra thư mục /app/publish
RUN dotnet publish "ApartmentManaging.API/ApartmentManaging.API.csproj" -c Release -o /app/publish

# 6. Sử dụng image .NET Runtime để chạy app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 7. Copy file publish từ stage build sang stage runtime
COPY --from=build /app/publish .

# 8. Mở port 80 để truy cập API
EXPOSE 80

# 9. Lệnh chạy ứng dụng
ENTRYPOINT ["dotnet", "ApartmentManaging.API.dll"]

# ---
# Giải thích từng bước:
# 1. FROM ... AS build: Dùng image SDK để build project.
# 2. WORKDIR /src: Đặt thư mục làm việc là /src.
# 3. COPY . .: Copy toàn bộ source code vào image (bao gồm các project phụ thuộc).
# 4. RUN dotnet restore: Khôi phục các package nuget cho project API.
# 5. RUN dotnet build: Build project API ở chế độ Release.
# 6. RUN dotnet publish: Publish project API ra thư mục /app/publish.
# 7. FROM ... AS runtime: Dùng image runtime nhẹ hơn để chạy app.
# 8. WORKDIR /app: Đặt thư mục làm việc là /app.
# 9. COPY --from=build ...: Copy file publish sang stage runtime.
# 10. EXPOSE 80: Mở port 80 cho API.
# 11. ENTRYPOINT ...: Lệnh chạy ứng dụng khi container khởi động.
