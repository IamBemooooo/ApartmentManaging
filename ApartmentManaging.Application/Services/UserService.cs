using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.Authentication;
using ApartmentManaging.Shared.DTOs.Requests.User;
using ApartmentManaging.Shared.Exceptions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Application.Services
{
    /// <summary>
    /// Triển khai dịch vụ quản lý người dùng, xử lý logic nghiệp vụ.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _IUserRepository; // Đã đổi tên thành IUserRepository

        /// <summary>
        /// Khởi tạo UserService với các dependency cần thiết.
        /// </summary>
        /// <param name="mapper">Đối tượng AutoMapper để ánh xạ DTO và Entity.</param>
        /// <param name="IUserRepository">Repository để thao tác CRUD và các truy vấn cụ thể với User.</param>
        public UserService(IMapper mapper, IUserRepository IUserRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _IUserRepository = IUserRepository ?? throw new ArgumentNullException(nameof(IUserRepository));
        }

        /// <summary>
        /// Đăng ký người dùng mới vào hệ thống.
        /// </summary>
        /// <param name="userCreateDto">Dữ liệu người dùng cần tạo.</param>
        /// <returns>Đối tượng User đã được tạo từ DB, hoặc null nếu thêm thất bại.</returns>
        public async Task<User?> RegisterUserAsync(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null) throw new ArgumentNullException(nameof(userCreateDto));

            // Logic nghiệp vụ: Hash mật khẩu ở đây
            // Trong thực tế, bạn sẽ sử dụng một dịch vụ hash mật khẩu chuyên dụng
            // string hashedPassword = _passwordHasher.HashPassword(userCreateDto.Password);
            // userCreateDto.PasswordHash = hashedPassword; // Cập nhật DTO với mật khẩu đã hash

            var newUser = _mapper.Map<User>(userCreateDto);
            return await _IUserRepository.AddAsync(newUser);
        }

        /// <summary>
        /// Cập nhật thông tin của người dùng hiện có.
        /// </summary>
        /// <param name="userUpdateDto">Dữ liệu người dùng cần cập nhật.</param>
        /// <returns>Đối tượng User đã được cập nhật, hoặc null nếu không tìm thấy.</returns>
        /// <exception cref="BusinessException">Ném ra nếu không tìm thấy người dùng.</exception>
        public async Task<User?> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null) throw new ArgumentNullException(nameof(userUpdateDto));

            var existingUser = await _IUserRepository.GetByIdAsync(userUpdateDto.Id);
            if (existingUser == null)
            {
                throw new BusinessException($"Không tìm thấy người dùng với Id: {userUpdateDto.Id}");
            }

            // Ánh xạ DTO sang entity để cập nhật các thuộc tính
            // Tùy thuộc vào cách IUserRepository.UpdateAsync hoạt động, bạn có thể map vào existingUser
            // _mapper.Map(userUpdateDto, existingUser);
            // return await _IUserRepository.UpdateAsync(existingUser);

            // Hoặc nếu UpdateAsync chấp nhận một entity mới và dựa vào Id để cập nhật
            var updatedEntity = _mapper.Map<User>(userUpdateDto);
            return await _IUserRepository.UpdateAsync(updatedEntity);
        }

        /// <summary>
        /// Xóa người dùng khỏi hệ thống.
        /// </summary>
        /// <param name="userId">ID của người dùng cần xóa.</param>
        /// <returns>True nếu xóa thành công, ngược lại là false.</returns>
        /// <exception cref="BusinessException">Ném ra nếu không tìm thấy người dùng.</exception>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var existingUser = await _IUserRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                throw new BusinessException($"Không tìm thấy người dùng với Id: {userId}");
            }

            return await _IUserRepository.DeleteAsync(userId);
        }

        /// <summary>
        /// Lấy thông tin người dùng theo ID.
        /// </summary>
        /// <param name="userId">ID của người dùng.</param>
        /// <returns>Đối tượng User nếu tìm thấy, hoặc null.</returns>
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _IUserRepository.GetByIdAsync(userId);
        }
    }
}
