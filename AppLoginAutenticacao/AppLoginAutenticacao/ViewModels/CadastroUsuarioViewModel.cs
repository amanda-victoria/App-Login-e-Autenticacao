using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppLoginAutenticacao.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(100, ErrorMessage = "O nome deve ter até 100 caracteres")]
        public string UsuNome { get; set; }

        [Required(ErrorMessage = "Informe o login")]
        [MaxLength(50, ErrorMessage = "O Login deve ter até 50 caracteres")]
        [Remote("SelectLogin", "Autenticacao", ErrorMessage = "O login já existe!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [MinLength(6, ErrorMessage ="A senha deve ter pelo menos 6 caracteres")]
        [MaxLength(100, ErrorMessage = "A senha deve ter até no máximo 100 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirme a Senha")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare(nameof(Senha), ErrorMessage ="As senhas são diferentes")]
        public string ConfirmaSenha { get; set; }

    }
}