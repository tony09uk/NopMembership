function createAndCompleteOrder() {
    var addToCartUrl = document.getElementById('addToCartUrl').value;
    var completeOrderUrl = document.getElementById('completeOrderUrl').value;
    var productId = document.getElementById('productId').value;

    OrderAndComplete.run(addToCartUrl, completeOrderUrl, productId);
}

var OrderAndComplete = {
    loadWaiting: false,
    usepopupnotifications: false,
    topcartselector: '',
    topwishlistselector: '',
    flyoutcartselector: '',
    localized_data: false,
    addToCartUrl: '',
    completeOrderUrl: '',
    formSelector: '#product-details-form',
    productId: 0,

    run: function (addToCartUrl, completeOrderUrl, productId) {
        this.addToCartUrl = addToCartUrl;
        this.completeOrderUrl = completeOrderUrl;
        this.productId = productId;

        this.addOrder();
    },

    addOrder: function () {
        var self = this;

        AjaxCart.setLoadWaiting(true);
        //todo: handle scenario where the shopping cart already has an item(option: always delete the item first)
        var addToCart = self.send(self.addToCartUrl, self.formSelector);

        var addOrder = addToCart.then(function (response) {
            if (response.message) {
                if (response.success === true) {
                    return self.send(self.completeOrderUrl, self.productId);
                }
                else {
                    if (AjaxCart.usepopupnotifications === true) {
                        displayPopupNotification(response.message, 'error', true);
                    }
                    else {
                        //no timeout for errors
                        displayBarNotification(response.message, 'error', 0);
                    }
                }
                return false; //todo: what should the return type be if it fails
            }
            if (response.redirect) {
                location.href = response.redirect;
                return true;
            }
            return false;
        });

        if (addOrder === false) {
            //todo: add error handling here
            AjaxCart.setLoadWaiting(false);
            return;
        }

        addOrder.done(function (response) {
            if (response.message) {
                //display notification
                if (response.success === true) {
                    //success
                    if (AjaxCart.usepopupnotifications === true) {
                        displayPopupNotification(response.message, 'success', true);
                    }
                    else {
                        //specify timeout for success messages
                        displayBarNotification(response.message, 'success', 3500);
                    }
                    location.href = response.redirect;
                    return true;
                }
                else {
                    //error
                    if (AjaxCart.usepopupnotifications === true) {
                        displayPopupNotification(response.message, 'error', true);
                    }
                    else {
                        //no timeout for errors
                        displayBarNotification(response.message, 'error', 0);
                    }
                }
                return false;
            }
        });

        addOrder.fail(function (data) {
            
        });

        addOrder.always(function (data) {
            AjaxCart.setLoadWaiting(false);
        });
    },

    send: function (urladd, data) {
        return $.ajax({
            cache: false,
            url: urladd,
            data: $(data).serialize(),
            type: "POST",
        });
    },
}