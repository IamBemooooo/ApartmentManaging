using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.Apartment;
using ApartmentManaging.Shared.DTOs.Response.Apartment;
using ApartmentManaging.Shared.Exceptions;
using AutoMapper;
using Azure.Core;

namespace ApartmentManaging.Application.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Apartment> _IBaseApartmentRepository;
        private readonly IGetPagedRepository<PagingRequest, PagingResponse<ApartmentViewDto>> _IGetPagedAparmentRepository;

        public ApartmentService(IMapper mapper,
            IBaseRepository<Apartment> IBaseApartmentRepository,
            IGetPagedRepository<PagingRequest, PagingResponse<ApartmentViewDto>> IGetPagedAparmentRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _IBaseApartmentRepository = IBaseApartmentRepository ?? throw new ArgumentNullException(nameof(IBaseApartmentRepository));
            _IGetPagedAparmentRepository = IGetPagedAparmentRepository ?? throw new ArgumentNullException(nameof(IGetPagedAparmentRepository));
        }

        public async Task<Apartment?> CreateApartmentAsync(ApartmentCreateDto apartment)
        {
            if (apartment == null) throw new ArgumentNullException(nameof(apartment));

            var entity = _mapper.Map<Apartment>(apartment);
            return await _IBaseApartmentRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteApartmentByIdAsync(int apartmentId)
        {
            var existingApartment = await _IBaseApartmentRepository.GetByIdAsync(apartmentId);

            if (existingApartment == null)
            {
                // Có thể log lỗi, hoặc throw exception tùy mục đích
                throw new BusinessException($"Không tìm thấy căn hộ");
            }

            return await _IBaseApartmentRepository.DeleteAsync(apartmentId);
        }


        public async Task<Apartment?> GetApartmentByIdAsync(int apartmentId)
        {
            return await _IBaseApartmentRepository.GetByIdAsync(apartmentId);
        }

        public async Task<PagingResponse<ApartmentViewDto>> GetPagedApartmentsAsync(PagingRequest pagingRequest)
        {
            if (pagingRequest == null) throw new ArgumentNullException(nameof(pagingRequest));

            var response = await _IGetPagedAparmentRepository.GetPagedAsync(pagingRequest);

            response.TotalPage = (int)Math.Ceiling((double)response.TotalRecord / pagingRequest.PageSize);

            return response;
        }


        public async Task<Apartment?> UpdateApartmentAsync(ApartmentUpdateDto apartment)
        {
            if (apartment == null) throw new ArgumentNullException(nameof(apartment));

            // Giả sử bạn có method GetByIdAsync để lấy đối tượng theo id
            var existingApartment = await _IBaseApartmentRepository.GetByIdAsync(apartment.Id);
            if (existingApartment == null)
            {
                // Ném lỗi hoặc trả về false, tùy cách bạn muốn xử lý
                // Ví dụ ném lỗi BusinessException:
                throw new BusinessException($"Không tìm thấy căn hộ");
            }

            var entity = _mapper.Map<Apartment>(apartment);
            return await _IBaseApartmentRepository.UpdateAsync(entity);
        }
    }
}
