using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdCampaign.BLL.Services.Adverts.DTO;
using AdCampaign.Common;
using AdCampaign.DAL.Entities;
using AdCampaign.DAL.Repositories.AdvertsStatistic;
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
        private readonly IAdvertStatisticRepository _statisticRepository;

        public ApplicationService(IApplicationRepository applicationRepository,
            IAdvertStatisticRepository statisticRepository)
        {
            _applicationRepository = applicationRepository;
            _statisticRepository = statisticRepository;
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

            await _statisticRepository.Increment(dto.AdvertId, AdvertStatisticType.Filled);

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