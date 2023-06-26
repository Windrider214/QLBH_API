AddItemToCart = function (e) {
    var item = {
        maSp: $(e).data('productid'),
        tenSp: $(e).data('productname'),
        soLuong: 1,
        donGia: $(e).data('price'),
        seoImage: $(e).data('image'),
    };
    console.log(item);
    if (item.maSp == '') {
        swal({
            text: "Hãy chọn ít nhất 1 sản phẩm !!!",
            icon: "error",
            button: "warning!",
        });
        return;
    } 
    var store = GetCookie('ShoppingCart');
    store = AddAndUpdateShoppingCart(store, item);
    swal({
        text: "Thêm giỏ hàng thành công !!!",
        icon: "success",
        button: "OK!",
    });

    //Update slh header
    $.ajax({
        type: 'GET',
        url: "/Cart/GetSLH",
        data: {},
        success: function (data) { 
            console.log(data.slh);
            $("#slh").html(data.slh);
        },
        error: function () {
            console.log("error:" + JSON.stringify(data));
        }
    });
}


RemoveItemFromCart = function (e) {

    var result = confirm("Bạn có chắc chắn muốn xóa sản phẩm này ?");
    if (result) {
        var item = {
            maSp: $(e).data('productid'),
            tenSp: $(e).data('productname'),
            soLuong: 1,
            donGia: $(e).data('price'),
            seoImage: $(e).data('image'),
        };
        var store = GetCookie('ShoppingCart');
        store = RemoveItemFromShoppingCart(store, item);

        $.ajax({
            type: 'POST',
            url: "/Cart/CheckOut",
            data: {},
            success: function (data) {
                $('#shoppingCart').html(data);
            },
            error: function (data) {
                console.log("error:" + JSON.stringify(data));

            }

        });

        //Update slh header
        $.ajax({
            type: 'GET',
            url: "/Cart/GetSLH",
            data: {},
            success: function (data) {
                console.log(data.slh);
                $("#slh").html(data.slh);
            },
            error: function () {
                console.log("error:" + JSON.stringify(data));
            }
        });
    }
}

UpdateItemToCart = function (e) {

    var item = {
        maSp: $(e).data('productid'),
    }

    var soluong = $('#txtSoLuong_' + item.maSp).val();

    if ($(e).data('type') != '0') {
        soluong = CalcMinusorPlusQuantity($(e).data('type'), item.maSp)

    }

    var store = GetCookie('ShoppingCart');
    var index = store.findIndex(function (d) {
        return d.maSp == item.maSp;
    });

    if (store.length == 0 || index == -1) {
        return;
    }

    store[index].soLuong = soluong;
    SetCookie('ShoppingCart', store);

    $.ajax({
        type: 'POST',
        url: "/Cart/CheckOut",
        data: {},
        success: function (data) {
            $('#shoppingCart').html(data);
        },
        error: function (data) {
            console.log("error:" + JSON.stringify(data));

        }

    });
}

CalcMinusorPlusQuantity = function (type, productid) {
    var $input_quantity = $("#txtSoLuong_" + productid);
    var sl = $("#txtSoLuong_" + productid).val();

    // giảm số lượng
    if (type == 1) {
        if (parseInt(sl, 10) > 1) {
            $input_quantity.val(parseInt($input_quantity.val()) - 1);
        }
    }
    // tăng số lượng
    if (type == 2) {
        $input_quantity.val(parseInt($input_quantity.val()) + 1);
    }

    return $("#txtSoLuong_" + productid).val();
}

AddAndUpdateShoppingCart = function (store, item) {
    console.log(store);
    console.log(item);
    var index = store.findIndex(function (d) {
        return d.maSp == item.maSp;
    });
    if (store.length == 0 || index == -1) {
        store.push(item);
        SetCookie('ShoppingCart', store);
        return store;
    } else {
        store[index].soLuong = parseInt(store[index].soLuong) + 1;
    }
    SetCookie('ShoppingCart', store);


    return store;
}

RemoveItemFromShoppingCart = function (store, item) {

    if (store.length > 0) {
        var index = store.findIndex(function (d) {
            return d.maSp == item.maSp;
        });

        store.splice(index, 1);
        SetCookie('ShoppingCart', store);
        return store;
    }
}

