using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TareaExamenClientes.Models
{
    public class Cliente : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre {2} y {1} caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = "";

        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        [Display(Name = "Teléfono (Opcional)")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateOnly FechaNacimiento { get; set; }

        // Validación personalizada para la edad (Lado del servidor)
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var edad = hoy.Year - FechaNacimiento.Year;
            
            // Resta un año si aún no ha cumplido años en el año actual
            if (FechaNacimiento > hoy.AddYears(-edad)) edad--;

            if (edad < 18)
            {
                yield return new ValidationResult("El cliente debe ser mayor de 18 años.", new[] { nameof(FechaNacimiento) });
            }
        }
    }
}