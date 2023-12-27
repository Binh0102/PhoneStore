<template>
    <v-main style="background-color: #EEEEEE;">
      <topbar-amin/>
        <side-bar/>
        <v-card>
    <v-tabs
      v-model="tab"
      color="deep-purple-accent-4"
      align-tabs="center"
      class="mt-2"
    >
      <v-tab :value="1">Chờ xác nhận</v-tab>
      <v-tab :value="2">Đang vận chuyển</v-tab>
      <v-tab :value="3">Đã giao hàng</v-tab>
    </v-tabs>
    <v-window v-model="tab">
      <v-window-item
        v-for="n in 3"
        :key="n"
        :value="n"
      >
        <v-container fluid>
            <v-table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Tên khách hàng</th>
                        <th>Số điện thoại</th>
                        <th>Địa chỉ</th>
                        <th>Ngày đặt</th>
                        <th>Tổng tiền</th>
                        <th>Hành động</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="order in filteredOrders" :key="order.id">
                        <td>{{ order.id }}</td>
                        <td>{{ getUserFullName(order.userId) }}</td>
                        <td>{{ order.phoneNumber }}</td>
                        <td>{{ order.address }}</td>
                        <td>{{ order.orderdate.slice(0,10) }}</td>
                        <td>{{ order.total }}</td>
                        <td>
                            <v-btn icon size="x-small" @click="updateStatus(order.id)" class="ml-1" v-if="tab !== 3"><v-icon>mdi-pencil</v-icon></v-btn>
                            <v-btn icon @click="deleteOrderId(order.id)" size="x-small" class="ml-1"><v-icon>mdi-delete</v-icon></v-btn>
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
                                    @click="deleteOrder(orderId) .then(() => {
                                        this.$store.dispatch('getOrders');
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
        </v-container>
      </v-window-item>
    </v-window>
  </v-card>

    </v-main>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import { format } from 'date-fns';

import SideBar from '../../components/SideBar.vue'
import TopbarAmin from '../../components/TopbarAdmin.vue'
export default {
  components: { SideBar, TopbarAmin },
  data(){
    return{
      tab:null,
      dialogDelete:false,
      item:{
        id: null,
        phoneNumber: '',
        address: '',
        total: null,
        orderdate: format(new Date(), 'yyyy-MM-dd'),
        status: null,
        userId: null,
      }
    }
  },
  created() {
    if(!this.isLogin){
      this.$router.push('/admin')
    }
    else{
      this.$store.dispatch('getUsers');
      this.$store.dispatch('getOrders');
      console.log(this.orders)
    }
    
  },
  methods: {
    getUserFullName(id) {
        const user = this.users.find(user => user.id == id);
        return user ? user.fullName : 1;
      },
      deleteOrderId(id){
        this.orderId = id;
        this.dialogDelete = true;
      },
      updateStatus(order) {
        this.$store.dispatch('getOrderId',order)
        this.item = this.order;
        this.item.status = this.item.status + 1;
        console.log(this.order)
        console.log(this.item)
        
        this.$store.dispatch('updateOrderStatus', this.item)
          .then(() => {
            this.$store.dispatch('getOrders');
          })
          .catch(error => {
            console.error(error);
          });
      },
      ...mapActions(["deleteOrder","getOrders"])
  },
  computed:{
    isLogin() {
      console.log(this.isLogin);
      return this.$store.state.isLogin;
    },
    ...mapGetters(["orders","users","order"]),
    filteredOrders() {
      if (this.tab === 1) {
        return this.orders.filter(order => order.status === 1);
      } else if (this.tab === 2) {
        return this.orders.filter(order => order.status === 2);
      } else if (this.tab === 3) {
        return this.orders.filter(order => order.status === 3);
      } else {
        return this.orders;
      }
    },
  },
}
</script>

<style>

</style>