<template>
    <div>
        <v-dialog max-width="700px"
        v-model="dialog">
            <v-form @submit.prevent="onEdit()">
                <v-card>
                    <v-card-title>
                        <span>Sửa sản phẩm</span>
                    </v-card-title>
                    <v-card-text>
                        <v-form>
                            <v-container>
                                <v-row>
                                    <v-col>
                                        <v-text-field
                                            v-model="product.productName"
                                            label="Tên sản phẩm"
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
                                    <v-autocomplete
                                        v-model="product.memoryStorage"
                                        :items="memoryStorages"
                                        label="Dung Lượng"
                                    ></v-autocomplete>
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
    props:['dialogEditProduct','item'],
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
    computed:{
        dialog:{
            get(){
                return this.dialogEditProduct;
            },
            set(value){
                if(!value){
                    this.$emit('close');
                }
            }
        },
        categoryNames() {
            return this.$store.getters.categories.map(category => ({
                id: category.id,
                categoryName: category.categoryName,  
            }));
        }
    },
    watch:{
        item:function(){
            this.product.id = this.item.id;
            this.product.productName = this.item.productName;
            this.product.price = this.item.price;
            this.product.image = this.item.image;
            this.product.categoryId = this.item.categoryId;
            this.product.memoryStorage = this.item.memoryStorage;
        }
    },
    methods:{
        ...mapActions(["getProducts"]),
        onEdit(){
            if(this.file == ''){
                axios.put('https://localhost:7121/api/Product/' + this.product.id, this.product)
                .then(respone =>{
                    console.log(respone.data);
                    this.$emit('close');
                    this.$emit('update');
                }).catch(e =>{
                    console.log(e)
                })
            }
            else{
                let formData = new FormData();
                console.log(this.file);
                formData.append('formfile',this.file[0]);
                axios.post('https://localhost:7121/api/Product/UploadImage?product=' + this.product.id, formData)
                .then(respone =>{
                    this.product.image = respone.data[0];
                    console.log(this.product)
                    axios.put('https://localhost:7121/api/Product/' + this.product.id, this.product)
                        .then(respone =>{
                            console.log(respone.data);
                            this.$emit('update');
                            this.$emit('close');
                        }).catch(e =>{
                            console.log(e)
                        })
                }).catch(e =>{
                    console.log(e);
                })
            }
        }
    },

}
</script>

<style>

</style>