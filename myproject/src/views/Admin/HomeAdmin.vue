<template>
  <v-main style="background-color: #EEEEEE;">
    <topbar-amin/>
    <side-bar/>
    <v-container>
      <v-row>
        <v-col cols="3">
          <v-card>
            <v-card-title class="p-3 pt-2" >
              <v-row align="center" class="mt-1 ml-1 row" style="background-color: #111111;">
                <v-col class="icon-lg mt-n4">
                  <v-avatar class="bg-gradient-dark shadow-dark border-radius-xl">
                    <v-icon color="white">mdi-currency-usd</v-icon>
                  </v-avatar>
                </v-col>
              </v-row>
              <v-row justify="flex-end" class="pt-1">
                <v-col class="">
                  <h4 class="mb-0">Doanh thu</h4>
                </v-col>
              </v-row>
            </v-card-title>
            <v-card-actions class="p-3">
              <p class="mb-0">
                <span class="text-success text-sm font-weight-bolder">{{ revenue() }}</span>  
              </p>
            </v-card-actions>
          </v-card>
          <br><br>
        </v-col>
        <v-col cols="3">
          <v-card>
            <v-card-title class="p-3 pt-2" >
              <v-row align="center" class="mt-1 ml-1 row" style="background-color: #FF3399;">
                <v-col class="icon-lg mt-n4">
                  <v-avatar class="bg-gradient-dark shadow-dark border-radius-xl">
                    <v-icon color="white">mdi-cart</v-icon>
                  </v-avatar>
                </v-col>
              </v-row>
              <v-row justify="flex-end" class="pt-1">
                <v-col >
                  <h4 class="mb-0">Số lượng đơn hàng</h4>
                </v-col>
              </v-row>
            </v-card-title>
            <v-card-actions class="p-3">
              <p class="mb-0">
                <span class="text-success text-sm font-weight-bolder">{{ orders.length }}</span>
              </p>
            </v-card-actions>
          </v-card>
          <br><br>
        </v-col>
        <v-col cols="3">
          <v-card>
            <v-card-title class="p-3 pt-2" >
              <v-row align="center" class="mt-1 ml-1 row" style="background-color: #00FF00;">
                <v-col class="icon-lg mt-n4">
                  <v-avatar class="bg-gradient-dark shadow-dark border-radius-xl">
                    <v-icon color="white">mdi-package-variant-closed</v-icon>
                  </v-avatar>
                </v-col>
              </v-row>
              <v-row justify="flex-end" class="pt-1">
                <v-col>
                  <h4 class="mb-0">Số lượng sản phẩm</h4>
                </v-col>
              </v-row>
            </v-card-title>
            <v-card-actions class="p-3">
              <p class="mb-0">
                <span class="text-success text-sm font-weight-bolder">{{ products.length }}</span>
              </p>
            </v-card-actions>
          </v-card>
          <br><br>
        </v-col>
        <v-col cols="3">
          <v-card>
            <v-card-title class="p-3 pt-2" >
              <v-row align="center" class="mt-1 ml-1 row" style="background-color: #0000FF;">
                <v-col class="icon-lg mt-n4">
                  <v-avatar class="bg-gradient-dark shadow-dark border-radius-xl">
                    <v-icon color="white">mdi-account</v-icon>
                  </v-avatar>
                </v-col>
              </v-row>
              <v-row justify="flex-end" class="pt-1">
                <v-col>
                  <h4 class="mb-0">Số lượng người dùng</h4>
                </v-col>
              </v-row>
            </v-card-title>
            <v-card-actions class="p-3">
              <p class="mb-0">
                <span class="text-success text-sm font-weight-bolder">{{ users.length }}</span>
              </p>
            </v-card-actions>
          </v-card>
          <br><br>
        </v-col>
      </v-row>
      <login-admin
      :dialogLoginAdmin="dialogLoginAdmin"
            @close="dialogLoginAdmin=false"
            v-if="!isLogin" v-model="dialogLoginAdmin"/>
    </v-container>
  </v-main>
</template>

<script>
import { mapGetters } from 'vuex';
import SideBar from '../../components/SideBar.vue';
import TopbarAmin from '../../components/TopbarAdmin.vue';
import LoginAdmin from './LoginAdmin.vue';
export default {
  components: { SideBar, TopbarAmin, LoginAdmin,  },
  data() {
    return {
      dialogLoginAdmin:false,
    };
  },
  computed: {
    isLogin() {
      console.log(this.isLogin)
      return this.$store.state.isLogin;
    },
    ...mapGetters(['products','users','orders']),
  },
  created(){
    console.log(this.isLogin)
    if(!this.isLogin){
      this.dialogLoginAdmin = true
    }
    else{
      console.log(this.revenue())
      this.$store.dispatch('getProuct');
      this.$store.dispatch('getOrder');
      this.$store.dispatch('getUsers');

    }
  },
  methods: {
    revenue() {
      return this.orders.reduce((total, order) => total + order.total, 0);
    },
  },};
</script>

<style scoped>
  .row{
    width: 64px; height: 64px; box-shadow: 0px 0px 8px;border-radius: 12px;
  }
</style>
