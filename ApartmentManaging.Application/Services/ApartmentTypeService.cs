using ApartmentManaging.Application.Interfaces;
using ApartmentManaging.Domain.Entities;
using ApartmentManaging.Domain.Interfaces.BaseInterfaces;
using ApartmentManaging.Infrastructure.Repositories;
using ApartmentManaging.Shared.DTOs.Common;
using ApartmentManaging.Shared.DTOs.Requests.ApartmentType;
using ApartmentManaging.Shared.Exceptions;
using AutoMapper;
using Azure.Core;

namespace ApartmentManaging.Application.Services
{
    public class ApartmentTypeService : IApartmentTypeService
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<ApartmentType> _IBaseApartmentTypeRepository;
        private readonly IGetPagedRepository<PagingRequest, PagingResponse<ApartmentType>> _IGetPagedAparmentTypeRepository;

        public ApartmentTypeService(IBaseRepository<ApartmentType> IBaseApartmentTypeRepository,
            IGetPagedRepository<PagingRequest, PagingResponse<ApartmentType>> IGetPagedAparmentTypeRepository,
            IMapper mapper)
        {
            _IBaseApartmentTypeRepository = IBaseApartmentTypeRepository ?? throw new ArgumentNullException(nameof(IBaseApartmentTypeRepository));
            _IGetPagedAparmentTypeRepository = IGetPagedAparmentTypeRepository ?? throw new ArgumentNullException(nameof(IGetPagedAparmentTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApartmentType?> AddApartmentTypeAsync(ApartmentTypeCreateDto request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var entity = _mapper.Map<ApartmentType>(request);

            return await _IBaseApartmentTypeRepository.AddAsync(entity);
        }

        public async Task<bool> DeleteApartmentTypeAsync(int id)
        {
            // Giả sử bạn có method GetByIdAsync để lấy đối tượng theo id
            var entity = await _IBaseApartmentTypeRepository.GetByIdAsync(id);
            if (entity == null)
            {
                // Ném lỗi hoặc trả về false, tùy cách bạn muốn xử lý
                // Ví dụ ném lỗi BusinessException:
                throw new BusinessException($"Không tìm thấy loại căn hộ");
            }

            return await _IBaseApartmentTypeRepository.DeleteAsync(id);
        }

        public async Task<PagingResponse<ApartmentType>> GetPagedApartmentTypesAsync(PagingRequest filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var pagedResult = await _IGetPagedAparmentTypeRepository.GetPagedAsync(filter);

            if (pagedResult == null)
            {
                // Khởi tạo một PagingResponse trống nếu repository trả về null
                pagedResult = new PagingResponse<ApartmentType>
                {
                    ListData = new List<ApartmentType>(),
                    TotalRecord = 0,
                    PageSize = filter.PageSize,
                    CurrentPage = filter.PageIndex,
                    TotalPage = 0
                };
                return pagedResult;
            }

            pagedResult.PageSize = filter.PageSize;
            pagedResult.CurrentPage = filter.PageIndex;

            if (pagedResult.TotalRecord > 0)
            {
                pagedResult.TotalPage = (int)Math.Ceiling((double)pagedResult.TotalRecord / filter.PageSize);
            }
            else
            {
                pagedResult.TotalPage = 0;
            }

            return pagedResult;
        }


        public async Task<ApartmentType?> UpdateApartmentTypeAsync(ApartmentType request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            // Giả sử bạn có method GetByIdAsync để lấy đối tượng theo id
            var entity = await _IBaseApartmentTypeRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                // Ném lỗi hoặc trả về false, tùy cách bạn muốn xử lý
                // Ví dụ ném lỗi BusinessException:
                throw new BusinessException($"Không tìm thấy loại căn hộ");
            }
            return await _IBaseApartmentTypeRepository.UpdateAsync(request);
        }
    }
}
