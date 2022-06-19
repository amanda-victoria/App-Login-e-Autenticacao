using AppLoginAutenticacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppLoginAutenticacao.ViewModels;
using MySql.Data.MySqlClient;
using AppLoginAutenticacao.Utils;
using System.Security.Claims;

namespace AppLoginAutenticacao.Controllers
{
    public class AutenticacaoController : Controller
    {

        Usuario novousuario = new Usuario();

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(CadastroUsuarioViewModel viewmodel)
        {
            if (!ModelState.IsValid)
                return View(viewmodel);

            novousuario.UsuNome = viewmodel.UsuNome;
            novousuario.Login = viewmodel.Login;
            novousuario.Senha = Hash.GerarHash(viewmodel.Senha);

            novousuario.InsertUsuario(novousuario);

            TempData["MensagemLogin"] = "Cadastro realizado com sucesso! Faça o Login.";
            return RedirectToAction("Login", "Autenticacao");
        }

        public ActionResult SelectLogin(string Login)
        {
            bool LoginExists;
            string login = novousuario.SelectLogin(Login);

            if (login.Length == 0)
                LoginExists = false;
            else
                LoginExists = true;

            return Json(!LoginExists, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(viewmodel.Login);

            if (usuario == null | usuario.Login != viewmodel.Login)
            {
                ModelState.AddModelError("Login", "Login Incorreto");
                return View(viewmodel);
            }

            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha Incorreta");
                return View(viewmodel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name,usuario.Login),
                new Claim("Login",usuario.Login)
            }, "AppAplicationCookies");

            Request.GetOwinContext().Authentication.SignIn(identity);

            if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) || Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("AppAplicationCookies");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AlterarSenha(AlterarSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            Usuario usuario = new Usuario();
            usuario = usuario.SelectUsuario(login);

            if (Hash.GerarHash(viewModel.NovaSenha) == usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }
            usuario.Senha = Hash.GerarHash(viewModel.NovaSenha);

            usuario.UpdateSenha(usuario);

            TempData["MensagemLogin"] = "Senha alterada com sucesso!";

            return RedirectToAction("Index", "Home");
        }
    }
}