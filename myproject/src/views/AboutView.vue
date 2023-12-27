  <template>
    <v-container>
      <v-btn @click="dialogabout=true">click</v-btn>
      <test
      :dialogabout="dialogabout"
      @close="dialogabout=false"/>
      
    </v-container>
    
  </template>

  <script>
import Test from './test.vue'
  export default {
    components: { Test },
      data(){
          return{
              dialogabout:false,
          }
      }
  }
  </script>

  <style>

  </style>