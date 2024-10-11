function sairSistema() {
    window.location.href = "/Sair";
}

function redirecionaTela(url) {
    window.location.href = url;
}

function getDados(url, data = {}, metodo = 'GET', dataType = 'json') {
    let dadosRetorno = null;
    $.ajax({
        method: metodo,
        url: url,
        data: data,
        dataType: dataType,
        async: false,
        success: function (data) {
            dadosRetorno = data;
        },
        error: function (e) {
        }
    });
    return dadosRetorno;
}

function notifica(titulo, mensagem, tipo = null, tempo = 4000, posicao = 'bottomCenter') {
    if (tipo == true) {
        iziToast.success({
            title: titulo,
            message: mensagem,
            position: posicao,
            timeout: tempo,
        });
    }
    else if (tipo == false) {
        iziToast.error({
            title: titulo,
            message: mensagem,
            position: posicao,
            timeout: tempo,
            icon: 'fa-solid fa-circle-exclamation',
            iconColor: 'white',
            backgroundColor: '#f9777f',
            titleColor: "white",
            messageColor: "white"
        });
    }
    else {
        iziToast.show({
            title: titulo,
            message: mensagem,
            position: posicao,
            timeout: tempo,
        });
    }
}