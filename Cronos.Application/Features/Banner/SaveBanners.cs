using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Banner
{
    //22.09.2022 Irem Kesemen
    public class SaveBanners
    {
        public class SaveBannersCommand : IRequest<bool>
        {
            public int Id { get; set; }
            public string ImageUrl { get; set; }
            public string AltText { get; set; }
            public string RedirectUrl { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime ModifiedDate { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int Order { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class SaveBannersCommandHandler : IRequestHandler<SaveBannersCommand, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;

            public SaveBannersCommandHandler(ApplicationContext context,IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }

            public async Task<bool> Handle(SaveBannersCommand request, CancellationToken cancellationToken)
            {
                bool SameOrder = false;
                
                var veriler = new BannerEntity();
                List<BannerEntity> banners = await _context.Banners.ToListAsync();
                veriler.Id = request.Id;
                veriler.AltText = request.AltText;
                veriler.CreatedDate = DateTime.UtcNow;  
                veriler.StartDate = request.StartDate;
                veriler.ModifiedDate = DateTime.UtcNow;    
                veriler.EndDate = request.EndDate;
                veriler.Order = request.Order;
                foreach (var item in banners)
                {
                    if(item.Order== veriler.Order)
                    {
                        SameOrder = true;
                    }

                }
                if (SameOrder)
                {
                    foreach(var item in banners)
                    {
                        if(item.Order >= veriler.Order)
                        {
                            item.Order++;
                            //veriler.Order = item.Order;
                        }
                    }
                    
                }
                else
                {
                    veriler.Order = request.Order;
                }
             
                veriler.IsDeleted = false;
                veriler.IsActive = true;
                veriler.ImageUrl = request.ImageUrl;
                veriler.RedirectUrl = request.RedirectUrl;
                _context.Banners.Add(veriler);
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
