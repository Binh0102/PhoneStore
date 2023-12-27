<template>
  <v-main style="background: url('https://i.pinimg.com/236x/93/e1/7d/93e17df5abb3c5de7f89d8edd026bc5c.jpg');
    background-size: cover;">
    <topbar-custom/>
    <v-container class="my-1" style=""> 
      <v-spacer></v-spacer>
      <v-carousel height="300" hide-delimiters cycle>
        <v-carousel-item
          src="https://img4.thuthuatphanmem.vn/uploads/2020/06/26/hinh-anh-banner-dien-may-thong-minh_033705387.png"
          cover
        ></v-carousel-item>
        <v-carousel-item
          src="https://thietkehaithanh.com/wp-content/uploads/2019/06/thietkehaithanh-banner-1-1.jpg"
          cover
        ></v-carousel-item>
        <v-carousel-item
          src="https://chuyenstandee.com/wp-content/uploads/2021/03/banner-quang-cao-du-khach-hang-hieu-qua-3-768x227.jpg"
          cover
        ></v-carousel-item>
      </v-carousel>
    </v-container>
    <form action="">
      <v-row md="5" class="pa-4">
        <v-col cols="12">
          <v-btn-toggle
          v-model="selectedCategoryId"
          color="primary"
          dark
          >
            <v-btn
            v-for="category in categories" :key="category.id"
              class="mx-2"
              rounded="xl"
              size="x-large"
              @click="getProductByCategoryId(category.id)"
            >
              {{ category.categoryName }}
            </v-btn>
          </v-btn-toggle>
        </v-col>
      </v-row>
    </form>
    <form>
      <v-row class="pl-10 pb-10">
        <v-col v-for="product in products" 
          :key="product.id" 
          cols="3"
          pt="20"
        >
          <v-card style="height: 470px;">
            <v-card-title  @click="dialogDetal=true,item = product">
              <v-img class="mx-auto" :src="product.image" height="300" width="80%" :aspect-ratio="1" cover></v-img>
            </v-card-title>
            <v-card-title class="fs-6" @click="dialogDetal=true,item = product">
              <v-p class="text-decoration-none" style="color: red; font-size: 25px;">{{ product.productName }}</v-p>
            </v-card-title>
            <v-row>
              <v-col>
                <span class="text-dark ml-5"></span>
              </v-col>
            </v-row>
            <v-row class="mt-1">
              <v-col>
                <span class="text-dark ml-5">{{ product.price }} vnđ</span>
              </v-col>
              <v-col c>
                <v-btn 
                  color="primary"
                  small
                  @click="addToCart(product),openDialog(product.productName)"
                >
                  <v-icon>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-plus">
                      <line x1="12" y1="5" x2="12" y2="19"></line>
                      <line x1="5" y1="12" x2="19" y2="12"></line>
                    </svg>
                  </v-icon>
                  Add
                </v-btn>
              </v-col>
            </v-row>
          </v-card>
        </v-col>
      </v-row>
    </form>
    <v-dialog v-model="dialog" max-width="400">
      <v-card>
        <v-card-title style="align-items: center;display: flex; justify-content: center;">Thông báo</v-card-title>
        <v-card-text style="align-items: center;display: flex; justify-content: center;">
          {{ productName }} đã được thêm vào giỏ hàng!
        </v-card-text>
        <v-card-actions style="align-items: center;display: flex; justify-content: center;">
          <v-btn @click="dialog=false" color="primary">Đóng</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    <detail
      :dialogDetail="dialogDetal"
      :item="item"
      @close="dialogDetal=false"
    />
  </v-main>
</template>

<script>
import {mapActions, mapGetters} from 'vuex'
import TopbarCustom from '../../components/TopbarCustom.vue';
import Detail from './Detail.vue';
export default {
  components: { Detail, TopbarCustom },
  data(){
    return{
      dialogDetal:false,
      selectedCategoryId: null,
      dialog:false,
      item:'',
      productName:''
    }
  },
  methods:{
    ...mapActions(['getProductByCategoryId','addToCart']),
    openDialog(productName) {
      this.productName = productName;
      this.dialog = true;
    },
  },
  created(){
    this.$store.dispatch('getProducts');
    this.$store.dispatch('getCategories');
    this.$store.dispatch('getProductId',this.$route.params.id)
    console.log(this.user)
  },
  computed:{
      ...mapGetters(["products", "categories","product",'user']),
  },
}
</script>

<style>

</style>