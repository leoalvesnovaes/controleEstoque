using AutoMapper;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers
{

    [Authorize(Roles = "Gerente ")]

    public class CadUsuarioController : Controller
    {

        //defini uma constante para nao exigir a senha como obrigatoria na hora do update
        private const string _senhaPadrao = "{$127;$188}";
        private const int _quantMaxLinhasPorPagina = 5;

        //------------------------------------------------------------------------------------------------------------------------
        [Authorize] /*metodo privado*/
        public ActionResult Index()
        {

            //To passando isso para as minha view
            ViewBag.SenhaPadrao = _senhaPadrao;

            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = Mapper.Map<List<UsuarioViewModel>>(UsuarioModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            //var quant = GrupoProdutoModel.RecuperarQuantidade();
            var quant = UsuarioModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]     //esse comando é para segurança anti ataque CSRF
        public JsonResult UsuarioPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            //fazer paginação
            var lista = Mapper.Map<List<UsuarioViewModel>>(UsuarioModel.RecuperarLista(pagina, tamPag, filtro, ordem: ordem));

            return Json(lista);
        }


        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]     //esse comando é para segurança anti ataque CSRF
        public ActionResult RecuperarUsuario(int id)
        {
            var vm = Mapper.Map<UsuarioViewModel>(UsuarioModel.RecuperarPeloId(id));

            return Json(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.ExcluirPeloId(id));
        }

        //------------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarUsuario(UsuarioViewModel model)
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
                try
                {

                    if (model.Senha == _senhaPadrao)
                    {
                        model.Senha = "";
                    }

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


    }
}