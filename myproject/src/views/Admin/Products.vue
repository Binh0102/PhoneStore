<template>
  <v-main style="background-color: #EEEEEE;">
    <topbar-amin/>
    <side-bar/>
    <v-row justify="center" class="mt-2 mb-2" style="background-color: white;">
        <v-col cols="3" class="ml-1 mt-2">
            <h2>Sản phẩm</h2>
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
        <v-col cols="2">
            <v-btn icon @click="dialogAddProduct=true"><v-icon>mdi-plus</v-icon></v-btn>
        </v-col>
    </v-row>
    <v-table>
        <thead>
            <tr>
                <th>ID</th>
                <th>Hình ảnh</th>
                <th>Tên sản phẩm</th>
                <th>Loại sản phẩm</th>
                <th>Dung Lượng</th>
                <th>Giá</th>
                <th>Hành động</th>
            </tr>
        </thead>
        <tbody>
            <tr  
            v-for="product in filteredProducts" :key="product.id" 
                
            >
                <td>{{ product.id }}</td>
                <td><v-img :src="product.image" :lazy-src="product.image" height="30" width="30" :aspect-ratio="1" cover></v-img></td>
                <td>{{ product.productName }}</td>
                <td>{{ getCategoryName(product.categoryId) }}</td>
                <td>{{ product.memoryStorage }}</td>
                <td>{{ product.price }}</td>
                <td>
                    <v-btn icon @click="dialogEditProduct=true, item=product" size="x-small" class="ml-1"><v-icon>mdi-pencil</v-icon></v-btn>
                    <v-btn icon @click="deleteProductId(product.id)" size="x-small" class="ml-1"><v-icon>mdi-delete</v-icon></v-btn>
                </td>

            </tr>
        </tbody>
    </v-table>
    <add-product
        :dialogAddProduct="dialogAddProduct"
        @close="dialogAddProduct=false"
        @update="getProducts"
    />
    <edit-product
        :dialogEditProduct="dialogEditProduct"
        :item="item"
        @close="dialogEditProduct=false"
        @update="getProducts"
    />
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
                            @click="deleteProduct(productId).then(() => {
                                this.$store.dispatch('getProducts');
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
import SideBar from '../../components/SideBar.vue'
import AddProduct from './AddProduct.vue'
import {mapActions, mapGetters} from 'vuex'
import EditProduct from './EditProduct.vue'
import TopbarAmin from '../../components/TopbarAdmin.vue'
export default {
  components: { SideBar, AddProduct, EditProduct, TopbarAmin, },
    data() {
        return {
        search: '',
        dialogAddProduct:false,
        dialogEditProduct:false,
        dialogDelete:false,
        productId:null,
        item:''
        }
    },
    methods: {
        
        getCategoryName(categoryId) {
            const category = this.categories.find(category => category.id === categoryId);
            return category ? category.categoryName : '';
        },
        deleteProductId(id){
            this.productId = id;
            this.dialogDelete = true;
        },
        ...mapActions(["deleteProduct","getProducts"])
    },
    created(){
        if(!this.isLogin){
            this.$router.push('/admin')
        }
        else{
            this.$store.dispatch('getProducts');
            this.$store.dispatch('getCategories');
        }
    },
    computed:{
        isLogin() {
            console.log(this.isLogin)
            return this.$store.state.isLogin;
        },
        ...mapGetters(["products","categories","product"]),
        filteredProducts() {
            return this.products.filter((product) =>
                product.productName.toLowerCase().includes(this.search.toLowerCase())
            );
        },
    },
}
</script>

<style>

</style>