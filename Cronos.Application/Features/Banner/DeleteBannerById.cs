using Cronos.Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.Banner
{
    //22.09.2022 Irem Kesemen
    public class DeleteBannertByIdCommand : IRequest<bool>
    {
        public DeleteBannertByIdCommand(int id)
        {
            this.Id = id;
        }

        public int Id { get; private set; }
        public class DeleteBannertByIdCommandHandler : IRequestHandler<DeleteBannertByIdCommand, bool>
        {
            private readonly ApplicationContext _context;
            public DeleteBannertByIdCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(DeleteBannertByIdCommand command, CancellationToken cancellationToken)
            {
               


                //silme işlemi : kendisini listenin sonuna alıp ondan öncekileri 1 azaltmalı.
                //geri getirme işlemi : aynı kalıyor

                var product = await _context.Banners.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                if (product == null) return false;
                
                if (product.IsDeleted == false) { 
                List<BannerEntity> entities = await _context.Banners.CmsDisplay().AsNoTracking().ToListAsync();
                var order = entities.LastOrDefault();
                    int oldorder = product.Order;

                product.Order = order.Order;
                foreach (var item in entities)
                {
                    if (item.Order > oldorder)
                    {
                        item.Order--;
                        _context.Banners.Update(item);
                    }
                }
            }
                product.IsDeleted = !product.IsDeleted;
            //kendisinden öncekileri bir azaltmalı
            product.ModifiedDate = DateTime.Now;
                _context.Banners.Update(product);
                var result = await _context.SaveChanges();
                if (result == 0)
                {
                    return false;
                }
                else return true;
            }
        }
    }
}
