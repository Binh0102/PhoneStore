<template>
    <v-main style="background-color: #EEEEEE;">
      <topbar-amin />
      <side-bar />
      <v-row justify="center" class="mt-2 mb-2" style="background-color: white;">
            <v-col cols="3" class="ml-1 mt-2">
                <h2>Danh sách chi tiết đơn hàng</h2>
            </v-col>
            <v-col cols="6">
                <v-row>
                <v-col>
                    <v-text-field
                    :loading="loading"
                    density="compact"
                    variant="solo"
                    label="Tìm kiếm"
                    append-inner-icon="mdi-magnify"
                    single-line
                    hide-details
                    v-model="search"
                    ></v-text-field>
                </v-col>
                </v-row>
            </v-col>
        </v-row>
        <v-table>
    <thead>
      <tr>
        <th>ID</th>
        <th>Tên sản phẩm</th>
        <th>Giá</th>
        <th>Số lượng</th>
        <th>Tổng tiền</th>
        <th>Hành động</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="orderdetail in filteredOrderdetail" :key="orderdetail.id">
        <td>{{ orderdetail.id }}</td>
        <td>{{getProductName( orderdetail.productId )}}</td>
        <td>{{ orderdetail.price }}</td>
        <td>{{ orderdetail.quantity }}</td>
        <td>{{ orderdetail.price * orderdetail.quantity }}</td>
        <td>
          <v-btn icon size="x-small" class="ml-1">
            <v-icon>mdi-delete</v-icon>
          </v-btn>
        </td>
      </tr>
    </tbody>
  </v-table>
        <v-dialog
        maxWidth="450px"
            v-model="dialogDelete"
        >
            <v-card>
                <v-alert>
                    <v-row align="center">
                        <v-col cols="12" class="text-center">
                            Ban co dong y xoa khong?
                        </v-col>
                    </v-row>
                    <v-row align="center">
                        <v-spacer></v-spacer>
                        <v-col cols="5">
                            <v-btn
                                variant="text"
                                @click="deleteUser(userId) .then(() => {
                                    this.$store.dispatch('getUsers');
                                }),
                                dialogDelete = false"
                            >Dong y</v-btn>
                        </v-col>
                        <v-col cols="6">
                            <v-btn 
                                variant="text"
                                @click="dialogDelete=false"
                            >Huy bo</v-btn>
                        </v-col>
                    </v-row>
                </v-alert>
            </v-card>
        </v-dialog>
    </v-main>
  </template>
  
  <script>
  import { mapGetters } from 'vuex';
  
  import SideBar from '../../components/SideBar.vue';
  import TopbarAmin from '../../components/TopbarAdmin.vue';
  
  export default {
    components: { SideBar, TopbarAmin },
    data() {
      return {
        search: '',
        tab: null,
        dialogDelete: false,
      };
    },
    created() {
        this.$store.dispatch('getOrderdetails');
        this.$store.dispatch('getProducts');
    //   if (!this.isLogin) {
    //     this.$router.push('/admin');
    //   } else {
    //   }
    },
    methods: {
        getProductName(productId) {
            const product = this.products.find(product => product.id === productId);
            return product ? product.productName : '';
        },
    },
    computed: {
      ...mapGetters(['orderdetails','products']),
      filteredOrderdetail() {
        return this.orderdetails.filter((orderdetail) =>
            this.getProductName(orderdetail.productId).toLowerCase().includes(this.search.toLowerCase())
            
        );
    },
    },
  };
  </script>
  
  <style>
  </style>
  
  