using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories.Application;

namespace AdCampaign.BLL.Services.Adverts
{
    public interface IApplicationService
    {
        Task<Result> Create(CreateApplicationDto dto);

        Task<Result<IEnumerable<ApplicationListItemDto>>> Get(long userId, Role userRole, long? advertId);
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Result> Create(CreateApplicationDto dto)
        {
            await _applicationRepository.Insert(new Application()
            {
                AdvertId = dto.AdvertId,
                DateCreated = DateTime.UtcNow,
                Phone = dto.Phone,
                Email = dto.Email
            });

            return new();
        }

        public async Task<Result<IEnumerable<ApplicationListItemDto>>> Get(long userId, Role userRole, long? advertId)
        {
            IEnumerable<Application> result;
            if (userRole == Role.Advertiser)
            {
                result = await _applicationRepository.Get(userId, advertId);
            }
            else
            {
                result = await _applicationRepository.Get(null, advertId);
            }

            return new(result.Select(application => new ApplicationListItemDto()
            {
                AdvertId = application.AdvertId,
                Email = application.Email,
                Phone = application.Phone,
                AdvertName = application.Advert.Name
            }));
        }
    }
}