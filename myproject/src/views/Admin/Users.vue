<template>
    <v-main style="background-color: #EEEEEE;">
        <topbar-amin/>
        <side-bar/>
        <v-row justify="center" class="mt-2 mb-2" style="background-color: white;">
            <v-col cols="3" class="ml-1 mt-2">
                <h2>Người dùng</h2>
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
                    v-model="query.search"
                    ></v-text-field>
                </v-col>
                </v-row>
            </v-col>
        </v-row>
        <v-table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Họ và Tên</th>
                    <th>Tên tài khoản</th>
                    <th>Email</th>
                    <th>Mật khẩu</th>
                    <th>Địa chỉ</th>
                    <th>Hành động</th>
                </tr>
            </thead>
            <tbody>
                <tr  
                v-for="user in filteredUsers" :key="user.id" 
                    
                >
                    <td>{{ user.id }}</td>
                    <td>{{ user. fullName}}</td>
                    <td>{{ user.username }}</td>
                    <td>{{ user.email }}</td>
                    <td>{{ user.password }}</td>
                    <td>{{ user.address }}</td>
                    <td>
                        <v-btn icon @click="deleteUserId(user.id)" size="x-small" class="ml-1"><v-icon>mdi-delete</v-icon></v-btn>
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
import SideBar from '../../components/SideBar.vue'
import {mapActions, mapGetters} from 'vuex'  
import TopbarAmin from '../../components/TopbarAdmin.vue'
export default {
    components: { SideBar, TopbarAmin},
    data() {
        return {
            query: {
                search: '',
            },
            userId:null,
            dialogDelete:false
        }
    },
    methods: {
        deleteUserId(id){
            this.userId = id;
            this.dialogDelete = true;
        },
        ...mapActions(["deleteUsers","getUsers"]),
    },
    created(){
        if(!this.isLogin){
            this.$router.push('/admin')
        }
        else{
            this.$store.dispatch('getUsers');
        }
    },
    computed:{
        isLogin() {
            console.log(this.isLogin)
            return this.$store.state.isLogin;
        },
        ...mapGetters(["users"]),
        filteredUsers() {
            return this.users.filter((user) =>
                user.username.toLowerCase().includes(this.query.search.toLowerCase()) ||
                user.fullName.toLowerCase().includes(this.query.search.toLowerCase()) ||
                user.email.toLowerCase().includes(this.query.search.toLowerCase())
            );
        },
    },
}
  </script>
  
  <style>
  
  </style>