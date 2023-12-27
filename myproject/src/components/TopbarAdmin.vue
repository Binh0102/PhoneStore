<template>
    <div>
        <v-app-bar style="background:url('https://i.pinimg.com/236x/e5/37/b2/e537b262108fd29bfdac4b9aae41f0cf.jpg')" app dark >
        <login-admin
        :dialogLoginAdmin="dialogLoginAdmin"
            @close="dialogLoginAdmin=false"/>
            
        </v-app-bar>
    </div>
  </template>
  
  <script>
import { mapActions } from 'vuex';
import LoginAdmin from '../views/Admin/LoginAdmin.vue';
    export default {
    components: { LoginAdmin, },
    data(){
        return{
            dialogLoginAdmin: false,
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