<template>
  <v-main>
    <topbar-custom/>
    <v-container fluid style="background-color: beige;" >
      <v-form @submit.prevent="addOrder" >
        <v-row justify="center" align="center" class="h-100">
          <v-col>
            <v-card>
              <v-card-text>
                <v-row>
                  <v-col lg="7">
                    <v-icon x-large @click="goBack">mdi-arrow-left</v-icon>
                    <v-p class="mb-1 display-1">  Giỏ hàng của bạn</v-p>
                    <v-row class="mb-4">
                      <v-col>
                        <v-p class="mb-0 display-2">Bạn đang có {{ cartData.length }} sản phẩm</v-p>
                      </v-col>
                    </v-row>
                    <v-card class="mb-3" v-for="item in cartData" :key="item.id">
                      <v-card-text>
                        <v-row justify="space-between" align="center">
                          <v-col class="d-flex flex-row align-items-center">
                            <v-img :src="item.item.image" class="img-fluid rounded-3" alt="Shopping item" style="width: 65px;"></v-img>
                            <v-col class="ms-3">
                              <h3>{{item.item.productName}}</h3>
                              <v-p class="small mb-0">Dung lượng: {{item.item.memoryStorage}}</v-p>
                            </v-col>
                          </v-col>
                          <v-col class="d-flex flex-row align-items-center">
                            <div style="width: 200px;">
                              <v-row>
                                <v-btn icon @click="minusQuantity(item.item)">
                                  <v-icon>mdi-minus</v-icon>
                                </v-btn>
                                  <v-p v-model="item.quantity" min="1" max="10" type="number" class="mt-2 align-center text-center" style="width:60px;font-size: 25px;">{{ item.quantity }}</v-p>
                                <v-btn icon @click="addQuantity(item.item)">
                                  <v-icon>mdi-plus</v-icon>
                                </v-btn>
                              </v-row>
                            </div>
                            <div>
                              <h3 class="mb-0">{{ item.item.price * item.quantity }} vnđ</h3>
                            </div>
                            <v-spacer></v-spacer>
                            <v-btn icon @click="deleteCart(item.item)" >
                              <v-icon>mdi-delete</v-icon>
                            </v-btn>
                          </v-col>
                        </v-row>
                      </v-card-text>
                    </v-card>
                  </v-col>
                  <v-col lg="5">
                    <v-card class="bg-primary text-white rounded-3">
                      <v-card-text>
                        <v-row justify="space-between" align="center" class="mb-4">
                          <v-col>
                            <h5 class="mb-0">Card details</h5>
                            <v-img src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-6.webp" class="img-fluid rounded-3" style="width: 45px;" alt="Avatar"></v-img>
                          </v-col>
                        </v-row>
                        <v-p class="small mb-2">Card type</v-p>
                        <v-icon class="ml-3 text-white">mdi-credit-card</v-icon>
                        <v-icon class="ml-3 text-white">mdi-card-account-details</v-icon>
                        <v-icon class="ml-3 text-white">mdi-credit-card-outline</v-icon>
                        <v-icon class="ml-3 text-white">mdi-wallet-giftcard</v-icon>

                        <v-form>
                          <v-text-field v-model="order.phoneNumber" label="Số điện thoại"></v-text-field>
                          <v-text-field v-model="order.address" label="Địa chỉ"></v-text-field>
                        </v-form>
                        <v-divider class="my-4"></v-divider>
                        <v-row justify="space-between" class="mb-2">
                          <v-p class="mb-0 ml-6">Tổng tiền hàng</v-p>
                          <v-p class="mb-0 mr-6">{{ totalCartAmount }} vnđ</v-p>
                        </v-row>
                        <v-row justify="space-between" class="mb-2">
                          <v-p class="mb-0 ml-6">Phí vận chuyển</v-p>
                          <v-p class="mb-0 mr-6">0 vnđ</v-p>
                        </v-row>
                        <v-row justify="space-between" class="mb-4">
                          <v-p class="mb-0 ml-6">Tổng tiền thanh toán</v-p>
                          <v-p class="mb-0 mr-6">{{ totalCartAmount }} vnđ</v-p>
                        </v-row>
                        <v-btn block large color="info" type="submit">
                          <v-row justify="space-between">
                            <span>{{ totalCartAmount }} vnđ</span>
                            <span>Đặt hàng <v-icon>mdi-arrow-right</v-icon></span>
                          </v-row>
                        </v-btn>
                      </v-card-text>
                    </v-card>
                  </v-col>
                </v-row>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
        <login
          :dialogLogin="dialogLogin"
          @close="dialogLogin=false"
        />
        <v-dialog v-model="dialog" max-width="400" persistent>
          <v-card>
            <v-card-title style="align-items: center;display: flex; justify-content: center;">Thông báo</v-card-title>
            <v-card-text style="align-items: center;display: flex; justify-content: center;">
              Đơn hàng của bạn đã được tạo thành công!              </v-card-text>
            <v-card-actions style="align-items: center;display: flex; justify-content: center;">
              <v-btn @click="dialog=false" color="primary">Đóng</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-form>
    </v-container>
  </v-main>
</template>
<script>
import { mapActions, mapGetters } from 'vuex';
import { format } from 'date-fns';
import TopbarCustom from '../../components/TopbarCustom.vue';
import Login from '../Login.vue';
import axios from 'axios';
export default {
components: { TopbarCustom, Login },
data(){
  return{
    dialogLogin:false,
    dialog:false,
    cartData:[],
    order:{
      id: 0,
      phoneNumber:'',
      address:'',
      total:null,
      orderdate: format(new Date(), 'yyyy-MM-dd'),
      status:1,
      userId:null
    },
    orderdetail:{
      id:0,
      productId:null,
      price: null,
      quantity: null,
      orderId: null,
    },
    user:{}
  }
},
  methods: {
    goBack() {
      // Use window.history.back() to navigate back
      window.history.back();
    },
    addOrder(){
      console.log(this.isLogin)
      this.order.userId  = this.user.id
      this.order.total = this.totalCartAmount;
      if(!this.isLogin){
        this.dialogLogin = true
      }
      else{
        this.dialog=true;
        axios.post('https://localhost:7121/api/Order',this.order)
        .then(responese =>{
          console.log(responese.status);
        }).catch(e =>{
          console.log(e)
        }).
        then(()=>{
          axios.get('https://localhost:7121/api/Order/GetLastOrders')
          .then(responese =>{
            this.orderdetail.orderId = responese.data.id;
            console.log(responese.data)
            this.cartData.forEach((item) => {
              this.orderdetail.productId = item.item.id;
              this.orderdetail.price = item.item.price;
              this.orderdetail.quantity = item.quantity;
              console.log(this.orderdetail)
              axios.post('https://localhost:7121/api/Orderdetail',this.orderdetail)
              .then(responese => {
                console.log(responese.status)
                alert("tạo đơi hz")
              }).catch(e =>{
                console.log(e)
              });
            })
          }).catch(e =>{
            console.log(e)
          })

        })
      }
    },
    ...mapActions(['deleteCart','addQuantity','minusQuantity']),
  },
  created(){
    this.cartData = this.$store.getters.cart;
    this.user = this.$store.getters.user;
  },
  computed:{
    ...mapGetters(['order']),
    totalCartAmount() {
      return this.cartData.reduce(
        (total, item) => total + item.item.price * item.quantity,0);
    },
    isLogin() {
      return this.$store.state.isLogin;
    }
  },  
}
  </script>
  
  <style>
  
  </style>
  