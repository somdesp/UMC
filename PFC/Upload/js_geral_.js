

id_pai = 0;
id_est = 0;
id_cid = 0;
id_loc = 0;
id_and = 0;
id_are = 0;
id_tip_chm = 0;
id_tip_eqp = 0;
id_prb = 0;
id_eqp = 0;
tab_counter = 1
id_serie = 0;
$(document).ready(function () {
    //carregaMenu();

});


function carregaMenu() {
    $.ajax({
        type: "POST",
        url: "ajax/menu.aspx",
        data: {},
        timeout: 30000,
        async: true,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {

            if (result != "99") {
                $("#mws-navigation").html(result);
                //faco o menu colapse
                $("div#mws-navigation ul li a, div#mws-navigation ul li span")
                   .bind('click', function (event) {
                       if ($(this).next('ul').size() !== 0) {
                           $(this).next('ul').slideToggle('fast', function () {
                               $(this).toggleClass('closed');
                           });
                           event.preventDefault();
                       }
                   });

                /* Responsive Layout Script */

                $("div#mws-navigation").live('click', function (event) {
                    console.log(event.target + " " + this)

                    if (event.target === this) {
                        $(this).toggleClass('toggled');
                    }
                });
            } else {
                location.href = "default.aspx"
            }

        },
        errror: function (error) {

        }
    });
}

function renderMenu() {

    // first example
    $("#browser").treeview();

    // second example
    $("#navigation").treeview({
        persist: "location",
        collapsed: true,
        unique: true
    });

    // third example
    $("#red").treeview({
        animated: "fast",
        collapsed: true,
        unique: true,
        persist: "cookie",
        toggle: function () {
            window.console && console.log("%o was toggled", this);
        }
    });

    // fourth example
    $("#black, #gray").treeview({
        control: "#treecontrol",
        persist: "cookie",
        cookieId: "treeview-black"
    });

}



function abrepag(pag, nome) {
    //$.ajax({
    //    type: "POST",
    //    url: 'log_acesso.aspx',
    //    data: {
    //        'pag': pag,
    //        'nome': nome,
    //    },
    //    timeout: 30000,
    //    async: true,
    //    beforeSend: function () {

    //    },
    //    complete: function () {

    //    },
    //    cache: false,
    //    success: function (result) {

    //    },
    //    errror: function (error) {

    //    }
    //});

    var nextTab = $('#tabs li').size() + 1;

    // create the tab
    $('<li><a href="#tab' + nextTab + '" data-toggle="tab">' + nome + '<span class="close"><i class="icon-remove"></i></span></a></li>').appendTo('#tabs');

    // create the tab content
    $('<div class="tab-pane" id="tab' + nextTab + '"><div id="tab-' + tab_counter + '">tab' + nextTab + ' content</div></div>').appendTo('.tab-content');

    // make the new tab active
    $('#tabs a:last').tab('show');


    ///////////////////////////////////////////////////
    var path = "";
    path = pag

    if (path.indexOf("?") > 0) {
        path = pag + "&indice=" + tab_counter;
    } else {
        path = pag + "?indice=" + tab_counter
    }
    var div = "";
    $.ajax({
        type: "POST",
        url: path,
        data: {},
        timeout: 30000,
        async: true,
        beforeSend: function () {

            div = "#tab-" + tab_counter;
            $(div).html("<img src='images/ajax-loader.gif'> Carregando página...");
            //$("#lightbox").fadeIn('slow', function () { });
        },
        complete: function () {
            //$("#lightbox").fadeOut('slow', function () { });
        },
        cache: false,
        success: function (result) {


            $(div).slideDown('slow', function () {
                $(div).html(result);
            })


        },
        errror: function (error) {

        }
    });
    tab_counter = tab_counter + 1;
}

function lb(status) {
    if (status == "1") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        //$("#lightbox").fadeIn();
    }
    else {
        //$("#lightbox").fadeOut();
    }
}


/*
funcao que busca os paises
*/

function buscaPaises(indi) {

    $("#dropEstado" + indi).html("<select id='ddlEstado" + indi + "' class='form-control'><option value='0'>Selecione um Estado</option></select>");
    $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Selecione uma Cidade</option></select>");
    $("#dropCliente" + indi).html("<select id='ddlCliente" + indi + "' class='form-control'><option value='0'>Selecione um Cliente</option></select>");
    $("#dropNegogio" + indi).html("<select id='ddlNegocio" + indi + "' class='form-control'><option value='0'>Selecione um Tipo de Negócio</option></select>");
    $("#dropAndar" + indi).html("<select id='ddlAndar" + indi + "' class='form-control'><option value='0'>Selecione um Andar</option></select>");
    $("#dropArea" + indi).html("<select id='ddlArea" + indi + "' class='form-control'><option value='0'>Selecione uma Área</option></select>");
    $.ajax({
        type: "POST",
        url: "ajax/buscaPaises.aspx",
        data: { "tab_counter": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                var div = "#dropPais" + indi;

                $(div).html(result);
                $("#dropEstado" + indi).html("<select id='ddlEstado" + indi + "' class='form-control'><option value='0'>Selecione um Estado</option></select>");
                $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Selecione uma Cidade</option></select>");


            }

        },
        errror: function (error) {

        }
    });
}
/////////////// busca regional
function buscaRegional(indi) {
    $("#dropRegional" + indi).html("<select id='ddlRegional" + indi + "' class='form-control'><option value='0'>Selecione uma Regional</option></select>");
    $.ajax({
        type: "POST",
        url: "ajax/buscaRegional.aspx",
        data: { "tab_counter": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropRegional" + indi).html(result);

            }

        },
        errror: function (error) {

        }
    });

}
///////////////
/////////////// busca regional
function buscaTipoEstoque(indi) {
    $("#dropTipoEstoque" + indi).html("<select id='ddlTipoEstoque" + indi + "' class='form-control'><option value='0'>Selecione um Estoque</option></select>");
    $.ajax({
        type: "POST",
        url: "ajax/buscaTipoEstoque.aspx",
        data: { "tab_counter": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropTipoEstoque" + indi).html(result);

            }

        },
        errror: function (error) {

        }
    });

}
///////////////
/*
funcao que busca os estados
*/
function buscaestados(idPais, indi) {
    $("#dropEstado" + indi).html("<select id='ddlEstado" + indi + "' class='form-control'><option value='0'>Aguarde...</option></select>");
    $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Aguarde...</option></select>");
    if (idPais != '0') {

        $.ajax({
            type: "POST",
            url: "ajax/buscaEstado.aspx",
            data: { 'id': idPais, "tab_counter": indi },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#dropEstado" + indi).html(result);
                    $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Selecione uma Cidade</option></select>");
                }

            },
            errror: function (error) {

            }
        });
    }
    else {
        $("#dropEstado" + indi).html("<select id='ddlEstado" + indi + "' class='form-control'><option value='0'>Selecione um Estado</option></select>");
        $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Selecione uma Cidade</option></select>");
    }



}
/*
funcao que busca as cidades
*/
function buscaCidades(idEst, indi) {

    $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Selecione uma Cidade</option></select>");
    if (idEst != '0') {
        $("#dropCidade" + indi).html("<select id='ddlCidade" + indi + "' class='form-control'><option value='0'>Aguarde...</option></select>");
        $.ajax({
            type: "POST",
            url: "ajax/buscaCidade.aspx",
            data: { 'id': idEst, "tab_counter": indi },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#dropCidade" + indi).html(result);

                }

            },
            errror: function (error) {

            }
        });
    }



}

///////////////////////////////// busca clientes
function buscaClientes(indi) {
    $("#dropNegogio" + indi).html("<select id='ddlNegocio" + indi + "' class='form-control'><option value='0'>Aguarde...</option></select>");

    $.ajax({
        type: "POST",
        url: "ajax/buscaCliente.aspx",
        data: { "tab_counter": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropCliente" + indi).html(result);
                $("#dropNegogio" + indi).html("<select id='ddlNegocio" + indi + "' class='form-control'><option value='0'>Selecione um Tipo de Negócio</option></select>");
            }

        },
        errror: function (error) {

        }
    });

}

///////////////////////////////// busca negocios
function buscaNegocios(idCli, indi) {

    $.ajax({
        type: "POST",
        url: "ajax/buscaNegocio.aspx",
        data: { 'id': idCli, "tab_counter": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropNegogio" + indi).html(result);

            }

        },
        errror: function (error) {

        }
    });


}




/////////////////////Função que busca Prestador - Tipo de Negócio
function buscaPrestador(idNeg, indi) {

    $.ajax({
        type: "POST",
        url: "ajax/buscaPrestador.aspx",
        data: {
            'id': idNeg,
            "tab_counter": indi
        },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')
                return
            } else {
                $("#ddlPrestador" + indi).html(result);
            }

        },
        errror: function (error) {

        }
    });
}


/////////////////////Função que busca Prestador - Local
function buscaPrestadorLoc(idLoc, indi) {

    $.ajax({
        type: "POST",
        url: "ajax/buscaPrestadorLoc.aspx",
        data: {
            'id': idLoc,
            "tab_counter": indi
        },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')
                return
            } else {
                $("#ddlPrestador" + indi).html(result);
            }

        },
        errror: function (error) {

        }
    });
}



/////////////////////////////////
/*
funcao que some com o auto complete e limpa o campo de busca
*/
function fecha_busca(indi) {
    //$("#autoc"+indi).slideUp(500);
    //$("#load").html("");
}

function fecha_busca_mat(indi) {
    $("#autoc_ac_mat" + indi).slideUp(500);
    //$("#load").html("");
}

/*
funcao que coloca o nome da localidade no txt
*/
function pega_idloc(idloc, nom_loc, ind, id_and, id_are) {

    $("#autoc" + ind).slideUp('slow', function () {
        $("#autoc" + ind).html("");
        $("#txtLocalidade" + ind).val(nom_loc);
        id_loc = idloc;
        $("#hdn_idloc" + ind).val(idloc);
        //limpo os campos de selecao de série
        $("#hdn_idSerie" + ind).val("0");
        $("#hdn_idTipChm" + ind).val("0");
        $("#hdn_idTipEqp" + ind).val("0");
        $("#txtSerie" + ind).val("");
        busca_andar(id_loc, ind, id_and, id_are);
    });

}

function pega_idMat(idmat, dsc_mat, ind) {
    $("#autoc_ac_mat" + ind).slideUp('slow', function () {
        $("#autoc_ac_mat" + ind).html("");
        $("#dsc_mat" + ind).val(dsc_mat);
        $("#hdn_idMat" + ind).val(idmat);
    });

}

/*
funcao que busca os andares
*/
function busca_andar(id, ind, id_and, id_are) {
    if (id == 0) {
        $("#txtLocalidade" + ind).focus();
        abre_pop("Selecione uma localidade");
        //$("#aviso").slideDown('slow');
    }
    else {
        $.ajax({
            type: "POST",
            url: "ajax/buscaAndares.aspx",
            data: { 'loc': id, "indice": ind },
            timeout: 30000,
            async: false,
            beforeSend: function () {
                $("#load_andar" + ind).html("<img src='images/ajax-loader.gif'>");
            },
            complete: function () {
                $("#load_andar" + ind).html("");
            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#dropAndar" + ind).html(result);

                    if (id_and != "" && typeof (id_and) != "undefined") {
                        setTimeout(function () {

                            $("#ddlAndar" + ind).val(id_and);
                            buscaAreas(id_and, ind, id_are)
                        }, 300);

                    }
                }

            },
            errror: function (error) {

            }
        });

    }

}
/*
funcao que busca os andares
*/
function buscaAreas(id, ind, id_are) {
    if (id != 0) {
        $.ajax({
            type: "POST",
            url: "ajax/buscaAreas.aspx",
            data: { 'loc': id, "indice": ind },
            timeout: 30000,
            async: false,
            beforeSend: function () {
                $("#load_area" + ind).html("<img src='images/ajax-loader.gif'>");
            },
            complete: function () {
                $("#load_area" + ind).html("");
            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#dropArea" + ind).html(result);
                    if (id_are != "") {
                        $("#ddlArea" + ind).val(id_are);
                        busca_dados_loc(ind);
                    }
                }

            },
            errror: function (error) {

            }
        });

    }

}

function pega_idSerie(idSer, nom_loc, ind, idloc, idtipchm, idtipeqp) {
    $("#autoc_serie" + ind).slideUp('slow', function () {
        $("#autoc_serie" + ind).html("");
        $("#txtSerie" + ind).val(nom_loc);
        id_serie = idSer;
        $("#hdn_idSerie" + ind).val(idSer);
        $("#hdn_idloc" + ind).val(idloc);
        $("#hdn_idTipChm" + ind).val(idtipchm);
        $("#hdn_idTipEqp" + ind).val(idtipeqp);
        $("#hdn_idSerie" + ind).val(idSer);
        //busca_dados_loc(ind);

        //busco o tecnico

        // tento popular os campos

    });

}
function fechar_pop() {
    $("#aviso").slideUp('slow', function () {
        abre_pop("");
        //verifico se esta com o pop aberto
        if ($("#pop").css("display") != "block") {
            $("#lightbox").fadeOut('slow');
        }
    });
}

function fechar_pop_light() {
    $("#aviso_light").slideUp('slow', function () {
        abre_pop("");

    });
}


/*
funcao que busca os dados da localidade , recebe idloc e nome da localidade
*/
function busca_dados_loc(indi) {
    if ($("#hdn_idloc" + indi).val() == 0) {


        abre_pop("Selecione uma localidade");
        return;
    }

    if ($("#ddlArea" + indi).val() == 0) {

        abre_pop("Selecione uma área");
        return
    }

    else {

        $.ajax({
            type: "POST",
            url: "ajax/busca_dados_localidade.aspx",
            data: { "loc": $("#hdn_idloc" + indi).val(), "indice": indi },
            timeout: 30000,
            async: false,
            beforeSend: function () {
                $("#lightbox").css("filter", "alpha(opacity=65)");
                //$("#lightbox").fadeIn();
            },
            complete: function () {

                //$("#lightbox").fadeOut();
            },
            cache: false,
            success: function (result) {

                var inf = result.split("|-|");
                $("#nom_loc" + indi).html(inf[0]);
                $("#cod_loc" + indi).html(inf[1]);
                $("#est_loc" + indi).html(inf[2]);
                $("#cid_loc" + indi).html(inf[3]);
                $("#end_loc" + indi).html(inf[4]);
                $("#bai_loc" + indi).html(inf[5]);
                //$("#dta_ate" + indi).html(inf[6]);
                //$("#txt_dta_ate" + indi).val(inf[7]);
                $("#hdn_idCli" + indi).val(inf[8]);

                $("#result_busca_chm" + indi).slideDown('slow', function () {
                    //busca_eqps($("#hdn_idloc" + indi).val(), indi);
                    //busco os tecnicos
                    busca_tecnico($("#hdn_idloc" + indi).val(), indi);
                    //verifico o tipo de cliente se for = 2 eu exibo a li de tecnico
                    if (inf[8] == 2) {
                        $("#div_tecnico_" + indi).hide();
                    }
                });

                //limpo
                $("#txt_dsc_prb_ac" + indi).val('');
                $("#ddl_equip" + indi).val('');
                $("#dta_ate" + indi).val('');
                $('html,body').animate({ scrollTop: $('#result_busca_chm' + indi).offset().top }, 1500);
                //se for pelo eqp eu busco as infos par apopuar a tela
                idE = $("#hdn_idSerie" + indi).val()

                if (idE != "0") {
                    busca_dados_eqp_sel(idE, indi);
                }

            },
            errror: function (error) {

            }
        });
    }
}

//////////////////////////////
function busca_dados_eqp_sel(idEqp, indi) {
    $.ajax({
        type: "POST",
        url: "ajax/atendimento/dados_eqp_sel.aspx",
        data: { 'idEqp': idEqp },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            dados = result.split("|")
            $("#ddl_fam_os_" + indi).val(dados[2]);
            popula_tipo_os(dados[2], indi, 'div_drop_tipoos_');
            $("#ddl_tipo_os_" + indi).val(dados[0]);
            $("#ddl_tipo_equipa_" + indi).val(dados[1]);
            procura_eqp(dados[3], dados[0], dados[1], indi);
            $("#ddl_equip" + indi).val(idEqp);
        },
        errror: function (error) {

        }
    });
}
//////////////////////////////

/*
funcao que busca os andares
*/
function busca_tecnico(id, ind) {

    $.ajax({
        type: "POST",
        url: "ajax/buscaTecnico.aspx",
        data: { 'loc': id, "indice": ind },
        timeout: 30000,
        async: false,
        beforeSend: function () {
            //$("#load_andar" + ind).html("<img src='images/ajax-loader.gif'>");
        },
        complete: function () {
            //$("#load_andar" + ind).html("");
        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropTecnico" + ind).html(result);
            }

        },
        errror: function (error) {

        }
    });
}
/*
funcao que busca os andares
*/
function buscaAreas(id, ind, id_are) {
    if (id != 0) {
        $.ajax({
            type: "POST",
            url: "ajax/buscaAreas.aspx",
            data: { 'loc': id, "indice": ind },
            timeout: 30000,
            async: false,
            beforeSend: function () {
                $("#load_area" + ind).html("<img src='images/ajax-loader.gif'>");
            },
            complete: function () {
                $("#load_area" + ind).html("");

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#dropArea" + ind).html(result);
                    $("#ddlArea" + ind).chosen({ search_contains: true });
                    if (id_are != "" && typeof (id_are) != "undefined") {
                        $("#ddlArea" + ind).val(id_are);

                        busca_dados_loc(ind);
                    }
                }

            },
            errror: function (error) {

            }
        });

    }

}
/*
funcao que busca os equipamentos, problemas etc
*/
function busca_eqps(id, indi) {

    $.ajax({
        type: "POST",
        url: "ajax/listaEqps.aspx",
        data: { "indice": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropEqp" + indi).html(result);
                // aqui verifico se o usuario veio de serie ou de localidade
                // se veio de série eu já mudo o tipo para o tipo selecionado

                listaTipoOS(id, indi);
            }

        },
        errror: function (error) {

        }
    });
}

/*
funcao que busca os tipo de os
*/
function listaTipoOS(id, indi) {
    $("#dropEquipamento" + indi).html("<select id='ddlEquipamento" + indi + "' class='form-control'><option value='0'>Selecione um Equipamento</option></select>");
    $("#dropProblema" + indi).html("<div class='mws-form-inline' style='float: left; width: 50%;'><div class='mws-form-row'><label>Problema:</label><div class='mws-form-item large'><select id='ddlProblema" + indi + "'  data-mini='true'  class='form-control'><option value='0'>Selecione um Problema</option></select></div></div></div>");
    $("#li_eqp" + indi + "").html("");
    //$("#dta_ate" + indi + "").html("-");

    vAndar = $('#ddlAndar' + indi).find('option').filter(':selected').val();
    vArea = $('#ddlArea' + indi).find('option').filter(':selected').val();
    vEqp = $("#hdn_idSerie" + indi).val();

    $.ajax({
        type: "POST",
        url: "ajax/busca_equipamento.aspx",
        data: { "id": id, "indice": indi, "vAndar": vAndar, "vArea": vArea, "vEqp": vEqp },
        timeout: 30000,
        async: false,
        beforeSend: function () {
            $("#load_eqp" + indi).html("<img src='images/ajax-loader.gif'>");
        },
        complete: function () {
            $("#load_eqp" + indi).html("");
        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropTipo" + indi).html(result);
                if ($("#hdn_idTipChm" + indi).val() != 0) {
                    $("#ddlTipo" + indi).val($("#hdn_idTipChm" + indi).val());
                    buscaTipoEquipamento($("#hdn_idTipChm" + indi).val(), id, indi);
                }
            }

        },
        errror: function (error) {

        }
    });
}

/*
funcao que busca os tipo de equipamento
*/
function buscaTipoEquipamento(idTipo, idLoc, indi) {

    // passo o valor selecionado para a variaval id_tip_chm
    id_tip_chm = idTipo;
    $("#dropProblema" + indi).html("<div class='mws-form-inline' style='float: left; width: 50%;'><div class='mws-form-row'><label>Problema:</label><div class='mws-form-item large'><select id='ddlProblema" + indi + "'  data-mini='true' class='form-control'><option value='0'>Selecione um Problema</option></select></div></div></div>");
    $("#li_eqp" + indi).html("");
    //$("#dta_ate" + indi).html("-");
    $.ajax({
        type: "POST",
        url: "ajax/busca_tipo_equipamento.aspx",
        data: { "idTipo": idTipo, "idLoc": idLoc, "indice": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {
            $("#load_prb" + indi).html("<img src='images/ajax-loader.gif'>");
        },
        complete: function () {
            $("#load_prb" + indi).html("");
        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropEquipamento" + indi).html(result);
                if ($("#hdn_idTipEqp" + indi).val() != 0) {
                    $("#ddlEquipamento" + indi).val($("#hdn_idTipEqp" + indi).val());
                    buscaProblema($("#hdn_idTipEqp" + indi).val(), idLoc, indi);
                }
            }

        },
        errror: function (error) {

        }
    });
}

/*
funcao que busca os problemas
*/
function buscaProblema(idTipo, idLoc, indi) {
    // passo o tipo para a variavel id_tip_eqp
    id_tip_eqp = idTipo;

    vAndar = $('#ddlAndar' + indi).find('option').filter(':selected').val();
    vArea = $('#ddlArea' + indi).find('option').filter(':selected').val();
    vEqp = $("#hdn_idSerie" + indi).val();

    $.ajax({
        type: "POST",
        url: "ajax/busca_problema.aspx",
        data: { "idTipo": idTipo, "idLoc": idLoc, "indice": indi, "vAndar": vAndar, "vArea": vArea, "vEqp": vEqp },
        timeout: 30000,
        async: false,
        beforeSend: function () {
            $("#load_prb" + indi).html("<img src='images/ajax-loader.gif'>");
        },
        complete: function () {
            $("#load_prb" + indi).html("");
        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                $("#dropProblema" + indi).html(result);
                if ($("#hdn_idSerie" + indi).val() != 0) {
                    $("#ddlEqp" + indi).val($("#hdn_idSerie" + indi).val());
                }
            }

        },
        errror: function (error) {

        }
    });
}

/*
funcao que calcula a criticidade do chamado
*/
function calc_criticidade(idPrb, indi) {
    // passo o valor para a variavel id_prb
    id_prb = idPrb;
    $.ajax({
        type: "POST",
        url: "ajax/calcula_criticidade.aspx",
        data: { "idPrb": idPrb, "indice": indi, "dta_abr": $("#dta_abr" + indi).val(), "hor_abr": $("#hor_abr" + indi).val(), "min_abr": $("#min_abr" + indi).val() },
        timeout: 30000,
        async: false,
        beforeSend: function () {
            $("#load_prb" + indi).html("<img src='images/ajax-loader.gif'>");
        },
        complete: function () {
            $("#load_prb" + indi).html("");
        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                var info = result.split("||");
                $("#dta_ate" + indi).html(info[0]);
                $("#txt_dta_ate" + indi).val(info[1]);
            }

        },
        errror: function (error) {

        }
    });
}

//Alterado por: Gabriela Biserra - 13/09/2016 - Adicionar parâmetros do tipo de negócio e tipo família
function busca_ger_est(page, indi) {
    $.ajax({
        type: "POST",
        url: "ajax/estoque/busca_dados_gerenciar.aspx",
    data: {
        "tab_counter": indi,
        "txt_par_num": $("#txt_par_num" + indi).val(),
        "txt_des_pec": $("#txt_des_pec" + indi).val(),
        "id_tip_est": $('#ddlTipoEstoque' + indi).find('option').filter(':selected').val(),
        "id_tip_fams_pec": $("#id_tip_fams_pec" + indi).val(),
        "id_tip_neg": $("#id_tip_neg" + indi).val(),
        "page": page
    },
    timeout: 60000,
    async: true,
    beforeSend: function () {
        loading_show(indi);
    },
    complete: function () {
        loading_hide(indi);
    },
    cache: false,
    success: function (result) {
        // verifico se a sessao expirou
        if (result == "99") {
            abre_pop('Sua sessao expirou, efetue o login novamente!')
            location.href = 'default.aspx'
            return
        }
        if (result == "0") {
            abre_pop('Sem dados para exibir!')
            return
        }
        if (result == "1") {
            abre_pop('Sistema temporariamente indisponivel!')
            return
        } else {
            $("#container" + indi).show();
        $("#container" + indi).html(result);
        $('#container' + indi + ' table tbody tr:odd').addClass('zebraUm');
        //$('.valores').priceFormat({
        //    prefix: '',
        //    centsSeparator: '',
        //    thousandsSeparator: ''
        //});
    }

},
errror: function (error) {

}
});
}


function abre_pop(msg, tipo) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-full-width",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    if (tipo == "info") {
        toastr.info(msg, "Informação")
    } else {
        toastr.warning(msg, "Aviso")
    }

}

function abre_pop_interna(msg) {
    $("#dados_dialog_interna").html(msg);
    $("#mws-jui-dialog-interna").dialog("option", { modal: true }).dialog("open");
}

/*
funcao que abre o chamado
*/
function abre_chamado(indi) {
    /*id_pai = 0;
    id_est = 0;
    id_cid = 0;
    id_loc = 0;
    id_and = 0;
    id_are = 0;
    id_tip_chm = 0;
    id_tip_eqp = 0;
    id_prb = 0;
    id_eqp = 0;*/
    var txt_sol = $("#txt_sol" + indi).val();
    var txt_dsc = $("#txt_dsc" + indi).val();
    var tip_chm = $("#ddlTipoOs_" + indi).find('option').filter(':selected').val();
    var tel_chm = $("#txt_tel" + indi).val();
    var eml_chm = $("#txt_eml" + indi).val();
    var obs_chm = $("#txt_obs" + indi).val();
    var dta_max_ate = $("#txt_dta_ate" + indi).val();
    var txt_nro_os = $("#txt_nro_os" + indi).val();

    var dta_abr = $("#dta_abr" + indi).val();
    var hor_abr = $("#hor_abr" + indi).val();
    var min_abr = $("#min_abr" + indi).val();

    id_eqp = $('#ddl_equip' + indi).find('option').filter(':selected').val();
    num_pri = $('#num_pri' + indi).find('option').filter(':selected').val();
    id_tip_fams = $("#ddl_fam_os_" + indi).find('option').filter(':selected').val();
    nat_os = $("#ddl_nat_os_" + indi).find('option').filter(':selected').val();
    id_emp = $("#ddl_emp_" + indi).find('option').filter(':selected').val();
    //id_pre_ser = $("#ddl_pres_" + indi).find('option').filter(':selected').val();
    id_pre_ser = $("#ddlPrestador" + indi).find('option').filter(':selected').val();
    desp = $("#ddl_despachar_" + indi).find('option').filter(':selected').val();
    var id_form_resp = $("#hdn_id_form" + indi).val();

    var idTecSel = $("#ddlTecnico" + indi).find('option').filter(':selected').val();

    if ($("#hdn_idloc" + indi).val() == 0) {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        //$("#lightbox").fadeIn();
        $("#txtLocalidade" + indi).focus();
        abre_pop("Selecione uma localidade!");
        //$("#aviso").slideDown('slow');

        return
    }
    //natureza
    if ($('#ddl_nat_os_' + indi).find('option').filter(':selected').val() == "0") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        abre_pop("Selecione a Natureza da OS");
        return
    }
    //familia
    if ($('#ddl_fam_os_' + indi).find('option').filter(':selected').val() == "0") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        abre_pop("Selecione a Família da OS");
        return
    }
    //tipo de os
    if ($('#ddl_tipo_os_' + indi).find('option').filter(':selected').val() == "0") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        abre_pop("Selecione o Tipo de OS da OS");
        return
    }
    //tipo de equipamento
    if ($('#ddl_tipo_equipa_' + indi).find('option').filter(':selected').val() == "0") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        abre_pop("Selecione o Tipo de Equipamento da OS");
        return
    }
    //problema
    if ($('#ddl_problema_' + indi).find('option').filter(':selected').val() == "0") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        abre_pop("Selecione o Problema da OS");
        return
    }
    if (txt_sol == "") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        //$("#lightbox").fadeIn();
        $("#txt_sol" + indi).focus();
        abre_pop("Digite o nome do solicitante!");
        //$("#aviso").slideDown('slow');

        return
    }

    //tipo de equipamento
    if ($('#ddl_tipo_equipa_' + indi).find('option').filter(':selected').val() == "0") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        abre_pop("Selecione o Tipo de Equipamento da OS");
        return
    }

    if (txt_dsc == "") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        //$("#lightbox").fadeIn();
        abre_pop("Digite a descrição do chamado!");
        //$("#aviso").slideDown('slow');


        return
    }


    //if (idTecSel == "0") {
    //    $("#lightbox").css("filter", "alpha(opacity=65)");
    //    //$("#lightbox").fadeIn();
    //    abre_pop("Selecione o tecnico!");
    //    //$("#aviso").slideDown('slow');
    //    return
    //}

    if ($("#retroativo_" + indi).val() == "1" && $("#txt_obs" + indi).val() == "") {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        //$("#lightbox").fadeIn();
        abre_pop("É obrigatório preencher o campo observação quando o chamado é retroativo / agendado");
        //$("#aviso").slideDown('slow');
        return
    }

    else {
        $("#lightbox").css("filter", "alpha(opacity=65)");
        //$("#lightbox").fadeIn();
        $.ajax({
            type: "POST",
            url: "ajax/grava_chamado.aspx",
            data: {
                "id_cid": id_cid, "id_loc": $("#hdn_idloc" + indi).val(),
                "id_prb": $('#ddl_problema_' + indi).find('option').filter(':selected').val(),
                "id_eqp": $('#ddl_equip' + indi).find('option').filter(':selected').val(),
                "sol_chm": txt_sol,
                "dsc_chm": txt_dsc,
                "obs_chm": obs_chm,
                "tel_chm": tel_chm,
                "eml_chm": eml_chm,
                "dta_max_ate": dta_max_ate,
                "txt_nro_os": txt_nro_os,
                "tip_chm": tip_chm,
                "dta_abr": dta_abr,
                "hor_abr": hor_abr,
                "min_abr": min_abr,
                "ddlAndar": $('#ddlAndar' + indi).find('option').filter(':selected').val(),
                "ddlArea": $('#ddlArea' + indi).find('option').filter(':selected').val(),
                "num_pri": num_pri, "id_tip_fams": id_tip_fams, "nat_os": nat_os,
                "id_tip_chm": $('#ddl_tipo_os_' + indi).find('option').filter(':selected').val(),
                "id_tip_eqp": $('#ddl_tipo_equipa_' + indi).find('option').filter(':selected').val(),
                "id_emp": id_emp,
                "id_pre_ser": id_pre_ser,
                "id_tec": $('#ddlTecnico' + indi).find('option').filter(':selected').val(), "desp": desp,
                "id_form_resp" : id_form_resp
            },
            timeout: 30000,
            async: true,
            beforeSend: function () {
                $("#lightbox").css("filter", "alpha(opacity=65)");
                loading_show(indi);
                $("#btn_abre_chamado" + indi).attr('disabled', true);
                $("#btn_abre_chamado" + indi).attr('value', "Aguarde...");
                $("#abrir" + indi).hide();
            },
            complete: function () {

                loading_hide(indi);
                $("#btn_abre_chamado" + indi).attr('disabled', false);
                $("#btn_abre_chamado" + indi).attr('value', "ABRIR CHAMADO");
                $("#abrir" + indi).show();
            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "sessao") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "chamado") {
                    abre_pop('Já existe um chamado aberto com esse ERP!')

                    return
                }
                if (result == "erro") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    abre_pop('Chamado aberto com sucesso!<br>OS Nº: ' + result, 'info');
                    //$("#aviso").slideDown('slow');



                    // zero as variavais
                    id_pai = 0;
                    id_est = 0;
                    id_cid = 0;
                    id_loc = 0;
                    id_and = 0;
                    id_are = 0;
                    id_tip_chm = 0;
                    id_tip_eqp = 0;
                    id_prb = 0;
                    id_eqp = 0;
                    $("#hdn_idloc" + indi).val('');
                    $("#id_form_resp" + indi).val('');
                    //adriano

                    /////////////////////////////////////
                    $.ajax({
                        type: "POST",
                        url: "atendimento/abrir.aspx?indice=" + indi,
                        data: {},
                        timeout: 30000,
                        async: false,
                        beforeSend: function () {

                        },
                        complete: function () {

                        },
                        cache: false,
                        success: function (result) {
                            var div = "#tab-" + (indi);
                            console.log(div)
                            $(div).slideDown('slow', function () {
                                $(div).html(result);
                            })


                        },
                        errror: function (error) {

                        }
                    });
                    ///////////////////////////////////

                }

            },
            errror: function (error) {

            }
        });
    }
}
////////////////////////////////////////////////////
function altera_chamado(id_chama, indi, pagina_atual, nome_pagina) {
    var txt_sol = $("#txt_sol").val();
    var txt_dsc = $("#txt_dsc").val();

    var tel_chm = $("#txt_tel").val();
    var eml_chm = $("#txt_eml").val();
    var obs_chm = $("#txt_obs").val();
    var dta_max_ate = $("#txt_dta_ate").val();

    var dta_ini = $("#dta_ini").val();
    var hor_ini = $("#hor_ini").val();
    var mot_pen = $("#mot_pen").val();
    var dta_ter = $("#dta_ter").val();
    var hor_ter = $("#hor_ter").val();
    var ser_exe = $("#ser_exe").val();

    var nro_os_cli = $("#txt_nro_os_alt").val();
    var idTec = $('#ddlTecnicoAlt').find('option').filter(':selected').val();

    var idTecdespacho = $('#ddlTecdespacho').find('option').filter(':selected').val();
    var id_emp = $('#ddlEmpresa').find('option').filter(':selected').val();
    //var id_pre_ser = $('#ddl_pre_alt').find('option').filter(':selected').val();
    var ddl_pre_alt = $('#ddl_pre_alt').find('option').filter(':selected').val();

    //Tipo de OSs
    var id_tip_chm = $("#ddlTipoOs_" + indi).find('option').filter(':selected').val();

    var sit_enc = $("#sit_enc").val();
    var cau_pro = $("#cau_pro").val();
    var sol_apl = $("#sol_apl").val();

    ddlTipo2 = $('#ddlTipo' + indi).find('option').filter(':selected').val();
    ddlEquipamento2 = $('#ddlEquipamento' + indi).find('option').filter(':selected').val();
    ddlEqp2 = $('#ddlEqp' + indi).find('option').filter(':selected').val();
    ddlProblema2 = $('#ddlProblema' + indi).find('option').filter(':selected').val();
    ddlStatus = $('#ddlStatus').find('option').filter(':selected').val();
    ddlAvaliacao = $('#ddlAvaliacao').find('option').filter(':selected').val();

    ins_fis_ade = $('#ins_fis_ade').find('option').filter(':selected').val();
    ele_ade = $('#ele_ade').find('option').filter(':selected').val();
    tel_ade = $('#tel_ade').find('option').filter(':selected').val();
    ten_afe = $("#ten_afe").val();
    eqp_fun = $('#eqp_fun').find('option').filter(':selected').val();
    cli_ori = $('#cli_ori').find('option').filter(':selected').val();
    tam_que = $('#tam_que').find('option').filter(':selected').val();
    ate_tel = $('#ate_tel').find('option').filter(':selected').val();
    ate_tel = $('#ddlFechadoDel').find('option').filter(':selected').val();
    nom_res = $('#nom_res').val();
    con_cha = $('#con_cha').val();
    car_rem = $('#car_rem').find('option').filter(':selected').val();
    maq_pre = $('#maq_pre').find('option').filter(':selected').val();

    if (txt_sol == "") {
        $("#txt_sol" + indi).focus();
        abre_pop("Digite o nome do solicitante!");
        //$("#aviso").slideDown('slow');
        return
    }
    if (txt_dsc == "") {
        abre_pop("Digite a descrição do chamado!");
        //$("#aviso").slideDown('slow');
        $("#txt_dsc" + indi).focus();
        return
    }
    //if (ddlTipo2 == 0) {
    //    abre_pop("Selecione o tipo de chamado!");
    //    //$("#aviso").slideDown('slow');
    //    return
    //}
    //if (ddlEquipamento2 == 0) {
    //    abre_pop("Selecione o tipo de equipamento!");
    //    //$("#aviso").slideDown('slow');
    //    return
    //}
    //if (ddlProblema2 == 0) {
    //    abre_pop("Selecione o problema!");
    //    //$("#aviso").slideDown('slow');
    //    return
    //}
    /////////////////
    // verifico os dados para fechamento
    /*
    var dta_ini = $("#dta_ini").val();
    var hor_ini = $("#hor_ini").val();
    var mot_pen = $("#mot_pen").val();
    var dta_ter = $("#dta_ter").val();
    var hot_ter = $("#hot_ter").val();
    1  Aberto
    2  Fechado
    3  Pendente
    4  Andamento
    5  Atendido
    */

    if (ddlStatus == "1") {
        dta_ini = "";
        hor_ini = "";
        mot_pen = "";
        dta_ter = "";
        hor_ter = "";
    } else if (ddlStatus == "2") {

        if (dta_ini == "") {
            abre_pop("Digite a data de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ini == "") {
            abre_pop("Digite a hora de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (dta_ter == "") {
            abre_pop("Digite a data de término!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ter == "") {
            abre_pop("Digite a hora de término!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (ser_exe == "") {
            abre_pop("Digite o serviço executado!");
            //$("#aviso").slideDown('slow');
            return;
        }

    //DATA INICIO
        var data1 = []
        //split por espaço
        data1 = dta_ini.split(" ")
        //split por barra
        data11 = data1[0].split("/")
        // console.log(data11[2] + "/" + data11[1] + "/" + data11[0] + "-" + ddlHorIni0 + ":" + ddlMinIni0);
        // monta a data com ano / mes / dia - hora:minuto
        var data2 = new Date(data11[2] + "/" + data11[1] + "/" + data11[0] + "-" + hor_ini )

        //DATA TERMINO
        var data10 = []
        //split por espaço
        data10 = dta_ter.split(" ")
        //split por barra
        data22 = data10[0].split("/")
        // console.log(data22[2] + "/" + data22[1] + "/" + data22[0] + "-" + ddlHorTer0 + ":" + ddlMinTer0);
        // monta a data com ano / mes / dia - hora:minuto
        var data3 = new Date(data22[2] + "/" + data22[1] + "/" + data22[0] + "-" + hor_ter)

        //VERIFICAR SE DATA INICIO E TERMINO É MENOR QUE A DATA DE ABERTURA       
   

        if (data2 > data3) {
            abre_pop("Data de início maior que data de término.");
            return;
        }

        /////////////////

        if (ddlAvaliacao == undefined) {
            abre_pop("Selecione uma avaliação");
            //$("#aviso").slideDown('slow');
            return;
            //$("#ddlAvaliacao").val("5");
        }

    } else if (ddlStatus == "3") {
        if (dta_ini == "") {
            abre_pop("Digite a data de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ini == "") {
            abre_pop("Digite a hora de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (mot_pen == "") {
            abre_pop("Digite o motivo da pendência!");
            //$("#aviso").slideDown('slow');
            return;
        }
    } else if (ddlStatus == "4") {
        if (dta_ini == "") {
            abre_pop("Digite a data de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ini == "") {
            abre_pop("Digite a hora de início!");
            //$("#aviso").slideDown('slow');
            return;
        }

        if (ddlAvaliacao == undefined) {
            abre_pop("Selecione uma avaliação");
            //$("#aviso").slideDown('slow');
            return;
            //$("#ddlAvaliacao").val("5");
        }

    } else if (ddlStatus == "5") {
        if (idTecdespacho == "0") {
            abre_pop("Selecione um técnico!");
            //$("#aviso").slideDown('slow');
            return;
        }
    }



    /////////////////

    $.ajax({
        type: "POST",
        url: "ajax/altera_chamado.aspx",
        data: {
            "txt_sol": txt_sol, "txt_dsc": txt_dsc, "tel_chm": tel_chm, "eml_chm": eml_chm, "obs_chm": obs_chm, "dta_max_ate": dta_max_ate,
            //"ddlTipo2": ddlTipo2,
            //"ddlEquipamento2": ddlEquipamento2, "ddlEqp2": ddlEqp2,
            "id_chama": id_chama, "ddlStatus": ddlStatus,
            "dta_ini": dta_ini, "hor_ini": hor_ini, "mot_pen": mot_pen, "dta_ter": dta_ter, "hor_ter": hor_ter, "ser_exe": ser_exe, "ddlAvaliacao": ddlAvaliacao,
            "idTec": idTec, "nro_os_cli": nro_os_cli, "idTecdespacho": idTecdespacho,
            'ins_fis_ade': ins_fis_ade,
            'ele_ade': ele_ade,
            'tel_ade': tel_ade,
            'ten_afe': ten_afe,
            'eqp_fun': eqp_fun,
            'cli_ori': cli_ori,
            'tam_que': tam_que,
            'ate_tel': ate_tel,
            'nom_res': nom_res,
            'con_cha': con_cha,
            'car_rem': car_rem,
            'id_sub': $('#ddlSubSistema').find('option').filter(':selected').val(),
            'id_com': $('#ddlComponente').find('option').filter(':selected').val(),
            'id_cau': $('#ddlCausa').find('option').filter(':selected').val(),
            'sit_enc': sit_enc,
            'cau_pro': cau_pro,
            'sol_apl': sol_apl,
            'maq_pre': maq_pre,
            'id_emp': id_emp,
            //'id_pre_ser': id_pre_ser
            "ddl_pre_alt": ddl_pre_alt
        },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {


        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result.indexOf("erro") > 0) {
                abre_pop(result);
                return;
            }
            if (result == "2") {
                abre_pop('A data de início não pode ser menor que a data de abertura!');
                return;
            }
            if (result == "4") {
                abre_pop('A data de término não pode ser menor que a data de início!');
                return;
            } else {
                abre_pop('Chamado alterado com sucesso!', 'info');
                $('#ajax-modal').modal('toggle');//fecha o modal
                $("#mws-jui-dialog-interna").dialog("close");

                loadData_consultar(pagina_atual, indi, 'paginacao_chamados_alterar.aspx');

                //busca_chamados_con(indi)

                // zero as variavais
                id_pai = 0;
                id_est = 0;
                id_cid = 0;
                id_loc = 0;
                id_and = 0;
                id_are = 0;
                id_tip_chm = 0;
                id_tip_eqp = 0;
                id_prb = 0;
                id_eqp = 0;

            }

        },
        errror: function (error) {

        }
    });

}
////////////////////////////////////////////////////
function loading_show(indi) {
    $('#loading' + indi).html("<img src='images/ajax-loader.gif'/>").fadeIn('fast');
}
function loading_hide(indi) {
    $('#loading' + indi).fadeOut('fast');
}

/*
funcao que chama a pag de buscar e faz loading e fade        
*/

function busca_chamados(indi, pagina) {
    $("#lightbox").css("filter", "alpha(opacity=65)");
    //$("#lightbox").fadeIn();
    $("#container" + indi).fadeOut();
    loadData_consultar(1, indi, pagina);
}

/////////////////////////////////////////////////
function busca_chamados_consultar(indi, pagina) {
    $("#lightbox").css("filter", "alpha(opacity=65)");
    //$("#lightbox").fadeIn();

    loadData_consultar(1, indi, pagina);
}


/////////////////////////////////////////////////
/*
funcao que faz o auto complete
recebe como parametro o texto que esta dentro de txtLocalidade
*/
function busca_local(loc, indice) {

    if (loc.length >= 3) {
        id_pai = $('#ddlPais' + indice).find('option').filter(':selected').val();;
        id_est = $('#ddlEstado' + indice).find('option').filter(':selected').val();
        id_cid = $('#ddlCidade' + indice).find('option').filter(':selected').val();
        id_cli = $('#ddlCliente' + indice).find('option').filter(':selected').val();
        id_neg = $('#ddlNegocio' + indice).find('option').filter(':selected').val();

        $.ajax({
            type: "POST",
            url: "ajax/auto_complete.aspx",
            data: { 'loc': loc, "id_pai": id_pai, "id_est": id_est, "id_cid": id_cid, "indice": indice, "cli": id_cli, "neg": id_neg },
            timeout: 30000,
            async: true,
            beforeSend: function () {
                $("#load" + indice).html("<img src='images/ajax-loader.gif'>");
            },
            complete: function () {
                $("#load" + indice).html("");
            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {


                    $("#autoc" + indice).slideDown('slow', function () {
                        $("#autoc" + indice).html(result);
                    });


                }

            },
            errror: function (error) {

            }
        });
    }
    else {
        $("#load").html("");
        $("#autoc").slideUp(500);
    }
    //limpo o idloc
    if (loc.length < 3) {
        $("#hdn_idloc" + indice).val('')
        $("#autoc" + indice).slideUp('slow', function () {

        });
        $("#result_busca_chm" + indice).slideUp('slow');
    }
}
////////////////////////////////////////////////////////////////////
function busca_material(loc, indice) {

    if (loc.length >= 3) {
        $.ajax({
            type: "POST",
            url: "ajax/financeiro/ac_material.aspx",
            data: { 'loc': loc, "indice": indice },
            timeout: 30000,
            async: true,
            beforeSend: function () {
                $("#load" + indice).html("<img src='images/ajax-loader.gif'>");
            },
            complete: function () {
                $("#load" + indice).html("");
            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {


                    $("#autoc_ac_mat" + indice).slideDown('slow', function () {
                        $("#autoc_ac_mat" + indice).html(result);
                    });


                }

            },
            errror: function (error) {

            }
        });
    }
    else {
        $("#autoc_ac_mat" + indice).html("");
        $("#autoc").slideUp(500);
    }
    //limpo o idloc
    if (loc.length < 3) {
        $("#hdn_idMat" + indice).val('')
        $("#autoc_ac_mat" + indice).slideUp('slow', function () {

        });

    }
}
///////////////////////// busca serie
function busca_serie(loc, indice, event) {

    if (event.keyCode == 13 && loc != "") {

        $.ajax({
            type: "POST",
            url: "ajax/auto_complete_serie.aspx",
            data: { 'serie': loc, "indice": indice },
            timeout: 30000,
            async: false,
            beforeSend: function () {
                $("#load_seie" + indice).html("<img src='images/ajax-loader.gif'>");
            },
            complete: function () {
                $("#load_seie" + indice).html("");
            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Equipamento inativo')

                    return
                } else {


                    $("#autoc_serie" + indice).slideDown('slow', function () {
                        $("#autoc_serie" + indice).html(result);
                    });


                }

            },
            errror: function (error) {

            }
        });
    }
    else {
        $("#hdn_idSerie" + indice).val("");
        $("#hdn_idSerie" + indice).val("");
        $("#hdn_idloc" + indice).val("");
        $("#hdn_idTipChm" + indice).val("");
        $("#hdn_idTipEqp" + indice).val("");
        $("#hdn_idSerie" + indice).val("");
        $("#load_seie" + indice).html("");
        $("#autoc_serie" + indice).slideUp(500);
        $("#result_busca_chm" + indice).slideUp('slow', function () {




        });
    }

}
/////////////////////////
/////// paginacao alterar chamado
function loadData(page, indi) {
    loading_show();
    var os = $("#txtOs" + indi).val();
    var dta_de = $("#txt_dta_de" + indi).val();
    var dta_ate = $("#txt_dta_ate" + indi).val();
    $.ajax
    ({
        type: "POST",
        url: "ajax/paginacao_chamados_alterar.aspx",
        data: { "page": page, "os": os, "dta_de": dta_de, "dta_ate": dta_ate, "indice": indi, "idloc": $("#hdn_idloc" + indi).val() },
        success: function (msg) {

            loading_hide();
            if (msg == "1") {

                $("#lightbox").css("filter", "alpha(opacity=65)");
                //$("#lightbox").fadeIn();
                abre_pop('Sistema temporariamente indisponivel');
                //$("#aviso").slideDown('slow');
                $("#container" + indi).html("");
                return;
            }
            if (msg == "0") {

                $("#lightbox").css("filter", "alpha(opacity=65)");
                $("#lightbox").fadeIn();
                abre_pop('Sem dados para exibir!');
                //$("#aviso").slideDown('slow');
                $("#container" + indi).html("");
                return;
            }
            $("#container" + indi).html(msg);
            $('#container' + indi + ' table tbody tr:odd').addClass('zebraUm');

            //$("#lightbox").fadeOut();

        }
    });
}

////////////////////////////////q
function busca_chamados_con(indi) {
    $("#container" + indi).fadeOut();
    var pagina = 1;

    var sPais = $("#ddlPais" + indi).find('option').filter(':selected').val();
    var sEst = $("#ddlEstado" + indi).find('option').filter(':selected').val();
    var sCid = $("#ddlCidade" + indi).find('option').filter(':selected').val();
    var sAnd = $("#ddlAndar" + indi).find('option').filter(':selected').val();
    var sAre = $("#ddlArea" + indi).find('option').filter(':selected').val();

    jQuery('#list' + indi).jqGrid('setGridParam', {
        postData: {
            page: 1, "os": $("#txtOs" + indi).val(), "dta_de": $("#txt_dta_de" + indi).val(),
            "dta_ate": $("#txt_dta_ate" + indi).val(), "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "sPais": sPais, "sEst": sEst, "sCid": sCid, "sAnd": sAnd, "sAre": sAre
        }, page: 1
    }).trigger("reloadGrid")
};


////////////////////////////////q
function busca_chamados_alt(indi) {
    $("#container" + indi).fadeOut();
    var pagina = 1;

    var sPais = $("#ddlPais" + indi).find('option').filter(':selected').val();
    var sEst = $("#ddlEstado" + indi).find('option').filter(':selected').val();
    var sCid = $("#ddlCidade" + indi).find('option').filter(':selected').val();
    var sAnd = $("#ddlAndar" + indi).find('option').filter(':selected').val();

    var sAre = $("#ddlArea" + indi).find('option').filter(':selected').val();

    jQuery('#list' + indi).jqGrid('setGridParam', {
        postData: {
            page: 1, "os": $("#txtOs" + indi).val(), "dta_de": $("#txt_dta_de" + indi).val(),
            "dta_ate": $("#txt_dta_ate" + indi).val(), "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "sPais": sPais, "sEst": sEst, "sCid": sCid, "sAnd": sAnd, "sAre": sAre
        }, page: 1
    }).trigger("reloadGrid")
};


/////// paginacao alterar chamado
/////// paginacao alterar chamado
function loadData_consultar(page, indi, pagina) {

    var os = $("#txtOs" + indi).val();
    var dta_de = $("#txt_dta_de" + indi).val();
    var dta_ate = $("#txt_dta_ate" + indi).val();
	var dta_max_de = $("#txt_dta_max_de" + indi).val();
    var dta_max_ate = $("#txt_dta_max_ate" + indi).val();
    var solicit = $("#txtSolicitante" + indi).val()
	
    var sPais = $("#ddlPais" + indi).find('option').filter(':selected').val();;
    var sEst = $("#ddlEstado" + indi).find('option').filter(':selected').val();
    var sCid = $("#ddlCidade" + indi).find('option').filter(':selected').val();

    var stats = $('#ddlStatus_' + indi).val();
    var atr = $('#ddlAtraso' + indi).find('option').filter(':selected').val();
    var topos = $('#ddlTipo' + indi).val();
    var id_cli = $('#ddlCliente' + indi).find('option').filter(':selected').val();
    var id_neg = $('#ddlNegocio' + indi).find('option').filter(':selected').val();

    var tip_chm = $('#ddlTipoChamado_' + indi).find('option').filter(':selected').val();
    var tec = $('#ddlTecnicos_' + indi).find('option').filter(':selected').val();
    var sta_chm = $('#ddlStatus_' + indi).find('option').filter(':selected').val();

    var sAnd = $("#ddlAndar" + indi).find('option').filter(':selected').val();
    var sAre = $("#ddlArea" + indi).find('option').filter(':selected').val();

    var sAre = $("#ddlArea" + indi).find('option').filter(':selected').val();

    var idEqui = $("#hdn_idSerie" + indi).val();

    var num_pri = $("#num_pri" + indi).find('option').filter(':selected').val();

    var id_nat = $("#ddl_nat_os_" + indi).find('option').filter(':selected').val();
    var id_tip_fams = $("#ddl_fam_os_" + indi).find('option').filter(':selected').val();
    var id_pre_ser = $("#ddlPrestador" + indi).find('option').filter(':selected').val();
    //var id_pre_ser = $("#ddl_pres_" + indi).find('option').filter(':selected').val();
    var id_tip_chm = $("#ddl_tipo_os_" + indi).find('option').filter(':selected').val();
    var id_tip_eqp = $("#ddl_tipo_equipa_" + indi).find('option').filter(':selected').val();

    //Tipo de OSs
    //var id_tip_chm = $("#ddlTipoOs_" + indi).find('option').filter(':selected').val();

    var id_per = $("#ddlPeriodicidade_" + indi).find('option').filter(':selected').val();
    var id_emp = $("#ddl_emp_" + indi).find('option').filter(':selected').val();
    //var id_pre_ser = $("#ddl_pres_" + indi).find('option').filter(':selected').val();
    var id_pre_ser = $("#ddlPrestador" + indi).find('option').filter(':selected').val(); //Preventiva
    //var id_pre_ser = $("#ddl_pres_" + indi).find('option').filter(':selected').val(); //Chamado

	//OSs em atraso
    var st_os_atraso = $("#chkOsAtraso" + indi);
    if (st_os_atraso.is(":checked")) { st_os_atraso = "1" } else { st_os_atraso = "0" }
	
	var ord = $("#ord" + indi).find('option').filter(':selected').val();
    var tip_ord = $("#tip_ord" + indi).find('option').filter(':selected').val();
	
    if (topos == null) {
        topos = '';
    }
    else {
        try { topos = topos.join(",") } catch (err) { topos = 0 }

    }

    if (stats == null) {
        stats = '';
    }
    else {
        try { stats = stats.join(",") } catch (err) { stats = 0 }

    }

    ///////////////////////////////////
    $.ajax
        ({
            type: "POST",
            url: "ajax/" + pagina,
            data: {
                "page": page, "os": os, "solicit": solicit, "dta_de": dta_de, "dta_ate": dta_ate, "dta_max_de": dta_max_de, "dta_max_ate": dta_max_ate, "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "dta_ate": $("#txt_dta_ate" + indi).val(), "indice": indi,
                "idloc": $("#hdn_idloc" + indi).val(), "sPais": sPais, "sEst": sEst, "sCid": sCid, "stats": stats, "atraso": atr, "tipos": topos, "id_cli": id_cli, "id_neg": id_neg, "tip_chm": tip_chm,
                "tec": tec, "sta_chm": sta_chm, "sAnd": sAnd, "sAre": sAre, "idEqui": idEqui, "num_pri": num_pri,
                "id_nat": id_nat, "id_tip_fams": id_tip_fams, "id_tip_chm": id_tip_chm, "id_tip_eqp": id_tip_eqp, "id_emp": id_emp, "id_per": id_per, "id_pre_ser": id_pre_ser, "st_os_atraso": st_os_atraso, "ord": ord, "tip_ord": tip_ord
            },
            beforeSend: function () {
                loading_show(indi);
            },
            complete: function () {
                loading_hide(indi);
            },
            success: function (msg) {
                if (msg == "1") {
                    abre_pop('Sistema temporariamente indisponivel');
                    $("#container" + indi).html("");
                    return;
                }
                if (msg == "0") {
                    abre_pop('Sem dados para exibir');
                    $("#container" + indi).html("");
                    return;
                }

                $("#container" + indi).html(msg);
                $('#container' + indi + ' table tbody tr:odd').addClass('zebraUm');
                $("#container" + indi).fadeIn();
                $('html,body').animate({ scrollTop: $('#container' + indi).offset().top }, 1500);
            }
        });
}


function detalhe_os(id, indi) {

    if ($("#det_os_" + indi + "_" + id).css("display") == "none") {
        $("#det_os_" + indi + "_" + id).fadeIn(500);
        $("#exp_det_os_" + indi + "_" + id).html("<img src='images/fecha.gif' alt=''>");
    } else {
        $("#det_os_" + indi + "_" + id).fadeOut(500);
        $("#exp_det_os_" + indi + "_" + id).html("<img src='images/abre.gif' alt=''>");
    }
}
////////////////////////////
function change_page(pa, indi) {
    var page = pa;
    loadData(page, indi);
}
function change_page_btn(indi, tot, nome_pagina) {
    var page = parseInt($('#txt_num_pag_' + indi).val());
    var no_of_pages = parseInt(tot);
    if (page != 0 && page <= no_of_pages) {
        loadData_consultar(page, indi, nome_pagina);
    } else {
        abre_pop('Digite uma página entre 1 e ' + no_of_pages);
        $('#txt_num_pag_' + indi).focus();
        return false;
    }
}
//    //////////////////////
//    $('#container' + indi + ' .pagination li.active').live('click', function () {
//        var page = $(this).attr('p');
//        loadData(page, nome_pagina);

//    });
//////////////////////
function busca_preventivas(indi, pagina) {
    $("#lightbox").css("filter", "alpha(opacity=65)");
    //$("#lightbox").fadeIn();
    load_preventiva(1, indi, pagina);
}
////////////////////////////////q
function busca_prevs(indi) {
    var pagina = 1;

    var sPais = $("#ddlPais" + indi).find('option').filter(':selected').val();
    var sEst = $("#ddlEstado" + indi).find('option').filter(':selected').val();
    var sCid = $("#ddlCidade" + indi).find('option').filter(':selected').val();
    var sAnd = $("#ddlAndar" + indi).find('option').filter(':selected').val();
    var sAre = $("#ddlArea" + indi).find('option').filter(':selected').val();

    jQuery('#list' + indi).jqGrid('setGridParam', {
        postData: {
            page: 1, "os": $("#txtOs" + indi).val(), "dta_de": $("#txt_dta_de" + indi).val(),
            "dta_ate": $("#txt_dta_ate" + indi).val(), "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "sPais": sPais, "sEst": sEst, "sCid": sCid, "sAnd": sAnd, "sAre": sAre
        }, page: 1
    }).trigger("reloadGrid")
};
/////// paginacao alterar chamado
function load_preventiva(page, indi, pagina) {
    loading_show();
    var os = $("#txtOs" + indi).val();
    var dta_de = $("#txt_dta_de" + indi).val();
    var dta_ate = $("#txt_dta_ate" + indi).val();

    var sPais = $("#ddlPais" + indi).find('option').filter(':selected').val();
    var sEst = $("#ddlEstado" + indi).find('option').filter(':selected').val();
    var sCid = $("#ddlCidade" + indi).find('option').filter(':selected').val();
    var sAnd = $("#ddlAndar" + indi).find('option').filter(':selected').val();
    var sAre = $("#ddlArea" + indi).find('option').filter(':selected').val();

    var w = document.body.offsetWidth;
    var lar = (80 * w - 30) / 100;

    /////////////////////////////////////
    jQuery("#list" + indi).jqGrid({
        url: 'ajax/' + pagina,
        datatype: "xml",

        colNames: ['Nº', 'LOCAL', 'DATA PROGRAMADA', 'EQUIPAMENTO', 'STATUS', '', '', ''],
        colModel: [
            { name: 'id_prv', index: 'id_prv', width: ((5 * lar) / 100), align: "center" },
            { name: 'nom_loc', index: 'nom_loc', width: ((30 * lar) / 100) },
            { name: 'dataMax', index: 'dataMax', width: ((15 * lar) / 100), align: "center" },
            { name: 'amount', index: 'amount', align: "left", width: ((30 * lar) / 100) },
            { name: 'dsc_sta_chm', index: 'dsc_sta_chm', align: "left", width: ((10 * lar) / 100) },
            { name: 'com', index: 'dsc_sta_chm', align: "center", width: ((5 * lar) / 100) },
            { name: 'alt', index: 'dsc_sta_chm', align: "center", width: ((5 * lar) / 100) },
            { name: 'cal', index: 'dsc_sta_chm', align: "center", width: ((5 * lar) / 100) }
        ],
        rowNum: 10,
        width: lar - 31,
        mtype: "POST",
        postData: { "page": page, "os": os, "dta_de": dta_de, "dta_ate": dta_ate, "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "sPais": sPais, "sEst": sEst, "sCid": sCid, "sAnd": sAnd, "sAre": sAre },
        height: "100%",
        //rowList: [10, 20, 30],
        pager: jQuery('#pager' + indi),
        //
        subGrid: true, subGridUrl: 'ajax/lista_rotinas_detalhe.aspx', subGridModel: [{ name: ['ROTINA', 'PERIODICIDADE'], width: [((70 * lar) / 120), ((30 * lar) / 120)] }],
        //
        viewrecords: true,

        caption: "Resultado da Busca:",
        loadComplete: function () {
            $("#container" + indi).fadeIn();
            var qtd = jQuery('#list' + indi).jqGrid('getGridParam', 'records');

            if (qtd != 0) {
                $("#result_prv" + indi).fadeIn();
            }
            else {
                $("#lightbox").css("filter", "alpha(opacity=65)");
                $("#lightbox").fadeIn();
                abre_pop('Sem dados para exibir!');
                //$("#aviso").slideDown('slow');

            }
        }

    });


    /////////////////////////////////////
    //    $.ajax
    //    ({
    //        type: "POST",
    //        url: "ajax/paginacao_preventivas_alterar.aspx",
    //        data: { "page": page, "os": os, "dta_de": dta_de, "dta_ate": dta_ate, "indice": indi, "idloc": $("#hdn_idloc" + indi).val() },
    //        success: function (msg) {

    //            loading_hide();
    //            if (msg == "1") {

    //                $("#lightbox").css("filter", "alpha(opacity=65)");
    //                //$("#lightbox").fadeIn();
    //                abre_pop('Sistema temporariamente indisponivel');
    //                //$("#aviso").slideDown('slow');
    //                $("#container" + indi).html("");
    //                return;
    //            }
    //            if (msg == "0") {

    //                $("#lightbox").css("filter", "alpha(opacity=65)");
    //                $("#lightbox").fadeIn();
    //                abre_pop('Sem dados para exibir!');
    //                //$("#aviso").slideDown('slow');
    //                $("#container" + indi).html("");
    //                return;
    //            }
    //            $("#container" + indi).html(msg);
    //            $('#container' + indi + ' table tbody tr:odd').addClass('zebraUm');
    //            
    //            //$("#lightbox").fadeOut();

    //        }
    //    });
}

//loadData(1);  // For first time page load default results
//$('#container' + indi + ' .pagination li.active').live('click', function () {
function muda_pagina(pa, indi) {
    var page = pa;
    loadData(page, indi);
}


//});
function muda_pagina_btn(indi) {
    var page = parseInt($('.goto').val());
    var no_of_pages = parseInt($('.total').attr('a'));
    if (page != 0 && page <= no_of_pages) {
        loadData(page, indi);
    } else {
        abre_pop('Digite uma página entre 1 e ' + no_of_pages);
        $('.goto').val("").focus();
        return false;
    }

}

/*
funcao que busca os dados do chamado para alteração
*/
function busca_chamado(idChm, idLoc, indi, pagina_atual) {
    id_loc = idLoc;

    ////////////////////////
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/form_alt_chamado.aspx', { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'overflow-y': 'scroll',
                'height': '80%',
                'padding': '5px'
            });
        });
    }, 1000);



    //$.ajax({
    //    type: "POST",
    //    url: "ajax/form_alt_chamado.aspx",

    //    data: { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual },
    //    timeout: 30000,
    //    async: false,
    //    beforeSend: function () {

    //    },
    //    complete: function () {

    //    },
    //    cache: false,
    //    success: function (result) {
    //        // verifico se a sessao expirou
    //        if (result == "99") {
    //            abre_pop('Sua sessao expirou, efetue o login novamente!')
    //            location.href = 'default.aspx'
    //            return
    //        }
    //        if (result == "1") {
    //            abre_pop('Sistema temporariamente indisponivel!')

    //            return
    //        } else {
    //            abre_pop_interna(result);

    //        }

    //    },
    //    errror: function (error) {
    //        $("#pop").slideUp('fast');
    //        //$("#lightbox").fadeOut('fast');
    //    }
    //});


}

////////////////////////////////////////////////////////////////
/*
funcao que busca os dados do chamado para alteração
*/
function busca_chamado_consultar(idChm, idLoc, indi, pagina_atual) {
    id_loc = idLoc;

    ////////////////////////
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/form_con_chamado.aspx', { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'padding': '5px'
            });
        });
    }, 1000);

    //abre_pop_interna("Carregando...");
    //    $.ajax({
    //        type: "POST",
    //        url: "ajax/form_con_chamado.aspx",

    //        data: { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual },
    //        timeout: 30000,
    //        async: false,
    //        beforeSend: function () {

    //        },
    //        complete: function () {

    //        },
    //        cache: false,
    //        success: function (result) {
    //            // verifico se a sessao expirou
    //            if (result == "99") {
    //                abre_pop('Sua sessao expirou, efetue o login novamente!')
    //                location.href = 'default.aspx'
    //                return
    //            }
    //            if (result == "1") {
    //                abre_pop('Sistema temporariamente indisponivel!')

    //                return
    //            } else {
    //                abre_pop_interna(result);
    //            }

    //        },
    //        errror: function (error) {
    //            $("#pop").slideUp('fast');
    //            //$("#lightbox").fadeOut('fast');
    //        }
    //    });


}


/*
Função que associa CHK com Corretiva
*/
function pop_alt_che(idChm, idLoc, indi, pagina_atual) {
    id_loc = idLoc;

    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('checklist/pop.aspx', { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'padding': '5px'
            });
        });
    }, 1000);
}


////////////////////////////////////////////////////////////////

/*
funcao que fecha o popUp
*/
function fechar_conteudo_pop() {
    $("#pop").slideUp('fast', function () {
        $("#conteudo_pop").empty();
        $("#lightbox").fadeOut('fast')
    });
}

function fechar_conteudo_pop_alt() {
    $("#pop_alt").slideUp('fast', function () {
        $("#conteudo_pop_alt").empty();
        $("#lightbox").fadeOut('fast')
    });
}

//////////////////////////
function busca_comentarios(chamdo, local) {
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/form_inc_comentario.aspx', { "idChm": chamdo, "idLoc": local }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%'
            });
        });
    }, 1000);


    //$.ajax({
    //    type: "POST",
    //    url: "ajax/form_inc_comentario.aspx",

    //    data: { "idChm": chamdo, "idLoc": local },
    //    timeout: 30000,
    //    async: false,
    //    beforeSend: function () {

    //    },
    //    complete: function () {

    //    },
    //    cache: false,
    //    success: function (result) {
    //        // verifico se a sessao expirou
    //        if (result == "99") {
    //            abre_pop('Sua sessao expirou, efetue o login novamente!')
    //            location.href = 'default.aspx'
    //            return
    //        }
    //        if (result == "1") {
    //            abre_pop('Sistema temporariamente indisponivel!')

    //            return
    //        } else {
    //            abre_pop_interna(result);
    //        }

    //    },
    //    errror: function (error) {
    //        $("#pop").slideUp('fast');
    //        //$("#lightbox").fadeOut('fast');
    //    }
    //});


}

function grava_comentario(chamado, locl) {
    var txt = $("#txt_nov_com").val();
    if (txt == "") {
        abre_pop('Digite um comentário!');
        $("#txt_nov_com").focus();
        return
    } else {
        $.ajax
        ({
            type: "POST",
            url: "ajax/grava_comentario.aspx",
            data: { "chamado": chamado, "txt": txt },
            success: function (msg) {
                abre_pop('Comentário incluído com sucesso!')
                busca_comentarios(chamado, locl);
            }
        });
    }

}

////////////////////////////////////////////////////
function check_status_chamado(val) {
    $("#dados_andamento").hide();
    $("#dados_atendido").hide();
    $("#dados_pendencia").hide();
    $("#dados_despacho").hide();

    if (val == "4" || val == "2") {
        $("#dados_andamento").slideDown(500);
        $("#dados_atendido").slideDown(500);
    }
    if (val == "3") {
        $("#dados_andamento").slideDown(500);
        $("#dados_pendencia").slideDown(500);
    }
    if (val == "5") {
        $("#dados_despacho").slideDown(500);
    }
    if (val == "6") {
        $("#dados_andamento").slideDown(500);
    }
}
function busca_eqps_preventiva(indi, tipo) {
    $("#lightbox").css("filter", "alpha(opacity=65)");
    //$("#lightbox").fadeIn();
    load_eqps_preventiva(1, indi, tipo);
}
////////////////////////////////////////////////////
/////// paginacao alterar chamado
function load_eqps_preventiva(page, indi, tipo) {

    if ($("#hdn_idloc" + indi).val() == "" && $("#hdn_idSerie" + indi).val() == "") {
        abre_pop("Selecione uma localidade ou um equipamento");
        //$("#aviso").slideDown('slow');
        $("#div_tip_eqp" + indi).html('');
        return;
    }

    id_and_prv = $('#ddlAndar' + indi).find('option').filter(':selected').val();
    id_are_prv = $('#ddlArea' + indi).find('option').filter(':selected').val();

    $.ajax
    ({
        type: "POST",
        url: "ajax/load_eqps_preventiva.aspx",
        data: { "idloc": $("#hdn_idloc" + indi).val(), "id_and": id_and_prv, "id_are": id_are_prv, "indice": indi, "ideqp": $("#hdn_idSerie" + indi).val() },
        beforeSend: function () {
            $("#loading" + indi).show();
        },
        complete: function () {
            $("#loading" + indi).hide();
        },
        success: function (msg) {
            if (msg == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (msg == "1") {
                abre_pop('Sistema temporariamente indisponivel');
                //$("#aviso").slideDown('slow');
                $("#container" + indi).html("");
                $("#result_prv" + indi).slideUp(500);
                $("#div_tip_eqp" + indi).html('');
                return;
            }
            if (msg == "0") {
                abre_pop('Sem dados para exibir!');
                //$("#aviso").slideDown('slow');
                $("#result_prv" + indi).slideUp(500);
                $("#div_tip_eqp" + indi).html('');
                return;
            }
            $("#div_tip_eqp" + indi).html(msg);
            $("#container" + indi).fadeIn();
            $("#result_prv" + indi).slideDown(500);
        }
    });
}

//////////////////////////////////////////////////////
function busca_eqps_preventiva(indi, tipo) {
    if ($("#hdn_idloc" + indi).val() == "") {
        abre_pop("Selecione uma localidade");
        //$("#aviso").slideDown('slow');
        return;
    }

    id_and_prv = $('#ddlAndar' + indi).find('option').filter(':selected').val();
    id_are_prv = $('#ddlArea' + indi).find('option').filter(':selected').val();
    ddl_tip_eqp = $('#ddl_tip_eqp' + indi).find('option').filter(':selected').val();

    $.ajax
    ({
        type: "POST",
        url: "ajax/busca_eqps_preventiva.aspx",
        data: { "idloc": $("#hdn_idloc" + indi).val(), "id_and": id_and_prv, "id_are": id_are_prv, "indice": indi, "ddl_tip_eqp": ddl_tip_eqp, "ideqp": $("#hdn_idSerie" + indi).val(), "tipo": tipo },
        beforeSend: function () {
            $("#loading_eqps_" + indi).show();
        },
        complete: function () {
            $("#loading_eqps_" + indi).hide();
        },
        success: function (msg) {

            $("#container" + indi).html(msg);
            $('html,body').animate({ scrollTop: $('#container' + indi).offset().top }, 1500);

        }
    });
}
/////////////////
function chama_grava_plano(indi, tipo) {
    $('body').modalmanager('loading');
    $("#btn_abr_prv" + indi).hide()
    setTimeout(function () { grava_plano(indi, tipo) }, 3000);

}
////////////////////////////////////////////////////////
// funcao que grava o plano de preventiva
function grava_plano(indi, tipo) {
    if (tipo == 1) {


        var rowCount = $('#plano' + indi + ' tr').length;
        // verifico se tem data preenchida

        for (a = 0; a < rowCount - 1; a++) {
            var data = $('#dta_prv' + a + '_' + indi).val();

            if (data != "" && data != undefined) {
                $("#loading" + indi).show()
                $("#btn_abr_prv" + indi).hide()
                var id_eqpo = $('#hdn_id_eqp' + a + '_' + indi).val();
                var id_perio = $('#ddl_per_' + a + '_' + indi).find('option').filter(':selected').val();
                var loc = $("#hdn_idloc" + indi).val()
                var and = $('#ddlAndar' + indi).find('option').filter(':selected').val();
                var are = $('#ddlArea' + indi).find('option').filter(':selected').val();
                var minutos = $('#tem_est' + a + '_' + indi).val();
                var tecnico = $('#ddl_tec_' + a + '_' + indi).val();
                var prestador = $('#ddl_pres_' + a + '_' + indi).val();
                //var ddl_pre_alt = $('#ddl_pre_alt' + a + '_' + indi).val();

                //gravo a preventiva
                $.ajax
                ({
                    type: "POST",
                    url: "ajax/grava_preventiva.aspx",
                    async: false,
                    data: {
                        "idloc": loc, "id_and": and, "id_are": are, "indice": indi, "id_perio": id_perio, "id_eqpo": id_eqpo, "data": data, "minutos": minutos, "tecnico": tecnico, "prestador": prestador//, "ddl_pre_alt": ddl_pre_alt
                    },
                    success: function (msg) {
                        if (msg == "99") {
                            abre_pop('Sua sessao expirou, efetue o login novamente!')
                            location.href = 'default.aspx'
                            return
                        } else {
                            $("#msg_" + indi).html('Abrindo preventivas: ' + (a + 1) + '/' + (rowCount));
                        }

                    }
                });
            }
        }
        busca_eqps_preventiva(indi, tipo);
        abre_pop('Preventivas abertas com sucesso', 'info');
        //$("#aviso").slideDown('slow');
        $("#loading" + indi).hide()
        $("#btn_abr_prv" + indi).show()
        $('body').modalmanager('loading');
    } else {


        var rowCount = $('#plano' + indi + ' tr').length;
        // verifico se tem data preenchida

        for (a = 0; a < rowCount - 1; a++) {
            var data = $('#dta_prv' + a + '_' + indi).val();

            if (data != "" && data != undefined) {
                $("#loading" + indi).show()
                $("#btn_abr_prv" + indi).hide()
                var id_eqpo = $('#hdn_id_eqp' + a + '_' + indi).val();
                var id_perio = $('#ddl_per_' + a + '_' + indi).find('option').filter(':selected').val();
                var loc = $("#hdn_idloc" + indi).val()
                var and = $('#ddlAndar' + indi).find('option').filter(':selected').val();
                var are = $('#ddlArea' + indi).find('option').filter(':selected').val();
                var minutos = $('#tem_est' + a + '_' + indi).val();
                var tecnico = $('#ddl_tec_' + a + '_' + indi).val();
                var prestador = $('#ddl_pres_' + a + '_' + indi).val();
                //var ddl_pre_alt = $('#ddl_pre_alt' + a + '_' + indi).val();

                //gravo a preventiva
                $.ajax
                ({
                    type: "POST",
                    url: "ajax/grava_preventiva_terceiro.aspx",
                    async: false,
                    data: {
                        "idloc": loc, "id_and": and, "id_are": are, "indice": indi, "id_perio": id_perio, "id_eqpo": id_eqpo, "data": data, "minutos": minutos, "tecnico": tecnico, "prestador": prestador//, "ddl_pre_alt": ddl_pre_alt
                    },
                    success: function (msg) {
                        if (msg == "99") {
                            abre_pop('Sua sessao expirou, efetue o login novamente!')
                            location.href = 'default.aspx'
                            return
                        } else {
                            $("#msg_" + indi).html('Abrindo preventivas: ' + (a + 1) + '/' + (rowCount));
                        }

                    }
                });
            }
        }
        busca_eqps_preventiva(indi, tipo);
        abre_pop('Preventivas abertas com sucesso', 'info');
        //$("#aviso").slideDown('slow');
        $("#loading" + indi).hide()
        $("#btn_abr_prv" + indi).show()
        $('body').modalmanager('loading');
    }
}

///////////////////////////////////////
function pop_cal_eqps() {
    window.open('relatorios_imp/mapao_preventiva.aspx', '', 'width=800,height=400,scrollbars=1');
}
////////////////////////////////////////////////////////////////
/*
funcao que busca os dados do chamado para alteração
*/
function busca_preventivas_alterar(idChm, idLoc, indi, pagina_atual) {
    id_loc = idLoc;

    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/form_alt_preventiva.aspx', { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%'
            });
        });
    }, 1000);

    //abre_pop_interna("Carregando...");
    //    $.ajax({
    //        type: "POST",
    //        url: "ajax/form_alt_preventiva.aspx",

    //        data: { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual },
    //        timeout: 30000,
    //        async: false,
    //        beforeSend: function () {

    //        },
    //        complete: function () {

    //        },
    //        cache: false,
    //        success: function (result) {
    //            // verifico se a sessao expirou
    //            if (result == "99") {
    //                abre_pop('Sua sessao expirou, efetue o login novamente!')
    //                location.href = 'default.aspx'
    //                return
    //            }
    //            if (result == "1") {
    //                abre_pop('Sistema temporariamente indisponivel!')

    //                return
    //            } else {
    //                abre_pop_interna(result);
    //                $("#pop").slideDown('fast');
    //            }

    //        },
    //        errror: function (error) {
    //            $("#pop").slideUp('fast');
    //            //$("#lightbox").fadeOut('fast');
    //        }
    //    });


}
////////////////////////////////////////////////////////////////
/*
funcao que busca os dados do chamado para alteração
*/
function busca_preventivas_consultar(idChm, idLoc, indi, pagina_atual) {
    id_loc = idLoc;

    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/form_con_preventiva.aspx', { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%'
            });
        });
    }, 1000);
    /*
    id_loc = idLoc;
    abre_pop_interna("Carregando...");
        $.ajax({
            type: "POST",
            url: "ajax/form_con_preventiva.aspx",

            data: { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    abre_pop_interna(result);
                     
                }

            },
            errror: function (error) {
                $("#pop").slideUp('fast');
                //$("#lightbox").fadeOut('fast');
            }
        });
        */

}
/////////////////////////////////////
//////////////////////////
function busca_comentarios_preventiva(chamdo, local) {
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/form_inc_comentario_prv.aspx', { "idChm": chamdo, "idLoc": local }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%'
            });
        });
    }, 1000);
    //////////////

    //$.ajax({
    //    type: "POST",
    //    url: "ajax/form_inc_comentario_prv.aspx",

    //    data: { "idChm": chamdo, "idLoc": local },
    //    timeout: 30000,
    //    async: false,
    //    beforeSend: function () {

    //    },
    //    complete: function () {

    //    },
    //    cache: false,
    //    success: function (result) {
    //        // verifico se a sessao expirou
    //        if (result == "99") {
    //            abre_pop('Sua sessao expirou, efetue o login novamente!')
    //            location.href = 'default.aspx'
    //            return
    //        }
    //        if (result == "1") {
    //            abre_pop('Sistema temporariamente indisponivel!')

    //            return
    //        } else {
    //            abre_pop_interna(result);

    //        }

    //    },
    //    errror: function (error) {
    //        $("#pop").slideUp('fast');
    //        //$("#lightbox").fadeOut('fast');
    //    }
    //});


}
///////////////////////////////////
function grava_comentario_preventiva(chamado, locl) {
    var txt = $("#txt_nov_com").val();
    if (txt == "") {
        abre_pop('Digite um comentário!');
        $("#txt_nov_com").focus();
        return
    } else {
        $.ajax
        ({
            type: "POST",
            url: "ajax/grava_comentario_prv.aspx",
            data: { "chamado": chamado, "txt": txt },
            success: function (msg) {
                busca_comentarios_preventiva(chamado, locl);
            }
        });
    }

}
////////////////////////////////////////////////////
function check_status_preventiva(val) {
    $("#dados_andamento").slideUp(500);
    $("#dados_atendido").slideUp(500);
    $("#dados_pendencia").slideUp(500);
    if (val == "1") {

    }
    if (val == "6" || val == "3") {
        $("#dados_andamento").slideDown(500);

    }
    if (val == "4" || val == "2") {
        $("#dados_andamento").slideDown(500);
        $("#dados_atendido").slideDown(500);

    }
    if (val == "5") {
        $("#dados_despacho").slideDown(500);
    }
    if (val == "3") {
        $("#dados_pendencia").slideDown(500);

    }
}

////////////////////////////////////////////////////
function altera_preventiva(id_chama, indi, pagina_atual) {

    var dta_ini = $("#dta_ini").val();

    var data_prog_prv = $("#data_prog_prv").val();

    var hor_ini = $("#hor_ini").val();
    var mot_pen = $("#mot_pen").val();
    var dta_ter = $("#dta_ter").val();
    var hor_ter = $("#hor_ter").val();
    var ser_exe = $("#ser_exe").val();
    var id_tec = $("#ddlTecdespacho").find('option').filter(':selected').val();

    var ddlStatus = $('#ddlStatus').find('option').filter(':selected').val();
    var ddlAvaliacao = $('#ddlAvaliacao').find('option').filter(':selected').val();
    var ddl_emp_alt = $('#ddl_emp_alt').find('option').filter(':selected').val();

    //aqui Prestador
    var ddl_pre_alt = $('#ddl_pre_alt').find('option').filter(':selected').val();

    if (data_prog_prv == "") {
        abre_pop("Digite a data programada!");
        return;
    }

    if (ddlStatus == "1") {
        dta_ini = "";
        hor_ini = "";
        mot_pen = "";
        dta_ter = "";
        hor_ter = "";
    } else if (ddlStatus == "2") {

        if (dta_ini == "") {
            abre_pop("Digite a data de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ini == "") {
            abre_pop("Digite a hora de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (dta_ter == "") {
            abre_pop("Digite a data de término!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ter == "") {
            abre_pop("Digite a hora de término!");
            //$("#aviso").slideDown('slow');
            return;
        }
        //if (ser_exe == "") {
        //    abre_pop("Digite o serviço executado!");
        //    //$("#aviso").slideDown('slow');
        //    return;
        //}
       

    } else if (ddlStatus == "3") {
        if (dta_ini == "") {
            abre_pop("Digite a data de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ini == "") {
            abre_pop("Digite a hora de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (mot_pen == "") {
            abre_pop("Digite o motivo da pendência!");
            //$("#aviso").slideDown('slow');
            return;
        }
    } else if (ddlStatus == "4") {
        if (dta_ini == "") {
            abre_pop("Digite a data de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        if (hor_ini == "") {
            abre_pop("Digite a hora de início!");
            //$("#aviso").slideDown('slow');
            return;
        }
        


    } else if (ddlStatus == "5") {
        if (id_tec == "0") {
            abre_pop("Selecione um técnico!");
            //$("#aviso").slideDown('slow');
            return;
        }
    }
    ////////////////////////
    // obrigo a preencher as preventivas rotinas
    if (ddlStatus == "2" || ddlStatus == "4") {
        var qtd = $("#rotinas_lista tr").size();
        for (a = 0; a < qtd - 1; a++) {
            var ddl = $('#med_' + a).find('option').filter(':selected').val();
            var txt = $('#txt_' + a).val();
            console.log(txt);
            var rot = $('#hdn_rot_' + a).val();
            //////////////////////
            //atualizo o banco
            $.ajax
            ({
                type: "POST",
                url: "ajax/atualiza_preventiva_rotina.aspx",
                data: { "ddl": ddl, "txt": txt, "rot": rot },
                success: function (msg) {

                }
            });
            //////////////////////
        }
    }

    ////////////////////////
    /////////////////

    $.ajax({
        type: "POST",
        url: "ajax/altera_preventiva.aspx",
        data: {
            "id_prv": id_chama, "ddlStatus": ddlStatus,
            "dta_ini": dta_ini, "hor_ini": hor_ini, "mot_pen": mot_pen, "dta_ter": dta_ter, "hor_ter": hor_ter, "ser_exe": ser_exe, "ddlAvaliacao": ddlAvaliacao, "id_tec": id_tec, "data_prog_prv": data_prog_prv, "ddl_emp": ddl_emp_alt,
            //"ddl_pre": ddl_pre_alt
            //"ddlPrestador": ddl_pre_alt
            "ddl_pre_alt": ddl_pre_alt
        },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {


        },
        cache: false,
        success: function (result) {
            // verifico se a sessao expirou
            if (result == "99") {
                abre_pop('Sua sessao expirou, efetue o login novamente!')
                location.href = 'default.aspx'
                return
            }
            if (result == "1") {
                abre_pop('Sistema temporariamente indisponivel!')

                return
            } else {
                abre_pop('Preventiva alterada com sucesso!', 'info');
                $('#ajax-modal').modal('toggle');//fecha o modal

                loadData_consultar(pagina_atual, indi, 'paginacao_preventivas_alterar.aspx');



            }

        },
        errror: function (error) {

        }
    });

}
////////////////////////////////////////////////////
//// busca status
function busca_status(indi) {
    $.ajax({
        type: "POST",
        url: "ajax/buscaStatusChamado.aspx",
        data: { "indice": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            $("#dropStatus" + indi).html(result);

        },
        errror: function (error) {

        }
    });
}
////////////////////////////////////////////////////
//// busca tipo
function busca_tipo(indi) {
    $.ajax({
        type: "POST",
        url: "ajax/buscaTipoChamado.aspx",
        data: { "indice": indi },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            $("#dropTipo" + indi).html(result);

        },
        errror: function (error) {

        }
    });
}
////////////////////////////////////////////////////
// relatorios chamados consultar
/////////////////////////////////////////////////
function busca_relatorios_chamados_consultar(indi, pagina) {
    $("#lightbox").css("filter", "alpha(opacity=65)");
    //$("#lightbox").fadeIn();

    load_relatorios_chamados_consultar(1, indi, pagina);
}
/////// paginacao alterar chamado
function load_relatorios_chamados_consultar(page, indi, pagina) {

    loading_show();
    var os = $("#txtOs" + indi).val();
    var dta_de = $("#txt_dta_de" + indi).val();
    var dta_ate = $("#txt_dta_ate" + indi).val();
    var stats = $('#ddlStatus' + indi).val();
    var atr = $('#ddlAtraso' + indi).find('option').filter(':selected').val();
    var topos = $('#ddlTipo' + indi).val();

    if (topos == null) {
        topos = '';
    }
    else {
        topos = topos.join(",")
    }

    if (stats == null) {
        stats = '';
    }
    else {
        stats = stats.join(",")
    }


    var entrei = 0;

    var w = document.body.offsetWidth;
    var lar = (80 * w - 30) / 100;

    /////////////////////////////////////
    jQuery("#list" + indi).jqGrid({
        url: 'ajax/relatorios/' + pagina,
        datatype: "xml",

        colNames: ['Nº', 'dif', 'LOCAL', 'DATA ABERTURA', 'DATA MÁX. ATENDIMENTO', 'TIPO', 'STATUS', '', ''],
        colModel: [
        { name: 'id_prv', index: 'id_prv', width: ((5 * lar) / 100), align: "center" },
        { name: 'dif', index: 'id_prv', width: ((5 * lar) / 100), align: "center", hidden: true },
        { name: 'nom_loc', index: 'nom_loc', width: ((30 * lar) / 100) },
        { name: 'dataab', index: 'dataMax', width: ((15 * lar) / 100), align: "center" },
        { name: 'dataMax', index: 'dataMax', width: ((15 * lar) / 100), align: "center" },
        { name: 'amount', index: 'amount', align: "left", width: ((30 * lar) / 100) },
        { name: 'dsc_sta_chm', index: 'dsc_sta_chm', align: "left", width: ((10 * lar) / 100) },
        { name: 'com', index: 'dsc_sta_chm', align: "center", width: ((5 * lar) / 100) },
        { name: 'alt', index: 'dsc_sta_chm', align: "center", width: ((5 * lar) / 100) }
        ],
        rowNum: 10,
        width: lar - 31,
        mtype: "POST",
        postData: { "page": page, "os": os, "dta_de": dta_de, "dta_ate": dta_ate, "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "stats": stats, "atraso": atr, "tipos": topos },
        height: "100%",
        //rowList: [10, 20, 30],
        pager: jQuery('#pager' + indi),
        //
        subGrid: true, subGridUrl: 'ajax/relatorios/lista_detalhe_chamado.aspx', subGridModel: [{ name: ['DESCRIÇAO'], width: [((100 * lar) / 120)], params: ['id_prv'] }],
        //
        viewrecords: true,

        caption: "Resultado da Busca:",
        loadComplete: function () {
            $("#container" + indi).fadeIn();
            var qtd = jQuery('#list' + indi).jqGrid('getGridParam', 'records');

            if (qtd != 0) {
                $("#result_prv" + indi).fadeIn();
                for (var i = 0; i < qtd - 1; i++) {

                    if (parseInt($("#" + i).find("td").eq(2).html()) > 0) {
                        $("#" + i).css("color", "red");
                    }

                }
            }
            else {
                $("#lightbox").css("filter", "alpha(opacity=65)");
                $("#lightbox").fadeIn();
                abre_pop('Sem dados para exibir!');
                //$("#aviso").slideDown('slow');

            }


        }

    });

}
function busca_relat_cham_con(indi) {
    $("#container" + indi).fadeOut();
    var pagina = 1;
    var stats = $('#ddlStatus' + indi).val();
    var atr = $('#ddlAtraso' + indi).find('option').filter(':selected').val();
    var topos = $('#ddlTipo' + indi).val();

    if (topos == null) {
        topos = '';
    }
    else {
        topos = topos.join(",")
    }


    if (stats == null) {
        stats = '';
    }
    else {
        stats = stats.join(",")
    }
    jQuery('#list' + indi).jqGrid('setGridParam', {
        postData: {
            page: 1, "os": $("#txtOs" + indi).val(), "dta_de": $("#txt_dta_de" + indi).val(),
            "dta_ate": $("#txt_dta_ate" + indi).val(), "indice": indi, "idloc": $("#hdn_idloc" + indi).val(), "stats": stats, "atraso": atr, "tipos": topos
        }, page: 1
    }).trigger("reloadGrid")
};

function exp_tel_chm_con() {
    document.rel_cha_con.submit();
}
////////////////////////////////////////////////////

/*
funcao que busca os dados do estoque para alteração
tipo = 0 = corretiva
tipo = 1 = preventiva
*/
function busca_estoque(idChm, idLoc, indi, pagina_atual, tipo) {
    id_loc = idLoc;
    $("#lightbox").css("filter", "alpha(opacity=65)");
    $("#lightbox").fadeIn('fast', function () {
        $.ajax({
            type: "POST",
            url: "ajax/estoque/form_alt_estoque.aspx",

            data: { "idChm": idChm, "idLoc": idLoc, "indice": indi, "pagina_atual": pagina_atual, "tipo": tipo },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#conteudo_pop").html(result);
                    $("#pop").slideDown('fast');
                }

            },
            errror: function (error) {
                $("#pop").slideUp('fast');
                //$("#lightbox").fadeOut('fast');
            }
        });

    });
}

// funcao que bsuca os componentes de acordo como subsistema recebido
function buscaComponentes(idSubsistema) {
    //$("#dropCausa").html("<select id='ddlCausa'><option value='0'>Selecione uma Causa</option></select>");
    $.ajax({
        type: "POST",
        url: "ajax/busca_componente.aspx",

        data: { "idSubsistema": idSubsistema },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {

            //$("#dropComponente").html(result);

        },
        errror: function (error) {

        }
    });
}
// fim buscaComponentes

// funcao que bsuca causa e defeito de acordo com o componente recebido
function buscaCausaDefeito(idcomp) {
    $.ajax({
        type: "POST",
        url: "ajax/busca_causa_defeito.aspx",

        data: { "idComp": idcomp },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {

            $("#dropCausa").html(result);

        },
        errror: function (error) {

        }
    });
}
// fim buscaComponentes

// funcao que remove a peca do chamado selecionado
function remove_peca(id) {
    var answer = confirm("Deseja realmente excluir esse item?")
    if (answer) {
        $.ajax({
            type: "POST",
            url: "ajax/estoque/remove_peca.aspx",
            data: { "id": id },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!');
                    location.href = 'default.aspx';
                    return;
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponível');
                    return;
                }
                else {
                    $('#linha_' + id).remove();
                    abre_pop('Item excluído com sucesso!');
                }

            },
            errror: function (error) {

            }
        });
    }
}
// fim remove_peca

function grava_pendencia_estoque_chamado(indi, idchm) {
    var qtd = $("#ddl_qtd_pec_pen_" + indi).val();
    var part = $("#part_numb_peca_" + indi).val();
    if (part == "") {
        abre_pop('Digite um Part Number');
        return;
    } else {
        $.ajax({
            type: "POST",
            url: "ajax/estoque/grava_pendenecia_peca_chamado.aspx",
            data: { "qtd": qtd, "part": part, "idchm": idchm },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!');
                    location.href = 'default.aspx';
                    return;
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponível');
                    return;
                }
                if (result == "10") {
                    abre_pop('Não existe uma peça com esse Part Number');
                    return;
                }
                else {
                    $("#pecas_utilizadas_" + indi).html(result);
                    $("#result_busca_peca_estoque_dados" + indi).html('');
                    $("#result_busca_peca_estoque" + indi).hide();
                    $("#part_numb_peca_" + indi).val('');
                    abre_pop('Pendência de peça solicitada com sucesso!');
                }

            },
            errror: function (error) {

            }
        });
    }

}



// funcao que verifica se o chamado foi fechado por teleofne, se sim habilita os campos
function fechadoTel(val) {
    if (val == 0) {
        $("#ul_fec_tel").hide();
    } else {
        $("#ul_fec_tel").show();
    }
}
// fim fechadoTel

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// funcao que formata moeda
(function ($) {

    $.fn.priceFormat = function (options) {

        var defaults =
          {
              prefix: 'US$ ',
              centsSeparator: '.',
              thousandsSeparator: ',',
              limit: false,
              centsLimit: 2,
              clearPrefix: false,
              allowNegative: false
          };

        var options = $.extend(defaults, options);

        return this.each(function () {

            // pre defined options
            var obj = $(this);
            var is_number = /[0-9]/;

            // load the pluggings settings
            var prefix = options.prefix;
            var centsSeparator = options.centsSeparator;
            var thousandsSeparator = options.thousandsSeparator;
            var limit = options.limit;
            var centsLimit = options.centsLimit;
            var clearPrefix = options.clearPrefix;
            var allowNegative = options.allowNegative;

            // skip everything that isn't a number
            // and also skip the left zeroes
            function to_numbers(str) {
                var formatted = '';
                for (var i = 0; i < (str.length) ; i++) {
                    char_ = str.charAt(i);
                    if (formatted.length == 0 && char_ == 0) char_ = false;

                    if (char_ && char_.match(is_number)) {
                        if (limit) {
                            if (formatted.length < limit) formatted = formatted + char_;
                        }
                        else {
                            formatted = formatted + char_;
                        }
                    }
                }

                return formatted;
            }

            // format to fill with zeros to complete cents chars
            function fill_with_zeroes(str) {
                while (str.length < (centsLimit + 1)) str = '0' + str;
                return str;
            }

            // format as price
            function price_format(str) {
                // formatting settings
                var formatted = fill_with_zeroes(to_numbers(str));
                var thousandsFormatted = '';
                var thousandsCount = 0;

                // split integer from cents
                var centsVal = formatted.substr(formatted.length - centsLimit, centsLimit);
                var integerVal = formatted.substr(0, formatted.length - centsLimit);

                // apply cents pontuation
                formatted = integerVal + centsSeparator + centsVal;

                // apply thousands pontuation
                if (thousandsSeparator) {
                    for (var j = integerVal.length; j > 0; j--) {
                        char_ = integerVal.substr(j - 1, 1);
                        thousandsCount++;
                        if (thousandsCount % 3 == 0) char_ = thousandsSeparator + char_;
                        thousandsFormatted = char_ + thousandsFormatted;
                    }
                    if (thousandsFormatted.substr(0, 1) == thousandsSeparator) thousandsFormatted = thousandsFormatted.substring(1, thousandsFormatted.length);
                    formatted = thousandsFormatted + centsSeparator + centsVal;
                }

                // if the string contains a dash, it is negative - add it to the begining (except for zero)
                if (allowNegative && str.indexOf('-') != -1 && (integerVal != 0 || centsVal != 0)) formatted = '-' + formatted;

                // apply the prefix
                if (prefix) formatted = prefix + formatted;

                return formatted;
            }

            // filter what user type (only numbers and functional keys)
            function key_check(e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                var typed = String.fromCharCode(code);
                var functional = false;
                var str = obj.val();
                var newValue = price_format(str + typed);

                // allow key numbers, 0 to 9
                if ((code >= 48 && code <= 57) || (code >= 96 && code <= 105)) functional = true;

                // check Backspace, Tab, Enter, Delete, and left/right arrows
                if (code == 8) functional = true;
                if (code == 9) functional = true;
                if (code == 13) functional = true;
                if (code == 46) functional = true;
                if (code == 37) functional = true;
                if (code == 39) functional = true;
                if (allowNegative && (code == 189 || code == 109)) functional = true; // dash as well

                if (!functional) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (str != newValue) obj.val(newValue);
                }

            }

            // inster formatted price as a value of an input field
            function price_it() {
                var str = obj.val();
                var price = price_format(str);
                if (str != price) obj.val(price);
            }

            // Add prefix on focus
            function add_prefix() {
                var val = obj.val();
                obj.val(prefix + val);
            }

            // Clear prefix on blur if is set to true
            function clear_prefix() {
                if ($.trim(prefix) != '' && clearPrefix) {
                    var array = obj.val().split(prefix);
                    obj.val(array[1]);
                }
            }

            // bind the actions
            $(this).bind('keydown', key_check);
            $(this).bind('keyup', price_it);

            // Clear Prefix and Add Prefix
            if (clearPrefix) {
                $(this).bind('focusout', function () {
                    clear_prefix();
                });

                $(this).bind('focusin', function () {
                    add_prefix();
                });
            }

            // If value has content
            if ($(this).val().length > 0) {
                price_it();
                clear_prefix();
            }


        });

    };

})(jQuery);
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// funcao que adiciona uma atividade na tabela de chamados atividades
function adiciona_atividade_fun(os, tip_os) {
    console.log('asdasd')
    var ddlTec0 = $('#ddlTec0').find('option').filter(':selected').val();
    var ddlAtv0 = $('#ddlAtv0').find('option').filter(':selected').val();
    //var ddlHorChe0 = $('#ddlHorChe0').find('option').filter(':selected').val();
    //var ddlMinChe0 = $('#ddlMinChe0').find('option').filter(':selected').val();
    var ddlHorIni0 = $('#ddlHorIni0').find('option').filter(':selected').val();
    var ddlMinIni0 = $('#ddlMinIni0').find('option').filter(':selected').val();
    var ddlHorTer0 = $('#ddlHorTer0').find('option').filter(':selected').val();
    var ddlMinTer0 = $('#ddlMinTer0').find('option').filter(':selected').val();
    //var ddlHorVia0 = $('#ddlHorVia0').find('option').filter(':selected').val();
    //var ddlMinVia0 = $('#ddlMinVia0').find('option').filter(':selected').val();

    //var dta_hor_che0 = $("#dta_hor_che0").val();
    var dta_hor_ini0 = $("#dta_hor_ini0").val();
    var dta_hor_ter0 = $("#dta_hor_ter0").val();


    if (ddlTec0 == "0" || ddlTec0 == "") {
        abre_pop('Selecione um Técnico');
        return
    }

    if (ddlAtv0 == "0" || ddlAtv0 == "") {
        abre_pop('Selecione uma Ativade');
        return
    }

    //////////////////////////////////////////////
    if (dta_hor_ini0 == "") {
        abre_pop('Selecione uma Data de Início');
        return;
    } else {
        if (!validaDat('dta_hor_ini0', dta_hor_ini0)) { }
    }

    if (ddlHorIni0 == "0" || ddlHorIni0 == "") {
        abre_pop('Selecione uma Hora de Início');
        return
    }

    if (ddlMinIni0 == "0" || ddlMinIni0 == "") {
        abre_pop('Selecione os Munitos do Início');
        return
    }
    ////////////////////////////////////////////////////
    if (dta_hor_ter0 == "") {
        abre_pop('Selecione uma Data de Término');
        return;
    } else {
        if (!validaDat('dta_hor_ter0', dta_hor_ter0)) { }
    }

    if (ddlHorTer0 == "0" || ddlHorTer0 == "") {
        abre_pop('Selecione uma Hora de Término');
        return
    }

    if (ddlMinTer0 == "0" || ddlMinTer0 == "") {
        abre_pop('Selecione os Munitos do Término');
        return
    }
    else {
        console.log('entrei')
        $.ajax({
            type: "POST",
            url: "ajax/inc_atividade_os.aspx",
            data: {
                'ddlTec0': ddlTec0,
                'ddlAtv0': ddlAtv0,
                'ddlHorIni0': ddlHorIni0,
                'ddlMinIni0': ddlMinIni0,
                'ddlHorTer0': ddlHorTer0,
                'ddlMinTer0': ddlMinTer0,
                'dta_hor_ini0': dta_hor_ini0,
                'dta_hor_ter0': dta_hor_ter0,
                'os': os,
                'tip_os': tip_os
            },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                var car = result
                var sp = car.split(",");

                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!');
                    location.href = 'default.aspx';
                    return;
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponível');
                    return;
                }
                if (sp[0] == "existe") {

                    abre_pop('Já Existe um apontamento para este técnico! OS:' + sp[1] + ' ' + sp[2]);
                    return;
                }
                else {
                    $("#reg_atend").html(result);
                    $("#reg_atend").show();
                    abre_pop("Atividade registrada com sucesso!", "info");
                }

            },
            errror: function (error) {

            }
        });

    }


}


////////////////////////////////////////////////////////////////////////////
function remove_atividade(atv) {
    var answer = confirm("Deseja realmente excluir esse item?")
    if (answer) {
        $.ajax({
            type: "POST",
            url: "ajax/del_atividade_os.aspx",
            data: { "atv": atv },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!');
                    location.href = 'default.aspx';
                    return;
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponível');
                    return;
                }
                else {
                    $("#reg_atend").html(result);
                    abre_pop("Ativade excluída com sucesso!");
                }

            },
            errror: function (error) {

            }
        });
    }
}
/////////////////////////////////////////////////////////////////////////////

// fim adiciona_atividade_fun()

function abre_popa_msg(msg) {
    $("#lightbox").css("filter", "alpha(opacity=65)");
    $("#lightbox").fadeIn();
    $("#txt_aviso_l").html(msg);
    $("#aviso_light").slideDown('slow');
}

///////////////////////////////////////////////////////////////////////
// funcao que valida a data
///////////////////////////////////////////////////////////////////////
function validaDat(campo, valor) {
    var date = valor;
    var ardt = new Array;
    var ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
    ardt = date.split("/");
    erro = false;
    if (date.search(ExpReg) == -1) {
        erro = true;
    }
    else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
        erro = true;
    else if (ardt[1] == 2) {
        if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
            erro = true;
        if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
            erro = true;
    }
    if (erro) {
        abre_pop("\"" + valor + "\" não é uma data válida!!!");
        campo.focus();
        campo.value = "";
        return false;
    }
    return true;
}
///////////////////////////////////////////////////////////////////////

//idChm, idLoc, indi, pagina_atual
function busca_req_peca() {
    //id_loc = idLoc;
    $("#lightbox").css("filter", "alpha(opacity=65)");
    $("#lightbox").fadeIn('fast', function () {
        $.ajax({
            type: "POST",
            url: "ajax/estoque/form_req_peca.aspx",

            data: {},
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#conteudo_pop").html(result);
                    $("#pop").slideDown('fast');
                }

            },
            errror: function (error) {
                $("#pop").slideUp('fast');
                //$("#lightbox").fadeOut('fast');
            }
        });

    });
}


//////////////////////////////////////////////////
// funcao que coloca preco
(function ($) {

    $.fn.priceFormat = function (options) {

        var defaults =
          {
              prefix: 'US$ ',
              centsSeparator: '.',
              thousandsSeparator: ',',
              limit: false,
              centsLimit: 2,
              clearPrefix: false,
              allowNegative: false
          };

        var options = $.extend(defaults, options);

        return this.each(function () {

            // pre defined options
            var obj = $(this);
            var is_number = /[0-9]/;

            // load the pluggings settings
            var prefix = options.prefix;
            var centsSeparator = options.centsSeparator;
            var thousandsSeparator = options.thousandsSeparator;
            var limit = options.limit;
            var centsLimit = options.centsLimit;
            var clearPrefix = options.clearPrefix;
            var allowNegative = options.allowNegative;

            // skip everything that isn't a number
            // and also skip the left zeroes
            function to_numbers(str) {
                var formatted = '';
                for (var i = 0; i < (str.length) ; i++) {
                    char_ = str.charAt(i);
                    if (formatted.length == 0 && char_ == 0) char_ = false;

                    if (char_ && char_.match(is_number)) {
                        if (limit) {
                            if (formatted.length < limit) formatted = formatted + char_;
                        }
                        else {
                            formatted = formatted + char_;
                        }
                    }
                }

                return formatted;
            }

            // format to fill with zeros to complete cents chars
            function fill_with_zeroes(str) {
                while (str.length < (centsLimit + 1)) str = '0' + str;
                return str;
            }

            // format as price
            function price_format(str) {
                // formatting settings
                var formatted = fill_with_zeroes(to_numbers(str));
                var thousandsFormatted = '';
                var thousandsCount = 0;

                // split integer from cents
                var centsVal = formatted.substr(formatted.length - centsLimit, centsLimit);
                var integerVal = formatted.substr(0, formatted.length - centsLimit);

                // apply cents pontuation
                formatted = integerVal + centsSeparator + centsVal;

                // apply thousands pontuation
                if (thousandsSeparator) {
                    for (var j = integerVal.length; j > 0; j--) {
                        char_ = integerVal.substr(j - 1, 1);
                        thousandsCount++;
                        if (thousandsCount % 3 == 0) char_ = thousandsSeparator + char_;
                        thousandsFormatted = char_ + thousandsFormatted;
                    }
                    if (thousandsFormatted.substr(0, 1) == thousandsSeparator) thousandsFormatted = thousandsFormatted.substring(1, thousandsFormatted.length);
                    formatted = thousandsFormatted + centsSeparator + centsVal;
                }

                // if the string contains a dash, it is negative - add it to the begining (except for zero)
                if (allowNegative && str.indexOf('-') != -1 && (integerVal != 0 || centsVal != 0)) formatted = '-' + formatted;

                // apply the prefix
                if (prefix) formatted = prefix + formatted;

                return formatted;
            }

            // filter what user type (only numbers and functional keys)
            function key_check(e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                var typed = String.fromCharCode(code);
                var functional = false;
                var str = obj.val();
                var newValue = price_format(str + typed);

                // allow key numbers, 0 to 9
                if ((code >= 48 && code <= 57) || (code >= 96 && code <= 105)) functional = true;

                // check Backspace, Tab, Enter, Delete, and left/right arrows
                if (code == 8) functional = true;
                if (code == 9) functional = true;
                if (code == 13) functional = true;
                if (code == 46) functional = true;
                if (code == 37) functional = true;
                if (code == 39) functional = true;
                if (allowNegative && (code == 189 || code == 109)) functional = true; // dash as well

                if (!functional) {
                    e.preventDefault();
                    e.stopPropagation();
                    if (str != newValue) obj.val(newValue);
                }

            }

            // inster formatted price as a value of an input field
            function price_it() {
                var str = obj.val();
                var price = price_format(str);
                if (str != price) obj.val(price);
            }

            // Add prefix on focus
            function add_prefix() {
                var val = obj.val();
                obj.val(prefix + val);
            }

            // Clear prefix on blur if is set to true
            function clear_prefix() {
                if ($.trim(prefix) != '' && clearPrefix) {
                    var array = obj.val().split(prefix);
                    obj.val(array[1]);
                }
            }

            // bind the actions
            $(this).bind('keydown', key_check);
            $(this).bind('keyup', price_it);

            // Clear Prefix and Add Prefix
            if (clearPrefix) {
                $(this).bind('focusout', function () {
                    clear_prefix();
                });

                $(this).bind('focusin', function () {
                    add_prefix();
                });
            }

            // If value has content
            if ($(this).val().length > 0) {
                price_it();
                clear_prefix();
            }


        });

    };

})(jQuery);
//////////////////////////////////////////////////
function pop_pecas(id_chm, id_loc, indi, pagina_atual) {

    $("#lightbox").css("filter", "alpha(opacity=65)");
    $("#lightbox").fadeIn('fast', function () {
        $.ajax({
            type: "POST",
            url: "ajax/form_inc_pecas_chamado.aspx",

            data: { "idChm": id_chm, "idLoc": id_loc, "indice": indi, "pagina_atual": pagina_atual },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!')
                    location.href = 'default.aspx'
                    return
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponivel!')

                    return
                } else {
                    $("#conteudo_pop").html(result);
                    $("#pop").slideDown('fast');
                }

            },
            errror: function (error) {
                $("#pop").slideUp('fast');
                //$("#lightbox").fadeOut('fast');
            }
        });

    });
}

function formatar(src, mask) {
    var i = src.value.length;
    var saida = mask.substring(0, 1);
    var texto = mask.substring(i)
    if (texto.substring(0, 1) != saida) {
        src.value += texto.substring(0, 1);
    }
}

function numbers(evt, campo) {

    try {
        //Run some code here
        var charCode = (evt.which) ? evt.which : event.keyCode

        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    catch (err) {
        //Handle errors here
    }

}

function dateMask(inputData, e) {
    if (document.all) // Internet Explorer
        var tecla = event.keyCode;
    else //Outros Browsers
        var tecla = e.which;

    if (tecla >= 47 && tecla < 58) { // numeros de 0 a 9 e "/"
        var data = inputData.value;
        if (data.length == 2 || data.length == 5) {
            data += '/';
            inputData.value = data;
        }
    } else if (tecla == 8 || tecla == 0) // Backspace, Delete e setas direcionais(para mover o cursor, apenas para FF)
        return true;
    else
        return false;
}

jQuery.fn.validaData = function () {
    $(this).change(function (event) {
        $valor = $(this).val();
        if ($valor) {
            $erro = "";
            var expReg = /^((0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[012])\/[1-2][0-9]\d{2})$/;
            if ($valor.match(expReg)) {
                var $dia = parseFloat($valor.substring(0, 2));
                var $mes = parseFloat($valor.substring(3, 5));
                var $ano = parseFloat($valor.substring(6, 10));

                if (($mes == 4 && $dia > 30) || ($mes == 6 && $dia > 30) || ($mes == 9 && $dia > 30) || ($mes == 11 && $dia > 30)) {
                    $erro = "Data incorreta! O mês especificado na data " + $valor + " contém 30 dias.";
                } else {
                    if ($ano % 4 != 0 && $mes == 2 && $dia > 28) {
                        $erro = "Data incorreta!! O mês especificado na data " + $valor + " contém 28 dias."
                    } else {
                        if ($ano % 4 == 0 && $mes == 2 && $dia > 29) {
                            $erro = "Data incorreta!! O mês especificado na data " + $valor + " contém 29 dias.";
                        }
                    }
                }
            } else {
                $erro = "Formato de Data para " + $valor + " é inválido";
            }
            if ($erro) {
                $(this).val('');
                alert($erro);
                setTimeout(function () { $(this).focus(); }, 50);
            } else {
                return $(this);
            }
        } else {
            return $(this);
        }
    });
}



function pop_upload(indi, id, tipo) {
    ////////////////////////
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('upload/frmUp.aspx', { "id": id, "tipo": tipo }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'padding': '5px'
            });
        });
    }, 1000);
    //$.ajax({
    //    type: "POST",
    //    url: "upload/frmUp.aspx",
    //    data: { "id": id, "tipo": tipo },
    //    timeout: 30000,
    //    async: true,
    //    beforeSend: function () {


    //    },
    //    complete: function () {

    //    },
    //    cache: false,
    //    success: function (result) {
    //        if (result == "99") {
    //            alert('Sua sessao expirou, efetue o login novamente!')
    //            location.href = 'default.aspx'
    //            return
    //        }
    //        abre_pop_interna(result);

    //    },
    //    errror: function (error) {

    //    }
    //});
}

function disableEnterKey(e) {
    var key;
    if (window.event)
        key = window.event.keyCode; //IE
    else
        key = e.which; //firefox      

    return (key != 13);
}



function muda_senha() {
    if ($("#txtSenhaAtu").val() == "") {
        abre_pop("Digite a senha atual");
        return;
    }

    if ($("#txtNovaSenha").val() == "") {
        abre_pop("Digite a senha nova");
        return;
    }
    if ($("#txtNovaSenha2").val() == "") {
        abre_pop("Digite a confirmacao da senha nova");
        return;
    }
    if ($("#txtNovaSenha2").val() != $("#txtNovaSenha").val()) {
        abre_pop("As novas senhas não conferem");
        return;
    }
    if ($("#txtNovaSenha").val().length < 5) {
        abre_pop("A senha deve ter no mínimo 5 caracteres!");
        return;
    } else {

        $.post('ajax/cadastro/usuarios/troca_senha.aspx', { "txtSenhaAtu": $("#txtSenhaAtu").val(), "txtNovaSenha": $("#txtNovaSenha").val() }, function (data) {
            if (data != "1") {
                if (data == "0") {
                    abre_pop('Senha alterada com sucesso!', 'info');
                    $('#ajax-modal').modal('toggle');//fecha o modal
                }
                // senha nao confere com a atual
                if (data == "2") {
                    abre_pop('Senha atual não confere!')
                }
                if (data == "99") {
                    abre_pop('Sua sessão expirou!');
                    location.href = "default.aspx";
                }


            }
            else {


                abre_pop('Sistema temporariamente indisponível.')
            }

        });
    }
}
/////////////////////////////// módulo agendamento reuniao
function pop_alt_reu(indi, id, page, tipo) {
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/agendamento/busca_reu.aspx', { "indice": indi, "id": id, "page": page, "tipo": tipo }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'padding': '5px',
                'overflow-y': 'scroll',
                'height': '80%'
            });
        });
    }, 1000);
}
///////////////////////////////////////////////
function pop_alt_inf(indi, id, page, tipo) {
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/agendamento/informe/busca_inf.aspx', { "indice": indi, "id": id, "page": page, "tipo": tipo }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'padding': '5px',
                'overflow-y': 'scroll',
                'height': '80%'
            });
        });
    }, 1000);
}

////////////////////////////////////////
function popula_tipo_os(val, indi, div) {
    $.ajax({
        type: "POST",
        url: "ajax/atendimento/busca_tipo_os.aspx",

        data: { "id_fams": val, "indice": indi, "div": div },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            $("#" + div + indi).html(result);

        },
        errror: function (error) {
            $("#pop").slideUp('fast');
            //$("#lightbox").fadeOut('fast');
        }
    });
}
///////////////////////////////////////
function procura_eqp(are, tipos, tipeqp, indi) {
    $.ajax({
        type: "POST",
        url: "ajax/atendimento/busca_eqp_area.aspx",

        data: { "id_are": are, "indice": indi, "id_tip_os": tipos, "id_tip_eqp": tipeqp },
        timeout: 30000,
        async: false,
        beforeSend: function () {

        },
        complete: function () {

        },
        cache: false,
        success: function (result) {
            $("#div_ddl_eqps_" + indi).html(result);

        },
        errror: function (error) {

            //$("#lightbox").fadeOut('fast');
        }
    });
}
///////////////////////////////////////
function pop_rel_ger(indi, id, page, tipo) {
    var $modal = $('#ajax-modal');

    $('body').modalmanager('loading');
    setTimeout(function () {
        $modal.load('ajax/relatorios/pop_rel_ger.aspx', { "indice": indi, "id": id, "page": page, "tipo": tipo }, function () {

            $('#ajax-modal').modal({
                backdrop: true,
                keyboard: true
            }).css({
                width: '90%',
                'margin-left': '-45%',
                'padding': '5px',
                'overflow-y': 'scroll',
                'height': '80%'
            });
        });
    }, 1000);
}


////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////
/// função para remover a despesa do banco e da tabela (forms form_alt_chamado e preventiva)
function remove_despesa(des) {
    var answer = confirm("Deseja realmente excluir essa despesa?")
    if (answer) {
        $.ajax({
            type: "POST",
            url: "ajax/del_despesas_os.aspx",
            data: { "des": des },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!');
                    location.href = 'default.aspx';
                    return;
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponível');
                    return;
                }
                else {
                    $("#des_os").html(result);
                    abre_pop("Despesa excluída com sucesso!", "info");
                }

            },
            errror: function (error) {

            }
        });
    }
}

/// função para remover a despesa do banco e da tabela (forms form_alt_chamado e preventiva)

function adiciona_despesa(os, tip_os) {
    console.log('entrou')
    var id_tip_des_os = $('#id_tip_des_os').find('option').filter(':selected').val();

    //var dta_hor_che0 = $("#dta_hor_che0").val();
    var obs_des_os = $("#obs_des_os").val();
    var valor_des_os = $("#valor_des_os").val();


    if (id_tip_des_os == "0") {
        abre_pop('Selecione um tipo de despesa');
        return
    }
    else {
        console.log('entrei')
        $.ajax({
            type: "POST",
            url: "ajax/inc_des_os.aspx",
            data: {
                'id_tip_des_os': id_tip_des_os,
                'obs_des_os': obs_des_os,
                'valor_des_os': valor_des_os,
                'nro_ors': os,
                'tip_os': tip_os
            },
            timeout: 30000,
            async: false,
            beforeSend: function () {

            },
            complete: function () {

            },
            cache: false,
            success: function (result) {
                // verifico se a sessao expirou
                if (result == "99") {
                    abre_pop('Sua sessao expirou, efetue o login novamente!');
                    location.href = 'default.aspx';
                    return;
                }
                if (result == "1") {
                    abre_pop('Sistema temporariamente indisponível');
                    return;
                }
                else {
                    $("#des_os").html(result);
                    $("#des_os").show();
                    abre_pop("Despesa registrada com sucesso!", "info");
                    $("#obs_des_os").val("");
                    $('#id_tip_des_os').find('option').filter(':selected').val("0");
                    $("#valor_des_os").val("");
                }

            },
            errror: function (error) {

            }
        });

    }


}
