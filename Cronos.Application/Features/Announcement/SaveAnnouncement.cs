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
        public class SaveAnnouncementCommand : IRequest<bool>
        {
            public SaveAnnouncementCommand(CreateAnnouncementDto obj)
            {
                this.Obj = obj;
            }

            public CreateAnnouncementDto Obj { get; private set; }
        }

        public class SaveAnnouncementHandler : IRequestHandler<SaveAnnouncementCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public SaveAnnouncementHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(SaveAnnouncementCommand request, CancellationToken cancellationToken)
            {
                bool isSameOrder = false;
                var announcement = new AnnouncementEntity();
                announcement = _mapper.Map(request.Obj,announcement);
                // Order mantığı
                // order sırasında aynı sırada veri bulunması durumunda eklenilecek sıradan sonraki sıralar 1 artar.
                // 27.09.2022 Murat Çalışkan
                List<AnnouncementEntity> entities = await _context.Announcements.ToListAsync();
                foreach (var item in entities)
                {
                    if (item.Order == announcement.Order)
                    {
                        isSameOrder = true;
                    }
                }
                if (isSameOrder == true)
                {
                    foreach (var item in entities)
                    {
                        if (item.Order >= announcement.Order)
                        {
                            item.Order++;
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                // order mantığı sonu

                await _context.Announcements.AddAsync(announcement);
                var result = await _context.SaveChangesAsync(cancellationToken);
                if(result == 0)
                {
                    return false;
                }
                
                return true;
            }
        }


    }
}
