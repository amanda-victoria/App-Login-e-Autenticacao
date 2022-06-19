using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppLoginAutenticacao.Models
{
    public class Usuario
    {
        [Required]
        public int UsuarioID { get; set; }
        [Required]
        [MaxLength(100)]
        public string UsuNome { get; set; }
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }
        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }

        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();

        public void InsertUsuario(Usuario usuario)
        {
            conexao.Open();
            comand.CommandText = "call spInsertUsuario(@UsuNome,@Login,@Senha);";
            comand.Parameters.Add("@UsuNome", MySqlDbType.VarChar).Value = usuario.UsuNome;
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            comand.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }

        public string SelectLogin(string vLogin)
        {
            conexao.Open();
            comand.CommandText = "call spSelectLogin(@Login);";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = vLogin;
            comand.Connection = conexao;
            string Login = (string)comand.ExecuteScalar();
            conexao.Close();
            if (Login == null)
                Login = "";
            return Login;

        }
        public Usuario SelectUsuario(string vLogin)
        {
            conexao.Open();
            comand.CommandText = "call spSelectUsuario(@Login);";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = vLogin;
            comand.Connection = conexao;
            var readUsuario = comand.ExecuteReader();
            var TempUsuario = new Usuario();

            if (readUsuario.Read())
            {
                TempUsuario.UsuarioID = int.Parse(readUsuario["UsuarioID"].ToString());
                TempUsuario.UsuNome = (readUsuario["UsuNome"].ToString());
                TempUsuario.Login = (readUsuario["Login"].ToString());
                TempUsuario.Senha = (readUsuario["Senha"].ToString());
            };
            readUsuario.Close();
            conexao.Close();
            return TempUsuario;
        }

        public void UpdateSenha(Usuario usuario)
        {
            conexao.Open();
            comand.Connection = conexao;
            comand.CommandText = "call spUpdateSenha(@Login,@Senha);";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            comand.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            
            comand.ExecuteReader();
            conexao.Close();
            
        }

        public void DeleteUsuario(Usuario usuario)
        {
            conexao.Open();
            comand.Connection = conexao;
            comand.CommandText = "call spDeleteUsuario(@Login);";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;

            comand.ExecuteReader();
            conexao.Close();
        }
    }
}