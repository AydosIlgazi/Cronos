using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cronos.Application.Features.Announcement
{
    public class UpdateAnnouncement
    {
        public class UpdateAnnouncementCommand : IRequest<AnnouncementEntity>
        {
            public UpdateAnnouncementCommand(AnnouncementEntity obj)
            {
                this.Obj = obj;
            }

            public AnnouncementEntity Obj { get; private set; }
        }

        public class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementCommand, AnnouncementEntity>
        {
            private readonly ApplicationContext _context;
            public UpdateAnnouncementHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
            }

            public async Task<AnnouncementEntity> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
            {
                var announcement = await _context.Announcements.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Obj.Id);
                if(announcement == null) {
                    return null; 
                }
                announcement = request.Obj;
                announcement.ModifiedDate = DateTime.Now;
                _context.Announcements.Update(announcement);
                await _context.SaveChangesAsync(cancellationToken);

                return announcement;
            }
        }

        public class DeleteAnnouncementCommand : IRequest<AnnouncementEntity>
        {

            public DeleteAnnouncementCommand(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class DeleteAnnouncementHandler : IRequestHandler<DeleteAnnouncementCommand, AnnouncementEntity>
        {
            private readonly ApplicationContext _context;
            public DeleteAnnouncementHandler(ApplicationContext context)
            {
                _context = context;
            }

            public async Task<AnnouncementEntity> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
            {
                var announcement = await _context.Announcements.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
                if (announcement == null)
                {
                    return null;
                }
                announcement.IsDeleted = !announcement.IsDeleted;
                announcement.ModifiedDate = DateTime.Now;
                _context.Announcements.Update(announcement);
                await _context.SaveChangesAsync(cancellationToken);

                return announcement;
            }
        }

    }
}
