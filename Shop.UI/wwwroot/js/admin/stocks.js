var app = new Vue({
    el: '#app',
    data: {
        products:  [],
        selectedProduct: null,
        newStock: {
            id: 0,
            description: 'Size',
            qty: 10
        } 
    },
    mounted(){
      this.getStock();  
    },
    methods: {
        getStock(){
            this.loading =  true;
            axios.get('/Admin/stock')
                .then(res => {
                    console.log(res);
                    this.products = res.data
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false
                })
        },
        selectProduct(product){
            this.selectProduct = product;
        }
    }
})