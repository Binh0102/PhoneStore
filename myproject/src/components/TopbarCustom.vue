<template>
    <div>
        <v-app-bar style="background:url('https://i.pinimg.com/236x/e5/37/b2/e537b262108fd29bfdac4b9aae41f0cf.jpg')" app dark >
            <v-btn icon
                to="/custom/index">
                <v-avatar>
                    <v-img
                        src="https://png.pngtree.com/png-clipart/20210627/original/pngtree-letter-b-logo-v1-png-image_6452331.jpg"
                        alt="John"
                    >
                    </v-img>
                </v-avatar>
            </v-btn>
        <v-text-title >Điện Thoại Hot Nhất Đang Chờ Bạn!</v-text-title>
        <v-spacer></v-spacer>
        <v-col cols="6">
            <v-row>
            <v-col>
                <v-text-field
                density="compact"
                variant="solo"
                label="Tìm kiếm"
                append-inner-icon="mdi-magnify"
                single-line
                hide-details
                ></v-text-field>
            </v-col>
            </v-row>
        </v-col>
        <v-spacer></v-spacer>
        <v-toolbar-items>
            
            <v-btn to="/cart">
                <v-badge color="red" :content="cartData.length" right overlap>
                    <v-icon>mdi-cart</v-icon>
                </v-badge>
            </v-btn>
            <v-btn>
                <v-badge color="red" content="4" right overlap>
                    <v-icon>mdi-bell</v-icon>
                </v-badge>
            </v-btn>
            <v-btn>
                <v-bagde color="red" content="4" right overlap>
                    <v-icon>mdi-phone</v-icon>
                </v-bagde>
            </v-btn>
            <v-btn id="menu-activator"  @click="isLogin ? logout() : dialogLogin = true">
                {{ isLogin ? 'Đăng xuất' : 'Đăng nhập' }}
                <v-icon>{{ isLogin ? 'mdi-logout':'mdi-login' }}</v-icon>
            </v-btn>
        </v-toolbar-items>
        <login
            :dialogLogin="dialogLogin"
            @close="dialogLogin=false"/>
        </v-app-bar>
    </div>
  </template>
  
  <script>
import { mapActions } from 'vuex';
import Login from '../views/Login.vue'
    export default {
    components: { Login, },
    data(){
        return{
            dialogLogin: false,
            cartData:[]
        }
    },
    created(){
        console.log(this.isLogin)
        this.cartData = this.$store.getters.cart;
    },
    computed:{
        isLogin() {
            return this.$store.state.isLogin;
        }
    },
    methods:{
        ...mapActions(['logout'])
    }
        
  }
  </script>
  
  <style>
  
  </style>