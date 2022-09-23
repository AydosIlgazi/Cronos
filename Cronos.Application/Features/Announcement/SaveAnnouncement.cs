using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronos.Application.Features.Announcement
{
    public class SaveAnnouncement
    {
        public class SaveAnnouncementCommand : IRequest<CreateAnnouncementDto>
        {
            public SaveAnnouncementCommand(CreateAnnouncementDto obj)
            {
                this.Obj = obj;
            }

            public CreateAnnouncementDto Obj { get; private set; }
        }

        public class SaveAnnouncementHandler : IRequestHandler<SaveAnnouncementCommand, CreateAnnouncementDto>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public SaveAnnouncementHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CreateAnnouncementDto> Handle(SaveAnnouncementCommand request, CancellationToken cancellationToken)
            {
                var announcement = new AnnouncementEntity();
                announcement = _mapper.Map(request.Obj,announcement);
                await _context.Announcements.AddAsync(announcement);
                await _context.SaveChangesAsync(cancellationToken);

                return request.Obj;
            }
        }


    }
}
