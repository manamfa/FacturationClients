$(function () {
    $('#categorieID').on('change', function () {
        var id = $(this).val();
        /*alert(id);*/
        $('#categorieID').append("<option value=''></option>")
        $.get('/tableachats/getProducts', { id: id }, function (data) {
            $('#produitID').empty();
            $.each(data, function (index, row) {
                $('#produitID').append("<option value='" + row.produitID + "'>" + row.nomproduit + "</option>")
            });
        });
    });
});
//Getting unit price
$(function () {
    $('#produitID').on('change', function () {
        var product_id = $(this).val();
        $('#produitID').append("<option value=''></option>")

        $.get('/tableachats/getUnitPrice', { product_id: product_id }, function (data) {
            $('.prixunitaire').val(data.prixdeventeunitaire);
        });
    });
});
//Calcute the total
$(function () {
    $('#Quantite').on('change', function () {
        var quantite = $(this).val();
        var prixunitaire = $('.prixunitaire').val();
       
        var Total = quantite * prixunitaire;
        //alert(total);
        $('#tot').val(Total);
    });
});





$(function () {
    $('#produitID').on('change', function () {
        var product_id = $(this).val();
        $('#produitID').append("<option value=''></option>")
        //alert(product_id);

        $.get('/historiquefacturations/getUnitPrice', { product_id: product_id }, function (data) {
            $('.prixunitaire').val(data.prixdeventeunitaire);
        });
    });
});