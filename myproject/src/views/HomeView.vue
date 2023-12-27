<!-- Cart.vue -->
<template>
  <v-container>
    <v-list>
      <v-list-item-group v-if="cart.length > 0">
        <v-list-item v-for="item in orders" :key="item.id">
          <v-list-item-content>
            <v-list-item-title>{{ item.userId }}</v-list-item-title>
            <v-list-item-subtitle>{{ item.price }}</v-list-item-subtitle>
          </v-list-item-content>
          <v-list-item-action>
            <v-btn @click="removeFromCart(index)" icon>
              <v-icon>mdi-delete</v-icon>
            </v-btn>
          </v-list-item-action>
        </v-list-item>
      </v-list-item-group>
      <v-list-item v-else>
        <v-list-item-content>No items in the cart</v-list-item-content>
      </v-list-item>
    </v-list>
  </v-container>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
  data() {
    return {
      cart: [],
    };
  },
  methods: {
    removeFromCart(index) {
      this.cart.splice(index, 1);
    },
  },
  created(){
    this.$store.dispatch('getOrders');
  },
  computed:{
    ...mapGetters(["orders"])
  }
};
</script>
