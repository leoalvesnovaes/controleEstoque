using AutoMapper;
using ControleEstoque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Cadastro
{

    [Authorize(Roles = "Gerente ")]


    public class CadUnidadeMedidaController : Controller
    {
        private const int _quantMaxLinhasPorPagina = 5;

        [Authorize]
        //-------------------------------------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            ViewBag.ListaTamPag = new SelectList(new int[] { _quantMaxLinhasPorPagina, 10, 15, 20 }, _quantMaxLinhasPorPagina);
            ViewBag.QuantMaxLinhasPorPagina = _quantMaxLinhasPorPagina;
            ViewBag.PaginaAtual = 1;

            var lista = Mapper.Map<List<UnidadeMedidaViewModel>>(UnidadeMedidaModel.RecuperarLista(ViewBag.PaginaAtual, _quantMaxLinhasPorPagina));
            var quant = UnidadeMedidaModel.RecuperarQuantidade();

            var difQuantPaginas = (quant % ViewBag.QuantMaxLinhasPorPagina) > 0 ? 1 : 0;
            ViewBag.QuantPaginas = (quant / ViewBag.QuantMaxLinhasPorPagina) + difQuantPaginas;

            return View(lista);
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]     //esse comando é para segurança anti ataque CSRF
        public JsonResult UnidadeMedidaPagina(int pagina, int tamPag, string filtro, string ordem)
        {
            //fazer paginação
            var lista = Mapper.Map<List<UnidadeMedidaViewModel>>(UnidadeMedidaModel.RecuperarLista(pagina, tamPag, filtro, ordem: ordem));

            return Json(lista);
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]     //esse comando é para segurança anti ataque CSRF
        public JsonResult RecuperarUnidadeMedida(int id)
        {
            var vm = Mapper.Map<UnidadeMedidaViewModel>(UnidadeMedidaModel.RecuperarPeloId(id));

            return Json(vm);
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirUnidadeMedida(int id)
        {
            return Json(UnidadeMedidaModel.ExcluirPeloId(id));
        }

        //-------------------------------------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarUnidadeMedida(UnidadeMedidaViewModel model)
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
                    var vm = Mapper.Map<UnidadeMedidaModel>(model);
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


        //-------------------------------------------------------------------------------------------------------------------
        //defini uma constante para nao exigir a senha como obrigatoria na hora do update
        //private const string _senhaPadrao = "{$127;$188}";




    }
}