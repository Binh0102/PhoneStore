import router from '@/router';
import axios from 'axios';
import { format } from 'date-fns';
import { createStore } from 'vuex'

export default createStore({
  state: {
    categories:[],
    orders:[],
    order:{
      id: null,
      phoneNumber: '',
      address: '',
      total: null,
      orderdate: format(new Date(), 'yyyy-MM-dd'),
      status: null,
      userId: null,

    },
    orderdetails:[],
    orderdetail:{
     id: null,
      price: null,
      quantity: null,
      productId: null,
      orderId: null,
    },
    products:[],
    product: {
     id: null,
      productName:'',
      price:null,
      image:'',
      categoryId:null,
      memoryStorage:''
    },
    users:[],
    user:{
      id: 0,
      fullName:'',
      username:'',
      email:'',
      password:'',
      address:'',
    },
    cart:[],
    token:null,
    isLogin: false,
  },
  getters: {
    categories : state => state.categories,
    category : state => state.category,
    orders : state => state.orders,
    order : state => state.order,
    orderdetails : state => state.orderdetails,
    orderdetail : state => state.orderdetail,
    products : state => state.products,
    product : state => state.product,
    users : state => state.users,
    user : state => state.user,
    cart: state => state.cart,
    isLogin: state => state.isLogin,
  },
  mutations: {
    setToken(state,token){
      state.token = token;
      state.isLogin = !!token;
    },
    getCategories(state, categories){
      state.categories = categories;
    },
    getOrders(state, orders){
      state.orders = orders;
    },
    getOrderdetails(state, orderdetails){
      state.orderdetails = orderdetails;
    },
    getProducts(state,products){
      state.products = products;
    },
    getUsers(state, users){
      state.users = users;
    },
    addUser(state,users){
      state.users.push(users);
    },
    addOrderdetail(state,orderdetails){
      state.orderdetails.push(orderdetails);
    },
    setUser(state, user) {
      state.user = user;
    },
    addOrder(state,orders){
      state.orders.push(orders);
    },
    getProductId(state,product){
      state.product.id = product.id;
      state.product.productName = product.productName;
      state.product.price = product.price;
      state.product.img = product.im;
      state.product.description = product.description
    },
    getOrderId(state,order){
      state.order.id =order.id;
      state.order.phoneNumber =order.phoneNumber;
      state.order.address =order.address;
      state.order.total =order.total;
      state.order.orderdate =order.orderdate;
      state.order.status =order.status;
      state.order.userId =order.userId;

    },
    editOrder(state,order){
      var index = state.orders.findIndex(m =>m.id == order.id)
      if(index>-1){
        state.orders[index] = order;
      }
    },
    deleteProduct(state,id){
      var index = state.products.findIndex(m =>m.id == id);
      if(index >-1){
        state.products.splice(index,1);
      }
    },
    deleteUsers(state,id){
      var index = state.users.findIndex(m =>m.id == id);
      if(index >-1){
        state.users.splice(index,1);
      }
    },
    deleteOrder(state,id){
      var index = state.orders.findIndex(m =>m.id == id);
      if(index >-1){
        state.orders.splice(index,1);
      }
    },
    getProductByCategoryId(state,id){
      state.products = state.products.filter(m => m.categoryId == id);
    },
    addToCart(state,product){
      let dem = 0;
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart[i].quantity++;
          dem++;
        };
      }
      if(dem == 0){
        state.cart.push({item: product, quantity:1})
      }
      console.log(state.cart)
    },
    deleteCart(state, product){
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart.splice(i, 1);
        }
      } 
    },
    addQuantity(state, product){
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart[i].quantity++;
        }
      }
      console.log(state.cart)
    },
    minusQuantity(state, product){
      for(let i = 0; i < state.cart.length; i++){
        if(state.cart[i].item.id == product.id){
          state.cart[i].quantity--;
          if(state.cart[i].quantity == 0){
            state.cart.splice(i,1);
          }
        }
      }
    }
  },
  actions: {
    login({ commit }, { username, password }) {
      axios.post(`https://localhost:7121/api/User/Login?account=${username}&password=${password}`)
        .then(response => {
          if (response.data.user.status === 1) {
            commit('setToken', response.data.token);
            commit('setUser',response.data.user)
            router.push('/custom/index')
          } else {
            alert('Sai tài khoản hoặc mật khẩu vui lòng nhập lại');
          }
        })
        .catch(e => {
          alert('Sai tài khoản hoặc mật khẩu vui lòng nhập lại');
          console.log(e)
        });
    },
    loginAdmin({ commit }, { username, password }) {
      axios.post(`https://localhost:7121/api/User/Login?account=${username}&password=${password}`)
        .then(response => {
          if (response.data.user.status === 2) {
            commit('setToken', response.data.token);
            commit('setUser',response.data.user)
            router.push('/admin')
          } else {
            alert('Sai tài khoản hoặc mật khẩu vui lòng nhập lại');
          }
        })
        .catch(error => {
          alert('Sai tài khoản hoặc mật khẩu vui lòng nhập lại');
          console.error(error);
        });
    },
    logout({commit}){
      commit('setToken',null)
      
    },
    getProductByCategoryId({commit},id){
      axios.get('https://localhost:7121/api/Product')
      .then(response =>{
        if(response.data){
          commit('getProducts', response.data);
          commit('getProductByCategoryId',id);
        }
      }).catch(e=>{
        console.log(e);
      })
    },
    getCategories({commit}){
      axios.get('https://localhost:7121/api/Category')
      .then(response =>{
        if(response.data){
          commit('getCategories', response.data);
        }
      }).catch(e=>{
        console.log(e);
      })
    },
    getOrders({commit}){
      axios.get('https://localhost:7121/api/Order')
      .then(response => {
        if(response.data){
          commit('getOrders', response.data);
        }
      }).catch(e => {
        console.log(e);
      })
    },
    getOrderId({commit},id){
      if(id){
        axios.get('https://localhost:7121/api/Order' + '/' + id)
        .then(response =>{
          commit("getOrderId",response.data)
        }).catch(e =>{
          console.log(e)
        })
      }
    },
    updateOrderStatus({ commit }, order) {
      return axios.put('https://localhost:7121/api/Order/'+order.id,order)
        .then(response => {
          commit('editOrder', response.data);
          return response.data;
        })
        .catch(error => {
          console.error(error);
          throw error;
        });
    },
    getOrderdetails({commit}){
      axios.get('https://localhost:7121/api/Orderdetail')
      .then(response => {
        if(response.data){
          commit  ('getOrderdetails',response.data);
        }
      }).catch(e => {
        console.log(e);
      })
    },
    addOrderdetail({commit},orderdetail){
      axios.post('https://localhost:7121/api/User',orderdetail)
      .then(response => {
        if(response.data){
          commit("addUser",response.data);
          alert("Đã đăng ký thành công")
        }
      }).catch(e => {
        console.log(e);
      })
    },
    getProducts({commit}){
      axios.get('https://localhost:7121/api/Product')
      .then(response =>{
        if(response.data){
          commit('getProducts', response.data);
        }
      }).catch(e=>{
        console.log(e);
      })
    },
    getUsers({commit}){
      axios.get('https://localhost:7121/api/User')
      .then(response => {
        if(response.data){
          commit('getUsers', response.data);
        }
      }).catch(e => {
        console.log(e);
      })
    },
    addUser({commit},user){
      axios.post('https://localhost:7121/api/User',user)
      .then(response => {
        if(response.data){
          commit("addUser",response.data);
          alert("Đã đăng ký thành công")
        }
      }).catch(e => {
        console.log(e);
      })
    },
    addOrder({commit},order){
      axios.post('https://localhost:7121/api/Order',order)
      .then(response => {
        axios.get('')
        .then(response =>{
          this.state.order.id = response.data.id;
          this.dispatch('addOrderdetail')
        })
          commit("addOrder",response.data);
          const latestOrderId = response.data.id;
          dispatch('getOrderId', latestOrderId);
      }).catch(e => {
        console.log(e);
      })
    },
    getProductId({commit},id){
      if(id){
        axios.get('https://localhost:7121/api/Product' + '/' + id)
        .then(response =>{
          commit("getProductId",response.data)
        }).catch(e =>{
          console.log(e)
        })
      }
    },
    deleteProduct({commit},id){
      if(id){
        axios.delete('https://localhost:7121/api/Product' + '/' + id)
        .then(response => {
          if(response.data){
            commit("deleteProduct",response.data.id);
            alert('bạn đã xóa product')
          }
        })
        .catch(e=>{
          console.log(e);
        })
      }
    },
    deleteOrder({commit},id){
      if(id){
        axios.delete('https://localhost:7121/api/Order' + '/' + id)
        .then(response => {
          if(response.data){
            commit("deleteOrder",response.data.id);
            alert('bạn đã xóa product')
          }
        })
        .catch(e=>{
          console.log(e);
        })
      }
    },
    deleteUsers({commit},id){
      if(id){
        axios.delete('https://localhost:7121/api/User' + '/' + id)
        .then(response => {
          if(response.data){
            commit("deleteUsers",response.data.id);
            alert('bạn đã xóa user')
          }
        })
        .catch(e=>{
          console.log(e);
        })
      }
    },
    addToCart({commit},product){
      commit('addToCart',product);

    },
    deleteCart({commit},product){
      commit('deleteCart',product);
    },
    addQuantity({commit},product){
      commit('addQuantity',product);
    },
    minusQuantity({commit},product){
      commit('minusQuantity',product);
    },
  },
  modules: {
  }
})
