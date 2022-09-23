using MediatR;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Announcement
{
    public class GetAnnouncements
    {
        public class GetAnnouncementQuery : IRequest<AnnouncementViewModel>
        {

        }

        public class GetAnnouncementHandler : IRequestHandler<GetAnnouncementQuery, AnnouncementViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetAnnouncementHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;   
            }

            public async Task<AnnouncementViewModel> Handle(GetAnnouncementQuery request, CancellationToken cancellationToken)
            {
                var announcements = await _context.Announcements.DisplayedEntities().ToListAsync(cancellationToken);

                AnnouncementViewModel announcementViewModel = new()
                {
                    Announcements = _mapper.Map<List<AnnouncementDto>>(announcements)
                };

                return announcementViewModel;

            }
        }

        //duyuru kartı görünümü amacıyla eklenmiştir
        //22.09.2022 Murat Çalışkan
        public class GetAnnouncementCardQuery : IRequest<AnnouncementCardViewModel>
        {

        }

        public class GetAnnouncementCardHandler : IRequestHandler<GetAnnouncementCardQuery, AnnouncementCardViewModel>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetAnnouncementCardHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AnnouncementCardViewModel> Handle(GetAnnouncementCardQuery request, CancellationToken cancellationToken)
            {
                var announcements = await _context.Announcements.DisplayedEntities().ToListAsync(cancellationToken);

                AnnouncementCardViewModel announcementCardViewModel = new()
                {
                    AnnouncementCards = _mapper.Map<List<AnnouncementCardDto>>(announcements)
                };

                return announcementCardViewModel;

            }
        }

        //duyurunun idsine göre getirilmesi amacıyla eklenmiştir.
        //23.09.2022 Murat Çalışkan
        public class GetAnnouncementByIdQuery: IRequest<AnnouncementEntity>
        {
            public GetAnnouncementByIdQuery(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class GetAnnouncementByIdHandler : IRequestHandler<GetAnnouncementByIdQuery, AnnouncementEntity>
        {
            private readonly ApplicationContext _context;

            public GetAnnouncementByIdHandler(ApplicationContext context)
            {
                _context = context;
            }

            public async Task<AnnouncementEntity> Handle(GetAnnouncementByIdQuery request, CancellationToken cancellationToken)
            {
                var announcement = await _context.Announcements.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
                if (announcement == null)
                {
                    return null;
                }
                return announcement;
            }
        }
    }
}
