<template>
  <v-dialog v-model="dialog">
    <v-container class="mt-8" style="background-color: aliceblue;">
      <v-row class="align-center">
        <v-col md="6">

              <v-img class="mx-auto" height="450px" width="60%" :aspect-ratio="1" cover
                :src="item.image"                
              />
        </v-col>
        <v-col md="6">
          <v-container> 
            <v-row>
              <v-col cols="10">
                <h1 class="mb-1">{{ item.productName }}</h1> 
              </v-col>
              <v-spacer></v-spacer>
              <v-col cols="2">
                <v-btn icon @click="$emit('close')" class="mr-2">
                  <v-icon>mdi-close</v-icon>
                </v-btn>
             </v-col>
            </v-row>
            <div class="fs-4">
              <span class="fw-bold text-dark">{{ item.price }} vnđ</span>
            </div>
            <v-divider class="my-6"></v-divider>
            <v-row class="mb-5">
              <v-btn-toggle
                color="green"
                dark
              >
                <v-btn @click="addMemoryStorage('128GB')" outlined class="mr-2">128Gb</v-btn>
                <v-btn @click="addMemoryStorage('512GB')" outlined class="mr-2">512Gb</v-btn>
                <v-btn @click="addMemoryStorage('1TB')" outlined>1TB</v-btn>
            </v-btn-toggle>
            </v-row>
            <v-form class="pt-2">
              <v-row>
              <v-btn icon @click="decrement">
                <v-icon>mdi-minus</v-icon>
              </v-btn>
                <v-p v-model="quantity" min="1" max="10" type="number" class="mt-2 align-center text-center rounded-circle" style="width:60px;font-size: 25px; border-radius: 50%;">{{ quantity }}</v-p>
              <v-btn icon @click="increment">
                <v-icon>mdi-plus</v-icon>
              </v-btn>
            </v-row>
            </v-form>
            <v-form class="pt-5">
            <v-btn @click="AddToCart(item)" class="mt-3" color="primary">
              <v-icon>
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-plus">
                      <line x1="12" y1="5" x2="12" y2="19"></line>
                      <line x1="5" y1="12" x2="19" y2="12"></line>
                </svg>
              </v-icon>
              Add to cart
            </v-btn>
          </v-form>
            <v-divider class="my-6"></v-divider>
            <v-simple-table>
              <template v-slot:default>
                <tbody>
                  <tr>
                    <td>Kiểu dáng</td>
                    <td>Đẹp, bắt mắt</td>
                  </tr>
                  <tr>
                    <td>Trạng thái:</td>
                    <td>Trong kho</td>
                  </tr>
                  <tr>
                    <td>Dung lượng:</td>
                    <td>{{ item.memoryStorage }}</td>
                  </tr>
                  <tr>
                    <td>Shipping:</td>
                    <td>
                      <small>
                        01 day shipping.
                        <span class="text-muted">( Free pickup today)</span>
                      </small>
                    </td>
                  </tr>
                </tbody>
              </template>
            </v-simple-table>
          </v-container>
        </v-col>
      </v-row>
    </v-container>
  </v-dialog>
</template>

<script>
import { mapActions } from 'vuex';
export default {
  props:['dialogDetail','item'],
  computed:{
    dialog:{
        get(){
            return this.dialogDetail;
        },
        set(value){
            if(!value){
                this.$emit('close');
            }
        } 
    },
  },
  data() {
    return {
      quantity: 1,
    };
  },
  methods: {
    AddToCart(){
      this.addToCart({ ...this.item, quantity: this.quantity });
      this.dialog = false;
    },
    addMemoryStorage(memoryStorage) {
      this.item.memoryStorage = memoryStorage;
    },
    increment() {
      this.quantity += 1;
    },
    decrement() {
      if (this.quantity > 1) {
        this.quantity -= 1;
      }
    },
    ...mapActions(['addToCart'])
  },
  created(){
    return this.$store.getters.cart
  }
}
</script>

<style>

</style>
