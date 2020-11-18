﻿function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_codigo').val(dados.Codigo);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}


//---------------------------------------------------------------------------------------------------------------------------
function set_focus_form() {
    $('#txt_nome').focus();
}



//---------------------------------------------------------------------------------------------------------------------------
function get_dados_inclusao() {
    return {
        Id: 0,
        Nome: '',
        Codigo: '',
        Ativo: true
    };
}

//---------------------------------------------------------------------------------------------------------------------------
function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        Codigo: $('#txt_codigo').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

//---------------------------------------------------------------------------------------------------------------------------
function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Codigo).end()
        .eq(2).html(param.Ativo ? 'SIM' : 'NÃO');
}

//---------------------------------------------------------------------------------------------------------------------------
//RETORNA INFORMAÇÕES DA GRADE           estava duplicando os registros da grade quando carregava inicial, depois voltava
//$(document).ready(function () {
//    var grid = $('#grid_cadastro > tbody');
//    for (var i = 0; i < linhas.length; i++) {
//        grid.append(criar_linha_grid(linhas[i]));
//    }
//});

function carrega() {
    var grid = $('#grid_cadastro > tbody');
    for (var i = 0; i < linhas.length; i++) {
        grid.append(criar_linha_grid(linhas[i]));
    }
}

//---------------------------------------------------------------------------------------------------------------------------