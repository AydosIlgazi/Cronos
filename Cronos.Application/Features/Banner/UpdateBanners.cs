using Cronos.Application.Data;

using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Banner
{
    //22.09.2022 Irem Kesemen
    public class UpdateBanners
    {
        public class UpdateBannersCommand : IRequest<bool>{
            public int Id { get; set; }
            public string ImageUrl { get; set; }
            public string AltText { get; set; }
            public string RedirectUrl { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int Order { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
            public class UpdateProductCommandHandler : IRequestHandler<UpdateBannersCommand, bool>
            {
                private readonly ApplicationContext _context;
                public UpdateProductCommandHandler(ApplicationContext context)
                {
                    _context = context;
                }
               

               
                public async Task<bool> Handle(UpdateBannersCommand command, CancellationToken cancellationToken)
                {
                    var product = _context.Banners.Where(a => a.Id == command.Id).FirstOrDefault();
                    if (product == null)
                    {
                        return default;
                    }
                    else
                    {
                        //şu an aynı sırada mı kalacak
                        bool SameOrder = false;
                        //yeni order daha mı küçük
                        bool NewOrderSmall = true;
                        var bannernow = await _context.Banners.AsNoTracking().FirstOrDefaultAsync(a=>a.Id == command.Id);
                        //önceki orderı al
                        int OrderBefore = bannernow.Order;
                        if (bannernow == null)
                        {
                            return false;
                        }
                        product.Order = command.Order;
                        if(product.Order > OrderBefore)
                        {
                            NewOrderSmall = false;
                        }
                        List<BannerEntity> banners = await _context.Banners.AsNoTracking().ToListAsync();
                        foreach(var item in banners)
                        {
                            if(item.Order==command.Order && item.Id != command.Id)
                            {
                                SameOrder = true;        
                            }
                        }
                        if(SameOrder == true)
                        {
                            foreach(var item in banners)
                            {
                                if(item.Order >= command.Order && item.Order<OrderBefore && NewOrderSmall==true && item.Id != command.Id)
                                {
                                    item.Order++;
                                    _context.Banners.Update(item);
                                }
                                else if(item.Order <= command.Order && item.Order>OrderBefore && NewOrderSmall==false && item.Id != command.Id)
                                {
                                    item.Order--;
                                    _context.Banners.Update(item);
                                }
                            }
                        }
                        product.Id = command.Id;
                        product.AltText = command.AltText;
                        product.StartDate = command.StartDate;
                        product.ModifiedDate = DateTime.Now;
                        product.EndDate = command.EndDate;
                       
                        product.IsDeleted = command.IsDeleted;
                        product.IsActive = command.IsActive;
                        product.ImageUrl = command.ImageUrl;
                        product.RedirectUrl = command.RedirectUrl;

                        _context.Banners.Update(product);
                        var result = await _context.SaveChangesAsync(cancellationToken);
                        if (result == 0)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }
    }
}
