<template>
  <div>
    <v-dialog max-width="700px"
    v-model="dialog">
        <v-form @submit.prevent="onSubmit()">
            <v-card>
                <v-card-title>
                    <span>Thêm mới sản phẩm</span>
                </v-card-title>
                <v-card-text>
                    <v-form>
                        <v-container>
                            <v-row>
                                <v-col>
                                    <v-text-field
                                        label="Tên sản phẩm"
                                        v-model="product.productName"
                                    >
                                    </v-text-field>
                                </v-col>
                                <v-col>
                                    <v-autocomplete
                                        v-model="product.categoryId"
                                        :items="categoryNames"
                                        label="Hãng"
                                        item-title="categoryName"
                                        item-value="id"
                                    ></v-autocomplete>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col>
                                    <v-text-field
                                        v-model="product.price"
                                        label="Giá"
                                    >
                                    </v-text-field>
                                </v-col>
                                <v-col>
                                    <v-combobox
                                        v-model="product.memoryStorage"
                                        :items="memoryStorages"
                                        label="Dung Lượng"
                                    ></v-combobox>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col>
                                    <v-file-input
                                        multiple
                                        v-model="file"
                                        label="Hình ảnh"
                                    >
                                    </v-file-input>
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-form>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="grey"
                        @click="$emit('close')">
                        Hủy
                    </v-btn>
                    <v-btn color="green"
                            type="submit"
                    >Lưu</v-btn>
                </v-card-actions>
            </v-card>
        </v-form>
    </v-dialog>
  </div>
</template>

<script>
import axios from 'axios';
import { mapActions } from 'vuex';

export default {
    data(){
        return{
            product: 
            {
                id: 0,
                productName:'',
                price: null,
                image:'',
                categoryId: null,
                memoryStorage:'',
            },
            file:'',
            memoryStorages:['128GB','512GB','1TB']
        }
    },
    props:['dialogAddProduct'],
    computed:{
        dialog:{
            get(){
                return this.dialogAddProduct;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            } 
        },
        categoryNames() {
            return this.$store.getters.categories;
        }
    },
    methods:{
        ...mapActions(['getProducts']),
        onSubmit(){
            console.log(this.product)
            axios.post('https://localhost:7121/api/Product',this.product)
            .then(response => {
                console.log(response.status);
                axios.get('https://localhost:7121/api/Product/GetLatProduct')
                .then(response =>{
                    this.product.id = response.data.id;
                    let formData = new FormData();
                    console.log(this.file);
                    formData.append('formfile',this.file[0]);
                    axios.post('https://localhost:7121/api/Product/UploadImage?product=' + this.product.id, formData)
                    .then(response =>{
                        this.product.image = response.data[0];
                        console.log(this.product);
                        axios.put('https://localhost:7121/api/Product' + '/' + this.product.id,this.product)
                        .then(response =>{
                            console.log(response.status);
                            alert('bạn đã thêm 1 sản phẩm')
                            this.$emit("update")
                            this.dialog = false;
                            
                        }).catch(e =>{
                            console.log(e);
                        })
                    }).catch(e =>{
                        console.log(e);
                    })
                }).catch(e =>{
                    console.log(e);
                })
            }).catch(e => {
                console.log(e);
            })
        },
        rerestdata(){
            this.product.id = 0;
            this.product.productName = '';
            this.product.price = null;
            this.product.image = '';
            this.product.categoryId = null;
            this.product.memoryStorage = '';
            this.file = null;
        }
    },
    created(){
        this.$store.dispatch('getCategories');
    },
    updated(){
        this.rerestdata();
    }
}
</script>

<style>

</style>