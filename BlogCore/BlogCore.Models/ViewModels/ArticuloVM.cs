using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class ArticuloVM
    {
        public Articulo Articulo { get; set; }

        //                                                  //SelectListItem es el que me va a permitir
        //                                                  //    crear el dropdown.
        public IEnumerable<SelectListItem>? ListaCategorias { get; set; }
    }
}
