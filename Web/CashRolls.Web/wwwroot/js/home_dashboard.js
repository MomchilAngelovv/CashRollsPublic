let stripe = Stripe('Stripe public key');

let checkoutButton = document.getElementById('checkout-button');

if (checkoutButton) {
    checkoutButton.addEventListener('click', async function () {
        let sessionId = await $.get('your url to get session fo payments')

        if (sessionId != null) {
            stripe.redirectToCheckout({
                sessionId: sessionId
            }).then(function (result) {
                alert(result.error.message)
            });
        }
    });
}
