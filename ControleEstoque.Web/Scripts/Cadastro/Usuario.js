function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id);
    $('#txt_nome').val(dados.Nome);
    $('#txt_email').val(dados.Email);
    $('#txt_login').val(dados.Login);
    $('#txt_senha').val(dados.Senha);
}

//---------------------------------------------------------------------------------
function set_focus_form() {
    $('#txt_nome').focus();
}

//---------------------------------------------------------------------------------
// nao tinha mais esse metodo na aulda 36
//function set_dados_grid(dados) {

//    return '<td>' + dados.Nome + '</td>' + '<td>' + dados.Login + '</td>' ;
//}


//---------------------------------------------------------------------------------
function get_dados_inclusao() {

    return {
        Id: 0,
        Nome: '',
        Email: '',
        Login: '',
        Senha: ''
    };
}

//---------------------------------------------------------------------------------
function get_dados_form() {

    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        Email: $('#txt_email').val(),
        Login: $('#txt_login').val(),
        Senha: $('#txt_senha').val()
    };

}

//---------------------------------------------------------------------------------
function preencher_linha_grid(param, linha) {
    linha
        .eq(0).html(param.Nome).end()
        .eq(1).html(param.Login);
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

