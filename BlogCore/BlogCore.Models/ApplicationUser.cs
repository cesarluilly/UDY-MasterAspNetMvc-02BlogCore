using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    //==================================================================================================================
    //                                                      //Muy importante, heredar de IdentityUser para que 
    //                                                      //    se pueda afectar en la DB.
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        public required string Direccion { get; set; }

        [Required(ErrorMessage = "La ciuedad es obligatoria")]
        public required string Ciudad { get; set; }

        [Required(ErrorMessage = "El país es obligatorio")]
        public required string Pais { get; set; }
    }
}
