using EjemploWebForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploWebForm.DAL
{
    interface UsuarioRepository
    {
        IList<Usuario> getAll();
        Usuario getById(Guid codigo);
        Usuario update(Usuario usuario);
        void delete(Guid codigo);
        int create(Usuario usuario);


    }
}
