﻿using AutoMapper;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Cadastro
{

    [Authorize(Roles = "Gerente ")]


    public class CadPerfilController : Controller
    {

        private const int _quantMaxLinhasPorPagina = 5;

        //-------------------------------------------------------------------------------------------------------------------
        [Authorize] /*metodo privado*/
        public ActionResult Index()
        {
            ViewBag.ListaUsuario = Mapper.Map<List<UsuarioViewModel>>(UsuarioModel.RecuperarLista());
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = Mapper.Map<List<PerfilViewModel>>(PerfilModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            var quant = PerfilModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]     //esse comando é para segurança anti ataque CSRF
        public JsonResult PerfilPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            //fazer paginação
            var lista = Mapper.Map<List<PerfilViewModel>>(PerfilModel.RecuperarLista(pagina, tamPag, filtro, ordem: ordem));

            return Json(lista);
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]     //esse comando é para segurança anti ataque CSRF
        public JsonResult RecuperarPerfil(int id)
        {
            var ret = Mapper.Map<PerfilViewModel>(PerfilModel.RecuperarPeloId(id));
            // TODO: carregar os usuários do perfil
            //ret.CarregarUsuarios();
            return Json(ret);
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirPerfil(int id)
        {
            return Json(PerfilModel.ExcluirPeloId(id));
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarPerfil(PerfilViewModel model, List<int> idUsuarios)
        {
            var resultado = "OK";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                model.Usuarios = new List<UsuarioModel>();
                if (idUsuarios == null || idUsuarios.Count == 0)
                {
                    model.Usuarios.Add(new UsuarioModel() { Id = -1 });
                }
                else
                {
                    foreach (var id in idUsuarios)
                    {
                        model.Usuarios.Add(new UsuarioModel() { Id = id });
                    }
                }

                try
                {
                    var vm = Mapper.Map<UsuarioModel>(model);
                    var id = vm.Salvar();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    resultado = "ERRO";
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        //defini uma constante para nao exigir a senha como obrigatoria na hora do update
        //private const string _senhaPadrao = "{$127;$188}";



    }
}