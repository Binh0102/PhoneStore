<template>
  <v-app>
    <router-view/>
    <pooter/>
  </v-app>
</template>

<script>
import Pooter from './components/Pooter.vue'
export default {
  components: { Pooter },

}
</script>

<style>

</style>