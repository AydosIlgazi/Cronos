using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cronos.Application.Features.SubMenu2
{
    public class UpdateSubMenu2
    {
        public class UpdateSubMenu2Command : IRequest<bool>
        {
            public UpdateSubMenu2Command(SubMenus2Dto obj)
            {
                this.Obj = obj;
            }

            public SubMenus2Dto Obj { get; private set; }
        }

        public class UpdateSubMenuHandler : IRequestHandler<UpdateSubMenu2Command, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public UpdateSubMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpdateSubMenu2Command request, CancellationToken cancellationToken)
            {
                var submenu2 = await _context.SubMenus2.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Obj.Id);
                if (submenu2 == null)
                {
                    return false;
                }

                var menuAsDto = request.Obj;

                submenu2 = _mapper.Map<Entities.Menu.SubMenu2>(request.Obj);


                submenu2.ModifiedDate = DateTime.Now;
                _context.SubMenus2.Update(submenu2);
                var isUpdated = await _context.SaveChangesAsync(cancellationToken);

                if (isUpdated > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public class DeleteSubMenu2Command : IRequest<bool>
        {

            public DeleteSubMenu2Command(int id)
            {
                this.Id = id;
            }

            public int Id { get; private set; }
        }

        public class DeleteMenuHandler : IRequestHandler<DeleteSubMenu2Command, bool>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public DeleteMenuHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(DeleteSubMenu2Command request, CancellationToken cancellationToken)
            {
                var submenu2 = await _context.SubMenus2.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id);
                if (submenu2 == null)
                {
                    return false;
                }
                submenu2.IsDeleted = true;
                submenu2.ModifiedDate = DateTime.Now;
                _context.SubMenus2.Update(submenu2);
                var isDeleted = await _context.SaveChangesAsync(cancellationToken);



                if (isDeleted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

    }
}
