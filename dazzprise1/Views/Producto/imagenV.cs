using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dazzprise1.Views.Producto
{
    public class imagenV
    {

        public int id { get; set; }
        public Nullable<int> precio { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string imagen { get; set; }


        public Nullable<int> id_catalogo { get; set; }
        public Nullable<int> id_categoria { get; set; }
        
        [Display(Name = "imagen")]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}