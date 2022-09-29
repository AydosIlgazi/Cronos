using Microsoft.EntityFrameworkCore.InMemory.Query.Internal;
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
            public UpdateAnnouncementCommand(UpdateAnnouncementDto obj)
            {
                this.Obj = obj;
            }

            public UpdateAnnouncementDto Obj { get; private set; }
        }

        public class UpdateAnnouncementHandler : IRequestHandler<UpdateAnnouncementCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public UpdateAnnouncementHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
            {
                bool isSameOrder = false;
                bool isNewOrderSmaller = true;
                var announcement = await _context.Announcements.FirstOrDefaultAsync(c => c.Id == request.Obj.Id);
                if(announcement == null) {
                    return false; 
                }
                int oldOrder = announcement.Order;
                announcement = _mapper.Map(request.Obj,announcement);
                if (announcement.Order > oldOrder) 
                {
                    isNewOrderSmaller = false;
                }
                announcement.ModifiedDate = DateTime.Now;
                List<AnnouncementEntity> entities = await _context.Announcements.ToListAsync();

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
                        }else if(item.Order <= announcement.Order && item.Order > oldOrder && isNewOrderSmaller == false && item.Id != announcement.Id)
                        {
                            item.Order--;
                        }
                    }
                }
                // order mantığı sonu

                var result =await _context.SaveChangesAsync(cancellationToken);

                if(result == 0)
                {
                    return false;
                }
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
                var announcement = await _context.Announcements.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (announcement == null)
                {
                    return false;
                }
                // Order mantığı
                // silinen duyurunun orderi en sondaki orderi alır. silinen duyurunun sonrasında bulunan orderlar 1 azalır.
                // 28.09.2022 Murat Çalışkan
                if(announcement.IsDeleted == false)
                {
                    List<AnnouncementEntity> entities = await _context.Announcements.DisplayedEntitiesCms().ToListAsync();
                    var lastItem = entities.LastOrDefault();
                    int oldOrder = announcement.Order;
                    announcement.Order = lastItem.Order;
                    foreach (var item in entities)
                    {
                        if (item.Order > oldOrder && item.Id !=announcement.Id)
                        {
                            item.Order--;
                        }
                    }

                }
                // Order mantığı sonu

                announcement.IsDeleted = !announcement.IsDeleted;
                announcement.ModifiedDate = DateTime.Now;
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
