using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace AppLoginAutenticacao.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage = "Informe a senha atual")]
        [MaxLength(100, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Display(Name = "Nova Senha")]
        [Required(ErrorMessage = "Informe a nova senha")]
        [MaxLength(50, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        [DataType(DataType.Password)]
        public string NovaSenha  { get; set; }

        [Display(Name = "Confirmar senha")]
        [Required(ErrorMessage = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "as senhas são diferentes")]
        public string Senha { get; set; }
    }
}