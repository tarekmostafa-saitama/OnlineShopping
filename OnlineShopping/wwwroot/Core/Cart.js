let plusbtngroup = document.querySelectorAll('.plus');let prodqtygroup = document.querySelectorAll('.qty');let prodqtygroupminus = document.querySelectorAll('.qty');let CartTotal = document.querySelector('.cart_total_amount');var counter = 0;let prodPrice = document.querySelectorAll('.price');let subTotal = document.querySelectorAll('.product-subtotal')document.onload = subTotalFunc();//fstTime=>Subtotalfunction subTotalFunc() {    for (let i = 0; i < prodPrice.length; i++) {        subTotal.item(i).textContent = parseInt(prodPrice.item(i).textContent) * parseInt(prodqtygroup.item(i).value);        counter += Number(subTotal.item(i).textContent);    }    CartTotal.textContent = counter;
}const calculateTotal = () => {
    counter = 0;
    for (let i = 0; i < subTotal.length; i++) {        counter += Number(subTotal.item(i).textContent);    }
    CartTotal.textContent = counter;
}
//plusfor (let i = 0; i < plusbtngroup.length; i++) {    plusbtngroup[i].addEventListener('click', () => {        prodqtygroup.item(i).value;        subTotal.item(i).textContent = parseInt(prodPrice.item(i).textContent) * parseInt(prodqtygroup.item(i).value);

        calculateTotal();
        //counter += parseInt(prodPrice.item(i).textContent);
        //CartTotal.textContent = counter;        //console.log(parseInt(prodqtygroupminus.item(i).value))    });}
// minuslet minusBtnGroup = document.querySelectorAll('.minus');for (let i = 0; i < minusBtnGroup.length; i++) {    minusBtnGroup[i].addEventListener('click', () => {        if (prodqtygroupminus.item(i).value != 0) {            prodqtygroupminus.item(i).value;            subTotal.item(i).textContent = parseInt(prodPrice.item(i).textContent) * parseInt(prodqtygroupminus.item(i).value);

            calculateTotal();
            //console.log(parseInt(prodqtygroupminus.item(i).value))
            //if (parseInt(prodqtygroupminus.item(i).value) > 1) {
            //    counter -= parseInt(prodPrice.item(i).textContent);
            //    CartTotal.textContent = counter;
            //    }             }    });}



        

