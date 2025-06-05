using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManaging.Domain.Entities
{
    /// <summary>
    /// Đại diện cho một căn hộ trong hệ thống
    /// </summary>
    public class Apartment
    {
        // Trường private lưu Id căn hộ
        private int _id;

        // Trường private lưu Id loại căn hộ
        private int _apartmentTypeId;

        // Trường private lưu tên căn hộ
        private string _name;

        // Trường private lưu địa chỉ căn hộ
        private string _address;

        // Trường private lưu số tầng (nếu có)
        private int? _floorCount;

        // Trường private lưu giá thuê hoặc bán căn hộ
        private decimal _price;

        // Trường private lưu ngày tạo căn hộ trong hệ thống
        private DateTime _createdDate;

        /// <summary>
        /// Id căn hộ
        /// </summary>
        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Id loại căn hộ
        /// </summary>
        public int ApartmentTypeId
        {
            get => _apartmentTypeId;
            set => _apartmentTypeId = value;
        }

        /// <summary>
        /// Tên căn hộ
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Địa chỉ căn hộ
        /// </summary>
        public string Address
        {
            get => _address;
            set => _address = value;
        }

        /// <summary>
        /// Số tầng của căn hộ (có thể null)
        /// </summary>
        public int? FloorCount
        {
            get => _floorCount;
            set => _floorCount = value;
        }

        /// <summary>
        /// Giá căn hộ
        /// </summary>
        public decimal Price
        {
            get => _price;
            set => _price = value;
        }

        /// <summary>
        /// Ngày tạo căn hộ trong hệ thống
        /// </summary>
        public DateTime CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value;
        }
    }
}
