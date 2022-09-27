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
        public class UpdateAnnouncementCommand : IRequest<bool>
        {
            public UpdateAnnouncementCommand(AnnouncementEntity obj)
            {
                this.Obj = obj;
            }

            public AnnouncementEntity Obj { get; private set; }
        }

        public class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementCommand, bool>
        {
            private readonly ApplicationContext _context;
            public UpdateAnnouncementHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
            }

            public async Task<bool> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
            {
                bool isSameOrder = false;
                bool isNewOrderSmaller = true;
                var announcement = await _context.Announcements.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Obj.Id);
                if(announcement == null) {
                    return false; 
                }
                int oldOrder = announcement.Order;
                announcement = request.Obj;
                if (announcement.Order > oldOrder) 
                {
                    isNewOrderSmaller = false;
                }
                announcement.ModifiedDate = DateTime.Now;
                List<AnnouncementEntity> entities = await _context.Announcements.AsNoTracking().ToListAsync();

                // Order mantığı
                // yeni atanan order eskisinden büyük ve aynı order varsa eski orderla yeni order arasında kalan orderlar 1 azalır.
                // tam tersi durumunda eski orderla yeni order arasında kalan orderlar 1 artar.
                // 27.09.2022 Murat Çalışkan
                foreach (var item in entities)
                {
                    if (item.Order == announcement.Order && item.Id != announcement.Id)
                    {
                        isSameOrder = true;
                    }
                }
                if (isSameOrder == true)
                {
                    foreach (var item in entities)
                    {
                        if (item.Order >= announcement.Order && item.Order<oldOrder && isNewOrderSmaller==true && item.Id != announcement.Id)
                        {
                            item.Order++;
                            _context.Announcements.Update(item);
                        }else if(item.Order <= announcement.Order && item.Order > oldOrder && isNewOrderSmaller == false && item.Id != announcement.Id)
                        {
                            item.Order--;
                            _context.Announcements.Update(item);
                        }
                    }
                }
                // order mantığı sonu

                _context.Announcements.Update(announcement);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }

        public class DeleteAnnouncementCommand : IRequest<bool>
        {

            public DeleteAnnouncementCommand(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class DeleteAnnouncementHandler : IRequestHandler<DeleteAnnouncementCommand, bool>
        {
            private readonly ApplicationContext _context;
            public DeleteAnnouncementHandler(ApplicationContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
            {
                var announcement = await _context.Announcements.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
                if (announcement == null)
                {
                    return false;
                }
                announcement.IsDeleted = !announcement.IsDeleted;
                announcement.ModifiedDate = DateTime.Now;
                _context.Announcements.Update(announcement);
                var result =await _context.SaveChangesAsync(cancellationToken);
                if(result == 0)
                {
                    return false;
                }
                return true;
            }
        }

    }
}
